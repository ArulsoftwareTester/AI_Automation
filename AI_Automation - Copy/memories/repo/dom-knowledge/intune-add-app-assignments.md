# Intune Add App - Assignments Page

## Page Info
- **URL Pattern**: `https://intune.microsoft.com/#view/Microsoft_Intune_Apps/AddApp*` (step 2)
- **Last Captured**: 2026-04-02T09:33:11Z
- **Source**: Error screenshot from TC_14673246, test logs
- **Iframes**: None on wizard pages (wizard is rendered on main page, NOT inside ReactView iframe)

## Page Header
- Breadcrumb: Home > Apps | All Apps
- Title: "Add App"
- Subtitle: "Microsoft Store app (new)"
- Close button (X) at top right

## Wizard Steps (Tab Navigation)
| Step | Label | Status | Aria Role |
|------|-------|--------|-----------|
| 1 | App information | Complete (green check) | tab |
| 2 | Assignments | Current/Active | tab |
| 3 | Review + create | Not started | tab |

## Assignments Sections

### Section 1: Required
```
<h3>Required</h3> (with info icon)
<grid>
  Column headers: Group mode | Group | Status | Filter mode | Filter | End user notifications | Installation dea...
  Content: "No assignments"
</grid>
<links>
  + Add group     (class: msportalfx-text-primary ext-controls-selectLink)
  + Add all users (class: msportalfx-text-primary ext-controls-selectLink) 
  + Add all devices (class: msportalfx-text-primary ext-controls-selectLink)
</links>
```

### Section 2: Available for enrolled devices
```
<h3>Available for enrolled devices</h3> (with info icon)
<grid>
  Column headers: Group mode | Group | Status | Filter mode | Filter | End user notifications | Installation dea...
  Content: "No assignments"
</grid>
<links>
  + Add group
  + Add all users
  + Add all devices
</links>
```

### Section 3: Uninstall
```
<h3>Uninstall</h3> (with info icon)
<grid>
  Column headers: Group mode | Group | Status | Filter mode | Filter | End user notifications | Installation dea...
  Content: (not visible, page cut off at bottom)
</grid>
<links>
  (not visible in screenshot — possibly below fold)
</links>
```

## Bottom Navigation
- Previous button (outlined style)
- Next button (blue/primary style)

## Sidebar (Left Navigation)
| Icon | Label | Active |
|------|-------|--------|
| 🏠 | Home | No |
| 📊 | Dashboard | No |
| 🔧 | All services | No |
| 🔍 | Explorer | No |
| 📱 | Devices | No |
| 📦 | Apps | **Yes** (current) |
| 🛡 | Endpoint security | No |
| 🤖 | Agents | No |
| 📈 | Reports | No |
| 👤 | Users | No |
| 👥 | Groups | No |
| ⚙ | Tenant administration | No |
| 🔧 | Troubleshooting + support | No |

## Key Observations
1. **"+ Add group" links use nth-based selection** — the framework uses `Nth(0)` for Required, `Nth(1)` for Available, `Nth(2)` for Uninstall
2. **The Uninstall section's "+ Add group" (Nth(2)) failed** — it timed out after ~30s
3. **The Uninstall section may be below the visible viewport** — the screenshot shows the bottom of the page is cut off right at the Uninstall section
4. **No iframe wraps the wizard** — the Add App wizard runs on the main page, not inside AppList.ReactView
5. The `IFrameName = null` in AllAppsUtils is correct for wizard pages

## Known Issues
- `+ Add group` Nth(2) for Uninstall section: **FAILED** — likely needs scroll-into-view before click
- Element class `msportalfx-text-primary ext-controls-selectLink` is shared across all 3 sections
- Nth-based selection is fragile (stability score: 3)
