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

| E015 | CreateButton | CommandBar | `[class*='ms-Button ms-Button--commandBar'][aria-label='Create']` | button | Create | (ReactView iframes) | 2026-04-02 | 9 |

| E016 | AddButton_DEAD | CommandBar | `[class*='ms-Button ms-Button--commandBar'][aria-label='Add']` | button | Add | (ReactView iframes) | 2026-04-02 | 0 (DEAD) |

## Stability Scoring
- 7-10 = Stable (aria-label, role, stable text)
- 4-6 = Moderate
- 1-3 = Fragile (dynamic IDs, shifting structure)


