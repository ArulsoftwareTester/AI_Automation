# Intune Test Automation  Workspace Instructions

## Project Overview
This is a Playwright-based C# test automation framework for Microsoft Intune portal testing. All code lives under PlaywrightTests/ and IntuneCanaryTests/.

## Code Style
- C# with NUnit test framework and Microsoft.Playwright.NUnit
- Async/await throughout  all test methods are async
- Use ar for local variables when the type is obvious from context

## Architecture
- **PlaywrightTests/**  Reusable framework: page objects, helpers, utilities, models
- **IntuneCanaryTests/**  Test classes organized by feature area (App Regression, Security Baseline, etc.)
- **TestData/** / **TestData_AppReggersion/**  JSON-driven test data
- **ExtentReports/**  HTML test reports generated per run

## Build and Test
- Build: `dotnet build`
- Run all tests: `dotnet test --settings headed.runsettings`
- Run specific test: `dotnet test --filter "FullyQualifiedName~TestMethodName" --settings headed.runsettings`
- When DLL is locked: `dotnet msbuild IntuneCanaryTests.csproj /t:Compile /p:BuildProjectReferences=false`

## Conventions
- NEVER use raw Playwright API calls  always use existing helpers (ElementHelper, ControlHelper, Controls, Navigation)
- NEVER create new locators or selectors when existing page objects cover them
- Use SelfHealingLocator for fragile elements instead of hardcoding new selectors
- All test classes extend PageTest and use abstract base class pattern with TestId/TestTitle/TestDescription
- Authentication always via SecurityBaseline.Login()  never IPLogin
- JSON test data follows the schema in TestData_AppReggersion/APPReggersion_Create.json
- Do NOT include "Excel Steps" in generated JSON unless explicitly requested
