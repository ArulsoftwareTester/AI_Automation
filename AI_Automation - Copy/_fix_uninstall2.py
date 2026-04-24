import os
file_path = r'C:\Users\v-arulmani\Downloads\AI_Automation 5\AI_Automation - Copy\PlaywrightTests\Playwright\Common\Utils\BaseUtils\Apps\ByPlatform\AllAppsUtils.cs'
with open(file_path, 'r', encoding='utf-8-sig') as f:
    lines = f.readlines()

# Find the method start line
start_idx = None
for i, line in enumerate(lines):
    if 'private async Task ClickUninstallAddGroupAsync(string groupName)' in line:
        start_idx = i
        break

if start_idx is None:
    print('Method not found')
    exit(1)

# The line after the opening brace is the comment + var iframeLocator line
# We need to replace lines start_idx+2 and start_idx+3 (comment and iframeLocator)
# and start_idx+4 (addGroupLink)

# Read the current lines at those positions
print(f'Found method at line {start_idx+1}')
print(f'Line {start_idx+3}: {lines[start_idx+2].rstrip()}')
print(f'Line {start_idx+4}: {lines[start_idx+3].rstrip()}')
print(f'Line {start_idx+5}: {lines[start_idx+4].rstrip()}')

# New replacement lines
new_lines = [
    '            var tabPanel = await GetTabPanelLocatorAsync(\"Assignments\");\n',
    '\n',
    '            // Primary: Gemini-proven xpath anchored on section heading (stable across portal updates)\n',
    '            ILocator addGroupLink;\n',
    '            if (!string.IsNullOrEmpty(IFrameName))\n',
    '            {\n',
    '                var iframeLocator = Elements.GetIFrameLocator(this.CurrentIPage, IFrameName);\n',
    "                addGroupLink = iframeLocator.Locator(\"xpath=//h3[text()='Uninstall']/following::a[text()='+ Add group' and contains(\" + '@' + \"class, 'msportalfx-text-primary') and contains(\" + '@' + \"class, 'ext-controls-selectLink')]\").First;\n",
    '            }\n',
    '            else\n',
    '            {\n',
    "                addGroupLink = tabPanel.Locator(\"xpath=//h3[text()='Uninstall']/following::a[text()='+ Add group' and contains(\" + '@' + \"class, 'msportalfx-text-primary') and contains(\" + '@' + \"class, 'ext-controls-selectLink')]\").First;\n",
    '            }\n',
]

# Replace lines start_idx+2, +3, +4 (the comment, iframeLocator, addGroupLink lines)
lines[start_idx+2:start_idx+5] = new_lines

with open(file_path, 'w', encoding='utf-8-sig') as f:
    f.writelines(lines)
print('Fix applied successfully')
