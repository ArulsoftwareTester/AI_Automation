# WinGet Store App Regression — Run Results History

## Latest Run: 2026-04-15 — 16/16 PASSED (100%)

| Field | Value |
|-------|-------|
| Status | **16 PASS / 0 FAIL** |
| TRX | `TestResults/WinGet_4Workers_20260415_190111.trx` |
| ExtentReport | `ExtentReports/TestReport_20260415_190122.html` |
| Duration | 786s (~13 min) |
| Workers | 4 (headed Chromium) |
| Runsettings | `headed_4workers.runsettings` |
| Self-Healing | 0 events |
| Exit Code | 0 (success) |

### All 16 Passing Test Method IDs
16408419, 16407789, 16408400, 16408410, 14673598, 16820429, 14673264, 16407791,
16408417, 16408405, 16820413, 14673246, 16408416, 16820432, 14673239, 16820381

### Locator Stability
- All locators resolved at Try 1
- Max observed: Try 6 for Name label on Properties page (portal loading delay, not instability)
- Healed locators cache has 4 entries from previous runs — none triggered
- Zero self-healing events

---

## Comparison: Before vs After Fixes

| Metric | Before (8 workers, 2026-04-10) | After (4 workers, 2026-04-15) |
|--------|-------------------------------|-------------------------------|
| TRX Result | 14 pass / 2 fail | 16 pass / 0 fail |
| ExtentReport | 8 pass / 8 fail | 16 pass / 0 fail |
| Self-Healing | 6 Install behavior heals | 0 heals needed |
| Duration | ~825s (crashed) | 786s (clean) |
| Exit Code | 1 (ABORT) | 0 (success) |

---

## Fixes Applied Between Runs

### 1. Worker Count: 8 → 4 (Critical)
- **Problem**: 8 parallel workers cause Node.js TLS crash (`!current_write_` assertion in `crypto_tls.cc`)
- **Root Cause**: Too many concurrent HTTPS/TLS connections to Intune portal overwhelm Node.js crypto layer
- **Symptom**: `TESTRUNABORT` with exit code 1, no results saved
- **Fix**: Created `headed_4workers.runsettings` with 4 workers
- **Impact**: Eliminated complete run failures

### 2. Install Behavior Step Skipped
- **Problem**: "Install behavior" field DISABLED in Intune portal for WinGet Store apps
- **Symptom**: 6 false failures in ExtentReport (self-healing recovered but logged step failures)
- **Fix**: `WinGetStoreAppRegressionTestBase.cs` lines 161-162 — replaced `ExecuteStepAsync` with skip log message
- **Verify**: Already skipped at lines 331-335
- **Impact**: Eliminated 6 false failures

### 3. Test Data Apps Now Available
- TC_16407789: "PowerShell to exe&msi Converter free" — now available in Microsoft Store catalog
- TC_16408419: "Adobe Lightroom" — now available in Microsoft Store catalog
- Previous test data referenced "PowerShell 7" and "7-Zip" which were not available

### 4. DisplayName Hyphen Pattern
- All 16 test cases use `Test-CaseN-AppName` format (e.g., `Test-Case14-WhatsApp5755610ad6dd7f2e`)
- Hyphens survive `CreateUniqueText()` in `Utils.cs` (splits by underscore only)
- JSON file: `TestData_AppReggersion/WinGet_StoreApp_Regression.json`

---

## Previous Run: 2026-04-10 — 14/16 PASSED

| Field | Value |
|-------|-------|
| Status | 14 PASS / 2 FAIL (test data issues) |
| Duration | ~10:37 (3 cumulative runs) |
| Workers | 8 (caused TLS crashes) |
| Self-Healing | 47 HEAL_SIGNAL events |
| Failures | TC_16407789 (app not in catalog), TC_16408419 (app not in catalog) |

---

## Stability Baseline (2026-04-15)
- **Recommended workers**: 4 (headed Chromium)
- **Recommended runsettings**: `headed_4workers.runsettings`
- **All 16 tests stable** — no flaky tests detected
- **All locators stable** — no self-healing needed
- **Portal known disabled fields**: Install Behavior (WinGet Store apps) — skip in test
