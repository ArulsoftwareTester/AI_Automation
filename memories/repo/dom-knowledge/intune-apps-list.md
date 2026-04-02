# Intune Apps List  DOM Knowledge
> Page: Apps > All apps
> URL pattern: `https://intune.microsoft.com/#view/Microsoft_Intune_Apps/...`
> Last captured: 2026-04-02 (from console logs of test TC_14673264)

## Iframes
| Name | Selector | Load Timing |
|------|----------|-------------|
| AppList.ReactView | `iframe[name="AppList.ReactView"][class*="fxs-reactview-frame-active"]` | ~15s after navigation |

## Key Elements (inside AppList.ReactView iframe)
| Element | Selector | Role | Notes |
|---------|----------|------|-------|
| Search Box Container | `[class*='ms-SearchBox-iconContainer']` | - | Click to focus search |
| Search Input | `[class*='ms-SearchBox-field'][placeholder='Search']` | searchbox | Type app name here |
| Command Bar | `[role='menubar'][class*='ms-CommandBar']` | menubar | Contains action buttons |
| Refresh Button | `[class*='ms-Button ms-Button--commandBar'][aria-label='Refresh']` | button | Refreshes app list |
| App List (Compact) | `[class*='ms-DetailsList--Compact']` | list | Main app list grid |
| App Row | `[class*='ms-DetailsRow-fields']` | row | Each app entry |
| Context Menu Button | `[class*='ms-Button ms-Button--icon'][aria-label='Click to open context menu']` | button | Right-click menu per app |
| Context Menu Callout | `[class*='ms-Callout ms-ContextualMenu-Callout']` | menu | Dropdown after context menu click |
| Delete Menu Item | `[class*='ms-ContextualMenu-link'][aria-label='Delete']` | menuitem | Delete app option |
| Confirm Dialog | `[class*='ms-Dialog-main']` | dialog | Confirmation popup |
| Yes Button | `[class*='ms-Dialog-main'] >> [class*='ms-Button'] >> has-text="Yes"` | button | Confirm delete |

## Navigation Path
1. Click sidebar "Apps"
2. Click menu item "All apps"
3. Wait for `AppList.ReactView` iframe to load
