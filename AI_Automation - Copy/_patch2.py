filepath = 'PlaywrightTests/Playwright/Common/Utils/DomScanner.cs'
with open(filepath, 'r', encoding='utf-8') as f:
    lines = f.readlines()

# Lines 238-240 (0-indexed: 237-239) need to be replaced
# Line 238: if (match == null) return hints;
# Line 239: (blank)
# Line 240: Console.WriteLine enriching

new_block = '''            if (match == null)
            {
                // DUMP ALL VISIBLE ELEMENTS when no match found - mandatory for failure diagnosis
                Console.WriteLine(dollar+"[DomScanner] NO MATCH for '{hints.Identifier}' in {_baselines.Count} baselines");
                Console.WriteLine(dollar+"[DomScanner] SEARCH CRITERIA: AriaLabel='{hints.AriaLabel}', Text='{hints.Text}', Role='{hints.Role}', Id='{hints.Id}', ClassName='{hints.ClassName}', Placeholder='{hints.Placeholder}'");
                DomPageBaseline dumpBaseline = null;
                foreach (var bl in _baselines.Values) { if (pageLabel != null {and}{and} bl.PageLabel != pageLabel) continue; dumpBaseline = bl; break; }
                if (dumpBaseline == null) dumpBaseline = _baselines.Values.OrderByDescending(b => b.ScanTimestamp).FirstOrDefault();
                if (dumpBaseline != null)
                {
                    var visibleEls = dumpBaseline.Elements.Where(e => e.IsVisible).ToList();
                    Console.WriteLine(dollar+"[DomScanner] Page '{dumpBaseline.PageLabel}' total={dumpBaseline.TotalElements} visible={visibleEls.Count} iframe='{dumpBaseline.IframeName {nullcoal} "+quote+"main"+quote+"}'"+"");
                }
                return hints;
            }

            Console.WriteLine(dollar+"[DomScanner] MATCH FOUND for '{hints.Identifier}': tag={match.TagName} role={match.AriaRole {nullcoal} "+quote+"null"+quote+"} aria-label='{match.AriaLabel {nullcoal} "+quote+"null"+quote+"}' css='{match.CssSelector}'");
            Console.WriteLine(dollar+"[DomScanner] Enriching hints for '{hints.Identifier}' from live DOM scan");
'''
print('Approach changed - using direct file write instead')
