---
name: self-healing-locator
description: "Wrap failing Playwright locators with SelfHealingLocator for auto-recovery. Use when: locator not found, timeout waiting for selector, element not visible, element not attached, Playwright selector broken, dynamic ID issue."
---

# Self-Healing Locator

## When to Use
- Test fails with `locator not found` or `timeout waiting for selector`
- Error contains `Timeout 30000ms exceeded` or `element is not visible`
- Locator broke due to Intune portal UI update
- Dynamic element IDs causing flaky tests

## Key File
```
PlaywrightTests/Playwright/Common/Utils/SelfHealingLocator.cs
```

## How SelfHealingLocator Works

It tries 6 fallback strategies in order. First one that finds a visible element wins:

| Order | Strategy | Uses | Hint Field |
|-------|----------|------|------------|
| 1 | Primary locator (as-is) | Original selector | — |
| 2 | `getByText` (exact) | Visible text | `HealingHints.Text` |
| 3 | `getByText` (partial) | Contains text | `HealingHints.Text` |
| 4 | `getByRole` + name | ARIA role + text | `HealingHints.Role` + `Text` |
| 5 | `getByLabel` | ARIA label | `HealingHints.Label` |
| 6 | `getByPlaceholder` | Placeholder attr | `HealingHints.Placeholder` |

## Procedure

### Step 1: Identify the Failing Locator
From the error log, extract:
- The selector that failed (e.g., `#policyName-input`)
- The file and line number
- The iframe context (if any)

### Step 2: Search Existing Code FIRST
```
Search PlaywrightTests/ for the failing selector
Check: Page Objects, Elements.cs, ControlHelper.cs
```
**CRITICAL**: If the locator exists in the codebase, REUSE it. Do NOT create a new one.

### Step 3: Get HealingHints
Check DOM knowledge base first (`/memories/repo/dom-knowledge/element-registry.md`).

If not available, capture live using Playwright MCP:
```
browser_snapshot → Find element in accessibility tree
```

Extract:
- **Text**: Visible text content of the element
- **Label**: `aria-label` attribute value
- **Role**: ARIA role (button, textbox, combobox, etc.)
- **Placeholder**: `placeholder` attribute value
- **Identifier**: A unique human-readable name (e.g., "SaveButton", "PolicyNameInput")

### Step 4: Wrap with SelfHealingLocator
Replace the direct locator call:

**Before (fragile):**
```csharp
var saveBtn = page.Locator("#save-btn-v1");
await saveBtn.ClickAsync();
```

**After (self-healing):**
```csharp
var primaryLocator = page.Locator("#save-btn-v1");
var hints = new HealingHints
{
    Identifier = "SaveButton",
    Text = "Save",
    Label = "Save",
    Role = AriaRole.Button
};
var saveBtn = await SelfHealingLocator.ResolveAsync(page, primaryLocator, hints, iframeName: "DeviceConfiguration.ReactView");
await saveBtn.ClickAsync();
```

### Step 5: For Iframe-Scoped Elements
If the element is inside an iframe (most Intune elements are):
```csharp
var hints = new HealingHints
{
    Identifier = "PolicyNameInput",
    Label = "Policy name",
    Placeholder = "Enter policy name",
    Role = AriaRole.Textbox
};
var resolved = await SelfHealingLocator.ResolveAsync(
    page,
    page.Locator("#policyNameInput"),
    hints,
    iframeName: "DeviceConfiguration.ReactView"
);
```

### Step 6: Validate
1. Re-run the test
2. Check log for `[SelfHealing] HEALED` → fallback worked
3. Check log for `[SelfHealing] Primary locator resolved` → original fixed itself
4. If `ALL fallback strategies failed` → hints are wrong, re-capture DOM

## Healing Report
At end of test run, call:
```csharp
SelfHealingLocator.DumpHealingReport();
```
If any locator shows 3+ heals → recommend permanent locator update.

## Common Intune HealingHints

| Element | Text | Label | Role | Iframe |
|---------|------|-------|------|--------|
| Save button | "Save" | "Save" | Button | varies per blade |
| Create button | "Create" | "Create" | Button | varies |
| Policy name input | — | "Policy name" / "Name" | Textbox | varies |
| Next button | "Next" | "Next" | Button | varies |
| Delete button | "Delete" | "Delete" | Button | varies |
| Search box | — | "Search" / "Filter" | Textbox | varies |
