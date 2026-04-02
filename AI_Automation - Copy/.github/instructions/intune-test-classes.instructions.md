---
description: "Use when writing, modifying, or generating C# test classes for Intune Canary Tests. Covers base class patterns, login methods, build workarounds, and ExtentReports conventions."
applyTo: "**/*.cs"
---
# Intune C# Test Class Conventions

## Base Class Pattern
- All test classes must extend `PageTest` (from `Microsoft.Playwright.NUnit`).
- Use abstract base classes with these required properties:
  - `TestId` — unique test case identifier
  - `TestTitle` — display name for ExtentReports
  - `TestDescription` — description logged during execution
- Concrete test classes inherit the base and override these properties plus call `RunTestAsync()`.

## Authentication
- Always use `SecurityBaseline.Login(IPage page)` for portal login.
- Do NOT use the legacy `IPLogin` method — it will not compile.
- Certificate-based auth uses PFX files from `auth-cert/` with relative path resolution via `AppDomain.CurrentDomain.BaseDirectory`.

## Test Setup
- Use `[SetUp]` to initialize ExtentReports via `TestInitialize.CreateTest(...)`.
- Log test start time, test ID, and category in SetUp.
- Capture screenshots with `ExtentReportHelper.CaptureScreenshot(Page, ...)` at key steps.

## Build Workaround
- When `IntuneCanaryTests.dll` is locked by an active testhost process, compile with:
  ```
  dotnet msbuild IntuneCanaryTests.csproj /t:Compile /p:BuildProjectReferences=false
  ```
- Do NOT attempt a full `dotnet build` when the DLL is locked.

## Namespaces
- All test classes use namespace `IntuneCanaryTests`.
- Common utilities are under `PlaywrightTests.Common.*` (Helper, Model, Utils).
