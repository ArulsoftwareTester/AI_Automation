import re

f = r'c:\Users\v-arulmani\Downloads\AI_Automation 5\AI_Automation - Copy\PlaywrightTests\Playwright\Common\Helper\ElementHelper.cs'
with open(f, 'r', encoding='utf-8') as fh:
    content = fh.read()

# Target: the first IsExistAsync (ILocator only, no IPage) healing block
# It has "via AsyncLocal page" in the log message
old_marker = 'var page = _currentPage.Value;\n                            if (page != null)\n                            {\n                                string locatorStr = locator.ToString();\n                                LogHelper.Warning'
idx = content.find(old_marker)
print(f'Found old marker at index: {idx}')

if idx < 0:
    print('ERROR: Could not find target text')
    exit(1)

# Find the full block to replace: from "var page = _currentPage.Value;" to the matching iframeName: null
# Replace just the key lines
old_text = '''                            var page = _currentPage.Value;
                            if (page != null)
                            {
                                string locatorStr = locator.ToString();
                                LogHelper.Warning(\$"[HEAL_SIGNAL] retryCount={count} locator={locatorStr} \u2014 triggering dynamic self-healing (via AsyncLocal page)");
                                try
                                {
                                    var hints = Utils.HealingHintsRegistry.GetOrBuildFromText(
                                        Utils.HealingHints.ParseFromLocator(locatorStr).Identifier, null);
                                    if (hints == null)
                                    {
                                        hints = Utils.HealingHints.ParseFromLocator(locatorStr);
                                        LogHelper.Info(\$"[SelfHealing] Auto-parsed hints: Role={hints.Role}, Text='{hints.Text}', AriaLabel='{hints.AriaLabel}'");
                                    }
                                    else
                                    {
                                        LogHelper.Info(\$"[SelfHealing] Using registered hints for '{hints.Identifier}'");
                                    }
                                    var healedLocator = await Utils.SelfHealingLocator.ResolveAsync(
                                        page, locator, hints, iframeName: null, timeoutMs: 5000);'''

new_text = '''                            var page = _currentPage.Value;

                            // Fallback: extract page from the locator itself (ILocator.Page)
                            if (page == null)
                            {
                                try { page = locator.Page; } catch { }
                            }

                            if (page != null)
                            {
                                string locatorStr = locator.ToString();

                                // Extract iframe name from locator string if present
                                string iframeName = null;
                                var iframeMatch = System.Text.RegularExpressions.Regex.Match(
                                    locatorStr, @"iframe\\[name=""([^""]+)""\\]");
                                if (iframeMatch.Success)
                                    iframeName = iframeMatch.Groups[1].Value;

                                LogHelper.Warning(\$"[HEAL_SIGNAL] retryCount={count} locator={locatorStr} \u2014 triggering dynamic self-healing (page from {(_currentPage.Value != null ? \\"AsyncLocal\\" : \\"locator.Page\\")}, iframe={iframeName ?? \\"none\\"})");
                                try
                                {
                                    var hints = Utils.HealingHintsRegistry.GetOrBuildFromText(
                                        Utils.HealingHints.ParseFromLocator(locatorStr).Identifier, null);
                                    if (hints == null)
                                    {
                                        hints = Utils.HealingHints.ParseFromLocator(locatorStr);
                                        LogHelper.Info(\$"[SelfHealing] Auto-parsed hints: Role={hints.Role}, Text='{hints.Text}', AriaLabel='{hints.AriaLabel}'");
                                    }
                                    else
                                    {
                                        LogHelper.Info(\$"[SelfHealing] Using registered hints for '{hints.Identifier}'");
                                    }
                                    var healedLocator = await Utils.SelfHealingLocator.ResolveAsync(
                                        page, locator, hints, iframeName: iframeName, timeoutMs: 5000);'''

idx2 = content.find(old_text)
print(f'Found full old text at index: {idx2}')
if idx2 >= 0:
    content = content[:idx2] + new_text + content[idx2+len(old_text):]
    with open(f, 'w', encoding='utf-8') as fh:
        fh.write(content)
    print('SUCCESS: Replaced healing block in IsExistAsync(ILocator)')
else:
    print('ERROR: Could not find full old text')
