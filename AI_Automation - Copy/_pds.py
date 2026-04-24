filepath = 'PlaywrightTests/Playwright/Common/Utils/DomScanner.cs'
with open(filepath, 'r', encoding='utf-8') as f:
    lines = f.readlines()

replacement = []
replacement.append('            if (match == null)\n')
replacement.append('            {\n')
replacement.append('                // DUMP ALL VISIBLE ELEMENTS when no match found\n')
replacement.append('            Console.WriteLine($"[DomScanner] NO MATCH for '{hints.Identifier}' in {_baselines.Count} baselines");\n')

