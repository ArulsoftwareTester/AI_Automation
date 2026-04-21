# Test Monitoring Skill

## Overview

This skill provides the complete signal dictionary, state machine, and parsing rules for real-time monitoring of Intune Playwright test execution via `dotnet test` console output. Used by the Monitor Agent.

---

## Test Execution State Machine

```
┌─────────────┐
│   IDLE      │  (waiting for terminal output)
└──────┬──────┘
       │ "Restore succeeded" / ".dll"
       ▼
┌─────────────┐
│  BUILDING   │  Build/restore phase
└──────┬──────┘
       │ "NUnit3TestExecutor discovered"
       ▼
┌─────────────┐
│ DISCOVERING │  Test discovery
└──────┬──────┘
       │ "[RunTest.exe][Info]" / "started..."
       ▼
┌─────────────────────────────────────────────────────┐
│                    EXECUTING                         │
│                                                      │
│  ┌─────────┐ ┌──────────┐ ┌────────────┐            │
│  │  LOGIN  │→│ NAVIGATE │→│ APP_CREATE │            │
│  └─────────┘ └──────────┘ └─────┬──────┘            │
│                                  │                    │
│              ┌───────────────┐   │  ┌──────────┐     │
│              │  ASSIGNMENTS  │◄──┘  │  REVIEW  │     │
│              └───────┬───────┘      └─────┬────┘     │
│                      │                    │           │
│                      └────────►┌──────────┘           │
│                                │                      │
│                         ┌──────▼─────┐                │
│                         │  CLEANUP   │                │
│                         └──────┬─────┘                │
└────────────────────────────────┼─────────────────────┘
                                 │
              ┌──────────────────┼────────────────┐
              │                  │                 │
       ┌──────▼──────┐  ┌───────▼──────┐  ┌──────▼──────┐
       │  COMPLETE   │  │   FAILED     │  │   ERROR     │
       │ (succeeded) │  │ (test fail)  │  │ (exception) │
       └─────────────┘  └──────────────┘  └─────────────┘
```

### State Transition Triggers

| Current State | Trigger Pattern | Next State |
|---------------|-----------------|------------|
| IDLE | `Restore succeeded` or `succeeded (` | BUILDING |
| BUILDING | `NUnit3TestExecutor discovered` | DISCOVERING |
| DISCOVERING | `started...` or first `[RunTest.exe][Info]` | EXECUTING |
| EXECUTING | `Step 1:` containing "Login" | LOGIN |
| LOGIN | `Step 2:` containing "Navigat" | NAVIGATE |
| NAVIGATE | `Step 3:` containing "Add" or "Click" | APP_CREATE |
| APP_CREATE | `Step 10:` containing "assignment" | ASSIGNMENTS |
| ASSIGNMENTS | `Step 11:` containing "Review" or "Create" | REVIEW |
| REVIEW | `Click to open context menu` or `Delete` | CLEANUP |
| CLEANUP | `Test summary:` | COMPLETE |
| Any | Priority 0 or Priority 1 failure signal | FAILED |
| Any | Build error before DISCOVERING | ERROR |

---

## Signal Parsing Rules

### How to Parse Console Output

Each poll returns the full terminal buffer. Track `lastParsedLine` count and only parse new lines.

For each new line, apply these rules **in priority order** (stop at first match per line):

```
FOR each new_line in output[lastParsedLine..]:
  1. Check Priority 0 patterns → IMMEDIATE FAILURE
  2. Check Priority 1 patterns → FAILURE DETAILS
  3. Check Priority 2 patterns → HEALING EVENT (log + continue)
  4. Check Priority 3 patterns → RETRY THRESHOLD (evaluate + continue)
  5. Check Priority 4 patterns → PROGRESS (update state + continue)
  6. Check DOM extraction patterns → KNOWLEDGE CAPTURE (accumulate)
```

---

## Signal Priority Reference

### Priority 0 — Immediate Failure Detection

These patterns indicate the test has encountered a blocking error. **Stop monitoring immediately** and compile failure report.

```regex
Timeout \d+ms exceeded
waiting for locator
element is not visible
element is not attached
PlaywrightException
TimeoutError
NUnit\.Framework\.Assertion
System\.InvalidOperationException
Microsoft\.Playwright\.PlaywrightException
```

