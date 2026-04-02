---
name: dom-knowledge
description: "Manage the DOM knowledge base for Intune portal pages. Use when: storing DOM snapshots, updating element registry, comparing DOM changes, checking if a page was previously captured, tracking element stability."
---

# DOM Knowledge Management

## When to Use
- Storing a new DOM snapshot after page exploration
- Looking up a previously captured page or element
- Comparing old vs new DOM to detect portal changes
- Updating element stability scores
- Checking if DOM knowledge is stale

## Storage Location
```
/memories/repo/dom-knowledge/
├── page-index.md          # Master index of all captured pages
├── element-registry.md    # Cross-page element lookup table
├── dom-change-log.md      # History of detected DOM changes
├── intune-apps-list.md    # Per-page DOM structure files
├── intune-app-create.md
├── intune-device-config.md
├── intune-compliance.md
├── intune-login.md
└── ...
```

## Procedure

### Reading DOM Knowledge

#### Find a page
1. Read `page-index.md` → Find the page by URL pattern or name
2. Read the page-specific file (e.g., `intune-app-create.md`) → Full DOM structure

#### Find an element
1. Read `element-registry.md` → Search by identifier, selector, or text
2. Check `Stable` column — `Yes` means reliable, `No` means frequently changes

#### Check for changes
1. Read `dom-change-log.md` → Recent changes with timestamps and impact

### Writing DOM Knowledge

#### New page captured
1. Create `/memories/repo/dom-knowledge/<page-slug>.md` with full DOM structure
2. Add entry to `page-index.md`:
   ```markdown
   | Page Name | URL Pattern | Last Captured | Elements Count | Iframes |
   | App Create | */Apps/AddApp* | 2026-04-02 14:30 | 63 | AppList.ReactView |
   ```
3. Add all key elements to `element-registry.md`

#### Existing page re-captured
1. Compare new DOM with stored DOM
2. Update the page file with new structure
3. For each changed element, add entry to `dom-change-log.md`:
   ```markdown
   | Date | Page | Element | Change Type | Old Value | New Value | Impact |
   | 2026-04-02 | App Create | SaveButton | attribute_changed | id="save-v1" | id="save-v2" | Locators using #save-v1 break |
   ```
4. Update `element-registry.md` — mark changed elements as `Stable: No`

### Page File Format
```markdown
# <Page Name> — DOM Structure
**URL**: `https://intune.microsoft.com/#view/...`
**Last Captured**: 2026-04-02 14:30
**Iframes**: AppList.ReactView

## Navigation Path
Devices → Configuration → Create

## Command Bar
| Button | Text | Enabled | Selector |
|--------|------|---------|----------|
| Create | "Create" | true | button[data-automationid='CreateButton'] |
| Save | "Save" | false | button[data-automationid='SaveButton'] |

## Form Elements
| Element | Tag | Role | Label | Placeholder | Selector | Iframe |
|---------|-----|------|-------|-------------|----------|--------|
| Policy Name | input | textbox | "Policy name" | "Enter name" | #policyNameInput | DeviceConfiguration.ReactView |

## Full Accessibility Tree
(paste from browser_snapshot output)
```

### Element Registry Format
```markdown
| Identifier | Page | Selector | Role | Text | Label | Iframe | Last Seen | Stable |
|------------|------|----------|------|------|-------|--------|-----------|--------|
| SaveButton | App Create | button[data-automationid='SaveButton'] | button | Save | Save | AppList.ReactView | 2026-04-02 | Yes |
```

## Staleness Rules
- **< 24 hours old**: Current — use directly
- **1-7 days old**: Likely valid — use but verify on failure
- **> 7 days old**: Stale — trigger re-capture before relying on it
- **Element marked unstable**: Always verify with live snapshot

## Change Impact Classification
| Change Type | Impact Level | Action |
|-------------|-------------|--------|
| `attribute_changed` (id, class) | High | Update locators, add SelfHealingLocator |
| `text_changed` | Medium | Update getByText hints |
| `structure_shifted` | High | Update CSS path locators |
| `visibility_changed` | Low | May need wait adjustments |
| `iframe_renamed` | Critical | Update all iframe references |
| `element_removed` | Critical | Feature may have been removed — check docs |
