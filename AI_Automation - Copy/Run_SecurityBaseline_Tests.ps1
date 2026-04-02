# Script to run all Security Baseline for Windows 10 and later tests in batches
# Uses 8 parallel workers in headless mode
$ErrorActionPreference = "Continue"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Running Security Baseline for Windows 10 and later Tests" -ForegroundColor Cyan
Write-Host "Mode: Batches of 50 tests with 8 parallel workers (Headless)" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

# Get all test class names from Security Baseline for Windows 10 and later folder
$path = "IntuneCanaryTests\Security Baseline for Windows 10 and later\"
$files = Get-ChildItem -Path $path -Recurse -Filter "*.cs" -File

$testClasses = @()
foreach ($file in $files) {
    try {
        $content = Get-Content -LiteralPath $file.FullName -Raw -ErrorAction Stop
        if ($content -match 'public\s+class\s+(\w+)') {
            $className = $matches[1]
            if ($content -match '\[TestFixture\]') {
                $testClasses += $className
            }
        }
    }
    catch {
        Write-Host "Warning: Could not read file $($file.Name)" -ForegroundColor Yellow
    }
}

Write-Host "`nTotal test classes found: $($testClasses.Count)" -ForegroundColor Green

# Display subfolder breakdown
Write-Host "`nSubfolders included:" -ForegroundColor Yellow
$subfolders = Get-ChildItem -Path $path -Directory
foreach ($subfolder in $subfolders) {
    $subfolderTests = Get-ChildItem -Path $subfolder.FullName -Recurse -Filter "*.cs" -File
    Write-Host "  - $($subfolder.Name): $($subfolderTests.Count) test files" -ForegroundColor Gray
}

# Divide into batches of 50
$batchSize = 50
$totalBatches = [Math]::Ceiling($testClasses.Count / $batchSize)
$allReports = @()
$totalPassed = 0
$totalFailed = 0
$totalSkipped = 0
$overallStartTime = Get-Date

Write-Host "`nWill run in $totalBatches batches of up to $batchSize tests each" -ForegroundColor Yellow
Write-Host "Using 8 parallel workers in HEADLESS mode`n" -ForegroundColor Yellow

for ($batchNum = 1; $batchNum -le $totalBatches; $batchNum++) {
    $startIdx = ($batchNum - 1) * $batchSize
    $endIdx = [Math]::Min($startIdx + $batchSize - 1, $testClasses.Count - 1)
    $batchTests = $testClasses[$startIdx..$endIdx]
    
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host "Batch $batchNum of $totalBatches (Tests $($startIdx + 1) to $($endIdx + 1))" -ForegroundColor Cyan
    Write-Host "Running $($batchTests.Count) tests with 8 parallel workers..." -ForegroundColor Cyan
    Write-Host "========================================" -ForegroundColor Cyan
    
    $batchStartTime = Get-Date
    
    # Build filter for this batch
    $filter = ($batchTests | ForEach-Object { "FullyQualifiedName~$_" }) -join "|"
    
    # Run batch with 8 parallel workers in headless mode
    $result = dotnet test "IntuneCanaryTests\IntuneCanaryTests.csproj" `
        --settings "headless_aggressive.runsettings" `
        --filter $filter `
        --logger:"console;verbosity=minimal" `
        --no-build `
        -- NUnit.NumberOfTestWorkers=8 `
        2>&1 | Out-String
    
    $batchEndTime = Get-Date
    $batchDuration = $batchEndTime - $batchStartTime
    
    # Parse results
    if ($result -match "Passed!\s+-\s+Failed:\s+(\d+),\s+Passed:\s+(\d+),\s+Skipped:\s+(\d+)") {
        $batchFailed = [int]$matches[1]
        $batchPassed = [int]$matches[2]
        $batchSkipped = [int]$matches[3]
        
        $totalFailed += $batchFailed
        $totalPassed += $batchPassed
        $totalSkipped += $batchSkipped
        
        Write-Host "`nBatch $batchNum Results:" -ForegroundColor Green
        Write-Host "  Passed: $batchPassed" -ForegroundColor Green
        Write-Host "  Failed: $batchFailed" -ForegroundColor $(if ($batchFailed -eq 0) { "Green" } else { "Red" })
        Write-Host "  Skipped: $batchSkipped" -ForegroundColor Yellow
        $durationStr = $batchDuration.ToString('hh\:mm\:ss')
        Write-Host "  Duration: $durationStr" -ForegroundColor Gray
    }
    elseif ($result -match "Failed!\s+-\s+Failed:\s+(\d+),\s+Passed:\s+(\d+),\s+Skipped:\s+(\d+)") {
        $batchFailed = [int]$matches[1]
        $batchPassed = [int]$matches[2]
        $batchSkipped = [int]$matches[3]
        
        $totalFailed += $batchFailed
        $totalPassed += $batchPassed
        $totalSkipped += $batchSkipped
        
        Write-Host "`nBatch $batchNum Results:" -ForegroundColor Red
        Write-Host "  Passed: $batchPassed" -ForegroundColor Green
        Write-Host "  Failed: $batchFailed" -ForegroundColor Red
        Write-Host "  Skipped: $batchSkipped" -ForegroundColor Yellow
        $durationStr = $batchDuration.ToString('hh\:mm\:ss')
        Write-Host "  Duration: $durationStr" -ForegroundColor Gray
    }
    else {
        Write-Host "`nBatch ${batchNum}: Could not parse results" -ForegroundColor Yellow
        Write-Host "Raw output snippet:" -ForegroundColor Gray
        Write-Host ($result | Select-Object -First 20) -ForegroundColor Gray
    }
    
    # Check for reports generated during this batch
    $extentReportsPath = "ExtentReports"
    if (Test-Path $extentReportsPath) {
        $batchReports = Get-ChildItem -Path $extentReportsPath -Filter "TestReport_*.html" -File | 
            Where-Object { $_.LastWriteTime -ge $batchStartTime } |
            Sort-Object LastWriteTime -Descending
        if ($batchReports) {
            $allReports += $batchReports
            Write-Host "  ExtentReport generated: $($batchReports[0].Name)" -ForegroundColor Cyan
        }
    }
    
    Write-Host ""
    
    # Brief pause between batches to allow resources to free up
    if ($batchNum -lt $totalBatches) {
        Write-Host "Pausing 5 seconds before next batch..." -ForegroundColor Gray
        Start-Sleep -Seconds 5
    }
}

