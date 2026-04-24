import os

filepath = 'PlaywrightTests/Playwright/Common/Utils/DomScanner.cs'
with open(filepath, 'r', encoding='utf-8') as f:
    lines = f.readlines()

# Replace lines 237-239 (0-indexed) = lines 238-240 (1-indexed)
# Line 238: "            if (match == null) return hints;\n"
# Line 239: "\n"
# Line 240: '            Console.WriteLine($"[DomScanner] Enriching hints for ...\n'

replacement = []
replacement.append('            if (match == null)\n')
replacement.append('            {\n')
replacement.append('                // DUMP ALL VISIBLE ELEMENTS when no match found - mandatory for failure diagnosis\n')
replacement.append('                Console.WriteLine($"[DomScanner] NO MATCH for \'{hints.Identifier}\' in {_baselines.Count} baselines");\n')
replacement.append('                Console.WriteLine($"[DomScanner] SEARCH CRITERIA: AriaLabel=\'{hints.AriaLabel}\', Text=\'{hints.Text}\', Role=\'{hints.Role}\', Id=\'{hints.Id}\', ClassName=\'{hints.ClassName}\', Placeholder=\'{hints.Placeholder}\'");\n')
replacement.append('                DomPageBaseline dumpBaseline = null;\n')
replacement.append('                foreach (var bl in _baselines.Values) { if (pageLabel != null && bl.PageLabel != pageLabel) continue; dumpBaseline = bl; break; }\n')
replacement.append('                if (dumpBaseline == null) dumpBaseline = _baselines.Values.OrderByDescending(b => b.ScanTimestamp).FirstOrDefault();\n')
replacement.append('                if (dumpBaseline != null)\n')
replacement.append('                {\n')
replacement.append('                    var visibleEls = dumpBaseline.Elements.Where(e => e.IsVisible).ToList();\n')
replacement.append('                    Console.WriteLine($"[DomScanner] Page \'{dumpBaseline.PageLabel}\' total={dumpBaseline.TotalElements} visible={visibleEls.Count} iframe=\'{dumpBaseline.IframeName ?? \\"main\\"}\'");\n')
replacement.append('                    // Dump ALL input/textbox/searchbox elements\n')
replacement.append('                    var inputs = visibleEls.Where(e => e.TagName == "input" || e.TagName == "textarea" || e.TagName == "select" || e.AriaRole == "searchbox" || e.AriaRole == "textbox" || e.AriaRole == "combobox").ToList();\n')
replacement.append('                    if (inputs.Count > 0)\n')
replacement.append('                    {\n')
replacement.append('                        Console.WriteLine($"[DomScanner] === INPUT/TEXTBOX/SEARCHBOX ELEMENTS ({inputs.Count}) ===");\n')
replacement.append('                        foreach (var el in inputs)\n')
replacement.append('                            Console.WriteLine($"[DomScanner]   tag={el.TagName} role={el.AriaRole ?? \\"null\\"} aria-label=\'{el.AriaLabel ?? \\"null\\"}\' placeholder=\'{el.Placeholder ?? \\"null\\"}\' id=\'{el.Id ?? \\"null\\"}\' class=\'{TrimClassName(el.ClassName)}\' type=\'{el.Type ?? \\"null\\"}\' css=\'{el.CssSelector}\'");\n')
replacement.append('                    }\n')
replacement.append('                    // Dump ALL elements with aria-labels\n')
replacement.append('                    var ariaLabeled = visibleEls.Where(e => !string.IsNullOrEmpty(e.AriaLabel)).ToList();\n')
replacement.append('                    Console.WriteLine($"[DomScanner] === ALL ARIA-LABELED ELEMENTS ({ariaLabeled.Count}) ===");\n')
replacement.append('                    foreach (var el in ariaLabeled)\n')
replacement.append('                        Console.WriteLine($"[DomScanner]   [{el.AriaRole ?? el.TagName}] aria-label=\'{el.AriaLabel}\' text=\'{(el.Text != null && el.Text.Length > 40 ? el.Text.Substring(0, 40) + \\"...\\" : el.Text ?? \\"\\")}\' css=\'{el.CssSelector}\'");\n')
replacement.append('                    // Show PARTIAL matches\n')
replacement.append('                    if (!string.IsNullOrEmpty(hints.AriaLabel))\n')
replacement.append('                    {\n')
replacement.append('                        var partials = visibleEls.Where(e =>\n')
replacement.append('                            (e.AriaLabel != null && e.AriaLabel.Contains(hints.AriaLabel, StringComparison.OrdinalIgnoreCase)) ||\n')
replacement.append('                            (e.Text != null && e.Text.Contains(hints.AriaLabel, StringComparison.OrdinalIgnoreCase)) ||\n')
replacement.append('                            (e.Placeholder != null && e.Placeholder.Contains(hints.AriaLabel, StringComparison.OrdinalIgnoreCase))).ToList();\n')
replacement.append('                        if (partials.Count > 0)\n')
replacement.append('                        {\n')
replacement.append('                            Console.WriteLine($"[DomScanner] === PARTIAL MATCHES containing \'{hints.AriaLabel}\' ({partials.Count}) ===");\n')
replacement.append('                            foreach (var el in partials)\n')
replacement.append('                                Console.WriteLine($"[DomScanner]   PARTIAL: [{el.AriaRole ?? el.TagName}] aria-label=\'{el.AriaLabel ?? \\"null\\"}\' text=\'{(el.Text != null && el.Text.Length > 50 ? el.Text.Substring(0, 50) + \\"...\\" : el.Text ?? \\"\\")}\' placeholder=\'{el.Placeholder ?? \\"null\\"}\' css=\'{el.CssSelector}\'");\n')
replacement.append('                        }\n')
replacement.append('                        else\n')
replacement.append('                            Console.WriteLine($"[DomScanner] === NO PARTIAL MATCHES for \'{hints.AriaLabel}\' in any aria-label, text, or placeholder ===");\n')
replacement.append('                    }\n')
replacement.append('                }\n')
replacement.append('                return hints;\n')
replacement.append('            }\n')
replacement.append('\n')
replacement.append('            Console.WriteLine($"[DomScanner] MATCH FOUND for \'{hints.Identifier}\': tag={match.TagName} role={match.AriaRole ?? \\"null\\"} aria-label=\'{match.AriaLabel ?? \\"null\\"}\' css=\'{match.CssSelector}\'");\n')
replacement.append('            Console.WriteLine($"[DomScanner] Enriching hints for \'{hints.Identifier}\' from live DOM scan");\n')

# Replace lines 237, 238, 239 with the new block
new_lines = lines[:237] + replacement + lines[240:]

with open(filepath, 'w', encoding='utf-8') as f:
    f.writelines(new_lines)

print(f'SUCCESS: Replaced 3 lines (238-240) with {len(replacement)} lines')
print(f'Total lines: {len(lines)} -> {len(new_lines)}')