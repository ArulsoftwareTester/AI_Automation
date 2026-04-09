# Element Registry  DOM Knowledge Base
> Cross-page element lookup. Updated from test execution console logs.

| ID | Element | Page | Selector | Role | Text | Iframe | Last Seen | Stability |
|----|---------|------|----------|------|------|--------|-----------|-----------|
| E001 | SidebarHome | Sidebar | `[role='presentation'][class*='fxs-sidebar'] >> [class*='fxs-sidebar-item-link'] >> has-text="Home"` | link | Home | - | 2026-04-02 | 8 |
| E002 | SidebarApps | Sidebar | `[role='presentation'][class*='fxs-sidebar'] >> [class*='fxs-sidebar-item-link'] >> has-text="Apps"` | link | Apps | - | 2026-04-02 | 8 |
| E003 | MenuScrollArea | Menu | `[class*='fxc-menu-scrollarea']` | navigation | - | - | 2026-04-02 | 7 |
| E004 | AllAppsMenuItem | Menu | `[class*='fxc-menu-listView-item'] >> has-text="All apps"` | menuitem | All apps | - | 2026-04-02 | 8 |
| E005 | SearchBox | AppList | `[class*='ms-SearchBox-field'][placeholder='Search']` | searchbox | - | AppList.ReactView | 2026-04-02 | 7 |
| E006 | SearchBoxIcon | AppList | `[class*='ms-SearchBox-iconContainer']` | - | - | AppList.ReactView | 2026-04-02 | 7 |
| E007 | RefreshButton | AppList | `[class*='ms-Button ms-Button--commandBar'][aria-label='Refresh']` | button | Refresh | AppList.ReactView | 2026-04-02 | 9 |
| E008 | AppDetailsList | AppList | `[class*='ms-DetailsList--Compact']` | list | - | AppList.ReactView | 2026-04-02 | 8 |
| E009 | AppRowFields | AppList | `[class*='ms-DetailsRow-fields']` | row | (dynamic) | AppList.ReactView | 2026-04-02 | 7 |
| E010 | ContextMenuBtn | AppList | `[class*='ms-Button ms-Button--icon'][aria-label='Click to open context menu']` | button | Context menu | AppList.ReactView | 2026-04-02 | 8 |
| E011 | DeleteMenuItem | AppList | `[class*='ms-ContextualMenu-link'][aria-label='Delete']` | menuitem | Delete | AppList.ReactView | 2026-04-02 | 9 |
| E012 | DialogMain | AppList | `[class*='ms-Dialog-main']` | dialog | - | AppList.ReactView | 2026-04-02 | 8 |
| E013 | DialogYesBtn | AppList | `[class*='ms-Dialog-main'] >> [class*='ms-Button'] >> has-text="Yes"` | button | Yes | AppList.ReactView | 2026-04-02 | 9 |
| E014 | CommandBar | AppList | `[role='menubar'][class*='ms-CommandBar']` | menubar | - | AppList.ReactView | 2026-04-02 | 8 |

| E015 | CreateButton | CommandBar | `[class*='ms-Button ms-Button--commandBar'][aria-label='Create']` | button | Create | (ReactView iframes) | 2026-04-09 | 9 |

| E016 | AddButton_DEAD | CommandBar | `[class*='ms-Button ms-Button--commandBar'][aria-label='Add']` | button | Add | (ReactView iframes) | 2026-04-02 | 0 (DEAD) |

## Stability Scoring
- 7-10 = Stable (aria-label, role, stable text)
- 4-6 = Moderate
- 1-3 = Fragile (dynamic IDs, shifting structure)

| E017 | AppTypeCombobox | SelectAppType | `internal:role=combobox[name="App Type"i]` | combobox | App Type | - | 2026-04-02 | 9 |
| E018 | AndroidStoreItem | SelectAppType | `internal:role=treeitem[name="Android store app"]` | treeitem | Android store app | - | 2026-04-02 | 9 |
| E019 | SelectButton | SelectAppType | `internal:role=button[name="Select"]` | button | Select | - | 2026-04-02 | 9 |
| E020 | ContextMenuCallout | AppList | `[class*='ms-Callout ms-ContextualMenu-Callout']` | callout | - | AppList.ReactView | 2026-04-02 | 8 |
| E021 | Win32AppItem | SelectAppType | `role=treeitem[name="Windows app (Win32)"]` | treeitem | Windows app (Win32) | - | 2026-04-09 | 9 |
| E022 | PackageFileUpload | AppInfo | file input | input | Upload .intunewin | - | 2026-04-09 | 8 |
| E023 | AppNameInput | AppInfo | text input (display name) | textbox | Display name | - | 2026-04-09 | 8 |
| E024 | DescriptionInput | AppInfo | text input (description) | textbox | Description | - | 2026-04-09 | 8 |
| E025 | PublisherInput | AppInfo | text input (publisher) | textbox | Publisher | - | 2026-04-09 | 8 |
| E026 | InstallTimeInput | Program | text input (install time) | textbox | Installation time | - | 2026-04-09 | 8 |
| E027 | MinOSDropdown | Requirements | combobox/dropdown | combobox | Minimum OS | - | 2026-04-09 | 8 |
| E028 | DetectionRuleType | DetectionRules | dropdown/combobox | combobox | Rules format | - | 2026-04-09 | 8 |
| E029 | WizardNextButton | Win32Wizard | `role=button[name="Next"]` | button | Next | - | 2026-04-09 | 9 |
| E030 | WizardCreateButton | ReviewCreate | `role=button[name="Create"]` | button | Create | - | 2026-04-09 | 9 |
| E031 | RequiredAssignGroup | Assignments | assignment section | group | Required | - | 2026-04-09 | 8 |
| E032 | AllUsersTarget | Assignments | assignment target | - | All users | - | 2026-04-09 | 8 |
| E033 | GroupBehaveDataDiv | Assignments | `~ div` siblings with class containing `azc-br-muted fxs-portal-hover` | div | (data row) | - | 2026-04-09 | 6 | Note: Non-data-row structural/separator divs exist as siblings — logic must skip them |
| E034 | InstallBehavior | Program Params | (text/dropdown locator for Install behavior) | - | Install behavior | - | 2026-04-09 | 5 | ⚠️ 150 retries on 2026-04-09 — portal latency. Mark as potentially unstable |
