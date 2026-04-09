using LogService;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using global::PlaywrightTests.Common.Model;
using global::PlaywrightTests.Common.Utils.BaseUtils.Apps.ByPlatform;
using global::PlaywrightTests.Common.Utils.BaseUtils.UtilInterface;
using global::PlaywrightTests.Common.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AventStack.ExtentReports;

namespace IntuneCanaryTests
{
    public abstract class WinGetStoreAppRegressionTestBase : PageTest
    {
        private ExtentTest? _test;

        protected abstract string RegressionTestCaseId { get; }

        protected abstract string TestDisplayName { get; }

        private string NumericTestId => RegressionTestCaseId.Replace("TC_", string.Empty, StringComparison.OrdinalIgnoreCase);

        public override BrowserNewContextOptions ContextOptions()
        {
            var certPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "auth-cert", "AIAutoPE_3.pfx"));
            Console.WriteLine($"Certificate path: {certPath}");
            return new BrowserNewContextOptions
            {
                ClientCertificates = new[] {
                    new ClientCertificate {
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
            _test.Info("Category: WinGet App Test Cases");
        }

        [TearDown]
        public void TestTearDown()
        {
            _test?.Info($"Test completed at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            // Dump self-healing report so agent and console can see what was healed
            SelfHealingLocator.DumpHealingReport();
        }

        protected async Task RunTestAsync()
        {
            string createdAppName = string.Empty;

            try
            {
                Console.WriteLine($"{GetType().Name} started...");
                _test?.Info("Test execution started");

                var testData = LoadTestData();
                var assignmentMode = GetAssignmentMode(testData.Parameters.Assignments);
                ValidateTestData(testData, assignmentMode);

                _test?.Info($"Loaded regression data for {testData.Parameters.DisplayName} and group {testData.Parameters.Assignments.SelectGroups}");

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
                var parameters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                var allAppsUtils = new AllAppsUtils(Page, environment);

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Open All Apps",
                    new ControlInfo { ControlType = "GoToMainPageAsync" });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Start app creation",
                    new ControlInfo { ControlType = "ClickAddButtonAsync" });

                await SelectAppTypeDirectlyAsync(testData.Parameters.AppType);

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    $"Select Store app {testData.Parameters.SearchApp}",
                    new ControlInfo
                    {
                        ControlType = "SelectTheMicrosoftStoreAppNewAsync",
                        OperationValue = testData.Parameters.SearchApp
                    });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Set app display name",
                    new ControlInfo
                    {
                        ControlType = "SetAppInformationNameAsync",
                        OperationValue = testData.Parameters.DisplayName
                    });

                createdAppName = parameters.TryGetValue("AppAutomationAppName", out var uniqueName)
                    ? uniqueName
                    : testData.Parameters.DisplayName;

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Set app description",
                    new ControlInfo
                    {
                        ControlType = "SetAppInformationDescriptionAsync",
                        OperationValue = testData.Parameters.Description
                    });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Set app publisher",
                    new ControlInfo
                    {
                        ControlType = "SetAppInformationPublisherAsync",
                        OperationValue = testData.Parameters.Publisher
                    });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    $"Set install behavior to {testData.Parameters.Assignments.InstallContext}",
                    new ControlInfo
                    {
                        ControlType = "SetAppInformationInstallBehaviorAsync",
                        OperationValue = testData.Parameters.Assignments.InstallContext
                    });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Continue to assignments",
                    new ControlInfo { ControlType = "ClickNextButtonAsync" });

                parameters = await ConfigureAssignmentsAsync(allAppsUtils, parameters, testData, assignmentMode);

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Mark assignment stage complete",
                    new ControlInfo { ControlType = "MarkAssignmentsCompleteAsync" });

                    parameters = await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        "Continue to review + create",
                        new ControlInfo { ControlType = "ClickNextButtonAsync" });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Create the Store app",
                    new ControlInfo { ControlType = "ClickCreateButtonWithoutWaitForUploadAsync" });

                await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
                await Page.WaitForTimeoutAsync(5000);

                var createScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, $"Create_Complete_{NumericTestId}");
                if (!string.IsNullOrEmpty(createScreenshot))
                {
                    _test?.Info("App created", MediaEntityBuilder.CreateScreenCaptureFromPath(createScreenshot).Build());
                }

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Verify created app name",
                    new ControlInfo
                    {
                        ControlType = "VerifyPropertyAsync",
                        Value = new List<string> { "Name", createdAppName }
                    });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Verify app description",
                    new ControlInfo
                    {
                        ControlType = "VerifyPropertyAsync",
                        Value = new List<string> { "Description", testData.Parameters.Description }
                    });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Verify app publisher",
                    new ControlInfo
                    {
                        ControlType = "VerifyPropertyAsync",
                        Value = new List<string> { "Publisher", testData.Parameters.Publisher }
                    });

                parameters = await VerifyAssignmentsAsync(allAppsUtils, parameters, testData, assignmentMode);

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Mark verification complete",
                    new ControlInfo { ControlType = "SuccessAppAutomationVerifyResult" });

                _test?.Info(testData.Parameters.DeviceValidation.AppInstallationValidation);
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

        private async Task<Dictionary<string, string>> ConfigureAssignmentsAsync(
            InterfaceUtils utils,
            Dictionary<string, string> parameters,
            WinGetRegressionTestCase testData,
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
            WinGetRegressionTestCase testData,
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

            parameters = await ExecuteStepAsync(
                utils,
                parameters,
                "Verify app install behavior",
                new ControlInfo
                {
                    ControlType = "VerifyPropertyAsync",
                    Value = new List<string>
                    {
                        "Install behavior",
                        testData.Parameters.Assignments.InstallContext
                    }
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
            try
            {
                var result = await utils.RunStepAsync(controlInfo);
                return result.Parameter;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[HEAL_REQUEST] step={stepDescription} controlType={controlInfo.ControlType} error={ex.Message}");
                // === LIVE DOM CAPTURE: Capture page source at moment of failure ===
                await CaptureDomAndAnalyzeAsync(stepDescription, controlInfo.ControlType, ex.Message);

                _test?.Fail($"Step failed: {stepDescription} \u2014 {ex.Message}");
                throw;
            }
        }

        private async Task SelectAppTypeDirectlyAsync(string appType)
        {
            _test?.Info($"Select app type {appType}");

            try
            {
                var appTypeComboBox = Page.GetByRole(AriaRole.Combobox, new() { Name = "App type", Exact = true });
                await appTypeComboBox.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 15000 });
                await appTypeComboBox.ClickAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[HEAL_SIGNAL] SelectAppTypeDirectlyAsync: App type combobox not found: {ex.Message}");
                // Capture live DOM and ask Gemini for correct locator
                var domHtml = await CaptureDomOnFailureAsync("SelectAppType_ComboBox");
                var hints = new HealingHints
                {
                    Identifier = "AppTypeCombobox",
                    Text = "App type",
                    Role = AriaRole.Combobox,
                    AriaLabel = "App type"
                };
                var healed = await SelfHealingLocator.ResolveAsync(Page, Page.GetByRole(AriaRole.Combobox, new() { Name = "App type" }), hints, iframeName: null, timeoutMs: 15000);
                await healed.ClickAsync();
            }

            var option = Page.GetByText(new Regex($"^{Regex.Escape(appType)}$", RegexOptions.IgnoreCase)).First;
            await option.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 30000 });
            await option.ClickAsync();

            var selectButton = Page.GetByRole(AriaRole.Button, new() { Name = "Select", Exact = true });
            await selectButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 30000 });
            await selectButton.ClickAsync();
        }

        /// <summary>
        /// Captures live DOM page source at moment of failure and saves to file.
        /// Like driver.getPageSource() in the Java Self_Healing project.
        /// </summary>
        private async Task<string> CaptureDomOnFailureAsync(string contextName)
        {
            try
            {
                string pageHtml = await Page.ContentAsync();
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var domDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "ExtentReports", "DomCaptures");
                Directory.CreateDirectory(domDir);
                var filePath = Path.Combine(domDir, $"DOM_{NumericTestId}_{contextName}_{timestamp}.html");
                await File.WriteAllTextAsync(filePath, pageHtml);
                Console.WriteLine($"[DOM_CAPTURE] Saved live DOM to: {filePath} ({pageHtml.Length} chars)");
                _test?.Info($"DOM captured at failure point: {contextName} ({pageHtml.Length} chars)");
                return pageHtml;
            }
            catch (Exception domEx)
            {
                Console.WriteLine($"[DOM_CAPTURE] Failed to capture DOM: {domEx.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Captures DOM and sends to Gemini AI to analyze what's on the page
        /// and suggest correct locators — same pattern as OpenAIHelper.GetLocatorsForPageAsJson()
        /// in the Java Self_Healing project.
        /// </summary>
        private async Task CaptureDomAndAnalyzeAsync(string stepDescription, string controlType, string errorMessage)
        {
            try
            {
                string pageHtml = await CaptureDomOnFailureAsync(controlType);
                if (string.IsNullOrEmpty(pageHtml)) return;

                // Send to Gemini AI for analysis if available
                if (AILocatorHelper.IsAvailable())
                {
                    var hints = HealingHintsRegistry.Get(controlType) ?? new HealingHints
                    {
                        Identifier = controlType,
                        Text = stepDescription
                    };

                    Console.WriteLine($"[AI_DOM_ANALYSIS] Sending DOM ({pageHtml.Length} chars) to Gemini for step '{stepDescription}'...");
                    var aiResult = await AILocatorHelper.FindLocatorAsync(pageHtml, hints);
                    if (aiResult.HasValue)
                    {
                        var (response, elapsedMs) = aiResult.Value;
                        Console.WriteLine($"[AI_DOM_ANALYSIS] Gemini found: {response.LocatorType}='{response.Locator}' in {elapsedMs}ms for step '{stepDescription}'");
                        _test?.Info($"AI DOM Analysis: Gemini suggests {response.LocatorType}='{response.Locator}' for '{controlType}'");
                    }
                    else
                    {
                        Console.WriteLine($"[AI_DOM_ANALYSIS] Gemini returned no result for step '{stepDescription}'");
                    }
                }
                else
                {
                    Console.WriteLine("[AI_DOM_ANALYSIS] GOOGLE_AI_API_KEY not set — skipping Gemini analysis");
                }
            }
            catch (Exception analysisEx)
            {
                Console.WriteLine($"[AI_DOM_ANALYSIS] Analysis failed: {analysisEx.Message}");
            }
        }

        /// <summary>
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
                    $"Cleanup created app {createdAppName}",
                    new ControlInfo { ControlType = "GoToMainPageAsync" });

                await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    $"Delete app {createdAppName}",
                    new ControlInfo { ControlType = "DeleteAppByNameAsync" });
            }
            catch (Exception cleanupEx)
            {
                _test?.Warning($"Cleanup failed for app '{createdAppName}': {cleanupEx.Message}");
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

        private static WinGetRegressionTestCase LoadTestData(string? regressionTestCaseId = null)
        {
            var testDataPath = Path.GetFullPath(Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "..",
                "..",
                "..",
                "..",
                "TestData_AppReggersion",
                "WinGet_StoreApp_Regression.json"));

            using var stream = File.OpenRead(testDataPath);
            var root = JsonSerializer.Deserialize<WinGetRegressionRoot>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var targetTestCaseId = regressionTestCaseId ?? throw new InvalidOperationException("Regression test case id is required.");

            return root?.TestCases?.FirstOrDefault(testCase =>
                       string.Equals(testCase.TestCaseId, targetTestCaseId, StringComparison.OrdinalIgnoreCase))
                   ?? throw new InvalidOperationException($"Test case {targetTestCaseId} was not found in {testDataPath}.");
        }

        private WinGetRegressionTestCase LoadTestData()
        {
            return LoadTestData(RegressionTestCaseId);
        }

        private static void ValidateTestData(WinGetRegressionTestCase testData, AssignmentMode assignmentMode)
        {
            if (!testData.Enabled)
            {
                throw new InvalidOperationException($"Test case {testData.TestCaseId} is disabled in regression data.");
            }

            if (!string.Equals(testData.Parameters.AppType, "Microsoft store app (new)", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Unexpected app type '{testData.Parameters.AppType}' for {testData.TestCaseId}.");
            }

            if (string.IsNullOrWhiteSpace(testData.Parameters.SearchApp) ||
                string.IsNullOrWhiteSpace(testData.Parameters.DisplayName) ||
                string.IsNullOrWhiteSpace(testData.Parameters.Description) ||
                string.IsNullOrWhiteSpace(testData.Parameters.Publisher) ||
                string.IsNullOrWhiteSpace(testData.Parameters.Assignments.InstallContext) ||
                string.IsNullOrWhiteSpace(testData.Parameters.Assignments.SelectGroups))
            {
                throw new InvalidOperationException($"Test data is incomplete for {testData.TestCaseId}.");
            }
        }

        private static AssignmentMode GetAssignmentMode(WinGetRegressionAssignments assignments)
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

            return modes.Count switch
            {
                1 => modes[0],
                0 => throw new InvalidOperationException($"No assignment mode configured for {assignments.SelectGroups}."),
                _ => throw new InvalidOperationException($"Multiple assignment modes are configured for {assignments.SelectGroups}.")
            };
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

        private enum AssignmentMode
        {
            Required,
            Available,
            Uninstall
        }

        private sealed class WinGetRegressionRoot
        {
            [JsonPropertyName("testCases")]
            public List<WinGetRegressionTestCase> TestCases { get; set; } = new();
        }

        protected sealed class WinGetRegressionTestCase
        {
            [JsonPropertyName("testCaseId")]
            public string TestCaseId { get; set; } = string.Empty;

            [JsonPropertyName("enabled")]
            public bool Enabled { get; set; }

            [JsonPropertyName("parameters")]
            public WinGetRegressionParameters Parameters { get; set; } = new();
        }

        protected sealed class WinGetRegressionParameters
        {
            [JsonPropertyName("AppType")]
            public string AppType { get; set; } = string.Empty;

            [JsonPropertyName("search app")]
            public string SearchApp { get; set; } = string.Empty;

            [JsonPropertyName("displayName")]
            public string DisplayName { get; set; } = string.Empty;

            [JsonPropertyName("description")]
            public string Description { get; set; } = string.Empty;

            [JsonPropertyName("publisher")]
            public string Publisher { get; set; } = string.Empty;

            [JsonPropertyName("Assignments")]
            public WinGetRegressionAssignments Assignments { get; set; } = new();

            [JsonPropertyName("Device Validation")]
            public WinGetRegressionDeviceValidation DeviceValidation { get; set; } = new();
        }

        protected sealed class WinGetRegressionAssignments
        {
            [JsonPropertyName("Required")]
            public string Required { get; set; } = string.Empty;

            [JsonPropertyName("Available for enrolled devices")]
            public string AvailableForEnrolledDevices { get; set; } = string.Empty;

            [JsonPropertyName("uninstall")]
            public string Uninstall { get; set; } = string.Empty;

            [JsonPropertyName("select groups")]
            public string SelectGroups { get; set; } = string.Empty;

            [JsonPropertyName("Install context")]
            public string InstallContext { get; set; } = string.Empty;
        }

        protected sealed class WinGetRegressionDeviceValidation
        {
            [JsonPropertyName("App Installation Validation")]
            public string AppInstallationValidation { get; set; } = string.Empty;
        }
    }
}