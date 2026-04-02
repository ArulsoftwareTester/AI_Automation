using AventStack.ExtentReports;
using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace IntuneCanaryTests
{
    [SetUpFixture]
    public class TestInitialize
    {
        private static ExtentReports? _extent;
        private static readonly AsyncLocal<ExtentTest?> _test = new AsyncLocal<ExtentTest?>();

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            _extent = ExtentReportHelper.GetExtent();
            Console.WriteLine("========================================");
            Console.WriteLine("Test Suite Execution Started");
            Console.WriteLine($"Time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine("========================================");
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            ExtentReportHelper.FlushReport();
            ExtentReportHelper.OpenReport();
            Console.WriteLine("========================================");
            Console.WriteLine("Test Suite Execution Completed");
            Console.WriteLine($"End Time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Total Execution Time: {ExtentReportHelper.GetTotalExecutionTime()}");
            Console.WriteLine("========================================");
        }

        public static ExtentTest CreateTest(string testName, string? description = null)
        {
            if (_extent == null)
            {
                throw new InvalidOperationException("ExtentReports is not initialized. Ensure GlobalSetup has been called.");
            }

            var test = string.IsNullOrEmpty(description) 
                ? _extent.CreateTest(testName) 
                : _extent.CreateTest(testName, description);
            
            _test.Value = test;
            return test;
        }

        public static ExtentTest? GetTest()
        {
            return _test.Value;
        }

        public static void LogTestResult(TestContext context)
        {
            var test = GetTest();
            if (test == null) return;

            var status = context.Result.Outcome.Status;
            var message = context.Result.Message;
            var stackTrace = context.Result.StackTrace;

            switch (status)
            {
                case TestStatus.Passed:
                    test.Pass("Test Passed");
                    break;
                case TestStatus.Failed:
                    test.Fail($"Test Failed: {message}");
                    if (!string.IsNullOrEmpty(stackTrace))
                    {
                        test.Fail($"<pre>{stackTrace}</pre>");
                    }
                    break;
                case TestStatus.Skipped:
                    test.Skip("Test Skipped");
                    break;
                case TestStatus.Inconclusive:
                    test.Warning("Test Inconclusive");
                    break;
            }
        }

        public static async Task LogStep(IPage page, string stepDescription, string screenshotName = "")
        {
            var test = GetTest();
            if (test == null) return;

            try
            {
                // Capture screenshot
                var screenshot = await ExtentReportHelper.CaptureScreenshot(page, string.IsNullOrEmpty(screenshotName) ? stepDescription : screenshotName);
                
                // Log step with screenshot
                if (!string.IsNullOrEmpty(screenshot) && File.Exists(screenshot))
                {
                    test.Info(stepDescription, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshot).Build());
                }
                else
                {
                    test.Info(stepDescription);
                }

                // Console log for visibility
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] [Step] {stepDescription}");
            }
            catch (Exception ex)
            {
                test.Warning($"{stepDescription} - Screenshot failed: {ex.Message}");
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] [Step] {stepDescription} - Screenshot failed");
            }
        }

        public static async Task LogSuccess(IPage page, string message, string screenshotName = "")
        {
            var test = GetTest();
            if (test == null) return;

            try
            {
                var screenshot = await ExtentReportHelper.CaptureScreenshot(page, string.IsNullOrEmpty(screenshotName) ? message : screenshotName);
                
                if (!string.IsNullOrEmpty(screenshot) && File.Exists(screenshot))
                {
                    test.Pass(message, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshot).Build());
                }
                else
                {
                    test.Pass(message);
                }

                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] [Success] {message}");
            }
            catch (Exception ex)
            {
                test.Pass($"{message} - Screenshot failed: {ex.Message}");
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] [Success] {message}");
            }
        }

        public static async Task LogFailure(IPage page, string message, string screenshotName = "")
        {
            var test = GetTest();
            if (test == null) return;

            try
            {
                var screenshot = await ExtentReportHelper.CaptureScreenshot(page, string.IsNullOrEmpty(screenshotName) ? message : screenshotName);
                
                if (!string.IsNullOrEmpty(screenshot) && File.Exists(screenshot))
                {
                    test.Fail(message, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshot).Build());
                }
                else
                {
                    test.Fail(message);
                }

                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] [Failure] {message}");
            }
            catch (Exception ex)
            {
                test.Fail($"{message} - Screenshot failed: {ex.Message}");
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] [Failure] {message}");
            }
        }

        public static async Task LogWarning(IPage page, string message, string screenshotName = "")
        {
            var test = GetTest();
            if (test == null) return;

            try
            {
                var screenshot = await ExtentReportHelper.CaptureScreenshot(page, string.IsNullOrEmpty(screenshotName) ? message : screenshotName);
                
                if (!string.IsNullOrEmpty(screenshot) && File.Exists(screenshot))
                {
                    test.Warning(message, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshot).Build());
                }
                else
                {
                    test.Warning(message);
                }

                Console.WriteLine($"[Warning] {message}");
            }
            catch (Exception ex)
            {
                test.Warning($"{message} - Screenshot failed: {ex.Message}");
                Console.WriteLine($"[Warning] {message}");
            }
        }
    }
}
