# MCP Server Implementation (2026-04-16)

## Project Location
- McpServer project: PlaywrightTests/McpServer/
- Entry point: PlaywrightTests/McpServer/Program.cs
- csproj: PlaywrightTests/McpServer/McpServer.csproj
- Added to solution: PlaywrightTests/PlaywrightTests.sln

## MCP Configuration
- Workspace mcp.json: .vscode/mcp.json (at workspace root AI_Automation 5/.vscode/)
- Server name: intune-playwright
- Transport: STDIO
- Command: dotnet run --project AI_Automation - Copy/PlaywrightTests/McpServer/McpServer.csproj
- Env: HEADED=true

## 28 Tools (6 categories)
1. Test Execution (3): list_tests, run_test, get_test_results
2. Self-Healing (4): heal_locator, get_healing_cache, lookup_healing_hints, record_permanent_fix
3. DOM Intelligence (5): scan_page_dom, scan_overlays, check_element_changed, enrich_hints, get_baseline
4. AI Locator (3): ai_find_locator, ai_extract_all_locators, ai_provider_status
5. UI Interaction (7): navigate_to, set_control, click_control, click_command_bar, find_element, get_page_info, list_control_types
6. Validation (6): take_screenshot, get_page_url, wait_for_page_load, evaluate_script, get_page_content, close_browser

## Agent Integration
- All 6 agents updated with intune-playwright/* tools
- Orchestrator, Debug, Fix, DOM Intelligence, Monitor, Memory
- Updated in both .github/agents/ (workspace root) and AI_Automation - Copy/.github/agents/

## Key Issues and Fixes

### publish/ folder type conflict (FIXED)
- dotnet publish -o ./publish dumped PlaywrightTests source into McpServer/publish/
- MSBuild compiled those .cs files again alongside ProjectReference = CS0436 duplicate types
- Fix: Delete publish/ and nupkg/ folders, add DefaultItemExcludes to csproj

### PackAsTool not supported (KNOWN LIMITATION)
- PackAsTool doesnt work with net8.0-windows8.0 or UseWPF/UseWindowsForms
- Use dotnet publish for distribution instead of dotnet tool
- Published zip: intune-mcp-server-v1.0.0.zip (334 MB framework-dependent)

### Tool discovery in Copilot Chat
- MCP server starts successfully, VS Code discovers 28 tools
- Tools appear in Configure Tools panel under intune-playwright
- Tools must be enabled in agent config AND new chat session started to use them
- Start new chat after enabling tools for them to be callable

## How to Start
1. Ctrl+Shift+P then MCP: List Servers then Start intune-playwright
2. Or VS Code auto-starts when Copilot calls any tool
3. Verify: logs show Discovered 28 tools and Application started

## How to Share
- Git (recommended): All config in repo - .vscode/mcp.json + .github/agents/ + McpServer/
- Zip: dotnet publish -c Release -r win-x64 --self-contained false -o ./publish
- Teammates need: .NET 8 runtime + Playwright browsers

## Dependencies (in Directory.Packages.props)
- ModelContextProtocol v1.2.0
- ModelContextProtocol.AspNetCore v1.2.0
- Microsoft.Extensions.Hosting v9.0.0

## IMPORTANT: Dont kill all dotnet processes
- taskkill on all dotnet processes kills the MCP server too
- Only kill testhost* processes when stopping stuck tests
- MCP server must be restarted from MCP: List Servers after being killed
