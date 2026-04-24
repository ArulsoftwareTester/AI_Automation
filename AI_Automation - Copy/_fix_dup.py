file_path = r'C:\Users\v-arulmani\Downloads\AI_Automation 5\AI_Automation - Copy\PlaywrightTests\Playwright\Common\Utils\BaseUtils\Apps\ByPlatform\AllAppsUtils.cs'
with open(file_path, 'r', encoding='utf-8-sig') as f:
    content = f.read()

# Find the SECOND #region Assignments Uninstall
first_pos = content.find('#region Assignments Uninstall')
second_pos = content.find('#region Assignments Uninstall', first_pos + 1)

if second_pos < 0:
    print('Only one region found, nothing to remove')
    exit(0)

print(f'First region at {first_pos}')
print(f'Second (duplicate) region at {second_pos}')

# Find the matching #endregion for the second region
# Also find the next #region after second_pos to know where to stop
next_region = content.find('#region Assignments Common Function', second_pos)
endregion = content.rfind('#endregion', second_pos, next_region)

if endregion < 0:
    # Find any #endregion after the second region
    endregion = content.find('#endregion', second_pos)

end_of_line = content.find('\n', endregion)

# Remove everything from the start of the line containing second_pos to end_of_line
# Find the start of the line containing second_pos
line_start = content.rfind('\n', 0, second_pos) + 1

block_to_remove = content[line_start:end_of_line+1]
print(f'Removing {len(block_to_remove)} chars from pos {line_start} to {end_of_line}')
print(f'First 80 chars: {block_to_remove[:80]}')
print(f'Last 80 chars: {block_to_remove[-80:]}')

content = content[:line_start] + content[end_of_line+1:]

with open(file_path, 'w', encoding='utf-8-sig') as f:
    f.write(content)
print('Duplicate region removed successfully')
