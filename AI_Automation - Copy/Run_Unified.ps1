# Run_Unified.ps1 — Unified test runner with adaptive workers, smart batching, and dependency awareness
# Replaces: Run_Batched_Tests.ps1, Run_SecurityBaseline_Tests.ps1, Run_SecurityBaseline_3Workers.ps1,
#           Run_WinGet_Parallel.ps1
#
# Usage:
#   .\Run_Unified.ps1 -Suite WinGet -Workers auto -Mode headless
#   .\Run_Unified.ps1 -Suite SecurityBaseline -BatchSize 50 -Workers 6
#   .\Run_Unified.ps1 -Suite AppRegression -Mode headed -Workers 2
#   .\Run_Unified.ps1 -Suite All -Workers auto -BatchSize auto

param(
    [Parameter(Mandatory = $true)]
    [ValidateSet("SecurityBaseline", "AppRegression", "WinGet", "Batched", "WindowsRegression", "All")]
    [string]$Suite,

    [Parameter(Mandatory = $false)]
    [string]$Workers = "auto",

    [Parameter(Mandatory = $false)]
    [string]$BatchSize = "auto",

    [Parameter(Mandatory = $false)]
    [ValidateSet("headed", "headless")]
    [string]$Mode = "headed",

    [Parameter(Mandatory = $false)]
    [string]$Filter = "",

    [Parameter(Mandatory = $false)]
    [switch]$NoBuild,

    [Parameter(Mandatory = $false)]
    [switch]$ApplyPermanentFixes
)

$ErrorActionPreference = "Continue"
$overallStartTime = Get-Date

# ============================================================
# ADAPTIVE WORKER SELECTION
# ============================================================
function Get-AdaptiveWorkerCount {
    param([string]$Requested, [string]$SuiteName)

    if ($Requested -ne "auto") {
        $count = [int]$Requested
        if ($count -ge 1 -and $count -le 16) { return $count }
    }

    # Detect available resources
    $cpuCores = (Get-CimInstance Win32_Processor | Measure-Object -Property NumberOfLogicalProcessors -Sum).Sum
    $totalMemGB = [math]::Round((Get-CimInstance Win32_OperatingSystem).TotalVisibleMemorySize / 1MB, 1)
    $availMemGB = [math]::Round((Get-CimInstance Win32_OperatingSystem).FreePhysicalMemory / 1MB, 1)

    # Heuristic: each browser worker needs ~0.5 CPU core and ~1.5 GB RAM
    $cpuBased = [math]::Floor($cpuCores / 2)
    $memBased = [math]::Floor($availMemGB / 1.5)

    $adaptive = [math]::Min($cpuBased, $memBased)
    $adaptive = [math]::Max(2, [math]::Min(8, $adaptive))

    # Suite-specific caps
    switch ($SuiteName) {
        "WinGet"            { $adaptive = [math]::Min($adaptive, 8) }
        "SecurityBaseline"  { $adaptive = [math]::Min($adaptive, 6) }
        "AppRegression"     { $adaptive = [math]::Min($adaptive, 4) }
        default             { $adaptive = [math]::Min($adaptive, 6) }
    }

    Write-Host "  Adaptive workers: $adaptive (CPU: $cpuCores cores, Available RAM: ${availMemGB}GB)" -ForegroundColor DarkGray
    return $adaptive
}

# ============================================================
# SMART BATCH SIZE
# ============================================================
function Get-AdaptiveBatchSize {
    param([string]$Requested, [int]$TotalTests)

    if ($Requested -ne "auto") {
        return [int]$Requested
    }

    # Heuristic: aim for 3-5 batches for any size
    if ($TotalTests -le 20) { return $TotalTests }
    if ($TotalTests -le 50) { return 25 }
    if ($TotalTests -le 100) { return 50 }
    return [math]::Ceiling($TotalTests / 4)
}

