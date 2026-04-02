$ErrorActionPreference = 'Stop'

$root = 'c:\Users\v-arulmani\Downloads\AI_Automation 5\AI_Automation - Copy'
$xlsx = Join-Path $root 'IDC Vendor Regression test 2511_Weekly Win32app Sanity Test Pass.xlsx'
$tmp = Join-Path $env:TEMP ("xlsx_" + [guid]::NewGuid().ToString())
New-Item -ItemType Directory -Path $tmp | Out-Null

try {
    $zipPath = Join-Path $tmp 'book.zip'
    Copy-Item $xlsx $zipPath
    Expand-Archive -Path $zipPath -DestinationPath $tmp

    [xml]$wb = Get-Content (Join-Path $tmp 'xl/workbook.xml')
    $ns = New-Object System.Xml.XmlNamespaceManager($wb.NameTable)
    $ns.AddNamespace('x','http://schemas.openxmlformats.org/spreadsheetml/2006/main')
    $sheetNodes = $wb.SelectNodes('//x:sheets/x:sheet',$ns)

    [xml]$rels = Get-Content (Join-Path $tmp 'xl/_rels/workbook.xml.rels')
    $rns = New-Object System.Xml.XmlNamespaceManager($rels.NameTable)
    $rns.AddNamespace('r','http://schemas.openxmlformats.org/package/2006/relationships')

    $shared = @()
    $sharedPath = Join-Path $tmp 'xl/sharedStrings.xml'
    if (Test-Path $sharedPath) {
        [xml]$ss = Get-Content $sharedPath
        $sns = New-Object System.Xml.XmlNamespaceManager($ss.NameTable)
        $sns.AddNamespace('x','http://schemas.openxmlformats.org/spreadsheetml/2006/main')
        foreach ($si in $ss.SelectNodes('//x:si',$sns)) {
            $tNodes = $si.SelectNodes('.//x:t',$sns)
            $txt = ''
            foreach ($t in $tNodes) { $txt += $t.InnerText }
            $shared += $txt
        }
    }

    function Get-CellValue([System.Xml.XmlNode]$cell, [string[]]$shared, [System.Xml.XmlNamespaceManager]$nsm) {
        $t = $cell.t
        if ($t -eq 'inlineStr') {
            $isNode = $cell.SelectSingleNode('./x:is',$nsm)
            if ($isNode) {
                $tNodes = $isNode.SelectNodes('.//x:t',$nsm)
                $txt = ''
                foreach ($tn in $tNodes) { $txt += $tn.InnerText }
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

    $all = @()
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
            $title = ''
            $type = ''

            foreach ($c in $row.SelectNodes('./x:c',$xns)) {
                $ref = $c.r
                if ($ref -match '^A') { $id = Get-CellValue $c $shared $xns }
                if ($ref -match '^B') { $type = Get-CellValue $c $shared $xns }
                if ($ref -match '^C') { $title = Get-CellValue $c $shared $xns }
            }

            if ($type -eq 'Test Case' -and $id -match '^[0-9]{7,}$') {
                $all += [pscustomobject]@{ Sheet = $sheetName; Id = $id; Title = $title }
            }
        }
    }

    $all = $all | Sort-Object Sheet, Id -Unique
    $out = Join-Path $root 'weekly_win32_ids_from_excel.txt'
    $all | ForEach-Object { "{0}`t{1}`t{2}" -f $_.Sheet,$_.Id,$_.Title } | Set-Content -Encoding UTF8 $out

    Write-Host "Wrote: $out"
    Write-Host "Count: $($all.Count)"
    Get-Content $out
}
finally {
    if (Test-Path $tmp) { Remove-Item -Recurse -Force $tmp }
}