$overallEndTime = Get-Date
$overallDuration = $overallEndTime - $overallStartTime
$overallDurationStr = $overallDuration.ToString('hh\:mm\:ss')

# Final consolidated summary
Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "CONSOLIDATED EXECUTION SUMMARY" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Test Suite: Security Baseline for Windows 10 and later" -ForegroundColor White
Write-Host "Total Batches Executed: $totalBatches" -ForegroundColor White
Write-Host "Workers per Batch: 8 (Headless Mode)" -ForegroundColor White
Write-Host "Total Duration: $overallDurationStr" -ForegroundColor Gray
Write-Host ""
Write-Host "CONSOLIDATED TEST RESULTS:" -ForegroundColor Yellow
Write-Host "  Total Tests: $($totalPassed + $totalFailed + $totalSkipped)" -ForegroundColor White
Write-Host "  Passed: $totalPassed" -ForegroundColor Green
Write-Host "  Failed: $totalFailed" -ForegroundColor $(if ($totalFailed -eq 0) { "Green" } else { "Red" })
Write-Host "  Skipped: $totalSkipped" -ForegroundColor Yellow

$passRate = if (($totalPassed + $totalFailed) -gt 0) { 
    [math]::Round(($totalPassed / ($totalPassed + $totalFailed)) * 100, 2) 
} else { 0 }
Write-Host "  Pass Rate: $passRate%" -ForegroundColor $(if ($passRate -ge 80) { "Green" } elseif ($passRate -ge 50) { "Yellow" } else { "Red" })

# Show all ExtentReports generated
Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "EXTENT REPORTS GENERATED" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

if ($allReports.Count -gt 0) {
    Write-Host "Total Reports: $($allReports.Count)" -ForegroundColor Green
    Write-Host ""
    for ($i = 0; $i -lt $allReports.Count; $i++) {
        $report = $allReports[$i]
        Write-Host "Report $($i + 1):" -ForegroundColor Cyan
        Write-Host "  Path: $($report.FullName)" -ForegroundColor White
        Write-Host "  Generated: $($report.LastWriteTime)" -ForegroundColor Gray
        Write-Host "  Size: $([math]::Round($report.Length / 1KB, 2)) KB" -ForegroundColor Gray
        Write-Host ""
    }
} else {
    Write-Host "No ExtentReports found in ExtentReports folder" -ForegroundColor Yellow
}

# Check for AdvancedReports
$advancedReportsPath = "AdvancedReports"
if (Test-Path $advancedReportsPath) {
    $advancedReports = Get-ChildItem -Path $advancedReportsPath -Filter "AdvancedReport_*.html" -File | 
        Where-Object { $_.LastWriteTime -ge $overallStartTime } |
        Sort-Object LastWriteTime -Descending
    if ($advancedReports) {
        Write-Host "========================================" -ForegroundColor Cyan
        Write-Host "ADVANCED REPORTS GENERATED" -ForegroundColor Cyan
        Write-Host "========================================" -ForegroundColor Cyan
        foreach ($report in $advancedReports) {
            Write-Host "  Path: $($report.FullName)" -ForegroundColor White
            Write-Host "  Generated: $($report.LastWriteTime)" -ForegroundColor Gray
            Write-Host "  Size: $([math]::Round($report.Length / 1KB, 2)) KB" -ForegroundColor Gray
            Write-Host ""
        }
    }
}

# Generate TRX Report Summary
$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"
$trxPath = "TestResults"
if (Test-Path $trxPath) {
    $trxFiles = Get-ChildItem -Path $trxPath -Filter "*.trx" -File | 
        Where-Object { $_.LastWriteTime -ge $overallStartTime } |
        Sort-Object LastWriteTime -Descending
    if ($trxFiles) {
        Write-Host "========================================" -ForegroundColor Cyan
        Write-Host "TRX TEST RESULTS FILES" -ForegroundColor Cyan
        Write-Host "========================================" -ForegroundColor Cyan
        foreach ($trx in $trxFiles) {
            Write-Host "  Path: $($trx.FullName)" -ForegroundColor White
            Write-Host "  Generated: $($trx.LastWriteTime)" -ForegroundColor Gray
            Write-Host ""
        }
    }
}

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "EXECUTION COMPLETE" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

if ($totalFailed -eq 0) {
    Write-Host "`nAll tests passed!" -ForegroundColor Green
    exit 0
} else {
    Write-Host "`n$totalFailed test(s) failed. Review the reports above." -ForegroundColor Yellow
    exit 1
}
