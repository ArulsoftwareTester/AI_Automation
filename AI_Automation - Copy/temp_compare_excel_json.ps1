$ErrorActionPreference = 'Stop'

$root = 'c:\Users\v-arulmani\Downloads\AI_Automation 5\AI_Automation - Copy'
$xlsx = Join-Path $root 'IDC Vendor Regression Test 2602_Windows Regression Test Pass A.xlsx'
$jsonPath = Join-Path $root 'TestData_AppReggersion\windows Regression.json'
$tmp = Join-Path $env:TEMP ('xlsx_' + [guid]::NewGuid().ToString())

New-Item -ItemType Directory -Path $tmp | Out-Null

try {
    $zipPath = Join-Path $tmp 'book.zip'
    Copy-Item $xlsx $zipPath
    Expand-Archive -Path $zipPath -DestinationPath $tmp

    [xml]$wb = Get-Content (Join-Path $tmp 'xl\workbook.xml')
    $ns = New-Object System.Xml.XmlNamespaceManager($wb.NameTable)
    $ns.AddNamespace('x','http://schemas.openxmlformats.org/spreadsheetml/2006/main')
    $sheetNodes = $wb.SelectNodes('//x:sheets/x:sheet',$ns)

    [xml]$rels = Get-Content (Join-Path $tmp 'xl\_rels\workbook.xml.rels')
    $rns = New-Object System.Xml.XmlNamespaceManager($rels.NameTable)
    $rns.AddNamespace('r','http://schemas.openxmlformats.org/package/2006/relationships')

    $shared = @()
    $sharedPath = Join-Path $tmp 'xl\sharedStrings.xml'
    if (Test-Path $sharedPath) {
        [xml]$ss = Get-Content $sharedPath
        $sns = New-Object System.Xml.XmlNamespaceManager($ss.NameTable)
        $sns.AddNamespace('x','http://schemas.openxmlformats.org/spreadsheetml/2006/main')
        foreach ($si in $ss.SelectNodes('//x:si',$sns)) {
            $txt = ''
            foreach ($t in $si.SelectNodes('.//x:t',$sns)) { $txt += $t.InnerText }
            $shared += $txt
        }
    }

    function Get-CellValue([System.Xml.XmlNode]$cell, [string[]]$shared, [System.Xml.XmlNamespaceManager]$nsm) {
        $t = $cell.t
        if ($t -eq 'inlineStr') {
            $isNode = $cell.SelectSingleNode('./x:is',$nsm)
            if ($isNode) {
                $txt = ''
                foreach ($tn in $isNode.SelectNodes('.//x:t',$nsm)) { $txt += $tn.InnerText }
                return $txt
            }
        }

        $vNode = $cell.SelectSingleNode('./x:v',$nsm)
        if (-not $vNode) { return '' }

        $v = $vNode.InnerText
        if ($t -eq 's') {
            if ($shared.Count -gt [int]$v) { return $shared[[int]$v] }
            return ''
        }
        return $v
    }

    $excelCases = @()
    foreach ($sheetNode in $sheetNodes) {
        $sheetName = $sheetNode.name
        $sheetRid = $sheetNode.GetAttribute('id','http://schemas.openxmlformats.org/officeDocument/2006/relationships')
        $target = ($rels.SelectSingleNode("//r:Relationship[@Id='$sheetRid']",$rns)).Target

        $targetNorm = $target.TrimStart('/')
        if ($targetNorm -notmatch '^xl/') { $targetNorm = 'xl/' + $targetNorm }
        $sheetPath = Join-Path $tmp ($targetNorm.Replace('/','\'))

        [xml]$sheet = Get-Content $sheetPath
        $xns = New-Object System.Xml.XmlNamespaceManager($sheet.NameTable)
        $xns.AddNamespace('x','http://schemas.openxmlformats.org/spreadsheetml/2006/main')
        $rows = $sheet.SelectNodes('//x:sheetData/x:row',$xns)

        foreach ($row in $rows) {
            $id = ''
            $type = ''
            $title = ''

            foreach ($c in $row.SelectNodes('./x:c',$xns)) {
                $ref = $c.r
                if ($ref -match '^A') { $id = Get-CellValue $c $shared $xns }
                if ($ref -match '^B') { $type = Get-CellValue $c $shared $xns }
                if ($ref -match '^C') { $title = Get-CellValue $c $shared $xns }
            }

            if ($type -eq 'Test Case' -and $id -match '^[0-9]{6,}$') {
                $excelCases += [pscustomobject]@{ Sheet = $sheetName; TestCaseId = "TC_$id"; Title = $title }
            }
        }
    }

    $excelCases = $excelCases | Sort-Object TestCaseId -Unique

    $json = Get-Content -Raw $jsonPath | ConvertFrom-Json
    $jsonIds = @($json.testCases | Where-Object { $_.testCaseId } | Select-Object -ExpandProperty testCaseId)
    $jsonSet = [System.Collections.Generic.HashSet[string]]::new([string[]]$jsonIds)

    $assignmentOrEdge = @($excelCases | Where-Object { $_.Title -match '(?i)assignment\s*filter|edge' })
    $missing = @($assignmentOrEdge | Where-Object { -not $jsonSet.Contains($_.TestCaseId) })
    $present = @($assignmentOrEdge | Where-Object { $jsonSet.Contains($_.TestCaseId) })

    Write-Host "Excel assignment/edge test cases: $($assignmentOrEdge.Count)"
    Write-Host "Present in JSON: $($present.Count)"
    Write-Host "Missing in JSON: $($missing.Count)"

    if ($missing.Count -gt 0) {
        Write-Host 'Missing list:'
        $missing | ForEach-Object { "{0}`t{1}`t{2}" -f $_.TestCaseId, $_.Sheet, $_.Title }
    }
}
finally {
    if (Test-Path $tmp) { Remove-Item -Recurse -Force $tmp }
}
