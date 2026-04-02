## Intune Testing Guidelines

- **15 years of Intune testing experience**  apply that depth to every analysis
- **NEVER apply blind/iterative fixes.** Always read the FULL code chain + DOM structure FIRST, understand the root cause completely, then apply the fix in ONE run
- **Analysis before fix**: Trace the full call chain from test  page object  helper  element helper  DOM. Understand every layer before touching code
- **Azure Portal DOM patterns**:
  - Intune admin center runs inside the Azure Portal shell
  - Content scrolls within blade containers, NOT the window
  - Lazy rendering: elements below the viewport fold may not be in the DOM
  - Each assignment section (Required, Available, Uninstall) is wrapped in xc-weave-pccontrol fxc-section-control fxc-base msportalfx-customHtml msportalfx-form-formelement
  - `+ Add group` links use class msportalfx-text-primary ext-controls-selectLink
  - Nth-based indexing is fragile when elements are lazy-loaded  prefer section-scoped locators
- **Fix quality bar**: Fixes must be robust against lazy rendering, viewport-dependent DOM, and Azure Portal's scrollable blade containers
- **Mouse.WheelAsync pitfalls**: Only works if mouse cursor is over the correct scrollable container; unreliable for Azure Portal blade content


---

## MANDATORY: DOM Knowledge Capture During Every Test Run

### NEVER SKIP THESE STEPS  This is a hard requirement:

1. **Phase 0 (Pre-Run)**: ALWAYS read `/memories/repo/dom-knowledge/page-index.md` and `element-registry.md` before running any test
2. **During Test Run**: Parse every `get_terminal_output` poll for locator data and DOM structure info (iframe names, selectors, element interactions)
3. **On Failure (P0 Priority)**: 
   - IMMEDIATELY capture DOM state from console output
   - Extract failing locator, stack trace, error type
   - Compare against stored DOM knowledge  has the page changed?
   - Delegate to Debug Agent with full failure package
   - Delegate to DOM Intelligence Agent for live capture if browser is still open
4. **Post-Run (ALWAYS)**: 
   - Extract ALL new locators/elements seen in console output
   - Update `element-registry.md` with new/changed elements
   - Update page-specific DOM files (e.g., `intune-apps-list.md`)
   - Update `dom-change-log.md` if any elements changed from stored state
   - Generate DOM Knowledge Dashboard summary
5. **Use the full Orchestrator Agent workflow** from `.github/agents/orchestrator-agent.agent.md`  every phase, every time

### Failure Signals to Parse in Real-Time:
- `Timeout \d+ms exceeded`  P0: Locator failure
- `waiting for locator`  P0: Element not found
- `element is not visible`  P0: Hidden element
- `PlaywrightException`  P0: Playwright error
- `Error Message:`  P1: Test assertion failed
- `Stack Trace:`  P1: Extract file + line
- `Failed!`  P2: Test completed with failure

### DOM Data to Extract from Console Logs:
- Iframe names and load status (e.g., `AppList.ReactView have been loaded`)
- Locator selectors (e.g., `Current Locator Locator@...`)
- Element interaction logs (e.g., `Hover "Refresh"`, `Click "Delete"`)
- Retry counts (e.g., `Try 7 check if current locator is exist`)
- High retry counts (>5) indicate unstable elements  flag in element-registry

---

## CONFIRMED UI CHANGE: Command Bar 'Add' button is now 'Create' (2026-04-02)

- The Intune portal command bar button changed from `aria-label='Add'` to `aria-label='Create'`
- **DO NOT change** `MSCommandBarWithMenubarRole.cs`  the fallback logic already handles this:
  - `ClickCommandBarAddButtonAsync()`: tries 'Add' first, falls back to 'Create' 
  - `ClickCommandBarCreateButtonAsync()`: tries 'Create' first, falls back to 'Add' 
- The primary selector is now: `[class*='ms-Button ms-Button--commandBar'][aria-label='Create']`
- **NEVER revert** `await ClickCommandBarButtonByAriaLabelAsync("Create")`  this is the correct current UI state