**Action**: Set state to FAILED, capture:
- The full error line
- The 5 lines preceding the error
- The last known `Current Locator` line
- The last known `Step N:` line
- The last known page/iframe context

### Priority 1 — Failure Detail Extraction

These appear after a Priority 0 signal or at test end. Extract the content.

```regex
Error Message:\s*(.+)
Stack Trace:\s*(.+)
Failed!
failed:\s+(\d+)     # from Test summary line
```

**Action**: Append to failure report:
- `Error Message:` → extract everything after the colon to the next blank line
- `Stack Trace:` → extract file path and line number
- `Failed!` → confirm failure in summary
- `failed: N` where N > 0 → failure count

### Priority 2 — Self-Healing Events

These indicate the SelfHealingLocator is active. **Continue monitoring** but log the event.

```regex
\[SelfHealing\] Primary locator FAILED
\[SelfHealing\] Auto-healed at retry (\d+)
\[SelfHealing\] HEALED '(.+)' via (.+)
\[SelfHealing\] \*\*\* PERMANENT FIX RECOMMENDED \*\*\*
\[HEAL_SIGNAL\] retryCount=(\d+) locator=(.+)
\[HEAL_REQUEST\] step=(.+) controlType=(.+) error=(.+)
\[SelfHealingRetry\] Attempt (\d+)/(\d+) failed
\[SelfHealing\] AI Strategy (\d+): (.+)
```

**Action**: Add to healing events list:
```
{
  timestamp: <from log line>,
  type: "primary_failed" | "auto_healed" | "healed_via" | "permanent_fix" | "heal_signal" | "heal_request",
  key: <locator key if available>,
  strategy: <strategy name if available>,
  retryCount: <N if available>,
  detail: <full line>
}
```

### Priority 3 — Retry Count Monitoring

The framework logs `Try \`N\`` patterns when checking if a locator exists.

```regex
Try `(\d+)` check if current locator is exist
```

**Action**: Extract N and evaluate:
- N = 1: Stable element, score = 9
- N = 2-5: Slightly slow, score = 7
- N = 6-15: Moderate delay, score = 5
- N = 16-30: Fragile locator, score = 3 — **add to healing watch list**
- N = 31-60: **WARNING** — locator struggling, check if `[SelfHealing]` follows
- N = 61-100: **CRITICAL** — healing likely failed
- N = 101-150: **EMERGENCY** — element probably doesn't exist
- N ≥ 150: **TERMINAL** — test will timeout

Correlate with the preceding `Current Locator` line to identify WHICH locator is struggling.

### Priority 4 — Progress Tracking

```regex
Step (\d+):\s*(.+)                    # test step progression
Iframe name: (.+), check if it's loaded   # iframe loading
Iframe name: (.+) have been loaded        # iframe confirmed
Current Locator (.+)                       # active locator
Hover "(.+)"                              # element hover
Click "(.+)"                              # element click
Test summary: total: (\d+), failed: (\d+), succeeded: (\d+)  # final result
succeeded \(.*\) →                        # build success
NUnit3TestExecutor discovered (\d+)       # test count
```

---

## DOM Knowledge Extraction

### What to Capture from Console Output

#### Locator Chains
Every `Current Locator Locator@...` line contains a full Playwright locator chain. Parse:

```
Current Locator Locator@iframe[name="AppList.ReactView"][class*="fxs-reactview-frame-active"]
  >> nth=0
  >> internal:control=enter-frame
  >> [class*='ms-SearchBox-field'][placeholder='Search']
```

Extract:
- **Iframe context**: `AppList.ReactView` with class `fxs-reactview-frame-active`
- **CSS selector**: `[class*='ms-SearchBox-field'][placeholder='Search']`
- **Nth index**: 0 (first match)
- **Stability**: from the `Try \`N\`` count on the following line

#### Iframe Registry
From `Iframe name: X have been loaded`:
- Iframe name (e.g., `AppList.ReactView`)
- Confirmed loaded = true
- iframe count from `Current iframe count: N`

#### Element Interactions
From `Hover "X"` and `Click "X"`:
- Element display name or aria-label
- Associate with the `Current Locator` line immediately before

### How to Structure Captured Knowledge

Build an accumulator during monitoring:

