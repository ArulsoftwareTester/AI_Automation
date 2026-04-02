---
description: "Use when generating, modifying, or reviewing JSON test data files or PowerShell scripts that produce JSON from Excel. Covers schema rules, forbidden sections, and reference patterns."
applyTo: "**/*.json, **/*.ps1"
---
# JSON Test Data & Generation Conventions

## Reference Schema
- For Windows app regression JSON, always use `TestData_AppReggersion/APPReggersion_Create.json` as the reference schema and value pattern.
- Match its nested `parameters` structure: installation behavior, requirements, detection rules, dependencies, supercedence, assignments, device validation.

## Forbidden Sections
- Do NOT include an "Excel Steps" section in generated regression JSON files unless the user explicitly requests it.

## JSON Structure Rules
- Top-level key is `testCases` (array).
- Each test case must include: `testCaseId`, `testName`, `description`, `category`, `priority`, `enabled`, and `parameters`.
- Keep parameter nesting consistent with the reference file — do not flatten nested objects.

## PowerShell JSON Generation
- Scripts that generate JSON from Excel follow the extract-as-ZIP pattern: extract `.xlsx` → parse XML with `XmlNamespaceManager` → iterate cells via `Get-CellValue()` → output JSON.
- Preserve this pattern when creating new generation scripts.
