# Run all 16 WinGet App Test Cases in parallel with self-healing enabled
# Configuration: 8 parallel workers, headless mode, 20-min per-test timeout
# Self-healing: AI (Gemini) + 16-strategy fallback cascade active during execution
$ErrorActionPreference = "Continue"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "WinGet App Test Cases - Parallel Execution with Self-Healing" -ForegroundColor Cyan
Write-Host "Mode: 8 parallel workers (Headless) | Self-Healing: ENABLED" -ForegroundColor Cyan
Write-Host "Per-Test Timeout: 20 minutes | Session Timeout: 2 hours" -ForegroundColor Cyan
Write-Host "Started: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

# All 16 WinGet test class names
$testClasses = @(
    "Test_14673239_App_Signoff_Test_WinGet_App_Test_Cases",
    "Test_14673246_App_Signoff_Test_WinGet_App_Test_Cases",
    "Test_14673264_App_Signoff_Test_WinGet_App_Test_Cases",
    "Test_14673598_App_Signoff_Test_WinGet_App_Test_Cases",
    "Test_16407789_App_Signoff_Test_WinGet_App_Test_Cases",
    "Test_16407791_App_Signoff_Test_WinGet_App_Test_Cases",
    "Test_16408400_App_Signoff_Test_WinGet_App_Test_Cases",
    "Test_16408405_App_Signoff_Test_WinGet_App_Test_Cases",
    "Test_16408410_App_Signoff_Test_WinGet_App_Test_Cases",
    "Test_16408416_App_Signoff_Test_WinGet_App_Test_Cases",
    "Test_16408417_App_Signoff_Test_WinGet_App_Test_Cases",
    "Test_16408419_App_Signoff_Test_WinGet_App_Test_Cases",
    "Test_16820381_App_Signoff_Test_WinGet_App_Test_Cases",
    "Test_16820413_App_Signoff_Test_WinGet_App_Test_Cases",
    "Test_16820429_App_Signoff_Test_WinGet_App_Test_Cases",
    "Test_16820432_App_Signoff_Test_WinGet_App_Test_Cases"
)

Write-Host "`nTotal test classes: $($testClasses.Count)" -ForegroundColor Green

# Verify .env file for self-healing API key
$envFile = ".env"
if (Test-Path $envFile) {
    $envContent = Get-Content $envFile -Raw
    if ($envContent -match "GOOGLE_AI_API_KEY") {
        Write-Host "Self-Healing: GOOGLE_AI_API_KEY found in .env - AI strategies (Gemini) ACTIVE" -ForegroundColor Green
    } else {
        Write-Host "Self-Healing: GOOGLE_AI_API_KEY NOT found - only static fallback strategies (4-16) will run" -ForegroundColor Yellow
    }
} else {
    Write-Host "Self-Healing: .env file not found - only static fallback strategies (4-16) will run" -ForegroundColor Yellow
}

Write-Host ""

# ---- Build Phase ----
Write-Host "========================================" -ForegroundColor Yellow
Write-Host "BUILDING PROJECT..." -ForegroundColor Yellow
Write-Host "========================================" -ForegroundColor Yellow

$buildStartTime = Get-Date
$buildResult = dotnet build "IntuneCanaryTests\IntuneCanaryTests.csproj" --configuration Debug 2>&1 | Out-String
$buildDuration = (Get-Date) - $buildStartTime

if ($LASTEXITCODE -ne 0) {
    Write-Host "Build FAILED! ($($buildDuration.TotalSeconds.ToString('F1'))s)" -ForegroundColor Red
    Write-Host $buildResult -ForegroundColor Gray
    Write-Host "Attempting to run tests anyway..." -ForegroundColor Yellow
} else {
    Write-Host "Build successful! ($($buildDuration.TotalSeconds.ToString('F1'))s)" -ForegroundColor Green
}

# ---- Test Execution Phase ----
Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "EXECUTING 16 TESTS WITH 8 PARALLEL WORKERS" -ForegroundColor Cyan
Write-Host "Self-Healing: Active (triggers at retry #30 per element)" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

