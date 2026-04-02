---
description: "Run a specific Intune test by case ID in headed or headless mode"
agent: "Orchestrator Agent"
---
Run test case `${{testCaseId}}` using `${{mode}}` mode (headed or headless).

1. Navigate to the AI_Automation - Copy directory
2. Run: `dotnet test --filter "FullyQualifiedName~${{testCaseId}}" --settings ${{mode}}.runsettings --logger "console;verbosity=detailed"`
3. Monitor output for pass/fail
4. If the test fails, follow the self-healing locator flow to diagnose and fix
5. Report the result with any healing actions taken
