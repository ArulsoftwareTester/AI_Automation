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
using global::PlaywrightTests.Common.Utils;

namespace IntuneCanaryTests
{
    public abstract class AndroidRegressionTestSetABase : PageTest
    {
        private ExtentTest? _test;
        private SmartStepExecutor? _smartStep;

        protected abstract string RegressionTestCaseId { get; }
        protected abstract string RegressionTestName { get; }
        protected abstract string TestDisplayName { get; }
        protected abstract string TestDescription { get; }
        protected abstract string DataFileName { get; }

        private string NumericTestId => RegressionTestCaseId.Replace("TC_", string.Empty, StringComparison.OrdinalIgnoreCase);

        public override BrowserNewContextOptions ContextOptions()
        {
            var certPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "auth-cert", "AIAutoPE_3.pfx"));
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
            _test.Info("Category: Test set A - Android Regression Test");
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
                    throw new InvalidOperationException($"Test case {RegressionTestCaseId} is disabled in {DataFileName}.");

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
                    _test?.Info("Login completed", MediaEntityBuilder.CreateScreenCaptureFromPath(loginScreenshot).Build());

                var environment = ResolveEnvironment(Page.Url);
                var allAppsUtils = new AllAppsUtils(Page, environment);
                _smartStep = new SmartStepExecutor(allAppsUtils, testData.Parameters.AppType);
                var parameters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                // Step 2: Navigate to All Apps
                _test?.Info("Step 2: Navigating to Apps > All Apps");
                parameters = await ExecuteStepAsync(allAppsUtils, parameters, "Navigate to All Apps",
                    new ControlInfo { ControlType = "GoToMainPageAsync" });

                // Step 3: Click Add button
                _test?.Info("Step 3: Clicking Add button");
                parameters = await ExecuteStepAsync(allAppsUtils, parameters, "Click Add button",
                    new ControlInfo { ControlType = "ClickAddButtonAsync" });

                // Step 4: Select app type
                _test?.Info($"Step 4: Selecting app type: {testData.Parameters.AppType}");
                var selectAppTypeUtils = new SelectAppTypeUtils(Page, environment);
                await selectAppTypeUtils.SelectAppTypeAsync(testData.Parameters.AppType);
                _test?.Pass($"App type '{testData.Parameters.AppType}' selected");

                // Step 5: Handle app-type-specific setup
                if (!string.IsNullOrWhiteSpace(testData.Parameters.SelectAppPackageFile))
                {
                    _test?.Info($"Step 5: Selecting app package file: {testData.Parameters.SelectAppPackageFile}");
                    parameters = await ExecuteStepAsync(allAppsUtils, parameters, "Select app package file",
                        new ControlInfo { ControlType = "ClickSelectAppPackageFileButtonAsync" });
                    var uploadUtils = new UploadFileUtils(Page, environment);
                    await uploadUtils.UploadFileAsync(testData.Parameters.SelectAppPackageFile);
                    _test?.Pass($"App package file '{testData.Parameters.SelectAppPackageFile}' uploaded");
                    await allAppsUtils.ClickOKBtnAsync();
                    _test?.Pass("Upload confirmed with OK");
                }
                else if (!string.IsNullOrWhiteSpace(testData.Parameters.AppstoreURL))
                {
                    _test?.Info($"Step 5: Setting Appstore URL: {testData.Parameters.AppstoreURL}");
                    parameters = await ExecuteStepAsync(allAppsUtils, parameters, "Set Appstore URL",
                        new ControlInfo { ControlType = "SetAppInformationAppstoreUrlAsync", OperationValue = testData.Parameters.AppstoreURL });
                }
                else if (!string.IsNullOrWhiteSpace(testData.Parameters.AppURL))
                {
                    _test?.Info($"Step 5: Setting App URL: {testData.Parameters.AppURL}");
                    parameters = await ExecuteStepAsync(allAppsUtils, parameters, "Set App URL",
                        new ControlInfo { ControlType = "SetAppInformationAppURLAsync", OperationValue = testData.Parameters.AppURL });
                }

