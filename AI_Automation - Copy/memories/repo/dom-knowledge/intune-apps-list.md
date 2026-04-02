# Intune Apps - All Apps Page

## Page Info
- **URL Pattern**: `https://intune.microsoft.com/#view/Microsoft_Intune_Apps/AllApps*`
- **Last Captured**: 2026-04-02
- **Source**: Test logs from TC_14673246
- **Iframe**: AppList.ReactView

## Key Elements
| Element | Selector | Iframe | Notes |
|---------|----------|--------|-------|
| App List Grid | [class*='ms-DetailsList--Compact'] | AppList.ReactView | Main grid containing app rows |
| Row Fields | [class*='ms-DetailsRow-fields'] | AppList.ReactView | Each row in the grid |
| Command Bar | MSCommandBarWithMenubarRole | AppList.ReactView | Contains Add/Create, Refresh, Filter, Export, Columns |
| Search Box | MS_SearchBox component | AppList.ReactView | Search for apps by name |
| Create Button | commandBar "Create" | AppList.ReactView | Was "Add" in older portal versions |
| Refresh Button | commandBar "Refresh" | AppList.ReactView | Refresh grid data |

## Iframe Details
- **Name**: `AppList.ReactView`
- **Active class**: `fxs-reactview-frame-active`
- **Full selector**: `iframe[name="AppList.ReactView"][class*="fxs-reactview-frame-active"]`
- **Load behavior**: Can have multiple instances, wait for single active frame
- **Load time**: Variable, uses 600-iteration wait loop in framework

## Grid Structure (inside iframe)
```
iframe[name="AppList.ReactView"]
  └── [class*='ms-DetailsList--Compact'] (Nth(0))
        └── [class*='ms-DetailsRow-fields'] (per row)
              └── has-text="AppName" (text match)
```

## Known Issues
- Grid row search for cleanup failed (app "Instagram97f447fb712b0f09" not found because app was never fully created)
- Command bar "Add" button was renamed to "Create" in PE environment