# ============================================================
# TEST DISCOVERY
# ============================================================
function Get-TestClasses {
    param([string]$SuiteName, [string]$CustomFilter)

    $testClasses = @()

    switch ($SuiteName) {
        "SecurityBaseline" {
            $path = "IntuneCanaryTests\Security Baseline for Windows 10 and later\"
            $files = Get-ChildItem -Path $path -Recurse -Filter "*.cs" -File | Where-Object {
                $_.FullName -notmatch "InternetExplorer|HoloLensSecurityBaseline|RemoteDesktopServices"
            }
            foreach ($file in $files) {
                try {
                    $content = Get-Content -LiteralPath $file.FullName -Raw -ErrorAction Stop
                    if ($content -match 'public\s+class\s+(\w+)' -and $content -match '\[TestFixture\]') {
                        $testClasses += $matches[1]
                    }
                } catch { }
            }
        }

        "WinGet" {
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
        }

        "AppRegression" {
            $path = "IntuneCanaryTests\TestCases\AppAutomation\"
            if (Test-Path $path) {
                $files = Get-ChildItem -Path $path -Recurse -Filter "*.cs" -File
                foreach ($file in $files) {
                    try {
                        $content = Get-Content -LiteralPath $file.FullName -Raw -ErrorAction Stop
                        if ($content -match 'public\s+class\s+(\w+)' -and $content -match '\[TestFixture\]') {
                            $testClasses += $matches[1]
                        }
                    } catch { }
                }
            }
        }

        "WindowsRegression" {
            $testClasses = @("Test_Set_A_Windows_Regression")
        }

        "Batched" {
            # Same as SecurityBaseline — legacy alias
            return Get-TestClasses -SuiteName "SecurityBaseline" -CustomFilter $CustomFilter
        }

        "All" {
            $testClasses += Get-TestClasses -SuiteName "SecurityBaseline" -CustomFilter ""
            $testClasses += Get-TestClasses -SuiteName "WinGet" -CustomFilter ""
            $testClasses += Get-TestClasses -SuiteName "AppRegression" -CustomFilter ""
        }
    }

    # Apply custom filter if specified
    if (![string]::IsNullOrEmpty($CustomFilter)) {
        $testClasses = $testClasses | Where-Object { $_ -match $CustomFilter }
    }

    return $testClasses
}

# ============================================================
# POST-BATCH CLEANUP
# ============================================================
function Invoke-PostBatchCleanup {
    # Kill orphaned browser processes that may have leaked
    $orphans = Get-Process -Name "msedge", "chrome", "chromium" -ErrorAction SilentlyContinue |
        Where-Object { $_.StartTime -lt (Get-Date).AddMinutes(-30) }

    if ($orphans.Count -gt 0) {
        Write-Host "  Cleaning up $($orphans.Count) orphaned browser processes..." -ForegroundColor DarkGray
        $orphans | Stop-Process -Force -ErrorAction SilentlyContinue
    }

    # Clear temp files older than 1 hour
    $tempPath = [System.IO.Path]::GetTempPath()
    Get-ChildItem "$tempPath\playwright*" -ErrorAction SilentlyContinue |
        Where-Object { $_.LastWriteTime -lt (Get-Date).AddHours(-1) } |
        Remove-Item -Recurse -Force -ErrorAction SilentlyContinue
}

# ============================================================
# TEST DEPENDENCY TRACKING
# ============================================================
$Script:FailedCreateTests = @{}

function Test-ShouldSkipDependentTest {
    param([string]$TestClassName)

    # If a "Create" test failed, skip corresponding Edit/Delete tests
    foreach ($failedCreate in $Script:FailedCreateTests.Keys) {
        # Extract test ID from class name (e.g., "Test_12345_..." → "12345")
        if ($failedCreate -match 'Test_(\d+)_') {
            $testId = $matches[1]
            if ($TestClassName -match $testId -and $TestClassName -ne $failedCreate) {
                Write-Host "  SKIPPED: '$TestClassName' — dependent on failed create test '$failedCreate'" -ForegroundColor Yellow
                return $true
            }
        }
    }
    return $false
}

# ============================================================
# MAIN EXECUTION
# ============================================================

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Unified Test Runner" -ForegroundColor Cyan
Write-Host "Suite: $Suite | Mode: $Mode | Workers: $Workers | BatchSize: $BatchSize" -ForegroundColor Cyan
Write-Host "Started: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

# Discover tests
$testClasses = Get-TestClasses -SuiteName $Suite -CustomFilter $Filter
if ($testClasses.Count -eq 0) {
    Write-Host "No test classes found for suite '$Suite' with filter '$Filter'" -ForegroundColor Red
    exit 1
}

Write-Host "`nDiscovered $($testClasses.Count) test classes" -ForegroundColor Green

# Compute adaptive values
$workerCount = Get-AdaptiveWorkerCount -Requested $Workers -SuiteName $Suite
$batchCount = Get-AdaptiveBatchSize -Requested $BatchSize -TotalTests $testClasses.Count

# Select runsettings
$settingsFile = switch ($Mode) {
    "headed"   { "headed.runsettings" }
    "headless" {
        if ($workerCount -ge 6) { "headless_aggressive.runsettings" }
        else { "headed.runsettings" }
    }
}
# Override if specific settings exist for this worker count
$specificSettings = "parallel_${workerCount}workers.runsettings"
if (Test-Path $specificSettings) { $settingsFile = $specificSettings }

Write-Host "Workers: $workerCount | Batch Size: $batchCount | Settings: $settingsFile" -ForegroundColor Yellow

