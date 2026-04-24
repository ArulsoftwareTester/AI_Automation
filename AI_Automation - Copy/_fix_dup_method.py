path = r'PlaywrightTests\Playwright\Common\Utils\WizardFlowRegistry.cs'
with open(path, 'r', encoding='utf-8') as f:
    lines = f.readlines()

# Find all lines with the two-param signature
sig = 'public static string? GetExpectedTab(string controlType, string appType)'
indices = [i for i, line in enumerate(lines) if sig in line]
print(f'Found {len(indices)} occurrences of two-param overload at lines: {[i+1 for i in indices]}')

if len(indices) == 2:
    # Remove the second block (from its docstring to closing brace)
    # Find the docstring start (/// <summary>) before the second occurrence
    start = indices[1]
    while start > 0 and '/// <summary>' not in lines[start]:
        start -= 1
    # Find the closing brace after the second occurrence
    end = indices[1]
    brace_count = 0
    found_open = False
    while end < len(lines):
        brace_count += lines[end].count('{') - lines[end].count('}')
        if '{' in lines[end]:
            found_open = True
        if found_open and brace_count == 0:
            break
        end += 1
    
    print(f'Removing lines {start+1} to {end+1}')
    # Also remove blank line before docstring if present
    if start > 0 and lines[start-1].strip() == '':
        start -= 1
    
    del lines[start:end+1]
    with open(path, 'w', encoding='utf-8') as f:
        f.writelines(lines)
    print('SUCCESS: Removed duplicate method')
else:
    print('No duplicate to remove')
