## test-run-monitoring.md
## MANDATORY: Active Test Monitoring from First Second

- **IMMEDIATELY after launching dotnet test as async/background**, start polling get_terminal_output actively
- Do NOT wait passively or say 'tests are running, I'll check later' — MONITOR CONTINUOUSLY
- Poll every 15-30 seconds from the moment the test starts, not just when you think output might appear
- Parse EVERY poll for: step logs, failure signals, retry counts, locator data, iframe load events
- The console output is BUFFERED — NUnit may hold output until test completes, but STILL POLL because:
  - Build errors appear immediately
  - Discovery phase output appears early
  - Some step logs stream in real-time depending on logger config
- When test completes, react IMMEDIATELY — do not wait for user to ask

### Anti-Patterns (NEVER DO):
- 'Tests are still running, I'll check again shortly' — then NOT checking for minutes
- Passive waiting without polling
- Saying 'you'll see the browsers' instead of actively monitoring
- Multiple polls with identical output and no analysis between them
- Treating monitoring as optional — it is MANDATORY

### Correct Pattern:
1. Launch test as async
2. First poll within 15 seconds
3. Parse output on every poll — extract locators, step progress, timing data
4. If output unchanged after 3 polls, note 'test still executing, no new output' but KEEP POLLING
5. On completion signal (Passed/Failed), immediately analyze and report
