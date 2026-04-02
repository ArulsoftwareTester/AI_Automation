$ErrorActionPreference = 'Stop'

$root = 'c:\Users\v-arulmani\Downloads\AI_Automation 5\AI_Automation - Copy'
$xlsx = Join-Path $root 'IDC Vendor Regression Test 2602_Windows Regression Test Pass A.xlsx'
$outPath = Join-Path $root 'TestData_AppReggersion\windows Regression_MicrosoftStoreLegacy_FromExcel.json'
$tmp = Join-Path $env:TEMP ('xlsx_' + [guid]::NewGuid().ToString())

New-Item -ItemType Directory -Path $tmp | Out-Null

try {
    $zipPath = Join-Path $tmp 'book.zip'
    Copy-Item $xlsx $zipPath
    Expand-Archive -Path $zipPath -DestinationPath $tmp

    [xml]$wb = Get-Content (Join-Path $tmp 'xl\workbook.xml')
    $ns = New-Object System.Xml.XmlNamespaceManager($wb.NameTable)
    $ns.AddNamespace('x', 'http://schemas.openxmlformats.org/spreadsheetml/2006/main')
    $sheetNodes = $wb.SelectNodes('//x:sheets/x:sheet', $ns)

    [xml]$rels = Get-Content (Join-Path $tmp 'xl\_rels\workbook.xml.rels')
    $rns = New-Object System.Xml.XmlNamespaceManager($rels.NameTable)
    $rns.AddNamespace('r', 'http://schemas.openxmlformats.org/package/2006/relationships')

    $shared = @()
    $sharedPath = Join-Path $tmp 'xl\sharedStrings.xml'
    if (Test-Path $sharedPath) {
        [xml]$ss = Get-Content $sharedPath
        $sns = New-Object System.Xml.XmlNamespaceManager($ss.NameTable)
        $sns.AddNamespace('x', 'http://schemas.openxmlformats.org/spreadsheetml/2006/main')
        foreach ($si in $ss.SelectNodes('//x:si', $sns)) {
            $txt = ''
            foreach ($t in $si.SelectNodes('.//x:t', $sns)) { $txt += $t.InnerText }
            $shared += $txt
        }
    }

    function Get-CellValue([System.Xml.XmlNode]$cell, [string[]]$shared, [System.Xml.XmlNamespaceManager]$nsm) {
        $t = $cell.t
        if ($t -eq 'inlineStr') {
            $isNode = $cell.SelectSingleNode('./x:is', $nsm)
            if ($isNode) {
                $txt = ''
                foreach ($tn in $isNode.SelectNodes('.//x:t', $nsm)) { $txt += $tn.InnerText }
                return $txt
            }
        }

        $vNode = $cell.SelectSingleNode('./x:v', $nsm)
        if (-not $vNode) { return '' }

        $v = $vNode.InnerText
        if ($t -eq 's') {
            if ($shared.Count -gt [int]$v) { return $shared[[int]$v] }
            return ''
        }
        return $v
    }

    function Make-TestName([string]$title) {
        if ([string]::IsNullOrWhiteSpace($title)) { return '' }
        $n = $title -replace '^\[IntuneSA\]:', ''
        $n = $n -replace '\[[^\]]+\]', '_'
        $n = $n -replace '[^A-Za-z0-9]+', '_'
        $n = $n.Trim('_')
        return "Windows_$n"
    }

    function Get-Assignments([string]$title) {
        $required = ''
        $available = ''
        $uninstall = ''

        if ($title -match '(?i)\[Required\]') { $required = 'Add group' }
        if ($title -match '(?i)\[Available\]') { $available = 'Add group' }
        if ($title -match '(?i)\[Uninstall\]') { $uninstall = 'Add group' }

        $obj = [ordered]@{
            'Required' = $required
            'Available for enrolled devices' = $available
            'uninstall' = $uninstall
            'select groups' = 'AI_Automation_User_group'
        }

        if ($title -match '(?i)assignment\s*filters?') {
            $filterGroup = 'Ai_Automation_matching'
            if ($title -match '(?i)No\s*Match|NoMatch') { $filterGroup = 'AI_Automation_Nonmatching' }

            $filterType = 'include'
            if ($title -match '(?i)\[Exclude\]') { $filterType = 'exclude' }

            $obj['AssignmentFiltergroup'] = $filterGroup
            $obj['AppManagementAssignmentFilterType'] = $filterType
        }

        return [pscustomobject]$obj
    }

    $cases = @()

    foreach ($sheetNode in $sheetNodes) {
        $sheetRid = $sheetNode.GetAttribute('id', 'http://schemas.openxmlformats.org/officeDocument/2006/relationships')
        $target = ($rels.SelectSingleNode("//r:Relationship[@Id='$sheetRid']", $rns)).Target

        $targetNorm = $target.TrimStart('/')
        if ($targetNorm -notmatch '^xl/') { $targetNorm = 'xl/' + $targetNorm }
        $sheetPath = Join-Path $tmp ($targetNorm.Replace('/', '\'))

        [xml]$sheet = Get-Content $sheetPath
        $xns = New-Object System.Xml.XmlNamespaceManager($sheet.NameTable)
        $xns.AddNamespace('x', 'http://schemas.openxmlformats.org/spreadsheetml/2006/main')
        $rows = $sheet.SelectNodes('//x:sheetData/x:row', $xns)

        foreach ($row in $rows) {
            $id = ''
            $type = ''
            $title = ''

            foreach ($c in $row.SelectNodes('./x:c', $xns)) {
                $ref = $c.r
                if ($ref -match '^A') { $id = Get-CellValue $c $shared $xns }
                if ($ref -match '^B') { $type = Get-CellValue $c $shared $xns }
                if ($ref -match '^C') { $title = Get-CellValue $c $shared $xns }
            }

            # Include all Test Case rows across the workbook. Shared Steps are skipped.
            if ($type -eq 'Test Case' -and $id -match '^[0-9]{6,}$') {
                $tcId = "TC_$id"
                $cases += [pscustomobject]@{
                    testCaseId = $tcId
                    testName = (Make-TestName $title)
                    description = $title
                    category = 'WinClassicApp'
                    priority = 'High'
                    enabled = $true
                    parameters = [pscustomobject]@{
                        'firstLink' = 'Apps'
                        'secondLink' = 'Windows'
                        'AppType' = 'Microsoft Store app (legacy)'
                        'displayName' = '17 App regression'
                        'description' = 'adfgadgadgsthyrth'
                        'publisher' = 'https://www.microsoft.com/store/p/<App-Name>/<ProductID>'
                        'appStoreUrl' = 'https://apps.microsoft.com/detail/9wzdncrfj2wl?hl=en-US&gl=US'
                        'Assignments' = (Get-Assignments $title)
                        'API parameters' = [pscustomobject]@{}
                        'Device Validation' = [pscustomobject]@{
                            'pre-requisite on device' = ''
                            'App Installation Validation' = 'Verify assignment/filter and deployment behavior as per test intent.'
                            'searchTerm' = ''
                            'expectedValue' = ''
                        }
                    }
                }
            }
        }
    }

    $cases = @($cases | Sort-Object testCaseId -Unique)
    $out = [pscustomobject]@{ testCases = $cases }
    $out | ConvertTo-Json -Depth 100 | Set-Content -Path $outPath -Encoding UTF8

    Write-Host "Created: $outPath"
    Write-Host "Cases: $($cases.Count)"
}
finally {
    if (Test-Path $tmp) { Remove-Item -Recurse -Force $tmp }
}
