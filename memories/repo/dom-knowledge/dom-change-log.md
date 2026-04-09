# DOM Change Log
> Tracks DOM changes detected between test runs

| Date | Page | Element | Change Type | Old Value | New Value | Impact |
|------|------|---------|-------------|-----------|-----------|--------|
| 2026-04-02 | CommandBar (All pages) | Add/Create Button | label_changed | aria-label='Add' | aria-label='Create' | `ClickCommandBarAddButtonAsync()` now falls back to 'Create'. `ClickCommandBarCreateButtonAsync()` uses 'Create' first (correct). DO NOT revert this  UI confirmed changed. |
| 2026-04-02 | - | - | initial_capture | - | - | First DOM knowledge base creation from console logs |
| 2026-04-09 | Win32 Create Wizard | All wizard elements | page_captured | - | 9 wizard pages captured | Test 4179742 PASS — full Win32 app create flow captured. No DOM changes vs expectations. |
| 2026-04-09 | AppList | VerifyPropertyAssignmentsAsync | stability_confirmed | CustomLogException (timing) | PASS — no timing issue | Previous DOM timing issue in VerifyPropertyAssignmentsAsync now resolved |
| 2026-04-09 | Assignments Grid | GroupBehaveDataLocators (sibling divs) | logic_fix | `foreach` threw on first non-matching div | `continue` + `groupFound` flag — checks ALL siblings | Non-data-row structural/separator divs in Assignments grid caused false failure. Fixed in AllAppsUtils.cs lines 1080-1120 |
| 2026-04-09 | Program Params | Install behavior locator | slow_portal | - | 150 retries before success | Portal latency — not a DOM change. Monitor for recurrence |
| 2026-04-09 | CommandBar | CreateButton (E015) | stability_confirmed | - | aria-label='Create' confirmed stable | Self-healing wrapper present but not triggered — primary locator works |

## Notes
- No DOM diffs detected on 2026-04-09 run — all elements match baseline
- Self-healing wrappers in AllAppsUtils.cs and MSCommandBarWithMenubarRole.cs are deployed but dormant
- Future runs will compare against these stored elements