                // Step 6: Set app name
                _test?.Info($"Step 6: Setting app name: {testData.Parameters.Name}");
                parameters = await ExecuteStepAsync(allAppsUtils, parameters, "Set app name",
                    new ControlInfo { ControlType = "SetAppInformationNameAsync", OperationValue = testData.Parameters.Name });
                createdAppName = parameters.TryGetValue("AppAutomationAppName", out var uniqueName) ? uniqueName : testData.Parameters.Name;

                // Step 7: Set app description
                _test?.Info($"Step 7: Setting app description: {testData.Parameters.Description}");
                parameters = await ExecuteStepAsync(allAppsUtils, parameters, "Set app description",
                    new ControlInfo { ControlType = "SetAppInformationDescriptionAsync", OperationValue = testData.Parameters.Description });

                // Step 8: Set publisher
                _test?.Info($"Step 8: Setting publisher: {testData.Parameters.Publisher}");
                parameters = await ExecuteStepAsync(allAppsUtils, parameters, "Set publisher",
                    new ControlInfo { ControlType = "SetAppInformationPublisherAsync", OperationValue = testData.Parameters.Publisher });

                // Step 9: Set Minimum OS (Android-specific)
                if (!string.IsNullOrWhiteSpace(testData.Parameters.MinimumOperatingSystem))
                {
                    _test?.Info($"Step 9: Setting minimum OS: {testData.Parameters.MinimumOperatingSystem}");
                    parameters = await ExecuteStepAsync(allAppsUtils, parameters, "Set minimum OS",
                        new ControlInfo { ControlType = "SetMinimumOperationSystemAsync", Value = new List<string> { testData.Parameters.MinimumOperatingSystem } });
                }

                // Click Next
                parameters = await ExecuteStepAsync(allAppsUtils, parameters, "Click Next",
                    new ControlInfo { ControlType = "ClickNextButtonAsync" });

                // Step 10: Configure assignments
                _test?.Info("Step 10: Configuring assignments");
                parameters = await ConfigureAssignmentsAsync(allAppsUtils, parameters, testData, assignmentMode);
                parameters = await ExecuteStepAsync(allAppsUtils, parameters, "Mark assignments complete",
                    new ControlInfo { ControlType = "MarkAssignmentsCompleteAsync" });

                // Click Next to Review + Create
                parameters = await ExecuteStepAsync(allAppsUtils, parameters, "Click Next to Review + Create",
                    new ControlInfo { ControlType = "ClickNextButtonAsync" });

                // Step 11: Create the app (store app uses "created successfully" notification)
                _test?.Info("Step 11: Creating the app");
                parameters = await ExecuteStepAsync(allAppsUtils, parameters, "Click Create",
                    new ControlInfo { ControlType = "ClickCreateButtonWithoutWaitForUploadAsync" });
                await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

