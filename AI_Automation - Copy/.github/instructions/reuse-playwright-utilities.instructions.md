---
description: "Use when writing, generating, or modifying Playwright test code, UI automation steps, or any code that interacts with web elements. Enforces reuse of existing PlaywrightTests utility classes instead of raw Playwright API calls."
applyTo: "**/*.cs"
---
# Reuse PlaywrightTests Utilities — No Raw Playwright Calls

When writing Intune test automation code, ALWAYS use the existing helper classes and controllers from `PlaywrightTests/Playwright/Common/` instead of writing raw Playwright API calls. The project has 100+ reusable methods covering every interaction pattern.

## Element Finding — Use ElementHelper

Use `ElementHelper` (Common/Helper/ElementHelper.cs) instead of raw `Page.Locator()` or `Page.QuerySelectorAsync()`.

| Instead of | Use |
|------------|-----|
| `Page.Locator("[role='button']")` | `ElementHelper.GetByRoleAsync(page, AriaRole.Button, name)` |
| `Page.Locator(".my-class")` | `ElementHelper.GetByClassAsync(page, "my-class")` |
| `Page.Locator("#my-id")` | `ElementHelper.GetByIDAsync(page, "my-id")` |
| `Page.GetByLabel("label")` | `ElementHelper.GetByAriaLabelAsync(page, "label")` |
| `Page.Locator("[data-telemetryname='x']")` | `ElementHelper.GetByAttributeDataTelemetrynameAsync(page, "x")` |
| Custom parent/sibling traversal | `ElementHelper.GetParentByChildLocatorAsync()`, `GetBrotherByLocatorAsync()` |
| Existence checks with retries | `ElementHelper.IsExistAsync()` |

All ElementHelper methods have overloads for `IPage`, `IFrameLocator`, and `ILocator`.

## Clicking & Interaction — Use ControlHelper

Use `ControlHelper` (Common/Helper/ControlHelper.cs) instead of raw `.ClickAsync()`.

| Instead of | Use |
|------------|-----|
| `locator.ClickAsync()` | `ControlHelper.ClickByClassAndHasTextAsync()` or role-based variant |
| Manual retry loops | `ControlHelper.RetryAsync()` |
| `locator.WaitForAsync()` | `ControlHelper.WaitElementEnabledAsync()`, `WaitElementIsVisibleAsync()` |
| Combobox open + select | `ControlHelper.SetComBoxRoleOptionRoleValueAsync()` |

## Form Controls — Use Controls.HandleAsync

Use `Controls` (Common/Utils/Controls.cs) for all form interactions. It supports 45+ control types via enum dispatch.

| Instead of | Use |
|------------|-----|
| `Page.FillAsync(selector, value)` | `Controls.SetValueForTextBoxAsync()` or `Controls.HandleAsync(ControlType.TextBox, ...)` |
| Manual checkbox toggle | `Controls.HandleAsync(ControlType.CheckBox, ...)` |
| Dropdown open + option click | `Controls.HandleAsync(ControlType.DropDownList, ...)` or `SetValueForDropDownAsync()` |
| Radio button selection | `Controls.HandleAsync(ControlType.Radio, ...)` |
| File upload with `SetInputFilesAsync` | `Controls.HandleAsync(ControlType.Upload, ...)` or `UploadController.UploadFileAsync()` |

## Navigation — Use Navigation Utils

Use `Navigation` (Common/Utils/NavigationUtils/Navigation.cs) instead of raw `Page.GotoAsync()` for in-app navigation.

- `Navigation.ToAsync(targetSideBar, targetMenuItem)` — config-driven sidebar + menu navigation
- `Navigation.BackAsync()` — go back
- Navigation selectors are defined in `NavigationConfigInfo.json`

## Grid Operations — Use IGrid Implementations

Use the appropriate grid controller from `Common/Controller/Grid/` for table interactions.

- `GetDisplayedRowHeadersAsync()`, `DeleteRowByRowHeaderAsync()`, `ClickRowHeaderToShowDetailAsync()`
- `GetGridAllDataAsync()` → returns DataTable
- `ClickColumnNameToSortASCAsync()`, `ClickColumnNameToSortDESCAsync()`
- Implementations: `Grid_MS_DetailsList`, `Grid_FXC_GC_Table`, `Grid_AZC_Grid`, `Grid_PCControl_GridViewModel`, etc.

## Dialogs & Popups — Use IConfirmDialog

Use dialog controllers from `Common/Controller/ConfirmDialog/` instead of custom dialog handling.

- `ClickDialogYesButtonAsync()`, `ClickDialogNoButtonAsync()`, `ClickDialogOKButtonAsync()`
- `ClickDialogDeleteButtonAsync()`, `ClickDialogCancelButtonAsync()`
- `GetDialogButtonLocatorByNameAsync(buttonName)`

## Notifications — Use NotificationController

- `NotificationController.VerifyAndCloseNotificationAsync(regexText, isDismiss)`
- `NotificationController.VerifyNoNotificationAsync()`

## Bottom Navigation (Footer Buttons) — Use IBottomNavigation

- `ClickBottomNavigationSpecialNameButtonAsync(buttonName)` — handles fallback patterns
- `VerifyBottomNavigationSpecialNameButtonStatusAsync(buttonName, status)`

## Waiting — Use CommonOperations or BaseController

| Instead of | Use |
|------------|-----|
| `Task.Delay(ms)` | `CommonOperations.WaitShortTime()`, `WaitMiddleTime()`, `WaitLongTime()` |
| `Thread.Sleep(ms)` | `BaseController.SleepShort()`, `SleepMiddle()`, `SleepLong()` |

## Search & Filter

- `SearchHelper.SearchTargetValueAsync()` — perform search
- `FilterHelper.ClickAddFiltersAsync()` — open filter menu

## Toolbar Actions

- `ToolBarHelper.ClickCreateProfileButtonAsync()`, `ClickRefreshButtonAsync()`
- Toolbar controllers: `MSCommandBar`, `FXSCommandBar`, `AZCToolBar`, `PCControlToolBar`

## Feature-Specific Utils (35+ domains)

Before writing custom logic, check `Common/Utils/FeatureUtils/` and `Common/Utils/BaseUtils/` for existing domain utils covering: Android, iOS, macOS, Windows, Security Baseline, Endpoint Security, Remote Help, Microsoft Tunnel, Assignment Filters, RBAC, Device Control, and more.

## Key Patterns to Follow

- **Async-first**: All helper methods are async — always use `await`.
- **IPage/IFrameLocator overloads**: Use the correct overload based on whether content is in a frame.
- **Built-in retry logic**: Prefer helpers with retry over writing manual retry loops.
- **ControlInfo model**: Pass interaction details via `ControlInfo` objects with `ControlType` and `OperationValue`.
- **CustomLogException**: Use the project's custom exception type for meaningful error messages.
