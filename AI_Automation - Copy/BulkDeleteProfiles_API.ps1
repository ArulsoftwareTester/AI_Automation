<#
Bulk delete Intune Security Baseline PROFILES (intents) using Microsoft Graph /beta.

The Automation_ profiles are "intents" created from a template.
- List intents:  GET    https://graph.microsoft.com/beta/deviceManagement/intents
- Delete intent: DELETE https://graph.microsoft.com/beta/deviceManagement/intents/{managedDeviceIntentId}

Requires permission: DeviceManagementConfiguration.ReadWrite.All
#>

param(
    # Delete only profiles whose displayName matches this regex (case-insensitive)
    [string]$NameMatchRegex = "^Automation_",

    # If set, only prints what it would delete (no DELETE calls)
    [switch]$DryRun,

    # Optional: export results to CSV
    [string]$CsvPath = ".\SecurityBaseline_DeleteResults.csv"
)

# -----------------------------
# 1) Acquire token
# -----------------------------
if (-not $env:GRAPH_TOKEN -or [string]::IsNullOrWhiteSpace($env:GRAPH_TOKEN)) {
    Write-Host "ERROR: Set your bearer token in environment variable GRAPH_TOKEN." -ForegroundColor Red
    Write-Host "Example: `$env:GRAPH_TOKEN = '<access_token>'"
    Write-Host ""
    Write-Host "To get a token, use one of:"
    Write-Host "  1) Connect-MgGraph -Scopes 'DeviceManagementConfiguration.ReadWrite.All'"
    Write-Host "     `$env:GRAPH_TOKEN = (Get-MgContext).AuthToken"
    Write-Host "  2) az account get-access-token --resource https://graph.microsoft.com --query accessToken -o tsv"
    exit 1
}

$headers = @{
    "Authorization" = "Bearer $($env:GRAPH_TOKEN)"
    "Accept"        = "application/json"
}

# -----------------------------
# 2) List all intents (security baseline profiles) — paged
# -----------------------------
Write-Host "Fetching all security baseline profiles (intents)..." -ForegroundColor Cyan
$intents = @()
$uri = "https://graph.microsoft.com/beta/deviceManagement/intents"

do {
    $resp = Invoke-RestMethod -Method GET -Uri $uri -Headers $headers
    if ($resp.value) { $intents += $resp.value }
    $uri = $resp.'@odata.nextLink'
} while ($uri)

if (-not $intents -or $intents.Count -eq 0) {
    Write-Host "No intents (profiles) returned from /deviceManagement/intents." -ForegroundColor Yellow
    exit 0
}

Write-Host "Total intents found: $($intents.Count)"

# -----------------------------
# 3) Filter to profiles matching the regex
# -----------------------------
$candidates = $intents | Where-Object {
    $_.displayName -match $NameMatchRegex
}

Write-Host "Profiles matching regex '$NameMatchRegex': $($candidates.Count)" -ForegroundColor Cyan

if ($candidates.Count -eq 0) {
    Write-Host "Nothing to delete." -ForegroundColor Yellow
    exit 0
}

# Show what will be deleted
Write-Host ""
Write-Host "Profiles to delete:" -ForegroundColor Yellow
$candidates | ForEach-Object { Write-Host "  - $($_.displayName) ($($_.id))" }
Write-Host ""

if (-not $DryRun) {
    $confirm = Read-Host "Proceed with deleting $($candidates.Count) profiles? (y/N)"
    if ($confirm -ne 'y') {
        Write-Host "Aborted." -ForegroundColor Yellow
        exit 0
    }
}

# -----------------------------
# 4) Delete each profile by ID
# -----------------------------
$deleted = 0
$failed = 0
$results = foreach ($intent in $candidates) {
    $id = $intent.id
    $name = $intent.displayName
    $delUri = "https://graph.microsoft.com/beta/deviceManagement/intents/$id"

    if ($DryRun) {
        Write-Host "[DRY-RUN] Would delete: $name ($id)"
        [pscustomobject]@{
            DisplayName = $name
            Id          = $id
            Action      = "DryRun"
            Deleted     = $false
            Status      = "NotExecuted"
            Error       = $null
        }
        continue
    }

    try {
        Invoke-RestMethod -Method DELETE -Uri $delUri -Headers $headers
        $deleted++
        Write-Host "[DELETED $deleted] $name" -ForegroundColor Green

        [pscustomobject]@{
            DisplayName = $name
            Id          = $id
            Action      = "DELETE"
            Deleted     = $true
            Status      = "204 No Content"
            Error       = $null
        }
    }
    catch {
        $errMsg = $_.Exception.Message
        $failed++
        Write-Host "[FAILED]  $name :: $errMsg" -ForegroundColor Red

        [pscustomobject]@{
            DisplayName = $name
            Id          = $id
            Action      = "DELETE"
            Deleted     = $false
            Status      = "Failed"
            Error       = $errMsg
        }
    }
}

# -----------------------------
# 5) Summary + export
# -----------------------------
Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  DELETED: $deleted  |  FAILED: $failed" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

$results | Format-Table -AutoSize

if ($CsvPath) {
    $results | Export-Csv -Path $CsvPath -NoTypeInformation
    Write-Host "Results exported to: $CsvPath"
}
