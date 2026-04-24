f = r'c:\Users\v-arulmani\Downloads\AI_Automation 5\AI_Automation - Copy\PlaywrightTests\Playwright\Common\Helper\ElementHelper.cs'
with open(f, 'r', encoding='utf-8') as fh:
    lines = fh.readlines()

print(f'Total lines: {len(lines)}')

# Find the target: line with "var page = _currentPage.Value;" in the first IsExistAsync (without IPage)
# This is around line 1224
target_line = None
for i, line in enumerate(lines):
    if '_currentPage.Value;' in line and i > 1200 and i < 1230:
        target_line = i
        break

print(f'Found _currentPage.Value at line {target_line + 1}: {lines[target_line].rstrip()}')

# Also find the "iframeName: null" line nearby
iframe_line = None
for i in range(target_line, target_line + 30):
    if 'iframeName: null' in lines[i]:
        iframe_line = i
        break

print(f'Found iframeName: null at line {iframe_line + 1}: {lines[iframe_line].rstrip()}')

# REPLACEMENTS:
# 1. After "var page = _currentPage.Value;" add locator.Page fallback
old_page_line = lines[target_line]
new_page_lines = old_page_line + \
    '                            // Fallback: extract page from the locator itself (ILocator.Page)\n' + \
    '                            // This allows healing for ALL callers  even those without IPage param\n' + \
    '                            if (page == null)\n' + \
    '                            {\n' + \
    '                                try { page = locator.Page; } catch { }\n' + \
    '                            }\n' + \
    '\n' + \
    '                            // Extract iframe name from locator string for targeted healing\n' + \
    '                            string iframeName = null;\n' + \
    '                            var iframeMatch = System.Text.RegularExpressions.Regex.Match(\n' + \
    '                                locator.ToString(), @"iframe\\[name=""([^""]+)""\\]");\n' + \
    '                            if (iframeMatch.Success)\n' + \
    '                                iframeName = iframeMatch.Groups[1].Value;\n'

lines[target_line] = new_page_lines

# 2. Replace "iframeName: null" with "iframeName: iframeName"
lines[iframe_line] = lines[iframe_line].replace('iframeName: null', 'iframeName: iframeName')

# 3. Update the log message to show page source
for i in range(target_line, target_line + 35):
    if 'via AsyncLocal page' in lines[i]:
        lines[i] = lines[i].replace(
            'triggering dynamic self-healing (via AsyncLocal page)',
            'triggering dynamic self-healing (page from ' + '{' + '(_currentPage.Value != null ? "AsyncLocal" : "locator.Page")' + '}' + ', iframe=' + '{' + 'iframeName ?? "none"' + '}' + ')'
        )
        print(f'Updated log message at line {i + 1}')
        break

with open(f, 'w', encoding='utf-8') as fh:
    fh.writelines(lines)

print('SUCCESS: Applied locator.Page fallback + iframe extraction')
