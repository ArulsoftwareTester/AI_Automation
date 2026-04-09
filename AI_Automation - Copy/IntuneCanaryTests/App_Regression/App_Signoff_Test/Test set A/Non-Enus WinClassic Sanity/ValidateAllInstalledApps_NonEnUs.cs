using AventStack.ExtentReports;
using NUnit.Framework;
using global::PlaywrightTests.Common.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    [Order(int.MaxValue)]
    public class ValidateAllInstalledApps_NonEnUs
    {
        private ExtentTest? _test;

        [SetUp]
        public void TestSetup()
        {
            _test = TestInitialize.CreateTest(
                TestContext.CurrentContext.Test.Name,
                "Validate All Installed Apps - Non-Enus WinClassic Sanity");
            _test.Info("Category: Test set A - Non-Enus WinClassic Sanity - Batch Validation");
        }

        [Test]
        public async Task ValidateAllPackagesViaWinget()
        {
            var testDataPath = Path.GetFullPath(Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "..", "..", "..", "..",
                "TestData_AppReggersion",
                "Non-Enus_winclassic_santity_required.json"));

            _test?.Info($"Loading test data from: {testDataPath}");

            using var stream = File.OpenRead(testDataPath);
            var root = JsonSerializer.Deserialize<ValidationRoot>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var testCases = root?.TestCases?
                .Where(tc => !string.IsNullOrWhiteSpace(tc.Parameters?.DeviceValidation?.SearchTerm))
                .ToList() ?? new List<ValidationTestCase>();

            _test?.Info($"Found {testCases.Count} test cases with searchTerm configured.");
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Found {testCases.Count} test cases to validate.");

            if (testCases.Count == 0)
            {
                _test?.Warning("No test cases with searchTerm found — skipping validation.");
                return;
            }

            // Wait 15 minutes for policy sync
            _test?.Info("Waiting 15 minutes for policies to sync to device...");
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Starting 15-minute wait for policy sync...");

            for (int i = 1; i <= 15; i++)
            {
                await Task.Delay(TimeSpan.FromMinutes(1));
                var waitMsg = $"[{DateTime.Now:HH:mm:ss}] Waited {i}/15 minutes...";
                Console.WriteLine(waitMsg);
                _test?.Info(waitMsg);
            }

            // Validate each package
            int passed = 0;
            int failed = 0;
            var failures = new List<string>();

            foreach (var tc in testCases)
            {
                var searchTerm = tc.Parameters!.DeviceValidation!.SearchTerm;
                var expectedValue = tc.Parameters.DeviceValidation.ExpectedValue;
                var testCaseId = tc.TestCaseId;

                _test?.Info($"[{testCaseId}] Validating: winget list --name \"{searchTerm}\" (expected: {expectedValue})");
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [{testCaseId}] Running: winget list --name \"{searchTerm}\"");

                var processInfo = new ProcessStartInfo
                {
                    FileName = "winget",
                    Arguments = $"list --name \"{searchTerm}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                string output;
                try
                {
                    using var process = Process.Start(processInfo);
                    output = await process!.StandardOutput.ReadToEndAsync();
                    await process.WaitForExitAsync();
                }
                catch (Exception ex)
                {
                    _test?.Warning($"[{testCaseId}] Failed to run winget: {ex.Message}");
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [{testCaseId}] winget error: {ex.Message}");
                    failures.Add($"{testCaseId}: winget command failed — {ex.Message}");
                    failed++;
                    continue;
                }

                bool appFound = output.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
                bool expectInstalled = expectedValue.Equals("installed", StringComparison.OrdinalIgnoreCase);

                if ((expectInstalled && appFound) || (!expectInstalled && !appFound))
                {
                    var status = expectInstalled ? "installed" : "not installed";
                    _test?.Pass($"[{testCaseId}] PASSED: \"{searchTerm}\" is {status} as expected.");
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [{testCaseId}] PASSED");
                    passed++;
                }
                else
                {
                    var actual = appFound ? "found (installed)" : "not found (not installed)";
                    var msg = $"[{testCaseId}] FAILED: \"{searchTerm}\" expected={expectedValue}, actual={actual}";
                    _test?.Fail(msg);
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {msg}");
                    failures.Add(msg);
                    failed++;
                }
            }

            // Summary
            var summary = $"Validation complete: {passed} passed, {failed} failed out of {testCases.Count} total.";
            Console.WriteLine($"\n[{DateTime.Now:HH:mm:ss}] {summary}");
            _test?.Info(summary);

            if (failures.Count > 0)
            {
                _test?.Fail($"Failed validations:\n{string.Join("\n", failures)}");
                Assert.Fail($"{failed} app validation(s) failed:\n{string.Join("\n", failures)}");
            }

            _test?.Pass("All app installation validations passed!");
        }

        // Lightweight model classes for JSON deserialization
        private sealed class ValidationRoot
        {
            [JsonPropertyName("testCases")]
            public List<ValidationTestCase> TestCases { get; set; } = new();
        }

        private sealed class ValidationTestCase
        {
            [JsonPropertyName("testCaseId")]
            public string TestCaseId { get; set; } = string.Empty;

            [JsonPropertyName("testName")]
            public string TestName { get; set; } = string.Empty;

            [JsonPropertyName("parameters")]
            public ValidationParameters? Parameters { get; set; }
        }

        private sealed class ValidationParameters
        {
            [JsonPropertyName("select app package file")]
            public string SelectAppPackageFile { get; set; } = string.Empty;

            [JsonPropertyName("Device Validation")]
            public ValidationDeviceValidation? DeviceValidation { get; set; }
        }

        private sealed class ValidationDeviceValidation
        {
            [JsonPropertyName("searchTerm")]
            public string SearchTerm { get; set; } = string.Empty;

            [JsonPropertyName("expectedValue")]
            public string ExpectedValue { get; set; } = string.Empty;
        }
    }
}
