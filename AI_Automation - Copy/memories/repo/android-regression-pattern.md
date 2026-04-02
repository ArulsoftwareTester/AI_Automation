# Android Regression Test Generation Pattern

## Pattern: Base Class + Thin Subclass (same as Windows Regression)

### Structure
```
Test set A/Android Regression Test/
 AndroidRegressionTestSetABase.cs     # Base class with full test flow
 952276_Android_Regression_<testName>.cs  # Thin subclass per test case
 952284_Android_Regression_<testName>.cs
 ... (74 total subclass files)
```

### Base Class: AndroidRegressionTestSetABase.cs
- Inherits from `PageTest`
- Abstract properties: `RegressionTestCaseId`, `RegressionTestName`, `TestDisplayName`, `TestDescription`, `DataFileName`
- `RunTestAsync()` handles full flow: Login  Navigate  Add App  Set fields  Assign  Create  Verify  Cleanup
- Handles 3 Android app types: LOB (APK upload), Store App (Appstore URL), Web Link (App URL)
- Android-specific: `MinimumOperatingSystem` at top-level parameter (not nested in Requirements)
- JSON data loaded from `TestData_AppReggersion/<DataFileName>`

### Thin Subclass Template
```csharp
[TestFixture]
public class Test_<ID>_Test_Set_A_Android_Regression_<TestName> : AndroidRegressionTestSetABase
{
    protected override string RegressionTestCaseId => @"TC_<ID>";
    protected override string RegressionTestName => @"<testName from JSON>";
    protected override string TestDisplayName => @"Test set A - Android Regression - <testName>";
    protected override string TestDescription => @"<description from JSON>";
    protected override string DataFileName => @"<json-file-name>.json";

    [Test]
    public async Task TestMethod_<ID>_Test_Set_A_Android_Regression_<TestName>Async()
    {
        await RunTestAsync();
    }
}
```

### JSON Data Files (6 files, 74 test cases total)
- `Android_StoreApp_AppRegression.json`  7 tests (store app via deep link)
- `Android_LOB_AppRegression.json`  13 tests (APK upload)
- `Android_AssignmentFilters_AppRegression.json`  9 tests (filter match/no-match)
- `Android_WebLink_AppRegression.json`  3 tests (web link apps)
- `Android_Conflict_Include_AppRegression.json`  16 tests (conflict resolution include)
- `Android_Conflict_Exclude_AppRegression.json`  26 tests (conflict resolution exclude)

### Android vs Windows Differences
| Feature | Windows | Android |
|---------|---------|---------|
| App types | LOB (appx/msi), Store (WinGet) | LOB (APK), Store (Play Store URL), Web Link |
| Min OS | Nested in `Requirements parameters` | Top-level `Minimum operating system` |
| Extra fields | Program params, Detection rules, Dependencies, Supercedence | Appstore URL, App URL, Targeted platform |
| JSON file | `windows Regression.json` (single) | 6 separate JSON files by category |
| Base class | `WindowsRegressionTestSetABase` | `AndroidRegressionTestSetABase` |
| DataFileName | Hardcoded to `windows Regression.json` | Abstract property per subclass |

### Generation Command (PowerShell)
Loop through JSON files, create thin .cs subclass for each testCase entry.
File name pattern: `<tcId>_Android_Regression_<testName>.cs`
