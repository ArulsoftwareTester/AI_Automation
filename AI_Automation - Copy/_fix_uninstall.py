import sys

file_path = r'PlaywrightTests\Playwright\Common\Utils\BaseUtils\Apps\ByPlatform\AllAppsUtils.cs'
with open(file_path, 'r', encoding='utf-8-sig') as f:
    lines = f.readlines()

# Find the method start and end
start_idx = None
end_idx = None
for i, line in enumerate(lines):
    if 'private async Task ClickUninstallAddGroupAsync(string groupName)' in line:
        start_idx = i
    if start_idx is not None and i > start_idx and line.strip() == '}' and not any(c in line for c in ['//']):
        # Check indent level - method closing brace at 8 spaces
        if line.startswith('        }') and len(line) - len(line.lstrip()) == 8:
            end_idx = i
            break

if start_idx is None or end_idx is None:
    print(f'Method not found: start={start_idx}, end={end_idx}')
    sys.exit(1)

print(f'Found method at lines {start_idx+1}-{end_idx+1}')

new_method = '''        private async Task ClickUninstallAddGroupAsync(string groupName)
        {
            var tabPanel = await GetTabPanelLocatorAsync("Assignments");
            var uninstallSection = await ControlHelper.GetLocatorByClassAndHasTextAsync(tabPanel, "fxc-weave-pccontrol fxc-section-control fxc-base msportalfx-customHtml msportalfx-form-formelement", "Uninstall", -1);
            await uninstallSection.ScrollIntoViewIfNeededAsync();

            var addGroupLinkBase = await ElementHelper.GetByClassAndHasTextAsync(uninstallSection, "msportalfx-text-primary ext-controls-selectLink", "+ Add group", waitUntilElementExist: false);
            try
            {
                await addGroupLinkBase.Nth(0).ScrollIntoViewIfNeededAsync(new() { Timeout = 10000 });
                await addGroupLinkBase.Nth(0).ClickAsync(new() { Timeout = 10000 });
            }
            catch (Exception ex)
            {
                LogHelper.LogInfo($"[HEAL_SIGNAL] ClickUninstallAddGroupAsync: primary locator failed: {ex.Message}. Attempting self-healing...");
                var hints = HealingHintsRegistry.Get("AddGroupLink_Uninstall") ?? new HealingHints
                {
                    Identifier = "AddGroupLink_Uninstall",
                    Text = "+ Add group",
                    ClassName = "msportalfx-text-primary ext-controls-selectLink",
                    Role = AriaRole.Link
                };
                var healed = await SelfHealingLocator.ResolveAsync(
                    this.CurrentIPage,
                    addGroupLinkBase.Nth(0),
                    hints,
                    iframeName: IFrameName,
                    timeoutMs: 15000);
                await healed.ScrollIntoViewIfNeededAsync();
                await healed.ClickAsync();
            }
            SelectGroupUtils selectGroupUtils = new SelectGroupUtils(this.CurrentIPage, this.CurrentEnv);
            await selectGroupUtils.SelectGroupAsync(groupName);
        }
'''

# Replace lines
new_lines = lines[:start_idx] + [new_method] + lines[end_idx+1:]

with open(file_path, 'w', encoding='utf-8') as f:
    f.writelines(new_lines)

print('SUCCESS: ClickUninstallAddGroupAsync updated with self-healing')
