$ErrorActionPreference = 'Stop'

Set-Location 'c:\Users\v-arulmani\Downloads\AI_Automation 5\AI_Automation - Copy'

$basePath = 'TestData_AppReggersion\windows Regression.json'
$aPath = 'TestData_AppReggersion\windows Regression_AssignmentFilter_FromExcel_Generated.json'
$bPath = 'TestData_AppReggersion\windows Regression_Edge_FromExcel_Generated.json'

$baseText = Get-Content -Raw $basePath
# Repair trailing commas before ] or } to parse legacy JSON.
$baseText = [regex]::Replace($baseText, ',(?=\s*\])', '')
$baseText = [regex]::Replace($baseText, ',(?=\s*\})', '')
$base = $baseText | ConvertFrom-Json

$a = Get-Content -Raw $aPath | ConvertFrom-Json
$b = Get-Content -Raw $bPath | ConvertFrom-Json

$all = @()
$all += @($base.testCases)
$all += @($a.testCases)
$all += @($b.testCases)

$seen = New-Object 'System.Collections.Generic.HashSet[string]'
$merged = New-Object System.Collections.ArrayList

foreach ($tc in $all) {
    if (-not $tc) { continue }

    $id = [string]$tc.testCaseId
    if ([string]::IsNullOrWhiteSpace($id)) {
        [void]$merged.Add($tc)
        continue
    }

    if ($seen.Add($id)) {
        [void]$merged.Add($tc)
    }
}

$out = [pscustomobject]@{ testCases = $merged }
$out | ConvertTo-Json -Depth 100 | Set-Content -Path $basePath -Encoding UTF8

$check = Get-Content -Raw $basePath | ConvertFrom-Json
Write-Host "FINAL_OK"
Write-Host ("FINAL_COUNT={0}" -f @($check.testCases).Count)
Write-Host ("SOURCE_NEW_COUNT={0}" -f (@($a.testCases).Count + @($b.testCases).Count))
