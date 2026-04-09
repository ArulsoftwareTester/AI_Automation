file_path = r'IntuneCanaryTests\App_Regression\App_Signoff_Test\WinGet App Test Cases\WinGetStoreAppRegressionTestBase.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    content = f.read()

# Find the exact line with "Step failed:"
import re
m = re.search(r'Step failed: \{stepDescription\}(.*?)\{ex\.Message\}', content)
if m:
    sep = m.group(1)
    print("Separator bytes:", sep.encode('utf-8').hex())
    print("Separator repr:", repr(sep))
else:
    print("Not found via regex")
    idx = content.find('Step failed:')
    if idx >= 0:
        print("Raw context:", repr(content[idx:idx+80]))