                var createScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, $"Create_Complete_{NumericTestId}");
                if (!string.IsNullOrEmpty(createScreenshot))
                    _test?.Info("App created", MediaEntityBuilder.CreateScreenCaptureFromPath(createScreenshot).Build());

                // Step 12: Verify
                _test?.Info("Step 12: Verifying created app properties");
                parameters = await ExecuteStepAsync(allAppsUtils, parameters, "Verify app name",
                    new ControlInfo { ControlType = "VerifyPropertyAsync", Value = new List<string> { "Name", createdAppName } });
                parameters = await VerifyAssignmentsAsync(allAppsUtils, parameters, testData, assignmentMode);
                parameters = await ExecuteStepAsync(allAppsUtils, parameters, "Mark verification complete",
                    new ControlInfo { ControlType = "SuccessAppAutomationVerifyResult" });

                _test?.Info(testData.Parameters.DeviceValidation?.AppInstallationValidation ?? TestDescription);
                _test?.Pass("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, $"Error_{NumericTestId}");
                if (!string.IsNullOrEmpty(errorScreenshot))
                    _test?.Fail($"Test failed: {ex.Message}", MediaEntityBuilder.CreateScreenCaptureFromPath(errorScreenshot).Build());
                else
                    _test?.Fail($"Test failed: {ex.Message}");
                throw;
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(createdAppName))
                    await TryCleanupCreatedAppAsync(createdAppName);
            }
        }

        private async Task<Dictionary<string, string>> ConfigureAssignmentsAsync(InterfaceUtils utils, Dictionary<string, string> parameters, AndroidRegressionTestCase testData, AssignmentMode mode)
        {
            var controlType = mode switch
            {
                AssignmentMode.Required => "ClickRequiredAddGroupAsync",
                AssignmentMode.Available => "ClickAvailableForEnrolledDevicesAddGroupAsync",
                AssignmentMode.Uninstall => "ClickUninstallAddGroupAsync",
                _ => throw new ArgumentOutOfRangeException(nameof(mode))
            };
            return await ExecuteStepAsync(utils, parameters, $"Assign {mode} group {testData.Parameters.Assignments.SelectGroups}",
                new ControlInfo { ControlType = controlType, OperationValue = testData.Parameters.Assignments.SelectGroups });
        }

        private async Task<Dictionary<string, string>> VerifyAssignmentsAsync(InterfaceUtils utils, Dictionary<string, string> parameters, AndroidRegressionTestCase testData, AssignmentMode mode)
        {
            var behavior = GetAssignmentBehaviorName(mode);
            return await ExecuteStepAsync(utils, parameters, $"Verify {behavior.ToLowerInvariant()} assignment",
                new ControlInfo { ControlType = "VerifyPropertyAssignmentsAsync", Value = new List<string> { behavior, testData.Parameters.Assignments.SelectGroups } });
        }

        private async Task<Dictionary<string, string>> ExecuteStepAsync(InterfaceUtils utils, Dictionary<string, string> parameters, string desc, ControlInfo info)
        {
            _test?.Info(desc);
            info.Parameter = parameters;

            // Use SmartStepExecutor when available (tab pre-check & retry on wrong-page)
            if (_smartStep != null && utils is AllAppsUtils)
            {
                var smartResult = await _smartStep.ExecuteWithGuardsAsync(info);
                return smartResult.Parameter;
            }

            var result = await utils.RunStepAsync(info);
            return result.Parameter;
        }

        private async Task TryCleanupCreatedAppAsync(string createdAppName)
        {
            try
            {
                var env = ResolveEnvironment(Page.Url);
                var allAppsUtils = new AllAppsUtils(Page, env);
                var p = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) { ["AppAutomationAppName"] = createdAppName };
                p = await ExecuteStepAsync(allAppsUtils, p, "Cleanup: navigate to All Apps", new ControlInfo { ControlType = "GoToMainPageAsync" });
                await ExecuteStepAsync(allAppsUtils, p, $"Cleanup: delete app {createdAppName}", new ControlInfo { ControlType = "DeleteAppByNameAsync" });
            }
            catch (Exception ex)
            {
                _test?.Warning($"Cleanup failed for app '{createdAppName}': {ex.Message}");
            }
        }

        private static string ResolveEnvironment(string url)
        {
            if (url.Contains("intuneSH", StringComparison.OrdinalIgnoreCase) || url.Contains("/sh/", StringComparison.OrdinalIgnoreCase)) return "SH";
            if (url.Contains("intuneCanary", StringComparison.OrdinalIgnoreCase) || url.Contains("canary", StringComparison.OrdinalIgnoreCase)) return "CTIP";
            return "PE";
        }

        private static void ValidateTestData(AndroidRegressionTestCase testData)
        {
            if (string.IsNullOrWhiteSpace(testData.Parameters.AppType)) throw new InvalidOperationException($"AppType missing for {testData.TestCaseId}.");
            if (string.IsNullOrWhiteSpace(testData.Parameters.Name)) throw new InvalidOperationException($"Name missing for {testData.TestCaseId}.");
            if (string.IsNullOrWhiteSpace(testData.Parameters.Assignments.SelectGroups)) throw new InvalidOperationException($"Assignment groups not configured for {testData.TestCaseId}.");
        }

        private static AssignmentMode GetAssignmentMode(AndroidRegressionAssignments a)
        {
            if (!string.IsNullOrWhiteSpace(a.Required)) return AssignmentMode.Required;
            if (!string.IsNullOrWhiteSpace(a.AvailableForEnrolledDevices)) return AssignmentMode.Available;
            if (!string.IsNullOrWhiteSpace(a.Uninstall)) return AssignmentMode.Uninstall;
            throw new InvalidOperationException("No assignment mode configured.");
        }

        private static string GetAssignmentBehaviorName(AssignmentMode m) => m switch
        {
            AssignmentMode.Required => "Required",
            AssignmentMode.Available => "Available for enrolled devices",
            AssignmentMode.Uninstall => "Uninstall",
            _ => throw new ArgumentOutOfRangeException(nameof(m))
        };

        private AndroidRegressionTestCase LoadTestData()
        {
            var path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "TestData_AppReggersion", DataFileName));
            if (!File.Exists(path)) throw new FileNotFoundException($"Android data file not found at {path}.");
            var json = File.ReadAllText(path);
            var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var root = JsonSerializer.Deserialize<AndroidRegressionRoot>(json, opts) ?? new AndroidRegressionRoot();
            return root.TestCases.FirstOrDefault(t => string.Equals(t.TestCaseId, RegressionTestCaseId, StringComparison.OrdinalIgnoreCase)
                && string.Equals(t.TestName, RegressionTestName, StringComparison.OrdinalIgnoreCase))
                ?? throw new InvalidOperationException($"Test case {RegressionTestCaseId} / {RegressionTestName} not found in {DataFileName}.");
        }

        #region Models
        private enum AssignmentMode { Required, Available, Uninstall }
        private sealed class AndroidRegressionRoot { [JsonPropertyName("testCases")] public AndroidRegressionTestCase[] TestCases { get; set; } = Array.Empty<AndroidRegressionTestCase>(); }
        private sealed class AndroidRegressionTestCase { [JsonPropertyName("testCaseId")] public string TestCaseId { get; set; } = ""; [JsonPropertyName("testName")] public string TestName { get; set; } = ""; [JsonPropertyName("description")] public string Description { get; set; } = ""; [JsonPropertyName("category")] public string Category { get; set; } = ""; [JsonPropertyName("priority")] public string Priority { get; set; } = ""; [JsonPropertyName("enabled")] public bool Enabled { get; set; } [JsonPropertyName("parameters")] public AndroidRegressionParameters Parameters { get; set; } = new(); }
        private sealed class AndroidRegressionParameters { [JsonPropertyName("firstLink")] public string FirstLink { get; set; } = ""; [JsonPropertyName("secondLink")] public string SecondLink { get; set; } = ""; [JsonPropertyName("AppType")] public string AppType { get; set; } = ""; [JsonPropertyName("select app package file")] public string SelectAppPackageFile { get; set; } = ""; [JsonPropertyName("Name")] public string Name { get; set; } = ""; [JsonPropertyName("Description")] public string Description { get; set; } = ""; [JsonPropertyName("Publisher")] public string Publisher { get; set; } = ""; [JsonPropertyName("Appstore URL")] public string AppstoreURL { get; set; } = ""; [JsonPropertyName("App URL")] public string AppURL { get; set; } = ""; [JsonPropertyName("Minimum operating system")] public string MinimumOperatingSystem { get; set; } = ""; [JsonPropertyName("Targeted platform")] public string TargetedPlatform { get; set; } = ""; [JsonPropertyName("Require a managed browser to open this link")] public string RequireManagedBrowser { get; set; } = ""; [JsonPropertyName("Assignments")] public AndroidRegressionAssignments Assignments { get; set; } = new(); [JsonPropertyName("Device Validation")] public AndroidRegressionDeviceValidation? DeviceValidation { get; set; } }
        private sealed class AndroidRegressionAssignments { [JsonPropertyName("Required")] public string Required { get; set; } = ""; [JsonPropertyName("Available for enrolled devices")] public string AvailableForEnrolledDevices { get; set; } = ""; [JsonPropertyName("uninstall")] public string Uninstall { get; set; } = ""; [JsonPropertyName("select groups")] public string SelectGroups { get; set; } = ""; }
        private sealed class AndroidRegressionDeviceValidation { [JsonPropertyName("pre-requisite on device")] public string PreRequisiteOnDevice { get; set; } = ""; [JsonPropertyName("App Installation Validation")] public string AppInstallationValidation { get; set; } = ""; [JsonPropertyName("searchTerm")] public string SearchTerm { get; set; } = ""; [JsonPropertyName("expectedValue")] public string ExpectedValue { get; set; } = ""; }
        #endregion
    }
}
