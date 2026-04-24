import os

filepath = 'PlaywrightTests/Playwright/Common/Utils/DomScanner.cs'
with open(filepath, 'r', encoding='utf-8') as f:
    lines = f.readlines()

# Find line numbers for the target block
for i, line in enumerate(lines):
    if 'if (match == null) return hints;' in line:
        print(f'Found at line {i+1}: {repr(line)}')
    if "Enriching hints for" in line and "DomScanner" in line:
        print(f'Enriching at line {i+1}: {repr(line)}')
