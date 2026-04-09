# Intune Canary Tests — Notes

## Test Run History

### 2026-04-09 — Test 4185880A (Non-Enus WinClassic Sanity - Win32App_DependentApp)
- **Status**: PASS (after fix)
- **Duration**: ~5.82 minutes
- **Root Cause**: DOM traversal logic bug in `AllAppsUtils.VerifyPropertyAssignmentsAsync` (lines 1080-1120). `foreach` over `GroupBehaveDataLocators` (sibling divs via `~ div` selector) threw `CustomLogException("The group behave is not correct")` on the FIRST sibling div whose class didn't contain `"azc-br-muted fxs-portal-hover"`. Non-data-row structural/separator divs caused immediate failure before the actual data row was checked.
- **Fix**: Changed to `continue` (skip) non-matching sibling divs, added `groupFound` flag. Only throws after ALL siblings checked and none matched.
- **Self-healing**: NOT triggered — issue was in verification logic, not locators
- **Observation**: "Install behavior" locator had 150 retries on program parameters page (portal slow)
- **Reports**: Initial: `ExtentReports/TestReport_20260409_142234.html` (FAILED), Re-run: `ExtentReports/TestReport_20260409_144312.html` (PASSED)

### 2026-04-09 — Test 4179742 (Non-Enus WinClassic Sanity - Win32App_Create)
- **Status**: PASS
- **Duration**: ~4.9 minutes (293613.6 ms)
- **Self-healing**: Wrappers deployed, NOT triggered (all primary locators stable)
- **Key**: VerifyPropertyAssignmentsAsync CustomLogException (DOM timing) is RESOLVED
- **Report**: ExtentReports/TestReport_20260409_140020.html

## Known Stable Patterns
- `ClickCommandBarButtonByAriaLabelAsync("Create")` — correct primary call, confirmed 2026-04-09
- Win32 app creation wizard: 9 pages, all locators stable across Non-ENUS locale
- AppList.ReactView iframe: stable iframe name, confirmed 2026-04-09
- SelfHealingLocator wrappers in AllAppsUtils.cs and MSCommandBarWithMenubarRole.cs — ready, dormant

## Known Issues (Resolved)
- VerifyPropertyAssignmentsAsync CustomLogException — **Root cause identified 2026-04-09**: DOM traversal `foreach` over sibling divs (`~ div`) broke on non-data-row structural/separator divs. Fixed by skipping non-matching divs with `continue` + `groupFound` flag. Not a timing issue — was a logic bug.
- "Install behavior" locator on program parameters page: 150 retries observed (2026-04-09) — portal latency, not a DOM change. Monitor for recurrence.
