# Intune Canary Tests - Memory Notes

## Failure Patterns (from TC_14673246 - WinGet Store App Uninstall)

Memory: Locator "AddGroupLink_Uninstall" failed — Nth(2) of [class*='msportalfx-text-primary ext-controls-selectLink']:has-text('+ Add group') timed out on Assignments page. Root cause: element likely below viewport fold, needs ScrollIntoViewIfNeededAsync before click. Stability score: 3 (fragile nth-based).

Memory: Locator "CreateButton_CommandBar" in PE environment: button text changed from "Add" to "Create". Framework uses ClickCommandBarAddButtonAsync() which fails 150 retries before timeout. Fix: use ClickCommandBarCreateButtonAsync() for PE.

Memory: WinGet Store App test cleanup phase searches for app in grid by name. If app creation was never completed (wizard abandoned mid-flow), the grid row won't exist and cleanup retries 150x3 times before giving up. This is expected behavior but wastes ~2 minutes.

Memory: Add App wizard pages (App Information, Assignments, Review + Create) render on MAIN PAGE, not inside AppList.ReactView iframe. The IFrameName=null is correct for wizard interactions. Only the All Apps grid page uses AppList.ReactView.

## DOM Knowledge Base
- Initial DOM knowledge base created on 2026-04-02 from TC_14673246 failure analysis
- Files stored in `/memories/repo/dom-knowledge/`
- Pages captured: Login, All Apps, Add App (App Info, Assignments, Review+Create), Select Group Panel
- 28 elements registered in element-registry.md
- 2 DOM changes logged in dom-change-log.md
