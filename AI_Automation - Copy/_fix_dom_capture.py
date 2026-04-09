import re

file_path = r'IntuneCanaryTests\App_Regression\App_Signoff_Test\WinGet App Test Cases\WinGetStoreAppRegressionTestBase.cs'

with open(file_path, 'r', encoding='utf-8-sig') as f:
    content = f.read()

# === REPLACEMENT 1: ExecuteStepAsync — add live DOM capture on failure ===
old_execute = '''        private async Task<Dictionary<string, string>> ExecuteStepAsync(
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
                _test?.Fail($"Step failed: {stepDescription} \u2014 {ex.Message}");
                throw;
            }
        }'''

new_execute = '''        private async Task<Dictionary<string, string>> ExecuteStepAsync(
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

                _test?.Fail($"Step failed: {stepDescription} \\u2014 {ex.Message}");
                throw;
            }
        }'''

if old_execute in content:
    content = content.replace(old_execute, new_execute)
    print("REPLACED ExecuteStepAsync")
else:
    print("WARNING: ExecuteStepAsync not found for replacement")
    # Debug: show what's there
    idx = content.find('private async Task<Dictionary<string, string>> ExecuteStepAsync')
    if idx > -1:
        print(f"Found at position {idx}")
        print(repr(content[idx:idx+200]))

# === REPLACEMENT 2: SelectAppTypeDirectlyAsync — wrap with DOM capture ===
old_select = '''        private async Task SelectAppTypeDirectlyAsync(string appType)
        {
            _test?.Info($"Select app type {appType}");

            var appTypeComboBox = Page.GetByRole(AriaRole.Combobox, new() { Name = "App type", Exact = true });
            await appTypeComboBox.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 30000 });
            await appTypeComboBox.ClickAsync();

            var option = Page.GetByText(new Regex($"^{Regex.Escape(appType)}$", RegexOptions.IgnoreCase)).First;
            await option.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 30000 });
            await option.ClickAsync();

            var selectButton = Page.GetByRole(AriaRole.Button, new() { Name = "Select", Exact = true });
            await selectButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 30000 });
            await selectButton.ClickAsync();
        }'''

new_select = '''        private async Task SelectAppTypeDirectlyAsync(string appType)
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
        }'''

if old_select in content:
    content = content.replace(old_select, new_select)
    print("REPLACED SelectAppTypeDirectlyAsync")
else:
    print("WARNING: SelectAppTypeDirectlyAsync not found for replacement")

# === ADD NEW METHODS: CaptureDomOnFailureAsync + CaptureDomAndAnalyzeAsync ===
# Insert before TryCleanupCreatedAppAsync
insert_marker = '        private async Task TryCleanupCreatedAppAsync(string createdAppName)'

dom_methods = '''        /// <summary>
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

'''

if insert_marker in content:
    content = content.replace(insert_marker, dom_methods + insert_marker)
    print("INSERTED CaptureDomOnFailureAsync + CaptureDomAndAnalyzeAsync")
else:
    print("WARNING: Insert marker not found")

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(content)

print("DONE: All DOM capture changes applied")
