file_path = r'C:\Users\v-arulmani\Downloads\AI_Automation 5\AI_Automation - Copy\PlaywrightTests\Playwright\Common\Utils\BaseUtils\Apps\ByPlatform\AllAppsUtils.cs'
with open(file_path, 'r', encoding='utf-8-sig') as f:
    content = f.read()

# Find the region markers to precisely locate the method
region_start = content.find('#region Assignments Uninstall')
region_end = content.find('#endregion', region_start)  # but there might be nested endregions
next_region = content.find('#region Assignments Common Function', region_start)

if region_start < 0 or next_region < 0:
    print(f'Region markers not found: start={region_start}, next={next_region}')
    exit(1)

print(f'Found region at char {region_start}')
print(f'Next region at char {next_region}')

# Find the #endregion just before #region Assignments Common Function
endregion_pos = content.rfind('#endregion', region_start, next_region)
print(f'endregion at char {endregion_pos}')

# Extract everything from #region Assignments Uninstall to #endregion (inclusive of the line)
end_of_endregion = content.find('\n', endregion_pos)

old_block = content[region_start:end_of_endregion+1]
print(f'Old block length: {len(old_block)} chars')
print(f'First 100: {old_block[:100]}')

at = chr(64)  # @ character

new_block = f'''#region Assignments Uninstall
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

                    // Try 3: Find all "+ Add group" links, pick the last one (Uninstall is last section)
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
        #endregion
'''

content = content[:region_start] + new_block + content[end_of_endregion+1:]

with open(file_path, 'w', encoding='utf-8-sig') as f:
    f.write(content)
print('Method replaced successfully')
