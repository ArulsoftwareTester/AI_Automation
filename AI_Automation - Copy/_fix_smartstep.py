path = r'PlaywrightTests\Playwright\Common\Utils\SmartStepExecutor.cs'
with open(path, 'r', encoding='utf-8-sig') as f:
    content = f.read()

# Replace the two call sites that use single-param GetExpectedTab
# 1. Retry hook (line ~48): var expectedTab = WizardFlowRegistry.GetExpectedTab(controlInfo.ControlType);
old1 = 'var expectedTab = WizardFlowRegistry.GetExpectedTab(controlInfo.ControlType);'
new1 = 'var expectedTab = WizardFlowRegistry.GetExpectedTab(controlInfo.ControlType, _appType);'

# 2. Pre-hook (line ~67): var expectedTab = WizardFlowRegistry.GetExpectedTab(controlType);
old2 = 'var expectedTab = WizardFlowRegistry.GetExpectedTab(controlType);'
new2 = 'var expectedTab = WizardFlowRegistry.GetExpectedTab(controlType, _appType);'

count1 = content.count(old1)
count2 = content.count(old2)
print(f'Found {count1} occurrences of retry-hook call')
print(f'Found {count2} occurrences of pre-hook call')

content = content.replace(old1, new1)
content = content.replace(old2, new2)

with open(path, 'w', encoding='utf-8') as f:
    f.write(content)
print('SUCCESS: Updated SmartStepExecutor.cs')