```
locators_discovered = [
  {
    identifier: "SearchBox",
    page: "All Apps",
    selector: "[class*='ms-SearchBox-field'][placeholder='Search']",
    iframe: "AppList.ReactView",
    retryCount: 1,
    stability: 9,
    timestamp: "2026-04-10 13:37:52"
  },
  ...
]

iframes_confirmed = [
  {
    name: "AppList.ReactView",
    className: "fxs-reactview-frame-active",
    count: 1,
    timestamp: "2026-04-10 13:37:50"
  }
]

interactions = [
  {
    action: "Click",
    target: "Delete",
    locator: "[class*='ms-ContextualMenu-link'][aria-label='Delete']",
    timestamp: "2026-04-10 13:38:27"
  }
]
```

---

## Failure Classification Matrix

When a failure is detected, classify it based on the error pattern:

| Error Pattern | Classification | Root Cause | Recommended Fix |
|---------------|---------------|------------|-----------------|
| `Timeout \d+ms exceeded` + locator | **Locator Failure** | Element not found / changed | SelfHealingLocator wrapper |
| `element is not visible` | **Visibility Issue** | Element exists but hidden/overlapped | Wait for visibility or scroll |
| `element is not attached` | **Detachment** | DOM re-rendered during interaction | Retry with fresh locator |
| `waiting for locator` | **Locator Failure** | Selector doesn't match any element | Update selector or add healing hints |
| `AssertionException` | **Data Mismatch** | Expected value ≠ actual value | Fix test data or expected value |
| `InvalidOperationException` | **Test Logic** | Test case disabled or data missing | Check JSON test data |
| `Navigation timeout` | **Page Load** | Intune portal slow or URL changed | Increase timeout or verify URL |
| Iframe-related timeout | **Iframe Issue** | ReactView not loaded | Wait for iframe or check portal state |
| `[SelfHealing]` then timeout | **Healing Failed** | All healing strategies exhausted | New locator pattern needed |
| Try count > 100 with no healing | **Missing Hints** | No HealingHints for this locator | Add hints to HealingHintsRegistry |

---

## Polling Strategy

### Single Test Monitoring
```
poll_interval = 10 seconds
max_silence = 60 seconds (3 consecutive empty polls → report hang)
max_duration = 600 seconds (10 minutes → report timeout)
```

### Batch Test Monitoring (future)
```
poll_interval = 15 seconds (more tests = more output)
track_per_test = true (separate state machine per test)
max_duration = 7200 seconds (2 hours for full batch)
```

---

## Report Templates

### Success Report
```markdown
## Monitor Report — PASSED

### Result
- **Status**: PASSED ✅
- **Duration**: {X}s
- **Test**: {TestClassName}.{TestMethodName}
- **Test ID**: TC_{NNNNNN}

### Steps Completed
1. ✅ Build & Restore ({X}s)
2. ✅ Test Discovery (1 test found)
3. ✅ Login to Intune Portal
4. ✅ Navigate to Apps > All Apps
5. ✅ Click Create/Add button
6. ✅ Select app type
7. ✅ Configure app information
8. ✅ Configure assignments
9. ✅ Review + Create
10. ✅ Cleanup (delete app)

### DOM Knowledge Captured
- **Locators**: {N} unique locators extracted
- **Iframes**: {list of confirmed iframes}
- **Elements**: {N} interact targets logged
- **All locators resolved on try 1**: {yes/no}
- **Fragile locators** (try > 5): {list or "none"}

### Healing Events
{none | list}
```

### Failure Report
```markdown
## Monitor Report — FAILED

### Result
- **Status**: FAILED ❌
- **Duration**: {X}s
- **Test**: {TestClassName}.{TestMethodName}
- **Failed at**: Step {N} — {step description}

### Error Details
- **Error Type**: {classification from matrix}
- **Error Message**: {exact error text}
- **Failing Locator**: {full locator chain}
- **Last Retry Count**: Try `{N}`
- **Last Known Page**: {iframe or URL context}

### Stack Trace
```
{extracted stack trace with file + line}
```

### Steps Before Failure
1. ✅ Login ({X}s)
...
N. ❌ {Failed step} — {error}

### Context for Debug Agent
- **DOM Knowledge State**: {element exists in registry: yes/no, last stability: N}
- **Healing Attempted**: {yes/no, which strategies}
- **Preceding Locators**: {last 3 successful locators}
- **Iframe Context**: {current iframe at failure time}
```
