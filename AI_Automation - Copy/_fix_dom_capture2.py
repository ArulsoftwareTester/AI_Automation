file_path = r'IntuneCanaryTests\App_Regression\App_Signoff_Test\WinGet App Test Cases\WinGetStoreAppRegressionTestBase.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    lines = f.readlines()

# === FIX 1: Modify ExecuteStepAsync catch block to add DOM capture ===
# Find the line with "[HEAL_REQUEST]" and add DOM capture after it
for i, line in enumerate(lines):
    if '[HEAL_REQUEST]' in line and 'Console.WriteLine' in line:
        # Check if DOM capture is already added
        if i+1 < len(lines) and 'CaptureDomAndAnalyzeAsync' in lines[i+1]:
            print("DOM capture already in ExecuteStepAsync")
            break
        # Insert the DOM capture call after the HEAL_REQUEST line
        indent = '                '
        new_line = indent + '// === LIVE DOM CAPTURE: Capture page source at moment of failure ===\n'
        new_line2 = indent + 'await CaptureDomAndAnalyzeAsync(stepDescription, controlInfo.ControlType, ex.Message);\n'
        new_line3 = '\n'
        lines.insert(i+1, new_line3)
        lines.insert(i+1, new_line2)
        lines.insert(i+1, new_line)
        print(f"INSERTED DOM capture in ExecuteStepAsync after line {i+1}")
        break

# === FIX 2: Wrap SelectAppTypeDirectlyAsync combobox with try/catch + healing ===
# Find the method and wrap the combobox part
select_start = None
for i, line in enumerate(lines):
    if 'private async Task SelectAppTypeDirectlyAsync(string appType)' in line:
        select_start = i
        break

if select_start is not None:
    # Check if already wrapped
    method_block = ''.join(lines[select_start:select_start+30])
    if 'CaptureDomOnFailureAsync' in method_block:
        print("SelectAppTypeDirectlyAsync already has DOM capture")
    else:
        # Find the WaitForAsync line for appTypeComboBox
        for j in range(select_start, min(select_start+15, len(lines))):
            if 'appTypeComboBox.WaitForAsync' in lines[j]:
                # Replace the 3 lines (WaitFor, Click on combobox) with try/catch version
                # Find the click line
                click_line = j + 1  # next line is ClickAsync
                
                old_timeout = lines[j]  # await appTypeComboBox.WaitForAsync(... Timeout = 30000 ...
                old_click = lines[click_line]  # await appTypeComboBox.ClickAsync();
                
                indent = '            '
                new_block = [
                    indent + 'try\n',
                    indent + '{\n',
                    indent + '    await appTypeComboBox.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 15000 });\n',
                    indent + '    await appTypeComboBox.ClickAsync();\n',
                    indent + '}\n',
                    indent + 'catch (Exception ex)\n',
                    indent + '{\n',
                    indent + '    Console.WriteLine($"[HEAL_SIGNAL] SelectAppTypeDirectlyAsync: App type combobox not found: {ex.Message}");\n',
                    indent + '    var domHtml = await CaptureDomOnFailureAsync("SelectAppType_ComboBox");\n',
                    indent + '    var hints = new HealingHints\n',
                    indent + '    {\n',
                    indent + '        Identifier = "AppTypeCombobox",\n',
                    indent + '        Text = "App type",\n',
                    indent + '        Role = AriaRole.Combobox,\n',
                    indent + '        AriaLabel = "App type"\n',
                    indent + '    };\n',
                    indent + '    var healed = await SelfHealingLocator.ResolveAsync(Page, appTypeComboBox, hints, iframeName: null, timeoutMs: 15000);\n',
                    indent + '    await healed.ClickAsync();\n',
                    indent + '}\n',
                ]
                
                # Remove old 2 lines and insert new block
                del lines[j:click_line+1]
                for k, new_line in enumerate(new_block):
                    lines.insert(j+k, new_line)
                
                print(f"WRAPPED SelectAppTypeDirectlyAsync combobox with healing (lines {j}-{click_line})")
                break

with open(file_path, 'w', encoding='utf-8') as f:
    f.writelines(lines)

print("DONE: All changes applied")
