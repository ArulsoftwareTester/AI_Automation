# Intune Win32 App Create Wizard — DOM Knowledge
> Captured from test 4179742 (Non-Enus WinClassic Sanity - Win32App_Create) on 2026-04-09
> Status: PASS | Duration: ~4.9 minutes | Self-healing: Not triggered (all primary locators stable)

## Flow Summary
Login → Apps > All Apps → Create → Select App Type (Windows app Win32) → Upload Package → App Information → Program → Requirements → Detection Rules → Dependencies → Supersedence → Assignments → Review+Create → Verify → Cleanup (Delete)

## Iframe Context
- **Primary iframe**: `AppList.ReactView` (Apps > All Apps page, command bar, app list)
- **Wizard pages**: Loaded within portal blade context after app type selection

## Wizard Pages Observed

### 1. Select App Type
| Element | Selector/Role | Text | Stability |
|---------|--------------|------|-----------|
| AppTypeCombobox | `role=combobox[name="App Type"]` | App Type | 9 (Stable) |
| Win32AppItem | `role=treeitem` | Windows app (Win32) | 9 (Stable) |
| SelectButton | `role=button[name="Select"]` | Select | 9 (Stable) |

### 2. App Information
| Element | Selector/Role | Text | Stability |
|---------|--------------|------|-----------|
| PackageFileUpload | file input | Upload .intunewin | 8 (Stable) |
| AppNameInput | text input | Display name | 8 (Stable) |
| DescriptionInput | text input | Description | 8 (Stable) |
| PublisherInput | text input | Publisher | 8 (Stable) |
| NextButton | `role=button` | Next | 9 (Stable) |

### 3. Program
| Element | Selector/Role | Text | Stability |
|---------|--------------|------|-----------|
| InstallTimeInput | text input | Installation time (mins) | 8 (Stable) |
| NextButton | `role=button` | Next | 9 (Stable) |

### 4. Requirements
| Element | Selector/Role | Text | Stability |
|---------|--------------|------|-----------|
| MinOSDropdown | combobox/dropdown | Minimum OS | 8 (Stable) |
| Win10_1607_Option | option/treeitem | Windows 10 1607 | 8 (Stable) |
| NextButton | `role=button` | Next | 9 (Stable) |

### 5. Detection Rules
| Element | Selector/Role | Text | Stability |
|---------|--------------|------|-----------|
| DetectionRuleType | dropdown/combobox | Rules format | 8 (Stable) |
| MSITypeOption | option | MSI | 8 (Stable) |
| NextButton | `role=button` | Next | 9 (Stable) |

### 6. Dependencies
| Element | Selector/Role | Text | Stability |
|---------|--------------|------|-----------|
| NextButton | `role=button` | Next | 9 (Stable) |

### 7. Supersedence
| Element | Selector/Role | Text | Stability |
|---------|--------------|------|-----------|
| NextButton | `role=button` | Next | 9 (Stable) |

### 8. Assignments
| Element | Selector/Role | Text | Stability |
|---------|--------------|------|-----------|
| RequiredGroup | assignment section | Required | 8 (Stable) |
| AllUsersTarget | assignment target | All users | 8 (Stable) |
| NextButton | `role=button` | Next | 9 (Stable) |

### 9. Review + Create
| Element | Selector/Role | Text | Stability |
|---------|--------------|------|-----------|
| CreateButton | `role=button` | Create | 9 (Stable) |

## Verification Elements
| Element | Description | Stability |
|---------|-------------|-----------|
| AppName | Verified post-create: app display name | 8 (Stable) |
| AppDescription | Verified post-create: description | 8 (Stable) |
| AppPublisher | Verified post-create: publisher | 8 (Stable) |
| RequiredAssignment | Verified post-create: required assignment target | 8 (Stable) |

## Self-Healing Status
- SelfHealingLocator wrappers present in:
  - `AllAppsUtils.cs` → `VerifyPropertyAssignmentsAsync`
  - `MSCommandBarWithMenubarRole.cs` → `ClickCommandBarButtonByAriaLabelAsync`
- **Status**: Ready but NOT activated — all primary locators resolved on first attempt
- **Previous issue**: `VerifyPropertyAssignmentsAsync` had CustomLogException (DOM timing) — now resolved

## Test Package
- File: `10 Fake MSI.intunewin`
- App name pattern: `TC{guid}` (e.g., `TCef36ff836d6a4416`)
- Cleanup: App deleted after verification

## Notes
- Non-ENUS locale test — confirms locators work across locales
- Installation time set to 5 minutes (non-default)
- Minimum OS: Windows 10 1607
- Detection: MSI type detection rules
