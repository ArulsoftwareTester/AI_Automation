# 🧠 Memory Instructions

- **[2026-03-31]** After creating WinGet Store apps, do NOT delete them (skip cleanup/deletion step).
- **[2026-03-31]** Command to run Non-Enus WinClassic Sanity test: `cd "C:\Users\v-arulmani\Downloads\AI_Automation 5\AI_Automation - Copy" && dotnet test "IntuneCanaryTests\IntuneCanaryTests.csproj" --filter "TestMethod_3045446_Non_Enus_WinClassic_Sanity" --settings headed.runsettings`
- **[2026-03-31]** For MSI-based Win32 apps: "Install behavior" on Program tab is disabled (System-only), "Install context" column does not exist in Assignments grid. Skip both setting and verifying install context for MSI apps. Always handle gracefully (try/catch or skip) instead of hard-failing.
- **[2026-03-31]** Self-healing rule: When a UI element (column, button, toggle) is not found during Intune app creation, check if it's disabled/hidden for that app type before failing. Wrap optional UI interactions in try/catch and log skip info.
- **[2026-03-31]** WindowsRegressionTestSetABase.cs should reuse PlaywrightTests project methods: AllAppsUtils (ByPlatform), SelectAppTypeUtils, UploadFileUtils, InterfaceUtils, and ControlInfo model. Use ExecuteStepAsync pattern with RunStepAsync delegation from WinGetStoreAppRegressionTestBase as the reference pattern.
- **[2026-03-31]** Cannot read live Intune portal DOM directly — requires a running Playwright browser session. To capture DOM during tests, use `Page.ContentAsync()` in Playwright. Existing UI dump files (ui_dump.xml, select_ui.xml, sign_in_ui.xml) are Android Company Portal dumps, not web portal. When debugging UI element selectors, use error screenshots + ControlHelper class patterns instead.

---

# Live Failure Signal Definitions

## Structured Signals (emitted by framework automatically)

| Signal Pattern | Severity | Meaning | Action |
|----------------|----------|---------|--------|
| `[HEAL_SIGNAL] retryCount=30` | WARNING | Locator struggling, self-healing triggered if hints available | Monitor for `[SelfHealing]` follow-up |
| `[HEAL_SIGNAL] retryCount=60` | CRITICAL | Self-healing likely failed or not available | Prepare Debug Agent diagnosis |
| `[HEAL_SIGNAL] retryCount=100` | EMERGENCY | Element likely not on page | Capture DOM immediately |
| `[SelfHealing] Auto-healed at retry N` | INFO | In-test healing succeeded | Log for Phase 6, continue monitoring |
| `[SelfHealing] Healing failed at retry N` | ERROR | All 15 strategies failed | Start Phase 2 immediately |
| `[SelfHealing] HEALED 'key' via strategy` | INFO | Specific healing succeeded | Record strategy in knowledge base |
| `[SelfHealing] *** PERMANENT FIX RECOMMENDED ***` | ACTION | 3+ heals same locator | Apply permanent fix in page object |
| `[SelfHealingRetry] Attempt N/M failed` | WARNING | SelfHealingControlHelper retry active | Monitor, healing happening between retries |
| `[HEAL_REQUEST] step=X controlType=Y error=Z` | ERROR | Test step failed after all healing | Delegate to Fix Agent |

## Key Files for Self-Healing

| File | Purpose |
|------|---------|
| `PlaywrightTests/Playwright/Common/Helper/ElementHelper.cs` | `IsExistAsync` (150-retry loop) + `IsExistWithHealingAsync` (heals at retry 30) |
| `PlaywrightTests/Playwright/Common/Helper/SelfHealingControlHelper.cs` | Enhanced retry wrapper with healing hooks |
| `PlaywrightTests/Playwright/Common/Utils/SelfHealingLocator.cs` | 15-strategy healing cascade engine |
| `PlaywrightTests/Playwright/Common/Utils/HealingHintsRegistry.cs` | Pre-populated hints for 28 known elements |
| `PlaywrightTests/Playwright/Common/Utils/AILocatorHelper.cs` | Strategy 15: Gemini AI locator (last resort) |

---

# TC_8195647A/B Supersedence Fix (2026-04-24)

- **[2026-04-24]** `CreateUniqueText()` in `BaseCommonUtils.cs` splits Name by `_` and keeps only the FIRST part + GUID. So `"TC_8195647A_025 Fake MSI.intunewin"` becomes `"TC" + guid` (e.g., `"TCd89294de"`). This means the JSON Name field cannot be used directly to search for apps in the Intune portal.
- **[2026-04-24]** Supersedence/Dependency B-tests that reference A-test apps CANNOT use the JSON Name as search keyword — the actual app name is GUID-based and unknown at JSON authoring time.
- **[2026-04-24]** Fix: Added `static ConcurrentDictionary<string, string> _createdAppNameMap` to `NonEnUsWinClassicSanityTestBase.cs` that maps JSON Name → runtime unique name. TC_A stores its mapping after creation; TC_B resolves the Supercedence value from the map before searching the portal.
- **[2026-04-24]** The portal "Add Apps" search box (aria-label: "Search by name, publisher") searches by Name and Publisher only — NOT Description. So the search keyword must match the actual unique app name.
- **[2026-04-24]** `SelectBySearchWithKeywordAsync` in `SelectFromGridBySearchUtils.cs` types the keyword into the search box AND clicks a gridcell matching that keyword. Both must match the actual app name.
- **[2026-04-24]** TC_A `VerifyPropertyAssignmentsAsync` timing fix: Added `WaitForLoadStateAsync(NetworkIdle)` + 3s delay before verifying assignments on the Properties page. The Properties page DOM may not be fully loaded when verification starts.
- **[2026-04-24]** With `headed.runsettings` (1 worker), tests run sequentially. Static fields like `_createdAppNameMap` persist across test fixtures in the same test run, so TC_A's data is available when TC_B runs.
- **[2026-04-24]** `AppNameList` in `AllAppsUtils.cs` is `private static Stack<string>` — stores unique app names but is NOT accessible from test classes. Use custom static dictionaries in test base classes for cross-test name sharing.

---

# Test Execution Ordering Rule (2026-04-24)

- **[2026-04-24]** For ALL test case pairs (A/B) from any JSON test data file: If test A has a non-empty `Dependencies` or `Supercedence` field referencing test B's app name, then B MUST run first before A. Always check the JSON `Dependencies` and `Supercedence` fields to determine run order. The independent test (no dependencies/supersedence) runs first to create the app, then the dependent test runs second to reference it.
