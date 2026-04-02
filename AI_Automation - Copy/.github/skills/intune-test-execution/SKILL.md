---
name: intune-test-execution
description: "Run Intune Playwright tests via dotnet test. Use when: executing tests, running specific test by case ID, batch test execution, choosing headed vs headless mode, interpreting test results, handling test failures."
argument-hint: "Test case ID or filter pattern (e.g., Test_14673246)"
---

# Intune Test Execution

## When to Use
- Running a specific Intune test by case ID
- Batch execution of test suites (Security Baseline, Admin Templates)
- Choosing the right runsettings (headed/headless/parallel)
- Parsing test output for pass/fail/skip
- Re-running failed tests after a fix

## MANDATORY: Invoke Page Exploration on Every Test Run
Before executing any test, the **intune-page-exploration** skill MUST be triggered to:
1. Identify which Intune portal page the test targets
2. Check if DOM knowledge exists for that page
3. If missing or stale → explore the page via Playwright MCP first
4. This ensures Debug/Fix agents have full page context if the test fails

## Project Structure
- **Solution**: `AI_Automation - Copy/AI_Automation.sln`
- **NUnit Tests**: `IntuneCanaryTests/IntuneCanaryTests.csproj`
- **Playwright Tests**: `PlaywrightTests/` (page objects, locators, utilities)

## Run Commands

### Single Test (Headed — for debugging)
```powershell
cd "C:\Users\v-arulmani\Downloads\AI_Automation 5\AI_Automation - Copy"
dotnet test --filter "FullyQualifiedName~Test_<CASE_ID>" --settings headed.runsettings --logger "console;verbosity=detailed"
```

### Single Test (Headless — for CI)
```powershell
dotnet test --filter "FullyQualifiedName~Test_<CASE_ID>" --settings headless_aggressive.runsettings --logger "console;verbosity=detailed"
```

### Batch Tests (Parallel Workers)
```powershell
# 3 workers
dotnet test --filter "<FILTER>" --settings parallel_3workers.runsettings --logger "console;verbosity=minimal"

# 4 workers
dotnet test --filter "<FILTER>" --settings parallel_4workers.runsettings --logger "console;verbosity=minimal"
```

### Security Baseline Suite
```powershell
.\Run_SecurityBaseline_Tests.ps1      # 8 parallel workers, headless
.\Run_SecurityBaseline_3Workers.ps1   # 3 parallel workers
```

### Batched Execution (50 tests per batch)
```powershell
.\Run_Batched_Tests.ps1               # 6 parallel workers per batch
```

## Runsettings Reference

| File | Mode | Workers | Use Case |
|------|------|---------|----------|
| `headed.runsettings` | Headed (visible browser) | 1 | Debugging, watching test |
| `headless_aggressive.runsettings` | Headless | 20 | Fast CI runs |
| `headless_6hour_timeout.runsettings` | Headless | varies | Long-running suites |
| `parallel_2workers.runsettings` | varies | 2 | Light parallel |
| `parallel_3workers.runsettings` | varies | 3 | Medium parallel |
| `parallel_4workers.runsettings` | varies | 4 | Heavy parallel |
| `security_baseline_3workers.runsettings` | varies | 3 | Security Baseline |

## Filter Patterns

```
FullyQualifiedName~Test_14673246          # Single test by case ID
FullyQualifiedName~SecurityBaseline       # All Security Baseline tests
FullyQualifiedName~AdminTemplates         # All Admin Template tests
FullyQualifiedName~Class1|FullyQualifiedName~Class2  # Multiple classes
```

## Interpreting Output

### Success
```
Passed!  - Failed: 0, Passed: 1, Skipped: 0
```

### Failure — Look For
- `Timeout 30000ms exceeded` → Locator or loading issue
- `locator not found` → Element selector broken
- `element is not visible` → Timing or DOM change
- `CustomLogException` → Framework-level failure (check SelfHealingLocator)
- `[SelfHealing] HEALED` → Locator was auto-healed (check healing report)
- `[SelfHealing] ALL fallback strategies failed` → Needs manual fix

### After Failure
1. Check ExtentReports: `ExtentReports/` for HTML report with screenshots
2. Check test logs for `[SelfHealing]` messages
3. If locator issue → delegate to self-healing-locator skill
4. If page changed → delegate to intune-page-exploration skill
