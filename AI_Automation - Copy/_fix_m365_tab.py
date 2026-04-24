import os

path = os.path.join('PlaywrightTests', 'Playwright', 'Common', 'Utils', 'WizardFlowRegistry.cs')
with open(path, 'r', encoding='utf-8-sig') as f:
    lines = f.readlines()

# Find the line with the single-param GetExpectedTab
insert_after = -1
brace_count = 0
in_method = False
for i, line in enumerate(lines):
    if 'public static string? GetExpectedTab(string controlType)' in line:
        in_method = True
        brace_count = 0
    if in_method:
        brace_count += line.count('{') - line.count('}')
        if brace_count == 0 and '{' in ''.join(lines[i-5:i+1]):
            insert_after = i
            break

if insert_after == -1:
    print('ERROR: Could not find GetExpectedTab method')
else:
    new_method = '''
        /// <summary>
        /// Returns the expected wizard tab for a ControlType, adjusted for the actual app type.
        /// For M365 apps, "App information" is remapped to "App suite information".
        /// </summary>
        public static string? GetExpectedTab(string controlType, string appType)
        {
            var tab = GetExpectedTab(controlType);
            if (tab == null) return null;

            // M365 apps use "App suite information" instead of "App information"
            if (tab.Equals("App information", StringComparison.OrdinalIgnoreCase)
                && appType.Contains("Microsoft 365", StringComparison.OrdinalIgnoreCase))
            {
                return "App suite information";
            }

            return tab;
        }
'''
    lines.insert(insert_after + 1, new_method)
    with open(path, 'w', encoding='utf-8') as f:
        f.writelines(lines)
    print(f'SUCCESS: Inserted new overload after line {insert_after + 1}')