# Check self-healing API key
$envFile = ".env"
if (Test-Path $envFile) {
    $envContent = Get-Content $envFile -Raw
    if ($envContent -match "GOOGLE_AI_API_KEY") {
        Write-Host "Self-Healing: AI strategies (Gemini) ACTIVE" -ForegroundColor Green
    } else {
        Write-Host "Self-Healing: Static fallback strategies only (no API key)" -ForegroundColor Yellow
    }
}

# Build phase (unless --NoBuild)
if (-not $NoBuild) {
    Write-Host "`n--- Building project ---" -ForegroundColor Yellow
    $buildStart = Get-Date
    $buildResult = dotnet build "IntuneCanaryTests\IntuneCanaryTests.csproj" --configuration Debug 2>&1 | Out-String
    $buildDuration = (Get-Date) - $buildStart

    if ($LASTEXITCODE -ne 0) {
        Write-Host "Build FAILED! ($($buildDuration.TotalSeconds.ToString('F1'))s)" -ForegroundColor Red
        Write-Host $buildResult
        exit 1
    }
    Write-Host "Build succeeded ($($buildDuration.TotalSeconds.ToString('F1'))s)" -ForegroundColor Green
}

# Divide into batches
$totalBatches = [Math]::Ceiling($testClasses.Count / $batchCount)
$totalPassed = 0
$totalFailed = 0
$totalSkipped = 0

Write-Host "`nWill run in $totalBatches batches of up to $batchCount tests each`n" -ForegroundColor Yellow

for ($batchNum = 1; $batchNum -le $totalBatches; $batchNum++) {
    $startIdx = ($batchNum - 1) * $batchCount
    $endIdx = [Math]::Min($startIdx + $batchCount - 1, $testClasses.Count - 1)
    $batchTests = $testClasses[$startIdx..$endIdx]

    # Filter out tests dependent on previously failed create tests
    $filteredBatch = @()
    foreach ($test in $batchTests) {
        if (-not (Test-ShouldSkipDependentTest -TestClassName $test)) {
            $filteredBatch += $test
        } else {
            $totalSkipped++
        }
    }

    if ($filteredBatch.Count -eq 0) {
        Write-Host "Batch $batchNum: All tests skipped (dependencies failed)" -ForegroundColor Yellow
        continue
    }

    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host "Batch $batchNum/$totalBatches ($($filteredBatch.Count) tests, $workerCount workers)" -ForegroundColor Cyan
    Write-Host "========================================" -ForegroundColor Cyan

    $batchStartTime = Get-Date

    # Build filter
    $dotnetFilter = ($filteredBatch | ForEach-Object { "FullyQualifiedName~$_" }) -join "|"

    # Run batch
    $output = dotnet test "IntuneCanaryTests\IntuneCanaryTests.csproj" `
        --filter $dotnetFilter `
        --logger:"console;verbosity=minimal" `
        --logger:"trx;LogFileName=batch_${batchNum}.trx" `
        --settings $settingsFile `
        --no-build 2>&1 | Out-String

    $batchDuration = (Get-Date) - $batchStartTime
    $batchExitCode = $LASTEXITCODE

    # Parse results
    if ($output -match "Passed:\s*(\d+)") { $totalPassed += [int]$matches[1] }
    if ($output -match "Failed:\s*(\d+)") {
        $batchFailed = [int]$matches[1]
        $totalFailed += $batchFailed

        # Track failed tests for dependency skipping
        if ($batchFailed -gt 0) {
            foreach ($test in $filteredBatch) {
                if ($test -match "Create" -or $output -match "$test.*Failed") {
                    $Script:FailedCreateTests[$test] = $true
                }
            }
        }
    }

    # Status
    $statusColor = if ($batchExitCode -eq 0) { "Green" } else { "Red" }
    Write-Host "Batch $batchNum complete: $($batchDuration.TotalMinutes.ToString('F1')) min (exit: $batchExitCode)" -ForegroundColor $statusColor

    # Post-batch cleanup
    Invoke-PostBatchCleanup
}

# ============================================================
# FINAL SUMMARY
# ============================================================
$overallDuration = (Get-Date) - $overallStartTime

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "UNIFIED TEST RUN COMPLETE" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Suite:      $Suite" -ForegroundColor White
Write-Host "Workers:    $workerCount" -ForegroundColor White
Write-Host "Batches:    $totalBatches" -ForegroundColor White
Write-Host "Duration:   $($overallDuration.ToString('hh\:mm\:ss'))" -ForegroundColor White
Write-Host "Passed:     $totalPassed" -ForegroundColor Green
Write-Host "Failed:     $totalFailed" -ForegroundColor $(if ($totalFailed -gt 0) { "Red" } else { "Green" })
Write-Host "Skipped:    $totalSkipped" -ForegroundColor $(if ($totalSkipped -gt 0) { "Yellow" } else { "White" })
Write-Host "========================================" -ForegroundColor Cyan

if ($totalFailed -gt 0) { exit 1 } else { exit 0 }
