# Element Registry

| Identifier | Page | Selector | Role | Text | Label | Iframe | Last Seen | Stability |
|------------|------|----------|------|------|-------|--------|-----------|-----------|
| SidebarApps | All Pages | a[class*='fxs-sidebar-item-link']:has-text('Apps') | link | Apps | Apps | None | 2026-04-02 | 9 (stable) |
| SidebarDevices | All Pages | a[class*='fxs-sidebar-item-link']:has-text('Devices') | link | Devices | Devices | None | 2026-04-02 | 9 (stable) |
| SidebarHome | All Pages | a[class*='fxs-sidebar-item-link']:has-text('Home') | link | Home | Home | None | 2026-04-02 | 9 (stable) |
| AddAppTitle | Add App Wizard | text="Add App" | heading | Add App | None | None | 2026-04-02 | 8 (stable) |
| AppTypeSubtitle | Add App Wizard | text="Microsoft Store app (new)" | text | Microsoft Store app (new) | None | None | 2026-04-02 | 7 |
| WizardTab_AppInfo | Add App Wizard | text="App information" | tab | App information | App information | None | 2026-04-02 | 8 |
| WizardTab_Assignments | Add App Wizard | text="Assignments" | tab | Assignments | Assignments | None | 2026-04-02 | 8 |
| WizardTab_ReviewCreate | Add App Wizard | text="Review + create" | tab | Review + create | Review + create | None | 2026-04-02 | 8 |
| AppTypeCombobox | Add App - App Type | getByRole(combobox, "App type") | combobox | None | App type | None | 2026-04-02 | 8 |
| SelectButton | Add App - App Type | getByRole(button, "Select") | button | Select | Select | None | 2026-04-02 | 8 |
| RequiredSection | Assignments | text="Required" | heading | Required | None | None | 2026-04-02 | 8 |
| AvailableSection | Assignments | text="Available for enrolled devices" | heading | Available for enrolled devices | None | None | 2026-04-02 | 8 |
| UninstallSection | Assignments | text="Uninstall" | heading | Uninstall | None | None | 2026-04-02 | 8 |
| AddGroupLink_Required | Assignments (Required) | [class*='msportalfx-text-primary ext-controls-selectLink']:has-text('+ Add group').Nth(0) | link | + Add group | None | None | 2026-04-02 | 5 (nth-based, fragile) |
| AddGroupLink_Available | Assignments (Available) | [class*='msportalfx-text-primary ext-controls-selectLink']:has-text('+ Add group').Nth(1) | link | + Add group | None | None | 2026-04-02 | 5 (nth-based, fragile) |
| AddGroupLink_Uninstall | Assignments (Uninstall) | [class*='msportalfx-text-primary ext-controls-selectLink']:has-text('+ Add group').Nth(2) | link | + Add group | None | None | 2026-04-02 | 3 (FAILED - nth=2 timeout) |
| AddAllUsersLink | Assignments | [class*='msportalfx-text-primary ext-controls-selectLink']:has-text('+ Add all users') | link | + Add all users | None | None | 2026-04-02 | 5 |
| AddAllDevicesLink | Assignments | [class*='msportalfx-text-primary ext-controls-selectLink']:has-text('+ Add all devices') | link | + Add all devices | None | None | 2026-04-02 | 5 |
| PreviousButton | Add App Wizard | getByRole(button, "Previous") | button | Previous | Previous | None | 2026-04-02 | 8 |
| NextButton | Add App Wizard | getByRole(button, "Next") | button | Next | Next | None | 2026-04-02 | 8 |
| CreateButton_CommandBar | All Apps | commandBar "Create" button | button | Create | Create | AppList.ReactView | 2026-04-02 | 7 (was "Add" in older portal) |
| AppListGrid | All Apps | [class*='ms-DetailsList--Compact'] | grid | None | None | AppList.ReactView | 2026-04-02 | 8 |
| AppListRow | All Apps | [class*='ms-DetailsRow-fields'] | row | (dynamic) | None | AppList.ReactView | 2026-04-02 | 7 |
| SearchBox | All Apps | MS_SearchBox component | searchbox | None | Search | AppList.ReactView | 2026-04-02 | 8 |
| RefreshButton | All Apps | commandBar "Refresh" button | button | Refresh | Refresh | AppList.ReactView | 2026-04-02 | 8 |
| CloseButton | Add App Wizard | getByRole(button, "Close") or X button | button | Close | Close content 'Add App' | None | 2026-04-02 | 7 |
| CopilotButton | Header | button:has-text('Copilot') | button | Copilot | Copilot | None | 2026-04-02 | 9 |
| ColumnHeaders_Assignments | Assignments Grid | text="Group mode", "Group", "Status", etc. | columnheader | (varies) | None | None | 2026-04-02 | 8 |
| NoAssignments | Assignments | text="No assignments" | text | No assignments | None | None | 2026-04-02 | 8 |
