file_path = r'C:\Users\v-arulmani\Downloads\AI_Automation 5\AI_Automation - Copy\PlaywrightTests\Playwright\Common\Utils\BaseUtils\Apps\ByPlatform\AllAppsUtils.cs'
with open(file_path, 'r', encoding='utf-8-sig') as f:
    content = f.read()

# Step 1: Fix the switch statement area - replace the method with original case statements
# Find the corrupt block: from "#region Assignments Uninstall\n        private async Task" to "#endregion\n                #region Assignments Common Function"
old_switch = '''                #region Assignments Uninstall
        private async Task ClickUninstallAddGroupAsync(string groupName)
        {
            var tabPanel = await GetTabPanelLocatorAsync("Assignments");

            // Try 1: Gemini-proven xpath anchored on section heading (most stable)
            try
            {
                ILocator addGroupLink;
                if (!string.IsNullOrEmpty(IFrameName))
                {
                    var iframeLocator = Elements.GetIFrameLocator(this.CurrentIPage, IFrameName);
                    addGroupLink = iframeLocator.Locator("xpath=//h3[text()='Uninstall']/following::a[text()='+ Add group' and contains(@class, 'msportalfx-text-primary') and contains(@class, 'ext-controls-selectLink')]").First;
                }
                else
                {
                    addGroupLink = tabPanel.Locator("xpath=//h3[text()='Uninstall']/following::a[text()='+ Add group' and contains(@class, 'msportalfx-text-primary') and contains(@class, 'ext-controls-selectLink')]").First;
                }
                await addGroupLink.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 15000 });
                await addGroupLink.ScrollIntoViewIfNeededAsync(new() { Timeout = 10000 });
                await addGroupLink.ClickAsync(new() { Timeout = 10000 });
            }
            catch (Exception ex)
            {
                LogHelper.Info($"[HEAL_SIGNAL] ClickUninstallAddGroupAsync: Gemini xpath failed: {ex.Message}. Trying section-scoped fallback...");

                // Try 2: Original section-based approach
                try
                {
                    var uninstallSection = await ControlHelper.GetLocatorByClassAndHasTextAsync(tabPanel, "fxc-weave-pccontrol fxc-section-control fxc-base msportalfx-customHtml msportalfx-form-formelement", "Uninstall", -1);
                    await uninstallSection.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 15000 });
                    await uninstallSection.ScrollIntoViewIfNeededAsync();
                    var addGroupLinkBase = await ElementHelper.GetByClassAndHasTextAsync(uninstallSection, "msportalfx-text-primary ext-controls-selectLink", "+ Add group", waitUntilElementExist: false);
                    await addGroupLinkBase.Nth(0).ScrollIntoViewIfNeededAsync(new() { Timeout = 10000 });
                    await addGroupLinkBase.Nth(0).ClickAsync(new() { Timeout = 10000 });
                }
                catch (Exception ex2)
                {
                    LogHelper.Info($"[HEAL_SIGNAL] ClickUninstallAddGroupAsync: section fallback failed: {ex2.Message}. Trying last-link fallback...");

                    // Try 3: Find all "+ Add group" links, pick the last one (Uninstall is last section)
                    try
                    {
                        ILocator allAddGroupLinks;
                        if (!string.IsNullOrEmpty(IFrameName))
                        {
                            allAddGroupLinks = Elements.GetIFrameLocator(this.CurrentIPage, IFrameName)
                                .Locator("a.ext-controls-selectLink", new() { HasText = "+ Add group" });
                        }
                        else
                        {
                            allAddGroupLinks = tabPanel.Locator("a.ext-controls-selectLink", new() { HasText = "+ Add group" });
                        }
                        int count = await allAddGroupLinks.CountAsync();
                        LogHelper.Info($"[HEAL_SIGNAL] Found {count} '+ Add group' links. Using last one for Uninstall.");
                        if (count > 0)
                        {
                            var lastLink = allAddGroupLinks.Nth(count - 1);
                            await lastLink.ScrollIntoViewIfNeededAsync(new() { Timeout = 10000 });
                            await lastLink.ClickAsync(new() { Timeout = 10000 });
                        }
                        else
                        {
                            throw new Exception("No '+ Add group' links found");
                        }
                    }
                    catch (Exception fallbackEx)
                    {
                        LogHelper.Info($"[HEAL_SIGNAL] All fallbacks failed: {fallbackEx.Message}. Attempting AI self-healing...");
                        var hints = HealingHintsRegistry.Get("AddGroupLink_Uninstall") ?? new HealingHints
                        {
                            Identifier = "AddGroupLink_Uninstall",
                            Text = "+ Add group",
                            ClassName = "msportalfx-text-primary ext-controls-selectLink",
                            Role = AriaRole.Link
                        };
                        var primaryLocator = tabPanel.Locator("a", new() { HasText = "+ Add group" }).Last;
                        var healed = await SelfHealingLocator.ResolveAsync(
                            this.CurrentIPage,
                            primaryLocator,
                            hints,
                            iframeName: IFrameName,
                            timeoutMs: 15000);
                        await healed.ScrollIntoViewIfNeededAsync();
                        await healed.ClickAsync();
                    }
                }
            }
            SelectGroupUtils selectGroupUtils = new SelectGroupUtils(this.CurrentIPage, this.CurrentEnv);
            await selectGroupUtils.SelectGroupAsync(groupName);
        }
        #endregion
                #region Assignments Common Function'''

