using AventStack.ExtentReports;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using global::PlaywrightTests.Common.Helper;
using global::PlaywrightTests.Common.Utils;
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
using System.Diagnostics;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    public abstract class NonEnUsWinClassicSanityTestBase : PageTest
    {
        private ExtentTest? _test;

        protected abstract string TestId { get; }
        protected abstract string TestTitle { get; }
        protected abstract string TestDescription { get; }

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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, TestTitle);
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info($"Test ID: {TestId}");
            _test.Info("Category: Test set A - Non-Enus WinClassic Sanity");
        }

        protected async Task RunTestAsync()
        {
            string createdAppName = string.Empty;
            string publisherName = string.Empty;

            try
            {
                Console.WriteLine($"{TestContext.CurrentContext.Test.Name} started...");
                _test?.Info("Test execution started");

                var testData = LoadTestData();
                AssignmentMode? assignmentMode = GetAssignmentMode(testData.Parameters.Assignments);
                ValidateTestData(testData);

                _test?.Info("Step 1: Logging into Intune Portal");
                var securityBaseline = new SecurityBaseline();
                await securityBaseline.Login(Page);
                _test?.Pass("Successfully logged into Intune Portal");

                var loginScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, $"Login_Complete_{TestId}");
                if (!string.IsNullOrEmpty(loginScreenshot))
                {
                    _test?.Info("Login completed", MediaEntityBuilder.CreateScreenCaptureFromPath(loginScreenshot).Build());
                }

                _test?.Info(TestDescription);

                var environment = ResolveEnvironment(Page.Url);
                var parameters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                var allAppsUtils = new AllAppsUtils(Page, environment);

                publisherName = ResolveDynamicPublisher(testData.Parameters.Publisher);
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

                await SelectAppTypeAsync(environment, parameters, testData.Parameters.AppType);

                await UploadWin32PackageAsync(environment, parameters, testData.Parameters.SelectAppPackageFile);

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Set app display name",
                    new ControlInfo
                    {
                        ControlType = "SetAppInformationNameAsync",
                        OperationValue = testData.Parameters.Name
                    });

                createdAppName = parameters.TryGetValue("AppAutomationAppName", out var uniqueName)
                    ? uniqueName
                    : testData.Parameters.Name;

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
                        OperationValue = publisherName
                    });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Continue to Program tab",
                    new ControlInfo { ControlType = "ClickNextButtonAsync" });

                await SetInstallationTimeRequiredAsync(testData.Parameters.ProgramParameters.InstallationTimeRequiredMinutes);
                await SetInstallBehaviorIfEnabledAsync(allAppsUtils, testData.Parameters.ProgramParameters.InstallBehavior);

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Continue to requirements",
                    new ControlInfo { ControlType = "ClickNextButtonAsync" });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    $"Set minimum operating system to {testData.Parameters.RequirementsParameters.MinimumOperatingSystem}",
                    new ControlInfo
                    {
                        ControlType = "SetMinimumOperationSystemAsync",
                        Value = new List<string> { testData.Parameters.RequirementsParameters.MinimumOperatingSystem }
                    });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Continue to detection rules",
                    new ControlInfo { ControlType = "ClickNextButtonAsync" });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    $"Set rules format to {testData.Parameters.DetectionRulesParameters.RulesFormat}",
                    new ControlInfo
                    {
                        ControlType = "SetRulesFormatAsync",
                        OperationValue = testData.Parameters.DetectionRulesParameters.RulesFormat
                    });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Click Add detection rule",
                    new ControlInfo { ControlType = "ClickRulesFormatAddButtonAsync" });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    $"Set rule type to {testData.Parameters.DetectionRulesParameters.RuleType}",
                    new ControlInfo
                    {
                        ControlType = "SetRuleTypeValueAsync",
                        OperationValue = testData.Parameters.DetectionRulesParameters.RuleType
                    });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Confirm detection rule",
                    new ControlInfo { ControlType = "ClickDetectionRuleOKButtonAsync" });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Continue to dependencies",
                    new ControlInfo { ControlType = "ClickNextButtonAsync" });

                if (!string.IsNullOrWhiteSpace(testData.Parameters.Dependencies))
                {
                    _test?.Info($"Adding dependency: {testData.Parameters.Dependencies}");
                    Console.WriteLine($"[Dependency] Adding dependency app: {testData.Parameters.Dependencies}");

                    parameters = await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        "Click Add dependency button",
                        new ControlInfo { ControlType = "ClickDependenciesAddButtonAsync" });

                    parameters = await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        $"Select dependency app {testData.Parameters.Dependencies}",
                        new ControlInfo
                        {
                            ControlType = "SelectDependencyAppsAsync",
                            Value = new List<string> { testData.Parameters.Dependencies, "No" }
                        });

                    Console.WriteLine($"[Dependency] Dependency added successfully: {testData.Parameters.Dependencies}");
                    _test?.Pass($"Dependency added: {testData.Parameters.Dependencies}");
                }
                else
                {
                    _test?.Info("No dependencies configured — skipping");
                }

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Continue to supersedence",
                    new ControlInfo { ControlType = "ClickNextButtonAsync" });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Continue to assignments",
                    new ControlInfo { ControlType = "ClickNextButtonAsync" });

                if (assignmentMode.HasValue)
                {
                    parameters = await ConfigureAssignmentsAsync(allAppsUtils, parameters, testData, assignmentMode.Value);

                    parameters = await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        "Mark assignment stage complete",
                        new ControlInfo { ControlType = "MarkAssignmentsCompleteAsync" });
                }
                else
                {
                    _test?.Info("No assignment mode configured (Delete test)  skipping assignment configuration");
                }

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Continue to review + create",
                    new ControlInfo { ControlType = "ClickNextButtonAsync" });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Create the Win32 app",
                    new ControlInfo { ControlType = "ClickCreateButtonWithoutWaitForUploadAsync" });

                await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
                await Page.WaitForTimeoutAsync(5000);

                var createScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, $"Create_Complete_{TestId}");
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
                        Value = new List<string> { "Publisher", publisherName }
                    });

                if (assignmentMode.HasValue)
                {
                    parameters = await VerifyAssignmentsAsync(allAppsUtils, parameters, testData, assignmentMode.Value);
                }
                else
                {
                    _test?.Info("No assignment mode  skipping assignment verification");
                }

                _test?.Info(testData.Parameters.DeviceValidation.AppInstallationValidation);

                Console.WriteLine("Test completed successfully!");
                _test?.Pass("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, $"Error_{TestId}");
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
        }

        private async Task<Dictionary<string, string>> ConfigureAssignmentsAsync(
            InterfaceUtils utils,
            Dictionary<string, string> parameters,
            NonEnUsWinClassicTestCase testData,
            AssignmentMode assignmentMode)
        {
            switch (assignmentMode)
            {
                case AssignmentMode.Required:
                    if (string.Equals(testData.Parameters.Assignments.Required, "All users", StringComparison.OrdinalIgnoreCase))
                    {
                        parameters = await ExecuteStepAsync(
                            utils,
                            parameters,
                            "Assign required all users",
                            new ControlInfo { ControlType = "ClickRequiredAddAllUsersAsync" });
                    }
                    else
                    {
                        parameters = await ExecuteStepAsync(
                            utils,
                            parameters,
                            $"Assign required group {testData.Parameters.Assignments.SelectGroups}",
                            new ControlInfo
                            {
                                ControlType = "ClickRequiredAddGroupAsync",
                                OperationValue = testData.Parameters.Assignments.SelectGroups
                            });
                    }
                    break;

                case AssignmentMode.Available:
                    if (string.Equals(testData.Parameters.Assignments.AvailableForEnrolledDevices, "All users", StringComparison.OrdinalIgnoreCase))
                    {
                        parameters = await ExecuteStepAsync(
                            utils,
                            parameters,
                            "Assign available all users",
                            new ControlInfo { ControlType = "ClickAvailableForEnrolledDevicesAllUsersAsync" });
                    }
                    else
                    {
                        parameters = await ExecuteStepAsync(
                            utils,
                            parameters,
                            $"Assign available group {testData.Parameters.Assignments.SelectGroups}",
                            new ControlInfo
                            {
                                ControlType = "ClickAvailableForEnrolledDevicesAddGroupAsync",
                                OperationValue = testData.Parameters.Assignments.SelectGroups
                            });
                    }
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

        private async Task SetInstallationTimeRequiredAsync(string minutes)
        {
            _test?.Info($"Set installation time required to {minutes} mins");

            try
            {
                var locator = await ControlHelper.GetLocatorByClassAndHasTextAsync(
                    Page,
                    "fxc-weave-pccontrol fxc-section-control fxc-base msportalfx-form-formelement fxc-has-label",
                    "Installation time required (mins)",
                    0);
                await ControlHelper.SetInputByClassAndTypeAsync(locator, "azc-input azc-formControl", "text", minutes, 0);
            }
            catch
            {
                throw new InvalidOperationException("Installation time required (mins) input was not found.");
            }
        }

        private async Task SetInstallBehaviorIfEnabledAsync(AllAppsUtils allAppsUtils, string installBehavior)
        {
            _test?.Info($"Checking if Install behavior is enabled");

            try
            {
                var optionPickerLocator = await ControlHelper.GetLocatorByClassAndHasTextAsync(
                    Page,
                    "fxc-weave-pccontrol fxc-section-control fxc-base msportalfx-form-formelement fxc-has-label",
                    "Install behavior",
                    0);

                var itemLocator = optionPickerLocator.Locator(".azc-optionPicker-item").First;
                var classAttr = await itemLocator.GetAttributeAsync("class");
                if (classAttr != null && classAttr.Contains("azc-disabled"))
                {
                    _test?.Info("Install behavior is disabled, skipping");
                    return;
                }

                await ControlHelper.ClickByClassAndHasTextAsync(optionPickerLocator, "fxs-portal-border azc-optionPicker-item", installBehavior, 0);
                _test?.Info($"Install behavior set to {installBehavior}");
            }
            catch
            {
                _test?.Info("Install behavior control not found or not interactive, skipping");
            }
        }

        private async Task<Dictionary<string, string>> VerifyAssignmentsAsync(
            InterfaceUtils utils,
            Dictionary<string, string> parameters,
            NonEnUsWinClassicTestCase testData,
            AssignmentMode assignmentMode)
        {
            var assignmentBehavior = GetAssignmentBehaviorName(assignmentMode);
            var assignmentTarget = GetAssignmentTargetName(testData.Parameters.Assignments, assignmentMode);

            parameters = await ExecuteStepAsync(
                utils,
                parameters,
                $"Verify {assignmentBehavior.ToLowerInvariant()} assignment target",
                new ControlInfo
                {
                    ControlType = "VerifyPropertyAssignmentsAsync",
                    Value = new List<string> { assignmentBehavior, assignmentTarget }
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

        private async Task SelectAppTypeAsync(string environment, Dictionary<string, string> parameters, string appType)
        {
            var selectAppTypeUtils = new SelectAppTypeUtils(Page, environment);
            await ExecuteStepAsync(
                selectAppTypeUtils,
                parameters,
                $"Select app type {appType}",
                new ControlInfo
                {
                    Operation = "SelectAppTypeAsync",
                    OperationValue = appType
                });
        }

        private async Task UploadWin32PackageAsync(string environment, Dictionary<string, string> parameters, string packageFileName)
        {
            var allAppsUtils = new AllAppsUtils(Page, ResolveEnvironment(Page.Url));
            await ExecuteStepAsync(
                allAppsUtils,
                parameters,
                "Open package picker",
                new ControlInfo { ControlType = "ClickSelectAppPackageFileButtonAsync" });

            var uploadFileUtils = new UploadFileUtils(Page, environment);
            await ExecuteStepAsync(
                uploadFileUtils,
                parameters,
                $"Upload package {packageFileName}",
                new ControlInfo
                {
                    Operation = "UploadFile",
                    OperationValue = packageFileName
                });

            await ExecuteStepAsync(
                uploadFileUtils,
                parameters,
                "Confirm uploaded package",
                new ControlInfo { Operation = "ClickOKButtonAsync" });

            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await Page.WaitForTimeoutAsync(3000);
        }

        private async Task ValidateAppInstallationOnDeviceAsync(NonEnUsWinClassicDeviceValidation deviceValidation)
        {
            if (string.IsNullOrWhiteSpace(deviceValidation.SearchTerm))
            {
                _test?.Info("No searchTerm configured — skipping device installation validation.");
                return;
            }

            _test?.Info($"Waiting 10 minutes for policy to sync to device before validating installation...");
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Starting 10-minute wait for policy sync...");

            for (int i = 1; i <= 10; i++)
            {
                await Task.Delay(TimeSpan.FromMinutes(1));
                var msg = $"[{DateTime.Now:HH:mm:ss}] Waited {i}/10 minutes...";
                Console.WriteLine(msg);
                _test?.Info(msg);
            }

            _test?.Info($"Running winget validation: winget list --name \"{deviceValidation.SearchTerm}\"");
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Executing: winget list --name \"{deviceValidation.SearchTerm}\"");

            var processInfo = new ProcessStartInfo
            {
                FileName = "winget",
                Arguments = $"list --name \"{deviceValidation.SearchTerm}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            string output;
            string error;
            try
            {
                using var process = Process.Start(processInfo);
                output = await process!.StandardOutput.ReadToEndAsync();
                error = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();
            }
            catch (Exception ex)
            {
                _test?.Warning($"Failed to run winget command: {ex.Message}");
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] winget command failed: {ex.Message}");
                return;
            }

            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] winget output:\n{output}");
            if (!string.IsNullOrWhiteSpace(error))
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] winget stderr:\n{error}");

            _test?.Info($"winget output:\n{output}");

            bool appFound = output.Contains(deviceValidation.SearchTerm, StringComparison.OrdinalIgnoreCase);
            bool expectInstalled = deviceValidation.ExpectedValue.Equals("installed", StringComparison.OrdinalIgnoreCase);

            if (expectInstalled && appFound)
            {
                _test?.Pass($"VALIDATION PASSED: \"{deviceValidation.SearchTerm}\" found in winget list — app is installed as expected.");
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] VALIDATION PASSED: App installed as expected.");
            }
            else if (!expectInstalled && !appFound)
            {
                _test?.Pass($"VALIDATION PASSED: \"{deviceValidation.SearchTerm}\" not found in winget list — app is not installed as expected.");
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] VALIDATION PASSED: App not installed as expected.");
            }
            else if (expectInstalled && !appFound)
            {
                _test?.Fail($"VALIDATION FAILED: Expected \"{deviceValidation.SearchTerm}\" to be installed, but it was NOT found in winget list.");
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] VALIDATION FAILED: App expected but not found.");
                Assert.Fail($"App \"{deviceValidation.SearchTerm}\" expected to be installed but was not found.");
            }
            else
            {
                _test?.Fail($"VALIDATION FAILED: Expected \"{deviceValidation.SearchTerm}\" to NOT be installed, but it WAS found in winget list.");
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] VALIDATION FAILED: App found but should not be installed.");
                Assert.Fail($"App \"{deviceValidation.SearchTerm}\" expected to not be installed but was found.");
            }
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

        private static string ResolveDynamicPublisher(string publisher)
        {
            return publisher.Replace("ddmmyyyymmss", DateTime.Now.ToString("ddMMyyyyHHmmss", System.Globalization.CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase);
        }

        private static NonEnUsWinClassicTestCase LoadTestData(string? testId = null)
        {
            var testDataPath = Path.GetFullPath(Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "..",
                "..",
                "..",
                "..",
                "TestData_AppReggersion",
                "Non-Enus_winclassic_santity"));

            using var stream = File.OpenRead(testDataPath);
            var root = JsonSerializer.Deserialize<NonEnUsWinClassicRoot>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var targetTestId = testId ?? throw new InvalidOperationException("Test id is required.");
            var targetCaseId = $"TC_{targetTestId}";

            return root?.TestCases?.FirstOrDefault(testCase =>
                       string.Equals(testCase.TestCaseId, targetCaseId, StringComparison.OrdinalIgnoreCase))
                   ?? throw new InvalidOperationException($"Test case {targetCaseId} was not found in {testDataPath}.");
        }

        private NonEnUsWinClassicTestCase LoadTestData()
        {
            return LoadTestData(TestId);
        }

        private static void ValidateTestData(NonEnUsWinClassicTestCase testData)
        {
            if (!testData.Enabled)
            {
                throw new InvalidOperationException($"Test case {testData.TestCaseId} is disabled in regression data.");
            }

            if (!string.Equals(testData.Category, "WinClassicApp", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Unexpected category '{testData.Category}' for {testData.TestCaseId}.");
            }

            if (!string.Equals(testData.Parameters.AppType, "Windows app (Win32)", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Unexpected app type '{testData.Parameters.AppType}' for {testData.TestCaseId}.");
            }

            if (string.IsNullOrWhiteSpace(testData.Parameters.SelectAppPackageFile) ||
                string.IsNullOrWhiteSpace(testData.Parameters.Name) ||
                string.IsNullOrWhiteSpace(testData.Parameters.Description) ||
                string.IsNullOrWhiteSpace(testData.Parameters.Publisher) ||
                string.IsNullOrWhiteSpace(testData.Parameters.ProgramParameters.InstallationTimeRequiredMinutes) ||
                string.IsNullOrWhiteSpace(testData.Parameters.ProgramParameters.InstallBehavior) ||
                string.IsNullOrWhiteSpace(testData.Parameters.RequirementsParameters.MinimumOperatingSystem) ||
                string.IsNullOrWhiteSpace(testData.Parameters.DetectionRulesParameters.RulesFormat))
            {
                throw new InvalidOperationException($"Test data is incomplete for {testData.TestCaseId}.");
            }
        }

        private static AssignmentMode? GetAssignmentMode(NonEnUsWinClassicAssignments assignments)
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
                0 => null,
                _ => throw new InvalidOperationException("Multiple assignment modes are configured in regression data.")
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

        private static string GetAssignmentTargetName(NonEnUsWinClassicAssignments assignments, AssignmentMode assignmentMode)
        {
            return assignmentMode switch
            {
                AssignmentMode.Required when string.Equals(assignments.Required, "All users", StringComparison.OrdinalIgnoreCase) => "All users",
                AssignmentMode.Required => assignments.SelectGroups,
                AssignmentMode.Available when string.Equals(assignments.AvailableForEnrolledDevices, "All users", StringComparison.OrdinalIgnoreCase) => "All users",
                AssignmentMode.Available => assignments.SelectGroups,
                AssignmentMode.Uninstall => assignments.SelectGroups,
                _ => throw new ArgumentOutOfRangeException(nameof(assignmentMode), assignmentMode, null)
            };
        }

        private enum AssignmentMode
        {
            Required,
            Available,
            Uninstall
        }

        private sealed class NonEnUsWinClassicRoot
        {
            [JsonPropertyName("testCases")]
            public List<NonEnUsWinClassicTestCase> TestCases { get; set; } = new();
        }

        protected sealed class NonEnUsWinClassicTestCase
        {
            [JsonPropertyName("testCaseId")]
            public string TestCaseId { get; set; } = string.Empty;

            [JsonPropertyName("testName")]
            public string TestName { get; set; } = string.Empty;

            [JsonPropertyName("description")]
            public string Description { get; set; } = string.Empty;

            [JsonPropertyName("category")]
            public string Category { get; set; } = string.Empty;

            [JsonPropertyName("enabled")]
            public bool Enabled { get; set; }

            [JsonPropertyName("parameters")]
            public NonEnUsWinClassicParameters Parameters { get; set; } = new();
        }

        protected sealed class NonEnUsWinClassicParameters
        {
            [JsonPropertyName("AppType")]
            public string AppType { get; set; } = string.Empty;

            [JsonPropertyName("select app package file")]
            public string SelectAppPackageFile { get; set; } = string.Empty;

            [JsonPropertyName("Name")]
            public string Name { get; set; } = string.Empty;

            [JsonPropertyName("Description")]
            public string Description { get; set; } = string.Empty;

            [JsonPropertyName("Publisher")]
            public string Publisher { get; set; } = string.Empty;

            [JsonPropertyName("program parameters")]
            public NonEnUsWinClassicProgramParameters ProgramParameters { get; set; } = new();

            [JsonPropertyName("Requirements parameters")]
            public NonEnUsWinClassicRequirementsParameters RequirementsParameters { get; set; } = new();

            [JsonPropertyName("Detection rules parameters")]
            public NonEnUsWinClassicDetectionRulesParameters DetectionRulesParameters { get; set; } = new();

            [JsonPropertyName("Dependencies")]
            public JsonElement? DependenciesRaw { get; set; }

            [JsonIgnore]
            public string Dependencies => DependenciesRaw?.ValueKind == JsonValueKind.String
                ? DependenciesRaw.Value.GetString() ?? string.Empty
                : string.Empty;

            [JsonPropertyName("Supercedence")]
            public string Supercedence { get; set; } = string.Empty;

            [JsonPropertyName("Assignments")]
            public NonEnUsWinClassicAssignments Assignments { get; set; } = new();

            [JsonPropertyName("Device Validation")]
            public NonEnUsWinClassicDeviceValidation DeviceValidation { get; set; } = new();
        }

        protected sealed class NonEnUsWinClassicProgramParameters
        {
            [JsonPropertyName("Installation time required (mins)")]
            public string InstallationTimeRequiredMinutes { get; set; } = string.Empty;

            [JsonPropertyName("Install behavior.")]
            public string InstallBehavior { get; set; } = string.Empty;
        }

        protected sealed class NonEnUsWinClassicRequirementsParameters
        {
            [JsonPropertyName("Minimum operating system")]
            public string MinimumOperatingSystem { get; set; } = string.Empty;
        }

        protected sealed class NonEnUsWinClassicDetectionRulesParameters
        {
            [JsonPropertyName("Rule type")]
            public string RuleType { get; set; } = string.Empty;

            [JsonPropertyName("Rules format")]
            public string RulesFormat { get; set; } = string.Empty;
        }

        protected sealed class NonEnUsWinClassicAssignments
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

        protected sealed class NonEnUsWinClassicDeviceValidation
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

        [TearDown]
        public void TestTearDown()
        {
            // Dump self-healing report at end of each test
            SelfHealingLocator.DumpHealingReport();
            TestInitialize.LogTestResult(TestContext.CurrentContext);
            _test?.Info($"Test ended at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test = null;
        }
    }
}
