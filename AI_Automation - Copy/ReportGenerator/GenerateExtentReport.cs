using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Net;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

class Program
{
    static void Main(string[] args)
    {
        string trxPath = args.Length > 0 ? args[0] : @"C:\AI_Automation\IntuneCanaryTests\TestResults\EndpointSecurity_TestResults.trx";
        string reportDir = @"C:\AI_Automation\ExtentReports";
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string reportPath = Path.Combine(reportDir, $"TestReport_{timestamp}.html");

        Console.WriteLine($"Reading TRX file: {trxPath}");
        Console.WriteLine($"Generating report: {reportPath}");

        // Ensure report directory exists
        Directory.CreateDirectory(reportDir);

        // Initialize ExtentReports
        var extent = new ExtentReports();
        var spark = new ExtentSparkReporter(reportPath);
        spark.Config.DocumentTitle = "Endpoint Security Test Report";
        spark.Config.ReportName = "Intune Canary Tests - Endpoint Security";
        spark.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Standard;
        extent.AttachReporter(spark);

        // Add system info
        extent.AddSystemInfo("Environment", "SH (SelfHost)");
        extent.AddSystemInfo("Test Framework", "NUnit + Playwright");
        extent.AddSystemInfo("Browser", "Chromium (Headless)");
        extent.AddSystemInfo("Test Run Date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        extent.AddSystemInfo("Machine", Environment.MachineName);

        // Parse TRX file
        XDocument doc = XDocument.Load(trxPath);
        XNamespace ns = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010";

        var results = doc.Descendants(ns + "UnitTestResult");

        int passed = 0, failed = 0, skipped = 0;

        foreach (var result in results)
        {
            string testName = result.Attribute("testName")?.Value ?? "Unknown";
            string outcome = result.Attribute("outcome")?.Value ?? "Unknown";
            string duration = result.Attribute("duration")?.Value ?? "00:00:00";
            
            var test = extent.CreateTest(testName);

            // Get stdout if available
            var stdOut = result.Descendants(ns + "StdOut").FirstOrDefault()?.Value;
            if (!string.IsNullOrEmpty(stdOut))
            {
                // Truncate long output
                if (stdOut.Length > 2000)
                {
                    stdOut = stdOut.Substring(0, 2000) + "\n... [truncated]";
                }
                test.Info($"<pre>{WebUtility.HtmlEncode(stdOut)}</pre>");
            }

            // Get error info if available
            var errorMessage = result.Descendants(ns + "Message").FirstOrDefault()?.Value;
            var stackTrace = result.Descendants(ns + "StackTrace").FirstOrDefault()?.Value;

            switch (outcome.ToLower())
            {
                case "passed":
                    test.Pass($"Test passed in {duration}");
                    passed++;
                    break;
                case "failed":
                    string errorDetails = "";
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        errorDetails = $"<b>Error:</b> {WebUtility.HtmlEncode(errorMessage)}";
                    }
                    if (!string.IsNullOrEmpty(stackTrace))
                    {
                        // Truncate long stack traces
                        if (stackTrace.Length > 1000)
                        {
                            stackTrace = stackTrace.Substring(0, 1000) + "\n... [truncated]";
                        }
                        errorDetails += $"<br/><br/><b>Stack Trace:</b><pre>{WebUtility.HtmlEncode(stackTrace)}</pre>";
                    }
                    test.Fail(errorDetails);
                    failed++;
                    break;
                case "notexecuted":
                case "skipped":
                    test.Skip("Test was skipped");
                    skipped++;
                    break;
                default:
                    test.Warning($"Unknown outcome: {outcome}");
                    break;
            }
        }

        extent.Flush();

        Console.WriteLine();
        Console.WriteLine("=".PadRight(60, '='));
        Console.WriteLine("TEST EXECUTION SUMMARY");
        Console.WriteLine("=".PadRight(60, '='));
        Console.WriteLine($"Total Tests: {passed + failed + skipped}");
        Console.WriteLine($"Passed:      {passed}");
        Console.WriteLine($"Failed:      {failed}");
        Console.WriteLine($"Skipped:     {skipped}");
        Console.WriteLine($"Pass Rate:   {(passed * 100.0 / (passed + failed + skipped)):F1}%");
        Console.WriteLine("=".PadRight(60, '='));
        Console.WriteLine();
        Console.WriteLine($"Report generated: {reportPath}");
    }
}
