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
    public abstract class WindowsRegressionTestSetABase : PageTest
    {
        private ExtentTest? _test;
        private SmartStepExecutor? _smartStep;
        private static readonly string _debugLogPath = Path.Combine(
            Path.GetDirectoryName(typeof(WindowsRegressionTestSetABase).Assembly.Location) ?? ".",
            "..", "..", "..", "..", "debug-step-log.txt");
        private static void DebugLog(string msg)
        {
            try { File.AppendAllText(_debugLogPath, $"[{DateTime.Now:HH:mm:ss.fff}] {msg}{Environment.NewLine}"); } catch { }
        }

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
            // Load .env file so GOOGLE_AI_API_KEY is available for AI-powered healing
            var envPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", ".env"));
            if (File.Exists(envPath))
            {
                DotNetEnv.Env.Load(envPath);
                Console.WriteLine($"[ENV] Loaded .env from: {envPath}");
                Console.WriteLine($"[ENV] GOOGLE_AI_API_KEY set: {!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GOOGLE_AI_API_KEY"))}");
            }

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
            bool shouldDeleteApp = true;

            try
            {
                Console.WriteLine($"{GetType().Name} started...");
                _test?.Info("Test execution started");

                var testData = LoadTestData();
                shouldDeleteApp = testData.Parameters.DeleteApp;
                if (!testData.Enabled)
                {
                    throw new InvalidOperationException($"Test case {RegressionTestCaseId} is disabled in windows Regression.json.");
                }

                var assignmentMode = GetAssignmentMode(testData.Parameters.Assignments);
                ValidateTestData(testData, assignmentMode);

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
                _smartStep = new SmartStepExecutor(allAppsUtils, testData.Parameters.AppType);
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

                // Step 8a: Set App URL (for Web Link apps)
                if (!string.IsNullOrWhiteSpace(testData.Parameters.AppURL))
                {
                    _test?.Info($"Step 8a: Setting App URL: {testData.Parameters.AppURL}");
                    parameters = await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        "Set App URL",
                        new ControlInfo
                        {
                            ControlType = "SetAppInformationAppURLAsync",
                            OperationValue = testData.Parameters.AppURL
                        });
                }


                // Step 8a2: Set Appstore URL (for Microsoft Store app legacy)
                if (!string.IsNullOrWhiteSpace(testData.Parameters.AppstoreURL))
                {
                    _test?.Info($"Step 8a2: Setting Appstore URL: {testData.Parameters.AppstoreURL}");
                    parameters = await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        "Set Appstore URL",
                        new ControlInfo
                        {
                            ControlType = "SetAppInformationAppstoreUrlAsync",
                            OperationValue = testData.Parameters.AppstoreURL
                        });
                }

                // Step 8b: Configure M365 App Suite (Default file format, Update channel, etc.)
                bool isEdgeApp = testData.Parameters.AppType.Contains("Microsoft Edge", StringComparison.OrdinalIgnoreCase);
                bool isM365App = !string.IsNullOrEmpty(testData.Parameters.OfficeSuiteAppDefaultFileFormat) ||
                    !string.IsNullOrEmpty(testData.Parameters.UpdateChannel);

                // Step 8a: Handle Microsoft Edge App Settings tab (Channel/Language between App Info and Assignments)
                if (isEdgeApp)
                {
                    _test?.Info("Step 8a: Navigating through Edge App Settings tab");

                    // Click Next from App Information to App Settings
                    parameters = await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        "Click Next to Edge App Settings",
                        new ControlInfo { ControlType = "ClickNextButtonAsync" });

                    // Set channel if specified (e.g. "dev", "beta", "stable")
                    if (!string.IsNullOrWhiteSpace(testData.Parameters.Channel))
                    {
                        var channelDisplayName = MapEdgeChannelToDisplayName(testData.Parameters.Channel);
                        _test?.Info($"Setting Edge channel: {channelDisplayName}");
                        parameters = await ExecuteStepAsync(
                            allAppsUtils,
                            parameters,
                            $"Set Edge channel to {channelDisplayName}",
                            new ControlInfo
                            {
                                ControlType = "SetAppSettingChannelAsync",
                                OperationValue = channelDisplayName
                            });
                    }

                    // Click Next from App Settings to Assignments
                    parameters = await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        "Click Next after Edge App Settings",
                        new ControlInfo { ControlType = "ClickNextButtonAsync" });
                }

                // Step 8b: Configure M365 App Suite (Default file format, Update channel, etc.)
                if (isM365App)
                {
                    _test?.Info("Step 8b: Configuring M365 App Suite settings");

                    // Click Next to navigate from App suite information (Tab 1) to Configure app suite (Tab 2)
                    parameters = await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        "Click Next to Configure App Suite",
                        new ControlInfo { ControlType = "ClickNextButtonAsync" });

                    // Excluded apps selection
                    if (testData.Parameters.ExcludedApps != null && testData.Parameters.ExcludedApps.Count > 0)
                    {
                        var excludedAppNames = testData.Parameters.ExcludedApps
                            .Where(kv => kv.Value)
                            .Select(kv => MapExcludedAppToDisplayName(kv.Key))
                            .Where(name => !string.IsNullOrEmpty(name))
                            .ToList()!;

                        if (excludedAppNames.Count > 0)
                        {
                            parameters = await ExecuteStepAsync(
                                allAppsUtils,
                                parameters,
                                $"Exclude Office apps: {string.Join(", ", excludedAppNames)}",
                                new ControlInfo
                                {
                                    ControlType = "SelectOfficeAppsByExcludeAsync",
                                    Value = excludedAppNames
                                });
                        }
                    }

                    // Architecture
                    if (!string.IsNullOrEmpty(testData.Parameters.OfficePlatformArchitecture))
                    {
                        parameters = await ExecuteStepAsync(
                            allAppsUtils,
                            parameters,
                            $"Set architecture to {testData.Parameters.OfficePlatformArchitecture}",
                            new ControlInfo
                            {
                                ControlType = "SetArchitectureAsync",
                                OperationValue = MapArchitectureToDisplayName(testData.Parameters.OfficePlatformArchitecture)
                            });
                    }

                    // Default file format
                    if (!string.IsNullOrEmpty(testData.Parameters.OfficeSuiteAppDefaultFileFormat))
                    {
                        parameters = await ExecuteStepAsync(
                            allAppsUtils,
                            parameters,
                            "Set default file format",
                            new ControlInfo
                            {
                                ControlType = "SetdefaultFileFormatAsync",
                                OperationValue = MapFileFormatToDisplayName(testData.Parameters.OfficeSuiteAppDefaultFileFormat)
                            });
                    }

                    // Update channel
                    if (!string.IsNullOrEmpty(testData.Parameters.UpdateChannel))
                    {
                        parameters = await ExecuteStepAsync(
                            allAppsUtils,
                            parameters,
                            "Set update channel",
                            new ControlInfo
                            {
                                ControlType = "SetUpdatechannelAsync",
                                OperationValue = MapUpdateChannelToDisplayName(testData.Parameters.UpdateChannel)
                            });
                    }

                    // Remove other versions of Office
                    if (testData.Parameters.ShouldUninstallOlderVersionsOfOffice)
                    {
                        parameters = await ExecuteStepAsync(
                            allAppsUtils,
                            parameters,
                            "Set remove other versions to Yes",
                            new ControlInfo
                            {
                                ControlType = "SetRemoveOtherVersionsAsync",
                                OperationValue = "Yes"
                            });
                    }

                    // Use shared computer activation
                    if (testData.Parameters.UseSharedComputerActivation)
                    {
                        parameters = await ExecuteStepAsync(
                            allAppsUtils,
                            parameters,
                            "Set use shared computer activation to Yes",
                            new ControlInfo
                            {
                                ControlType = "SetUseSharedComputerActivationAsync",
                                OperationValue = "Yes"
                            });
                    }

                    // Accept EULA
                    if (testData.Parameters.AutoAcceptEula)
                    {
                        parameters = await ExecuteStepAsync(
                            allAppsUtils,
                            parameters,
                            "Set accept EULA to Yes",
                            new ControlInfo
                            {
                                ControlType = "SetAcceptEulaAsync",
                                OperationValue = "Yes"
                            });
                    }

                    // Languages
                    if (testData.Parameters.LocalesToInstall != null && testData.Parameters.LocalesToInstall.Count > 0)
                    {
                        parameters = await ExecuteStepAsync(
                            allAppsUtils,
                            parameters,
                            $"Set languages: {string.Join(", ", testData.Parameters.LocalesToInstall)}",
                            new ControlInfo
                            {
                                ControlType = "SetLanguagesAsync",
                                Value = testData.Parameters.LocalesToInstall
                            });
                    }

                    // Click Next to proceed from Configure App Suite to Assignments
                    parameters = await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        "Click Next after Configure App Suite",
                        new ControlInfo { ControlType = "ClickNextButtonAsync" });
                }

                // Step 9: Handle Win32 specific parameters (program, requirements, detection rules)
                if (testData.Parameters.ProgramParameters != null)
                {
                    _test?.Info("Step 9a: Configuring program parameters");
                    await ConfigureProgramParametersAsync(allAppsUtils, parameters, testData.Parameters.ProgramParameters);
                }

                // Click Next to proceed to Requirements (for Win32 apps) or Assignments
                // Skip for M365 apps Ã¯Â¿Â½ Step 8b already navigated from Configure App Suite to Assignments
                // Skip for Edge apps Ã¯Â¿Â½ Step 8a already navigated from App Settings to Assignments
                if (!isM365App && !isEdgeApp)
                {
                    parameters = await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        "Click Next",
                        new ControlInfo { ControlType = "ClickNextButtonAsync" });
                }

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
                if (assignmentMode != AssignmentMode.None)
                {
                    _test?.Info("Step 10: Configuring assignments");
                    parameters = await ConfigureAssignmentsAsync(allAppsUtils, parameters, testData, assignmentMode);

                    parameters = await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        "Mark assignments complete",
                        new ControlInfo { ControlType = "MarkAssignmentsCompleteAsync" });
                }
                else
                {
                    _test?.Info("Step 10: No assignments configured Ã¯Â¿Â½ skipping");
                }

                // Click Next to go to Review + Create
                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Click Next to Review + Create",
                    new ControlInfo { ControlType = "ClickNextButtonAsync" });

                // Step 11: Create the app (SmartCreate adapts wait strategy per app type)
                _test?.Info("Step 11: Creating the app");
                var createControlType = WizardFlowRegistry.GetCreateVariant(testData.Parameters.AppType);
                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    $"Click Create ({createControlType})",
                    new ControlInfo { ControlType = createControlType });

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

                if (assignmentMode != AssignmentMode.None)
                {
                    parameters = await VerifyAssignmentsAsync(allAppsUtils, parameters, testData, assignmentMode);
                }

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
                if (shouldDeleteApp && !string.IsNullOrWhiteSpace(createdAppName))
                {
                    await TryCleanupCreatedAppAsync(createdAppName);
                }
                else if (!shouldDeleteApp && !string.IsNullOrWhiteSpace(createdAppName))
                {
                    _test?.Info($"Skipping cleanup: DeleteApp=false for '{createdAppName}'");
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


            // Configure assignment filter if specified
            if (!string.IsNullOrWhiteSpace(testData.Parameters.Assignments.AssignmentFiltergroup))
            {
                var filterModeCellType = assignmentMode switch
                {
                    AssignmentMode.Required => "ClickRequiredFilterModeCellAsync",
                    AssignmentMode.Available => "ClickAvailableFilterModeCellAsync",
                    AssignmentMode.Uninstall => "ClickUninstallFilterModeCellAsync",
                    _ => throw new ArgumentOutOfRangeException(nameof(assignmentMode))
                };

                parameters = await ExecuteStepAsync(
                    utils,
                    parameters,
                    $"Click filter mode cell for {testData.Parameters.Assignments.SelectGroups}",
                    new ControlInfo
                    {
                        ControlType = filterModeCellType,
                        OperationValue = testData.Parameters.Assignments.SelectGroups
                    });

                if (parameters.ContainsKey("FilterModeNotAvailable"))
                {
                    Console.WriteLine($"[AssignmentFilter] Skipping SetAssignmentFilter — 'Filter mode' column not available for this app type");
                    _test?.Info("Filter mode column not available in assignment grid — skipping filter configuration");
                    parameters.Remove("FilterModeNotAvailable");
                }
                else
                {
                    parameters = await ExecuteStepAsync(
                        utils,
                        parameters,
                        $"Set assignment filter '{testData.Parameters.Assignments.AssignmentFiltergroup}' ({testData.Parameters.Assignments.AppManagementAssignmentFilterType})",
                        new ControlInfo
                        {
                            ControlType = "SetAssignmentFilterAsync",
                            Value = new List<string>
                            {
                                testData.Parameters.Assignments.AssignmentFiltergroup,
                                testData.Parameters.Assignments.AppManagementAssignmentFilterType
                            }
                        });
                }
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
            DebugLog($"STEP START: {stepDescription} | ControlType={controlInfo.ControlType} | OpValue={controlInfo.OperationValue}");
            _test?.Info(stepDescription);
            controlInfo.Parameter = parameters;

            try
            {
                // Use SmartStepExecutor when available (provides tab pre-check & retry on wrong-page)
                if (_smartStep != null && utils is AllAppsUtils)
                {
                    var result = await _smartStep.ExecuteWithGuardsAsync(controlInfo);
                    DebugLog($"STEP DONE:  {stepDescription} | ControlType={controlInfo.ControlType}");
                    return result.Parameter;
                }

                var directResult = await utils.RunStepAsync(controlInfo);
                DebugLog($"STEP DONE:  {stepDescription} | ControlType={controlInfo.ControlType}");
                return directResult.Parameter;
            }
            catch (Exception ex)
            {
                DebugLog($"STEP FAIL:  {stepDescription} | ControlType={controlInfo.ControlType} | Error={ex.Message}");
                throw;
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

        private static void ValidateTestData(WindowsRegressionTestCase testData, AssignmentMode assignmentMode)
        {
            if (string.IsNullOrWhiteSpace(testData.Parameters.AppType))
            {
                throw new InvalidOperationException($"AppType is missing for test case {testData.TestCaseId}.");
            }

            if (string.IsNullOrWhiteSpace(testData.Parameters.Name))
            {
                throw new InvalidOperationException($"Name is missing for test case {testData.TestCaseId}.");
            }

            if (assignmentMode != AssignmentMode.None && string.IsNullOrWhiteSpace(testData.Parameters.Assignments.SelectGroups))
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
                return AssignmentMode.None;
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

        /// <summary>
        /// Maps Graph API excluded app key names to Intune portal UI display names.
        /// Returns null for deprecated apps no longer shown in the portal dropdown.
        /// </summary>
        private static string? MapExcludedAppToDisplayName(string apiName)
        {
            return apiName.ToLowerInvariant() switch
            {
                "access" => "Access",
                "excel" => "Excel",
                "onenote" => "OneNote",
                "outlook" => "Outlook",
                "powerpoint" => "PowerPoint",
                "publisher" => "Publisher",
                "lync" => "Skype for Business",
                "teams" => "Teams",
                "word" => "Word",
                // Deprecated / not in current portal dropdown
                "groove" => null,        // OneDrive for Business - removed from M365 suite selector
                "infopath" => null,  // InfoPath - removed
                "sharepointdesigner" => null, // SharePoint Designer - removed
                "bing" => null,          // Bing News - not in dropdown
                "onedrive" => null, // OneDrive - not in dropdown
                "visio" => null,         // Visio - separate license app
                _ => apiName            // Fallback: pass as-is
            };
        }

        private static string MapArchitectureToDisplayName(string apiValue)
        {
            return apiValue.ToLowerInvariant() switch
            {
                "x64" => "64-bit",
                "x86" => "32-bit",
                "both" => "64-bit",
                _ => apiValue
            };
        }

        private static string MapFileFormatToDisplayName(string apiValue)
        {
            return apiValue.ToLowerInvariant() switch
            {
                "officeopendocumentformat" => "Office Open Document Format",
                "officeopenxmlformat" => "Office Open XML Format",
                _ => apiValue
            };
        }

        private static string MapUpdateChannelToDisplayName(string apiValue)
        {
            return apiValue.ToLowerInvariant() switch
            {
                "current" => "Current Channel",
                "monthlyenterprise" => "Monthly Enterprise Channel",
                "semiannual" => "Semi-Annual Enterprise Channel",
                "deferred" => "Semi-Annual Enterprise Channel",
                "firstcurrent" => "Current Channel (Preview)",
                "firstdeferred" => "Semi-Annual Enterprise Channel (Preview)",
                _ => apiValue
            };
        }

        private static string MapEdgeChannelToDisplayName(string channel)
        {
            return channel.ToLowerInvariant() switch
            {
                "stable" => "Stable",
                "beta" => "Beta",
                "dev" => "Dev",
                "canary" => "Canary",
                _ => channel
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
            None,
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

            [JsonPropertyName("App URL")]
            public string AppURL { get; set; } = string.Empty;

            [JsonPropertyName("appStoreUrl")]
            public string AppstoreURL { get; set; } = string.Empty;

            [JsonPropertyName("channel")]
            public string Channel { get; set; } = string.Empty;

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

            // M365 Office Suite configuration
            [JsonPropertyName("officeSuiteAppDefaultFileFormat")]
            public string OfficeSuiteAppDefaultFileFormat { get; set; } = string.Empty;

            [JsonPropertyName("updateChannel")]
            public string UpdateChannel { get; set; } = string.Empty;

            [JsonPropertyName("officePlatformArchitecture")]
            public string OfficePlatformArchitecture { get; set; } = string.Empty;

            [JsonPropertyName("autoAcceptEula")]
            public bool AutoAcceptEula { get; set; }

            [JsonPropertyName("useSharedComputerActivation")]
            public bool UseSharedComputerActivation { get; set; }

            [JsonPropertyName("localesToInstall")]
            public List<string>? LocalesToInstall { get; set; }

            [JsonPropertyName("installProgressDisplayLevel")]
            public string InstallProgressDisplayLevel { get; set; } = string.Empty;

            [JsonPropertyName("shouldUninstallOlderVersionsOfOffice")]
            public bool ShouldUninstallOlderVersionsOfOffice { get; set; }

            [JsonPropertyName("targetVersion")]
            public string TargetVersion { get; set; } = string.Empty;

            [JsonPropertyName("updateVersion")]
            public string UpdateVersion { get; set; } = string.Empty;

            [JsonPropertyName("excludedApps")]
            public Dictionary<string, bool>? ExcludedApps { get; set; }

            [JsonPropertyName("DeleteApp")]
            public bool DeleteApp { get; set; } = true;

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

            [JsonPropertyName("AssignmentFiltergroup")]
            public string AssignmentFiltergroup { get; set; } = string.Empty;

            [JsonPropertyName("AppManagementAssignmentFilterType")]
            public string AppManagementAssignmentFilterType { get; set; } = string.Empty;
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