new_switch = '''                #region Assignments Uninstall
                case "ClickUninstallAddGroupAsync":
                    await ClickUninstallAddGroupAsync(controlInfo.OperationValue);
                    break;
                #endregion
                #region Assignments Common Function'''

if old_switch in content:
    content = content.replace(old_switch, new_switch)
    print('Step 1: Switch case restored')
else:
    print('ERROR: Could not find corrupt block in switch')
    exit(1)

# Step 2: Check if the method region already exists elsewhere
if 'private async Task ClickUninstallAddGroupAsync' in content:
    print('Step 2: Method already exists elsewhere - skipping insertion')
else:
    print('Step 2: Need to insert method')
    # Find where to insert: after "#endregion" that follows ClickAvailableForEnrolledDevicesBehaveUninstallOnDeviceRemovalCellAsync
    anchor = 'private async Task ClickAvailableForEnrolledDevicesBehaveUninstallOnDeviceRemovalCellAsync'
    anchor_pos = content.find(anchor)
    if anchor_pos < 0:
        print('ERROR: Could not find anchor method')
        exit(1)
    # Find the #endregion after this method
    endregion_after = content.find('#endregion', anchor_pos)
    end_of_line = content.find('\n', endregion_after)
    
    at = chr(64)
    method_code = f'''
        #region Assignments Uninstall
        private async Task ClickUninstallAddGroupAsync(string groupName)
        {{
            var tabPanel = await GetTabPanelLocatorAsync("Assignments");

            // Try 1: Gemini-proven xpath anchored on section heading (most stable)
            try
            {{
                ILocator addGroupLink;
                if (!string.IsNullOrEmpty(IFrameName))
                {{
                    var iframeLocator = Elements.GetIFrameLocator(this.CurrentIPage, IFrameName);
                    addGroupLink = iframeLocator.Locator("xpath=//h3[text()='Uninstall']/following::a[text()='+ Add group' and contains({at}class, 'msportalfx-text-primary') and contains({at}class, 'ext-controls-selectLink')]").First;
                }}
                else
                {{
                    addGroupLink = tabPanel.Locator("xpath=//h3[text()='Uninstall']/following::a[text()='+ Add group' and contains({at}class, 'msportalfx-text-primary') and contains({at}class, 'ext-controls-selectLink')]").First;
                }}
                await addGroupLink.WaitForAsync(new() {{ State = WaitForSelectorState.Visible, Timeout = 15000 }});
                await addGroupLink.ScrollIntoViewIfNeededAsync(new() {{ Timeout = 10000 }});
                await addGroupLink.ClickAsync(new() {{ Timeout = 10000 }});
            }}
            catch (Exception ex)
            {{
                LogHelper.Info($"[HEAL_SIGNAL] ClickUninstallAddGroupAsync: Gemini xpath failed: {{ex.Message}}. Trying section-scoped fallback...");

                // Try 2: Original section-based approach
                try
                {{
                    var uninstallSection = await ControlHelper.GetLocatorByClassAndHasTextAsync(tabPanel, "fxc-weave-pccontrol fxc-section-control fxc-base msportalfx-customHtml msportalfx-form-formelement", "Uninstall", -1);
                    await uninstallSection.WaitForAsync(new() {{ State = WaitForSelectorState.Visible, Timeout = 15000 }});
                    await uninstallSection.ScrollIntoViewIfNeededAsync();
                    var addGroupLinkBase = await ElementHelper.GetByClassAndHasTextAsync(uninstallSection, "msportalfx-text-primary ext-controls-selectLink", "+ Add group", waitUntilElementExist: false);
                    await addGroupLinkBase.Nth(0).ScrollIntoViewIfNeededAsync(new() {{ Timeout = 10000 }});
                    await addGroupLinkBase.Nth(0).ClickAsync(new() {{ Timeout = 10000 }});
                }}
                catch (Exception ex2)
                {{
                    LogHelper.Info($"[HEAL_SIGNAL] ClickUninstallAddGroupAsync: section fallback failed: {{ex2.Message}}. Trying last-link fallback...");

                    // Try 3: Find all "+ Add group" links, pick the last one
                    try
                    {{
                        ILocator allAddGroupLinks;
                        if (!string.IsNullOrEmpty(IFrameName))
                        {{
                            allAddGroupLinks = Elements.GetIFrameLocator(this.CurrentIPage, IFrameName)
                                .Locator("a.ext-controls-selectLink", new() {{ HasText = "+ Add group" }});
                        }}
                        else
                        {{
                            allAddGroupLinks = tabPanel.Locator("a.ext-controls-selectLink", new() {{ HasText = "+ Add group" }});
                        }}
                        int count = await allAddGroupLinks.CountAsync();
                        LogHelper.Info($"[HEAL_SIGNAL] Found {{count}} '+ Add group' links. Using last one for Uninstall.");
                        if (count > 0)
                        {{
                            var lastLink = allAddGroupLinks.Nth(count - 1);
                            await lastLink.ScrollIntoViewIfNeededAsync(new() {{ Timeout = 10000 }});
                            await lastLink.ClickAsync(new() {{ Timeout = 10000 }});
                        }}
                        else
                        {{
                            throw new Exception("No '+ Add group' links found");
                        }}
                    }}
                    catch (Exception fallbackEx)
                    {{
                        LogHelper.Info($"[HEAL_SIGNAL] All fallbacks failed: {{fallbackEx.Message}}. Attempting AI self-healing...");
                        var hints = HealingHintsRegistry.Get("AddGroupLink_Uninstall") ?? new HealingHints
                        {{
                            Identifier = "AddGroupLink_Uninstall",
                            Text = "+ Add group",
                            ClassName = "msportalfx-text-primary ext-controls-selectLink",
                            Role = AriaRole.Link
                        }};
                        var primaryLocator = tabPanel.Locator("a", new() {{ HasText = "+ Add group" }}).Last;
                        var healed = await SelfHealingLocator.ResolveAsync(
                            this.CurrentIPage,
                            primaryLocator,
                            hints,
                            iframeName: IFrameName,
                            timeoutMs: 15000);
                        await healed.ScrollIntoViewIfNeededAsync();
                        await healed.ClickAsync();
                    }}
                }}
            }}
            SelectGroupUtils selectGroupUtils = new SelectGroupUtils(this.CurrentIPage, this.CurrentEnv);
            await selectGroupUtils.SelectGroupAsync(groupName);
        }}
        #endregion'''
    
    content = content[:end_of_line+1] + method_code + '\n' + content[end_of_line+1:]
    print(f'Step 2: Method inserted after line {end_of_line}')

with open(file_path, 'w', encoding='utf-8-sig') as f:
    f.write(content)
print('File fixed successfully')
