# WinGet Store App Test Findings

## Last Updated: 2026-04-15

## Critical Configuration Findings

### Worker Count Limit
- **Max safe workers**: 4 (headed Chromium)
- 8 workers causes Node.js TLS crash: `!current_write_` assertion in `crypto_tls.cc`
- Root cause: Concurrent HTTPS/TLS connections to Intune portal overwhelm Node.js crypto layer
- Produces `TESTRUNABORT` with exit code 1, no results saved
- Use `headed_4workers.runsettings` — NOT `headed_8workers.runsettings`

### Install Behavior Field — DISABLED in Portal
- The "Install behavior" field is DISABLED for WinGet Store apps in Intune portal
- Control type: `azc-optionsGroupField` with `msportalfx-form-formelement-disabled`
- Step skipped in `WinGetStoreAppRegressionTestBase.cs` lines 161-162
- Verification skipped at lines 331-335
- `SetOptionPickerAsync` in `AllAppsUtils.cs` detects disabled via `aria-disabled` or CSS class

## Test Data Findings

### DisplayName Pattern
- Use hyphen format: `Test-CaseN-AppName` (e.g., `Test-Case14-WhatsApp5755610ad6dd7f2e`)
- `CreateUniqueText()` in `Utils.cs` splits by underscore — hyphens survive intact
- JSON: `TestData_AppReggersion/WinGet_StoreApp_Regression.json`

### Microsoft Store Catalog Availability (as of 2026-04-15)
- "PowerShell to exe&msi Converter free" — AVAILABLE (TC_16407789)
- "Adobe Lightroom" — AVAILABLE (TC_16408419)
- Note: These were unavailable on 2026-04-10 — catalog availability can change

## Locator Fixes Applied

### SetOptionPickerAsync (AllAppsUtils.cs ~line 1370)
- Multi-pattern CSS class matching (3 patterns) + disabled field detection
- Skips when field has `aria-disabled` or `msportalfx-form-formelement-disabled` class

### ClickUninstallAddGroupAsync (AllAppsUtils.cs ~line 950)
- Primary: Gemini-proven xpath `//h3[text()='Uninstall']/following::a[text()='+ Add group' and contains(@class, 'msportalfx-text-primary') and contains(@class, 'ext-controls-selectLink')]`
- Fallback chain: Gemini xpath → all "+ Add group" links (last) → SelfHealingLocator.ResolveAsync

## Locator Stability (2026-04-15)
- All locators resolve at Try 1
- Max observed: Try 6 for Name label on Properties page (portal loading delay)
- Zero self-healing events in clean 16/16 run
- Healed locators cache: 4 entries from previous runs, none triggered

## Portal UI Changes (Confirmed)

### CSS Class Patterns for Form Elements
- Original: `fxc-weave-pccontrol fxc-section-control fxc-base msportalfx-form-formelement fxc-has-label`
- With customHtml: `...fxc-base msportalfx-customHtml msportalfx-form-formelement...`
- Disabled: `...fxc-base msportalfx-form-formelement msportalfx-form-formelement-disabled...`

### Uninstall Section
- Uses `<h3>` tag — xpath `//h3[text()='Uninstall']` is stable anchor
