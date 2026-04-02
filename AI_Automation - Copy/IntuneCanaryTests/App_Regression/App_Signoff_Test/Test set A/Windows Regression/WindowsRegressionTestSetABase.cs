using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using global::PlaywrightTests.Common.Model;
using global::PlaywrightTests.Common.Utils.BaseUtils.Apps.ByPlatform;
using global::PlaywrightTests.Common.Utils.BaseUtils.PopUp;
using global::PlaywrightTests.Common.Utils.BaseUtils.UtilInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AventStack.ExtentReports;

namespace IntuneCanaryTests
{
    public abstract class WindowsRegressionTestSetABase : PageTest
    {
        private ExtentTest? _test;

        protected abstract string RegressionTestCaseId { get; }

        protected abstract string RegressionTestName { get; }

        protected abstract string TestDisplayName { get; }

        protected abstract string TestDescription { get; }

        private string NumericTestId => RegressionTestCaseId.Replace("TC_", string.Empty, StringComparison.OrdinalIgnoreCase);

        public override BrowserNewContextOptions ContextOptions()
        {
            var certPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "auth-cert", "AIAutoPE_3.pfx"));
            Console.WriteLine($"Certificate path: {certPath}");
            return new BrowserNewContextOptions
            {
                ClientCertificates = new[]
                {
                    new ClientCertificate
                    {
                        Origin = "https://certauth.login.microsoftonline.com/c0219094-a70e-402c-8dd2-fd89f7d64010/certauth",
                        PfxPath = certPath,
                        Passphrase = "Admin@123"
                    }
                }
            };
        }

        [SetUp]
        public void TestSetup()
        {
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, TestDisplayName);
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info($"Test ID: {NumericTestId}");
            _test.Info("Category: Test set A - Windows Regression");
        }

        [TearDown]
        public void TestTearDown()
        {
            _test?.Info($"Test completed at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        }

        protected async Task RunTestAsync()
        {
            string createdAppName = string.Empty;

            try
            {
                Console.WriteLine($"{GetType().Name} started...");
                _test?.Info("Test execution started");

                var testData = LoadTestData();
                if (!testData.Enabled)
                {
                    throw new InvalidOperationException($"Test case {RegressionTestCaseId} is disabled in windows Regression.json.");
                }

                var assignmentMode = GetAssignmentMode(testData.Parameters.Assignments);
                ValidateTestData(testData);

                _test?.Info($"Loaded regression data for {testData.TestName} (AppType: {testData.Parameters.AppType})");

                // Step 1: Login
                _test?.Info("Step 1: Logging into Intune Portal");
                var securityBaseline = new SecurityBaseline();
                await securityBaseline.Login(Page);
                _test?.Pass("Successfully logged into Intune Portal");

                var loginScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, $"Login_Complete_{NumericTestId}");
                if (!string.IsNullOrEmpty(loginScreenshot))
                {
                    _test?.Info("Login completed", MediaEntityBuilder.CreateScreenCaptureFromPath(loginScreenshot).Build());
                }

                var environment = ResolveEnvironment(Page.Url);
                var allAppsUtils = new AllAppsUtils(Page, environment);
                var parameters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                // Step 2: Navigate to All Apps
                _test?.Info("Step 2: Navigating to Apps > All Apps");
                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Navigate to All Apps",
                    new ControlInfo { ControlType = "GoToMainPageAsync" });

                // Step 3: Click Add button
                _test?.Info("Step 3: Clicking Add button");
                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Click Add button",
                    new ControlInfo { ControlType = "ClickAddButtonAsync" });

                // Step 4: Select app type
                _test?.Info($"Step 4: Selecting app type: {testData.Parameters.AppType}");
                var selectAppTypeUtils = new SelectAppTypeUtils(Page, environment);
                await selectAppTypeUtils.SelectAppTypeAsync(testData.Parameters.AppType);
                _test?.Pass($"App type '{testData.Parameters.AppType}' selected");

                // Step 5: Select app package file (for LOB apps)
                if (!string.IsNullOrWhiteSpace(testData.Parameters.SelectAppPackageFile))
                {
                    _test?.Info($"Step 5: Selecting app package file: {testData.Parameters.SelectAppPackageFile}");
                    parameters = await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        "Select app package file",
                        new ControlInfo { ControlType = "ClickSelectAppPackageFileButtonAsync" });

                    // Upload the file via the UploadFileUtils
                    var uploadUtils = new UploadFileUtils(Page, environment);
                    await uploadUtils.UploadFileAsync(testData.Parameters.SelectAppPackageFile);
                    _test?.Pass($"App package file '{testData.Parameters.SelectAppPackageFile}' uploaded");

                    // Click OK to confirm the upload and proceed to App Information form
                    await allAppsUtils.ClickOKBtnAsync();
                    _test?.Pass("Upload confirmed with OK");
                }

                // Step 6: Set app name
                _test?.Info($"Step 6: Setting app name: {testData.Parameters.Name}");
                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Set app name",
                    new ControlInfo
                    {
                        ControlType = "SetAppInformationNameAsync",
                        OperationValue = testData.Parameters.Name
                    });

                createdAppName = parameters.TryGetValue("AppAutomationAppName", out var uniqueName)
                    ? uniqueName
                    : testData.Parameters.Name;

                // Step 7: Set app description
                _test?.Info($"Step 7: Setting app description: {testData.Parameters.Description}");
                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Set app description",
                    new ControlInfo
                    {
                        ControlType = "SetAppInformationDescriptionAsync",
                        OperationValue = testData.Parameters.Description
                    });

                // Step 8: Set publisher
                _test?.Info($"Step 8: Setting publisher: {testData.Parameters.Publisher}");
                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Set publisher",
                    new ControlInfo
                    {
                        ControlType = "SetAppInformationPublisherAsync",
                        OperationValue = testData.Parameters.Publisher
                    });

                // Step 9: Handle Win32 specific parameters (program, requirements, detection rules)
                if (testData.Parameters.ProgramParameters != null)
                {
                    _test?.Info("Step 9a: Configuring program parameters");
                    await ConfigureProgramParametersAsync(allAppsUtils, parameters, testData.Parameters.ProgramParameters);
                }

                // Click Next to proceed to Requirements (for Win32 apps) or Assignments
                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Click Next",
                    new ControlInfo { ControlType = "ClickNextButtonAsync" });

                if (testData.Parameters.RequirementsParameters != null)
                {
                    _test?.Info("Step 9b: Configuring requirements");
                    await ConfigureRequirementsAsync(allAppsUtils, parameters, testData.Parameters.RequirementsParameters);

                    parameters = await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        "Click Next after Requirements",
                        new ControlInfo { ControlType = "ClickNextButtonAsync" });
                }

                if (testData.Parameters.DetectionRulesParameters != null)
                {
                    _test?.Info("Step 9c: Configuring detection rules");
                    await ConfigureDetectionRulesAsync(allAppsUtils, parameters, testData.Parameters.DetectionRulesParameters);

                    parameters = await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        "Click Next after Detection Rules",
                        new ControlInfo { ControlType = "ClickNextButtonAsync" });
                }

                // Skip Dependencies and Supersedence pages (click Next) if they exist
                if (!string.IsNullOrWhiteSpace(testData.Parameters.Dependencies))
                {
                    parameters = await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        "Click Next after Dependencies",
                        new ControlInfo { ControlType = "ClickNextButtonAsync" });
                }

                if (!string.IsNullOrWhiteSpace(testData.Parameters.Supercedence))
                {
                    parameters = await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        "Click Next after Supersedence",
                        new ControlInfo { ControlType = "ClickNextButtonAsync" });
                }

                // Step 10: Configure assignments
                _test?.Info("Step 10: Configuring assignments");
                parameters = await ConfigureAssignmentsAsync(allAppsUtils, parameters, testData, assignmentMode);

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Mark assignments complete",
                    new ControlInfo { ControlType = "MarkAssignmentsCompleteAsync" });

                // Click Next to go to Review + Create
                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Click Next to Review + Create",
                    new ControlInfo { ControlType = "ClickNextButtonAsync" });

                // Step 11: Create the app
                _test?.Info("Step 11: Creating the app");
                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Click Create",
                    new ControlInfo { ControlType = "ClickCreateButtonAsync" });

                await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

                var createScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, $"Create_Complete_{NumericTestId}");
                if (!string.IsNullOrEmpty(createScreenshot))
                {
                    _test?.Info("App created", MediaEntityBuilder.CreateScreenCaptureFromPath(createScreenshot).Build());
                }

                // Step 12: Verify created app
                _test?.Info("Step 12: Verifying created app properties");
                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Verify app name",
                    new ControlInfo
                    {
                        ControlType = "VerifyPropertyAsync",
                        Value = new List<string> { "Name", createdAppName }
                    });

                parameters = await VerifyAssignmentsAsync(allAppsUtils, parameters, testData, assignmentMode);

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Mark verification complete",
                    new ControlInfo { ControlType = "SuccessAppAutomationVerifyResult" });

                _test?.Info(testData.Parameters.DeviceValidation?.AppInstallationValidation ?? TestDescription);
                _test?.Pass("Test completed successfully!");
                Console.WriteLine("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, $"Error_{NumericTestId}");
                if (!string.IsNullOrEmpty(errorScreenshot))
                {
                    _test?.Fail($"Test failed with exception: {ex.Message}", MediaEntityBuilder.CreateScreenCaptureFromPath(errorScreenshot).Build());
                }
                else
                {
                    _test?.Fail($"Test failed with exception: {ex.Message}");
                }

                throw;
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(createdAppName))
                {
                    await TryCleanupCreatedAppAsync(createdAppName);
                }
            }
        }

        private async Task ConfigureProgramParametersAsync(
            AllAppsUtils allAppsUtils,
            Dictionary<string, string> parameters,
            WindowsRegressionProgramParameters programParams)
        {
            if (!string.IsNullOrWhiteSpace(programParams.InstallBehavior))
            {
                await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    $"Set install behavior to {programParams.InstallBehavior}",
                    new ControlInfo
                    {
                        ControlType = "SetAppInformationInstallBehaviorAsync",
                        OperationValue = programParams.InstallBehavior
                    });
            }
        }

        private async Task ConfigureRequirementsAsync(
            AllAppsUtils allAppsUtils,
            Dictionary<string, string> parameters,
            WindowsRegressionRequirementsParameters reqParams)
        {
            if (!string.IsNullOrWhiteSpace(reqParams.ArchitectureType))
            {
                await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Set OS architecture",
                    new ControlInfo
                    {
                        ControlType = "SetOperationSystemArchitectureAsync",
                        Value = new List<string> { reqParams.ArchitectureType }
                    });
            }

            if (!string.IsNullOrWhiteSpace(reqParams.MinimumOperatingSystem))
            {
                await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Set minimum OS",
                    new ControlInfo
                    {
                        ControlType = "SetMinimumOperationSystemAsync",
                        Value = new List<string> { reqParams.MinimumOperatingSystem }
                    });
            }
        }

        private async Task ConfigureDetectionRulesAsync(
            AllAppsUtils allAppsUtils,
            Dictionary<string, string> parameters,
            WindowsRegressionDetectionRulesParameters detectionParams)
        {
            if (!string.IsNullOrWhiteSpace(detectionParams.RulesFormat))
            {
                await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    $"Set rules format: {detectionParams.RulesFormat}",
                    new ControlInfo
                    {
                        ControlType = "SetRulesFormatAsync",
                        OperationValue = detectionParams.RulesFormat
                    });
            }

            if (string.Equals(detectionParams.RulesFormat, "Use a custom detection script", StringComparison.OrdinalIgnoreCase))
            {
                if (!string.IsNullOrWhiteSpace(detectionParams.ScriptFile))
                {
                    await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        $"Upload detection script: {detectionParams.ScriptFile}",
                        new ControlInfo
                        {
                            ControlType = "SelectScriptFileAsync",
                            OperationValue = detectionParams.ScriptFile
                        });
                }

                if (!string.IsNullOrWhiteSpace(detectionParams.RunScriptAs32BitProcessOn64BitClients))
                {
                    await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        "Set run script as 32-bit",
                        new ControlInfo
                        {
                            ControlType = "SetRunScriptAs32BitProcessOn64BitClientsAsync",
                            OperationValue = detectionParams.RunScriptAs32BitProcessOn64BitClients
                        });
                }

                if (!string.IsNullOrWhiteSpace(detectionParams.EnforceScriptSignatureCheckAndRunScriptSilently))
                {
                    await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        "Set enforce script signature check",
                        new ControlInfo
                        {
                            ControlType = "SetEnforceScriptSignatureCheckAndRunScriptSilentlyAsync",
                            OperationValue = detectionParams.EnforceScriptSignatureCheckAndRunScriptSilently
                        });
                }
            }
            else if (string.Equals(detectionParams.RuleType, "MSI", StringComparison.OrdinalIgnoreCase))
            {
                if (!string.IsNullOrWhiteSpace(detectionParams.RuleType))
                {
                    await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        "Set rule type to MSI",
                        new ControlInfo
                        {
                            ControlType = "SetRuleTypeValueAsync",
                            OperationValue = detectionParams.RuleType
                        });
                }
            }
        }

        private async Task<Dictionary<string, string>> ConfigureAssignmentsAsync(
            InterfaceUtils utils,
            Dictionary<string, string> parameters,
            WindowsRegressionTestCase testData,
            AssignmentMode assignmentMode)
        {
            switch (assignmentMode)
            {
                case AssignmentMode.Required:
                    parameters = await ExecuteStepAsync(
                        utils,
                        parameters,
                        $"Assign required group {testData.Parameters.Assignments.SelectGroups}",
                        new ControlInfo
                        {
                            ControlType = "ClickRequiredAddGroupAsync",
                            OperationValue = testData.Parameters.Assignments.SelectGroups
                        });
                    break;

                case AssignmentMode.Available:
                    parameters = await ExecuteStepAsync(
                        utils,
                        parameters,
                        $"Assign available group {testData.Parameters.Assignments.SelectGroups}",
                        new ControlInfo
                        {
                            ControlType = "ClickAvailableForEnrolledDevicesAddGroupAsync",
                            OperationValue = testData.Parameters.Assignments.SelectGroups
                        });
                    break;

                case AssignmentMode.Uninstall:
                    parameters = await ExecuteStepAsync(
                        utils,
                        parameters,
                        $"Assign uninstall group {testData.Parameters.Assignments.SelectGroups}",
                        new ControlInfo
                        {
                            ControlType = "ClickUninstallAddGroupAsync",
                            OperationValue = testData.Parameters.Assignments.SelectGroups
                        });
                    break;
            }

            return parameters;
        }

        private async Task<Dictionary<string, string>> VerifyAssignmentsAsync(
            InterfaceUtils utils,
            Dictionary<string, string> parameters,
            WindowsRegressionTestCase testData,
            AssignmentMode assignmentMode)
        {
            var assignmentBehavior = GetAssignmentBehaviorName(assignmentMode);

            parameters = await ExecuteStepAsync(
                utils,
                parameters,
                $"Verify {assignmentBehavior.ToLowerInvariant()} assignment group",
                new ControlInfo
                {
                    ControlType = "VerifyPropertyAssignmentsAsync",
                    Value = new List<string> { assignmentBehavior, testData.Parameters.Assignments.SelectGroups }
                });

            return parameters;
        }

        private async Task<Dictionary<string, string>> ExecuteStepAsync(
            InterfaceUtils utils,
            Dictionary<string, string> parameters,
            string stepDescription,
            ControlInfo controlInfo)
        {
            _test?.Info(stepDescription);
            controlInfo.Parameter = parameters;
            var result = await utils.RunStepAsync(controlInfo);
            return result.Parameter;
        }

        private async Task TryCleanupCreatedAppAsync(string createdAppName)
        {
            try
            {
                var environment = ResolveEnvironment(Page.Url);
                var allAppsUtils = new AllAppsUtils(Page, environment);
                var parameters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    ["AppAutomationAppName"] = createdAppName
                };

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    $"Cleanup: navigate to All Apps",
                    new ControlInfo { ControlType = "GoToMainPageAsync" });

                await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    $"Cleanup: delete app {createdAppName}",
                    new ControlInfo { ControlType = "DeleteAppByNameAsync" });
            }
            catch (Exception cleanupEx)
            {
                _test?.Warning($"Cleanup failed for app '{createdAppName}': {cleanupEx.Message}");
                Console.WriteLine($"Cleanup failed: {cleanupEx.Message}");
            }
        }

        private static string ResolveEnvironment(string currentUrl)
        {
            if (currentUrl.Contains("intuneSH", StringComparison.OrdinalIgnoreCase) ||
                currentUrl.Contains("/sh/", StringComparison.OrdinalIgnoreCase))
            {
                return "SH";
            }

            if (currentUrl.Contains("intuneCanary", StringComparison.OrdinalIgnoreCase) ||
                currentUrl.Contains("canary", StringComparison.OrdinalIgnoreCase))
            {
                return "CTIP";
            }

            return "PE";
        }

        private static void ValidateTestData(WindowsRegressionTestCase testData)
        {
            if (string.IsNullOrWhiteSpace(testData.Parameters.AppType))
            {
                throw new InvalidOperationException($"AppType is missing for test case {testData.TestCaseId}.");
            }

            if (string.IsNullOrWhiteSpace(testData.Parameters.Name))
            {
                throw new InvalidOperationException($"Name is missing for test case {testData.TestCaseId}.");
            }

            if (string.IsNullOrWhiteSpace(testData.Parameters.Assignments.SelectGroups))
            {
                throw new InvalidOperationException($"Assignment groups not configured for test case {testData.TestCaseId}.");
            }
        }

        private static AssignmentMode GetAssignmentMode(WindowsRegressionAssignments assignments)
        {
            var modes = new List<AssignmentMode>();

            if (!string.IsNullOrWhiteSpace(assignments.Required))
            {
                modes.Add(AssignmentMode.Required);
            }

            if (!string.IsNullOrWhiteSpace(assignments.AvailableForEnrolledDevices))
            {
                modes.Add(AssignmentMode.Available);
            }

            if (!string.IsNullOrWhiteSpace(assignments.Uninstall))
            {
                modes.Add(AssignmentMode.Uninstall);
            }

            if (modes.Count == 0)
            {
                throw new InvalidOperationException("No assignment mode configured.");
            }

            // Return the first configured mode (some tests configure multiple, e.g. Required + Uninstall)
            return modes[0];
        }

        private static string GetAssignmentBehaviorName(AssignmentMode assignmentMode)
        {
            return assignmentMode switch
            {
                AssignmentMode.Required => "Required",
                AssignmentMode.Available => "Available for enrolled devices",
                AssignmentMode.Uninstall => "Uninstall",
                _ => throw new ArgumentOutOfRangeException(nameof(assignmentMode), assignmentMode, null)
            };
        }

        private WindowsRegressionTestCase LoadTestData()
        {
            return LoadTestData(RegressionTestCaseId, RegressionTestName);
        }

        private static WindowsRegressionTestCase LoadTestData(string regressionTestCaseId, string regressionTestName)
        {
            var regressionDataPath = Path.GetFullPath(
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "..",
                    "..",
                    "..",
                    "..",
                    "TestData_AppReggersion",
                    "windows Regression.json"));

            if (!File.Exists(regressionDataPath))
            {
                throw new FileNotFoundException($"Windows regression data file was not found at {regressionDataPath}.");
            }

            var json = File.ReadAllText(regressionDataPath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var root = JsonSerializer.Deserialize<WindowsRegressionRoot>(json, options) ?? new WindowsRegressionRoot();

            var testCase = root.TestCases.FirstOrDefault(test =>
                string.Equals(test.TestCaseId, regressionTestCaseId, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(test.TestName, regressionTestName, StringComparison.OrdinalIgnoreCase));

            if (testCase is null)
            {
                throw new InvalidOperationException($"Unable to find test case {regressionTestCaseId} / {regressionTestName} in windows Regression.json.");
            }

            return testCase;
        }

        #region Test Data Models

        private enum AssignmentMode
        {
            Required,
            Available,
            Uninstall
        }

        private sealed class WindowsRegressionRoot
        {
            [JsonPropertyName("testCases")]
            public WindowsRegressionTestCase[] TestCases { get; set; } = Array.Empty<WindowsRegressionTestCase>();
        }

        private sealed class WindowsRegressionTestCase
        {
            [JsonPropertyName("testCaseId")]
            public string TestCaseId { get; set; } = string.Empty;

            [JsonPropertyName("testName")]
            public string TestName { get; set; } = string.Empty;

            [JsonPropertyName("description")]
            public string Description { get; set; } = string.Empty;

            [JsonPropertyName("category")]
            public string Category { get; set; } = string.Empty;

            [JsonPropertyName("priority")]
            public string Priority { get; set; } = string.Empty;

            [JsonPropertyName("enabled")]
            public bool Enabled { get; set; }

            [JsonPropertyName("parameters")]
            public WindowsRegressionParameters Parameters { get; set; } = new();
        }

        private sealed class WindowsRegressionParameters
        {
            [JsonPropertyName("firstLink")]
            public string FirstLink { get; set; } = string.Empty;

            [JsonPropertyName("secondLink")]
            public string SecondLink { get; set; } = string.Empty;

            [JsonPropertyName("AppType")]
            public string AppType { get; set; } = string.Empty;

            [JsonPropertyName("select app package file")]
            public string SelectAppPackageFile { get; set; } = string.Empty;

            [JsonPropertyName("Type of app install file")]
            public string TypeOfAppInstallFile { get; set; } = string.Empty;

            [JsonPropertyName("Name")]
            public string Name { get; set; } = string.Empty;

            [JsonPropertyName("Description")]
            public string Description { get; set; } = string.Empty;

            [JsonPropertyName("Publisher")]
            public string Publisher { get; set; } = string.Empty;

            [JsonPropertyName("program parameters")]
            public WindowsRegressionProgramParameters? ProgramParameters { get; set; }

            [JsonPropertyName("Requirements parameters")]
            public WindowsRegressionRequirementsParameters? RequirementsParameters { get; set; }

            [JsonPropertyName("Detection rules parameters")]
            public WindowsRegressionDetectionRulesParameters? DetectionRulesParameters { get; set; }

            [JsonPropertyName("Dependencies")]
            public string Dependencies { get; set; } = string.Empty;

            [JsonPropertyName("Supercedence")]
            public string Supercedence { get; set; } = string.Empty;

            [JsonPropertyName("Assignments")]
            public WindowsRegressionAssignments Assignments { get; set; } = new();

            [JsonPropertyName("Device Validation")]
            public WindowsRegressionDeviceValidation? DeviceValidation { get; set; }
        }

        private sealed class WindowsRegressionProgramParameters
        {
            [JsonPropertyName("Installation time required (mins)")]
            public string InstallationTimeRequired { get; set; } = string.Empty;

            [JsonPropertyName("Install behavior.")]
            public string InstallBehavior { get; set; } = string.Empty;
        }

        private sealed class WindowsRegressionRequirementsParameters
        {
            [JsonPropertyName("check operating system architecture")]
            public string CheckOperatingSystemArchitecture { get; set; } = string.Empty;

            [JsonPropertyName("architecture type")]
            public string ArchitectureType { get; set; } = string.Empty;

            [JsonPropertyName("Minimum operating system")]
            public string MinimumOperatingSystem { get; set; } = string.Empty;
        }

        private sealed class WindowsRegressionDetectionRulesParameters
        {
            [JsonPropertyName("Rule type")]
            public string RuleType { get; set; } = string.Empty;

            [JsonPropertyName("Rules format")]
            public string RulesFormat { get; set; } = string.Empty;

            [JsonPropertyName("Script file")]
            public string ScriptFile { get; set; } = string.Empty;

            [JsonPropertyName("Run script as 32 bit process on 64 bit clients")]
            public string RunScriptAs32BitProcessOn64BitClients { get; set; } = string.Empty;

            [JsonPropertyName("Enforce script signature check and run script silently")]
            public string EnforceScriptSignatureCheckAndRunScriptSilently { get; set; } = string.Empty;
        }

        private sealed class WindowsRegressionAssignments
        {
            [JsonPropertyName("Required")]
            public string Required { get; set; } = string.Empty;

            [JsonPropertyName("Available for enrolled devices")]
            public string AvailableForEnrolledDevices { get; set; } = string.Empty;

            [JsonPropertyName("uninstall")]
            public string Uninstall { get; set; } = string.Empty;

            [JsonPropertyName("select groups")]
            public string SelectGroups { get; set; } = string.Empty;
        }

        private sealed class WindowsRegressionDeviceValidation
        {
            [JsonPropertyName("pre-requisite on device")]
            public string PreRequisiteOnDevice { get; set; } = string.Empty;

            [JsonPropertyName("App Installation Validation")]
            public string AppInstallationValidation { get; set; } = string.Empty;

            [JsonPropertyName("searchTerm")]
            public string SearchTerm { get; set; } = string.Empty;

            [JsonPropertyName("expectedValue")]
            public string ExpectedValue { get; set; } = string.Empty;
        }

        #endregion
    }
}