# Build NUnit filter for all 16 test classes
$filter = ($testClasses | ForEach-Object { "FullyQualifiedName~$_" }) -join " | "

Write-Host "`nFilter: $filter" -ForegroundColor Gray
Write-Host ""

$testStartTime = Get-Date

# Run all 16 tests in parallel with 8 workers
$result = dotnet test "IntuneCanaryTests\IntuneCanaryTests.csproj" `
    --settings "winget_8workers_headless.runsettings" `
    --filter $filter `
    --logger:"console;verbosity=normal" `
    --logger:"trx;LogFileName=WinGet_Parallel_Results_$(Get-Date -Format 'yyyyMMdd_HHmmss').trx" `
    --no-build `
    -- NUnit.NumberOfTestWorkers=8 NUnit.DefaultTimeout=1200000 `
    2>&1 | Out-String

$testEndTime = Get-Date
$testDuration = $testEndTime - $testStartTime

# ---- Parse Test Results ----
$totalPassed = 0
$totalFailed = 0
$totalSkipped = 0

if ($result -match "Passed!\s+-\s+Failed:\s+(\d+),\s+Passed:\s+(\d+),\s+Skipped:\s+(\d+)") {
    $totalFailed = [int]$matches[1]
    $totalPassed = [int]$matches[2]
    $totalSkipped = [int]$matches[3]
} elseif ($result -match "Failed!\s+-\s+Failed:\s+(\d+),\s+Passed:\s+(\d+),\s+Skipped:\s+(\d+)") {
    $totalFailed = [int]$matches[1]
    $totalPassed = [int]$matches[2]
    $totalSkipped = [int]$matches[3]
} else {
    Write-Host "Could not parse test results from output" -ForegroundColor Yellow
    Write-Host "Raw output (last 50 lines):" -ForegroundColor Gray
    $outputLines = $result -split "`n"
    Write-Host ($outputLines | Select-Object -Last 50 | Out-String) -ForegroundColor Gray
}

# ---- Self-Healing Activity Report ----
Write-Host "`n========================================" -ForegroundColor Magenta
Write-Host "SELF-HEALING ACTIVITY REPORT" -ForegroundColor Magenta
Write-Host "========================================" -ForegroundColor Magenta

$healedCount = 0
$healSignalCount = 0
$permanentFixCount = 0

# Extract healing events from output
$outputLines = $result -split "`n"

foreach ($line in $outputLines) {
    if ($line -match "\[AI-HEALED\]") {
        $healedCount++
        Write-Host "  $($line.Trim())" -ForegroundColor Green
    }
    elseif ($line -match "\[HEAL_SIGNAL\]") {
        $healSignalCount++
        Write-Host "  $($line.Trim())" -ForegroundColor Yellow
    }
    elseif ($line -match "\[SelfHealing\]") {
        Write-Host "  $($line.Trim())" -ForegroundColor Cyan
    }
    elseif ($line -match "PERMANENT FIX") {
        $permanentFixCount++
        Write-Host "  $($line.Trim())" -ForegroundColor Red
    }
    elseif ($line -match "\[AI_PAGE_LOCATORS_JSON\]") {
        Write-Host "  [AI Page Locator Extraction triggered]" -ForegroundColor Magenta
    }
}

if ($healedCount -eq 0 -and $healSignalCount -eq 0) {
    Write-Host "  No self-healing events detected (all locators resolved on first attempt)" -ForegroundColor Green
} else {
    Write-Host "`n  Summary:" -ForegroundColor Magenta
    Write-Host "    AI-Healed elements: $healedCount" -ForegroundColor $(if ($healedCount -gt 0) { "Green" } else { "Gray" })
    Write-Host "    Heal signals raised: $healSignalCount" -ForegroundColor $(if ($healSignalCount -gt 0) { "Yellow" } else { "Gray" })
    Write-Host "    Permanent fix needed: $permanentFixCount" -ForegroundColor $(if ($permanentFixCount -gt 0) { "Red" } else { "Gray" })
}

