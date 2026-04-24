import sys

file_path = r'PlaywrightTests\Playwright\Common\Utils\FeatureUtils\AssignmentFilterUtils.cs'
with open(file_path, 'r', encoding='utf-8-sig') as f:
    content = f.read()

# Find and replace the method body line by line
lines = content.split('\n')
start_idx = None
end_idx = None

for i, line in enumerate(lines):
    if 'public static async Task EditFilterForAssignmentPage(IPage' in line and start_idx is None:
        start_idx = i
    if start_idx is not None and i > start_idx:
        stripped = line.strip()
        # Count braces to find method end
        if not hasattr(sys, '_brace_count'):
            sys._brace_count = 0
        # We need a different approach - find the closing brace
        pass

# Simpler approach: just replace the signature line and the body
# The method is from line 695 to line 724 (0-indexed: 694 to 723)
sig_line = None
for i, line in enumerate(lines):
    if 'public static async Task EditFilterForAssignmentPage(IPage' in line:
        sig_line = i
        break

if sig_line is None:
    print('ERROR: Method not found')
    sys.exit(1)

print(f'Found method at line {sig_line + 1}')

# Find the method end - count braces from the opening {
brace_count = 0
method_started = False
end_line = None
for i in range(sig_line, len(lines)):
    for ch in lines[i]:
        if ch == '{':
            brace_count += 1
            method_started = True
        elif ch == '}':
            brace_count -= 1
            if method_started and brace_count == 0:
                end_line = i
                break
    if end_line is not None:
        break

print(f'Method ends at line {end_line + 1}')

new_method_lines = [
    '        public static async Task EditFilterForAssignmentPage(IPage?page, string searchValue, string filterBehave = "Include filtered devices in assignment", int index = 0, string searchBoxPlaceholder = "Search by name", string iframeName = null)',
    '        {',
    '            try',
    '            {',
    '                if (!filterBehave.IsNullOrEmpty())',
    '                {',
    "                    // Scope 'Edit filter' to iframe when provided (e.g., Apps use AppList iframe)",
    '                    ILocator editFilter;',
    '                    if (!string.IsNullOrEmpty(iframeName))',
    '                    {',
    '                        var iframeLocator = ElementHelper.GetIFrameLocator(page, iframeName);',
    '                        editFilter = iframeLocator.Locator("//*[text()=\'Edit filter\' and @class=\'fxc-gcflink-link\']").Nth(index);',
    '                    }',
    '                    else',
    '                    {',
    '                        editFilter = page.Locator("//*[text()=\'Edit filter\' and @class=\'fxc-gcflink-link\']").Nth(index);',
    '                    }',
    '',
    '                    // Self-healing wrapper for resilience against DOM changes',
    '                    var hints = HealingHintsRegistry.Get("EditFilter_Assignment") ?? new HealingHints',
    '                    {',
    '                        Identifier = "EditFilter_Assignment",',
    '                        Text = "Edit filter",',
    '                        ClassName = "fxc-gcflink-link",',
    '                        Role = AriaRole.Link',
    '                    };',
    '                    var healedEditFilter = await SelfHealingLocator.ResolveAsync(page, editFilter, hints, iframeName: iframeName, timeoutMs: 15000);',
    '                    await healedEditFilter.ClickAsync();',
    '',
    '                    // Filter selection blade opens at page level (outside iframe)',
    '                    await DevicesBarUtils.ClickButtonNameAsync(page, "Radio", filterBehave);',
    '                    var searchBox = page.GetByPlaceholder(searchBoxPlaceholder);',
    '                    await OperationForLocators(page, OperateType.InputTextbox, searchBox, null, searchValue);',
    '                    CommonOperations.WaitMiddleTime();',
    "                    var searchResult = page.Locator($\"//*[@class = 'fxs-portal-hover fxs-portal-focus azc-grid-row']//*[text()='{searchValue}']\").First;",
    '                    await OperationForLocators(page, OperateType.Click, searchResult);',
    '                    await Controls.ClickByBtnNameAsync(page, "Select");',
    '                }',
    '            }',
    '            catch (CustomLogException)',
    '            {',
    '                throw;',
    '            }',
    '            catch (Exception ex)',
    '            {',
    '                LogHelper.Error(ex.Message);',
    '                throw new CustomLogException("Edit Filter For Assignment Page failed.", ex);',
    '            }',
    '        }',
]

# Replace lines
new_lines = lines[:sig_line] + new_method_lines + lines[end_line + 1:]
with open(file_path, 'w', encoding='utf-8') as f:
    f.write('\n'.join(new_lines))

print('SUCCESS: Method replaced')
