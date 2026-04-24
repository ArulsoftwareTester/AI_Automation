import sys

file_path = r'PlaywrightTests\Playwright\Common\Utils\HealingHintsRegistry.cs'
with open(file_path, 'r', encoding='utf-8-sig') as f:
    content = f.read()

# Find the closing of the dictionary - last entry before '};'
target = '            ["Dialog_YesButton"] = new HealingHints\n            {\n                Identifier = "Dialog_YesButton",\n                Text = "Yes",\n                Role = AriaRole.Button,\n                IsButton = true\n            }\n        };'

replacement = '''            ["Dialog_YesButton"] = new HealingHints
            {
                Identifier = "Dialog_YesButton",
                Text = "Yes",
                Role = AriaRole.Button,
                IsButton = true
            },

            // -- Assignment Filter Elements --
            ["EditFilter_Assignment"] = new HealingHints
            {
                Identifier = "EditFilter_Assignment",
                Text = "Edit filter",
                ClassName = "fxc-gcflink-link",
                Role = AriaRole.Link
            },
            ["FilterRadio_Include"] = new HealingHints
            {
                Identifier = "FilterRadio_Include",
                Text = "Include filtered devices in assignment",
                Role = AriaRole.Radio
            },
            ["FilterRadio_Exclude"] = new HealingHints
            {
                Identifier = "FilterRadio_Exclude",
                Text = "Exclude filtered devices in assignment",
                Role = AriaRole.Radio
            },
            ["FilterSearchBox"] = new HealingHints
            {
                Identifier = "FilterSearchBox",
                Role = AriaRole.Textbox,
                Label = "Search by name"
            },
            ["FilterSelectButton"] = new HealingHints
            {
                Identifier = "FilterSelectButton",
                Text = "Select",
                Role = AriaRole.Button,
                IsButton = true
            }
        };'''

if target in content:
    content = content.replace(target, replacement)
    with open(file_path, 'w', encoding='utf-8') as f:
        f.write(content)
    print('SUCCESS: HealingHintsRegistry updated')
else:
    # Try with \r\n
    target_crlf = target.replace('\n', '\r\n')
    if target_crlf in content:
        replacement_crlf = replacement.replace('\n', '\r\n')
        content = content.replace(target_crlf, replacement_crlf)
        with open(file_path, 'w', encoding='utf-8') as f:
            f.write(content)
        print('SUCCESS: HealingHintsRegistry updated (CRLF)')
    else:
        print('ERROR: Target not found')
        # Debug: show area around Dialog_YesButton
        lines = content.split('\n')
        for i, line in enumerate(lines):
            if 'Dialog_YesButton' in line:
                for j in range(max(0,i-1), min(len(lines), i+8)):
                    print(f'  L{j+1}: [{repr(lines[j])}]')
