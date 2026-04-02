# DOM Change Log
> Tracks DOM changes detected between test runs

| Date | Page | Element | Change Type | Old Value | New Value | Impact |
|------|------|---------|-------------|-----------|-----------|--------|
| 2026-04-02 | CommandBar (All pages) | Add/Create Button | label_changed | aria-label='Add' | aria-label='Create' | `ClickCommandBarAddButtonAsync()` now falls back to 'Create'. `ClickCommandBarCreateButtonAsync()` uses 'Create' first (correct). DO NOT revert this  UI confirmed changed. |
| 2026-04-02 | - | - | initial_capture | - | - | First DOM knowledge base creation from console logs |

## Notes
- No DOM diffs yet  this is the initial baseline
- Future runs will compare against these stored elements

