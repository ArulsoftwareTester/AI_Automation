---
name: intune-page-exploration
description: "Browse Intune portal pages live using Playwright MCP and enrich with Microsoft Learn docs. Use when: running tests, executing test, dotnet test, test run, test execution, exploring Intune UI, capturing page structure, understanding portal blades, verifying expected elements, building DOM knowledge for unknown pages, test failure analysis, before test run, during test run."
---

# Intune Page Exploration

## When to Use
- **Every test run** — automatically explore the target Intune page before/during test execution
- Before running tests on a page not yet in DOM knowledge base
- When a test fails on an unfamiliar Intune portal blade
- To verify what elements SHOULD exist on an Intune page
- To capture full DOM structure of a new Intune feature
- To cross-reference portal UI with official documentation

## MANDATORY: Auto-Invoke During Test Runs

This skill MUST activate whenever a test is being executed. Follow this flow:

### Pre-Test Exploration
Before `dotnet test` runs:
1. **Identify target page** — Parse the test case to determine which Intune blade it targets
   - Look at test JSON in `TestData/` or `TestData_AppReggersion/` for the test case ID
   - Look at the test class to see which page objects it uses
2. **Check DOM knowledge** — Read `/memories/repo/dom-knowledge/page-index.md`
   - If page exists and is < 24h old → skip exploration, use cached knowledge
   - If page is stale or missing → proceed with exploration below
3. **Explore the target page** using Playwright MCP (Steps 1-7 below)
4. **Provide context to test** — DOM knowledge is now available for Debug/Fix agents if the test fails

### During Test Failure
When a test fails:
1. **Immediately capture** the current page state via Playwright MCP
2. **Compare** with stored DOM knowledge — what changed?
3. **Fetch Intune docs** via Microsoft Learn MCP — is the behavior expected?
4. **Feed all context** to Debug Agent for root cause analysis

## Procedure

### Step 1: Navigate to Intune Portal (Playwright MCP)
```
browser_navigate → https://intune.microsoft.com
```
Wait for the portal to fully load. Intune uses React-based views inside iframes (`*.ReactView`).

### Step 2: Navigate to Target Blade
Use `browser_click` to navigate through the portal menu:
- **Devices** → Configuration, Compliance, Enrollment, etc.
- **Apps** → All apps, App configuration, App protection
- **Endpoint security** → Security baselines, Antivirus, Firewall
- **Tenant administration** → Roles, Connectors, Customization

### Step 3: Wait for React Views to Load
```
browser_wait_for → Wait for iframe or specific element to appear
```
Key iframes to wait for:
- `AppList.ReactView` — App listing pages
- `DeviceConfiguration.ReactView` — Device config pages
- `CompliancePolicy.ReactView` — Compliance pages
- `SecurityBaseline.ReactView` — Security baseline pages

### Step 4: Capture Full Page Structure
```
browser_snapshot → Get accessibility tree (roles, labels, states)
browser_take_screenshot → Visual reference
browser_evaluate → Extract raw DOM attributes, iframe list, element counts
```

### Step 5: Extract Key Elements
For each page, identify and record:
- **Navigation elements**: Menu items, breadcrumbs
- **Command bar**: Create, Save, Delete, Refresh buttons
- **Form elements**: Inputs, dropdowns, toggles, radio buttons
- **Table elements**: Column headers, row data, pagination
- **Iframe structure**: Which ReactView iframe contains which elements

### Step 6: Enrich with Microsoft Learn Documentation
```
microsoft_docs_search → "Intune <feature-name> configuration"
microsoft_docs_fetch → Fetch specific doc page for the feature
microsoft_code_sample_search → "Intune Graph API <feature>"
```

Use docs to verify:
- Expected settings/options on the page
- Valid values for dropdowns and configuration fields
- Graph API endpoint that backs the portal page
- Whether a missing element is expected behavior or a portal issue

### Step 7: Store to DOM Knowledge Base
Send all captured data to Memory Agent for storage in:
- `/memories/repo/dom-knowledge/page-index.md` — Add page entry
- `/memories/repo/dom-knowledge/<page-name>.md` — Full DOM structure
- `/memories/repo/dom-knowledge/element-registry.md` — Individual elements

## Common Intune Portal Patterns

### Authentication
- Portal URL: `https://intune.microsoft.com`
- Auth redirects through `login.microsoftonline.com`
- Certificate auth via `certauth.login.microsoftonline.com`

### UI Structure
- Left nav menu → Blade selection
- Command bar at top of each blade
- Content area with iframes (`*.ReactView`)
- Flyout panels for create/edit wizards

### Wizard Pattern (Create/Edit)
Most Intune create flows follow:
1. **Basics** tab → Name, Description, Platform
2. **Configuration** tab → Settings specific to policy type
3. **Assignments** tab → Groups, filters
4. **Review + Create** tab → Summary, Create button

### Elements That Frequently Change
- Dynamic IDs on form inputs (use `aria-label` instead)
- Button text changes between portal updates
- Iframe names can shift (e.g., `AppList.ReactView` → `Apps.ReactView`)
- React component keys regenerate on re-render
