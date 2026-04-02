# Script to run all Administrative Templates tests in batches of 50 with 6 parallel workers
$ErrorActionPreference = "Continue"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Running All Administrative Templates Tests" -ForegroundColor Cyan
Write-Host "Mode: Batches of 50 tests with 6 parallel workers each" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

# Get all test class names
$path = "IntuneCanaryTests\Security Baseline for Windows 10 and later\"
$files = Get-ChildItem -Path $path -Recurse -Filter "*.cs" -File | Where-Object { $_.FullName -notmatch "InternetExplorer|HoloLensSecurityBaseline|RemoteDesktopServices" }

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

# Divide into batches of 50
$batchSize = 50
$totalBatches = [Math]::Ceiling($testClasses.Count / $batchSize)
$allReports = @()
$totalPassed = 0
$totalFailed = 0
$totalSkipped = 0
$overallStartTime = Get-Date

Write-Host "Will run in $totalBatches batches of up to $batchSize tests each`n" -ForegroundColor Yellow

for ($batchNum = 1; $batchNum -le $totalBatches; $batchNum++) {
    $startIdx = ($batchNum - 1) * $batchSize
    $endIdx = [Math]::Min($startIdx + $batchSize - 1, $testClasses.Count - 1)
    $batchTests = $testClasses[$startIdx..$endIdx]
    
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host "Batch $batchNum of $totalBatches (Tests $($startIdx + 1) to $($endIdx + 1))" -ForegroundColor Cyan
    Write-Host "Running $($batchTests.Count) tests with 6 parallel workers..." -ForegroundColor Cyan
    Write-Host "========================================" -ForegroundColor Cyan
    
    $batchStartTime = Get-Date
    
    # Build filter for this batch
    $filter = ($batchTests | ForEach-Object { "FullyQualifiedName~$_" }) -join "|"
    
    # Run batch with 6 parallel workers
    $result = dotnet test "IntuneCanaryTests\IntuneCanaryTests.csproj" `
        --filter $filter `
        --logger:"console;verbosity=minimal" `
        --no-build `
        -- NUnit.NumberOfTestWorkers=6 `
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
    }
    
    # Check for reports generated during this batch
    $extentReportsPath = "IntuneCanaryTests\ExtentReports"
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
}

$overallEndTime = Get-Date
$overallDuration = $overallEndTime - $overallStartTime
$overallDurationStr = $overallDuration.ToString('hh\:mm\:ss')

# Final consolidated summary
Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "CONSOLIDATED EXECUTION SUMMARY" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Total Batches Executed: $totalBatches" -ForegroundColor White
Write-Host "Total Duration: $overallDurationStr" -ForegroundColor Gray
Write-Host ""
Write-Host "CONSOLIDATED TEST RESULTS:" -ForegroundColor Yellow
Write-Host "  Total Tests: $($totalPassed + $totalFailed + $totalSkipped)" -ForegroundColor White
Write-Host "  Passed: $totalPassed" -ForegroundColor Green
Write-Host "  Failed: $totalFailed" -ForegroundColor $(if ($totalFailed -eq 0) { "Green" } else { "Red" })
Write-Host "  Skipped: $totalSkipped" -ForegroundColor Yellow

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
    Write-Host "No ExtentReports found" -ForegroundColor Yellow
}

# Check for AdvancedReports
$advancedReportsPath = "IntuneCanaryTests\AdvancedReports"
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