# ---- Consolidated Summary ----
Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "CONSOLIDATED EXECUTION SUMMARY" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Test Suite: WinGet App Test Cases (App Signoff)" -ForegroundColor White
Write-Host "Total Tests: $($testClasses.Count)" -ForegroundColor White
Write-Host "Parallel Workers: 8" -ForegroundColor White
Write-Host "Mode: Headless" -ForegroundColor White
Write-Host "Self-Healing: Enabled (16-strategy cascade + Gemini AI)" -ForegroundColor White
$durationStr = $testDuration.ToString('hh\:mm\:ss')
Write-Host "Duration: $durationStr" -ForegroundColor Gray
Write-Host ""
Write-Host "TEST RESULTS:" -ForegroundColor Yellow
Write-Host "  Passed:  $totalPassed" -ForegroundColor Green
Write-Host "  Failed:  $totalFailed" -ForegroundColor $(if ($totalFailed -eq 0) { "Green" } else { "Red" })
Write-Host "  Skipped: $totalSkipped" -ForegroundColor Yellow

$passRate = if (($totalPassed + $totalFailed) -gt 0) {
    [math]::Round(($totalPassed / ($totalPassed + $totalFailed)) * 100, 2)
} else { 0 }
Write-Host "  Pass Rate: $passRate%" -ForegroundColor $(if ($passRate -ge 80) { "Green" } elseif ($passRate -ge 50) { "Yellow" } else { "Red" })

# ---- Report Collection ----
Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "REPORTS" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

# ExtentReports
$extentReportsPath = "ExtentReports"
if (Test-Path $extentReportsPath) {
    $reports = Get-ChildItem -Path $extentReportsPath -Filter "TestReport_*.html" -File |
        Where-Object { $_.LastWriteTime -ge $testStartTime } |
        Sort-Object LastWriteTime -Descending
    if ($reports) {
        Write-Host "ExtentReports:" -ForegroundColor Green
        foreach ($report in $reports) {
            Write-Host "  $($report.FullName)" -ForegroundColor White
            Write-Host "  Generated: $($report.LastWriteTime) | Size: $([math]::Round($report.Length / 1KB, 2)) KB" -ForegroundColor Gray
        }
    }
}

# TRX Files
$trxPath = "TestResults"
if (Test-Path $trxPath) {
    $trxFiles = Get-ChildItem -Path $trxPath -Filter "WinGet_Parallel_Results_*.trx" -File |
        Where-Object { $_.LastWriteTime -ge $testStartTime } |
        Sort-Object LastWriteTime -Descending
    if ($trxFiles) {
        Write-Host "`nTRX Results:" -ForegroundColor Green
        foreach ($trx in $trxFiles) {
            Write-Host "  $($trx.FullName)" -ForegroundColor White
            Write-Host "  Generated: $($trx.LastWriteTime)" -ForegroundColor Gray
        }
    }
}

# AdvancedReports
$advancedReportsPath = "AdvancedReports"
if (Test-Path $advancedReportsPath) {
    $advReports = Get-ChildItem -Path $advancedReportsPath -Filter "AdvancedReport_*.html" -File |
        Where-Object { $_.LastWriteTime -ge $testStartTime } |
        Sort-Object LastWriteTime -Descending
    if ($advReports) {
        Write-Host "`nAdvanced Reports:" -ForegroundColor Green
        foreach ($report in $advReports) {
            Write-Host "  $($report.FullName)" -ForegroundColor White
        }
    }
}

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "EXECUTION COMPLETE" -ForegroundColor Cyan
Write-Host "Finished: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

if ($totalFailed -eq 0 -and ($totalPassed + $totalSkipped) -gt 0) {
    Write-Host "`nAll tests passed!" -ForegroundColor Green
    exit 0
} else {
    Write-Host "`n$totalFailed test(s) failed. Review reports above." -ForegroundColor Yellow
    if ($healedCount -gt 0) {
        Write-Host "Note: $healedCount element(s) were self-healed during execution." -ForegroundColor Magenta
    }
    if ($permanentFixCount -gt 0) {
        Write-Host "ACTION: $permanentFixCount element(s) need permanent locator fixes (healed 3+ times)." -ForegroundColor Red
    }
    exit 1
}
