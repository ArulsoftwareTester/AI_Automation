using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.Playwright;

namespace IntuneCanaryTests
{
    public static class ExtentReportHelper
    {
        private static ExtentReports? _extent;
        private static readonly object _lock = new object();
        private static string _reportPath = string.Empty;
        private static DateTime _executionStartTime;
        private static DateTime _executionEndTime;

        public static ExtentReports GetExtent()
        {
            if (_extent == null)
            {
                lock (_lock)
                {
                    if (_extent == null)
                    {
                        // Get project root directory (4 levels up from bin/Debug/net10.0-windows)
                        string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", ".."));
                        string reportsDir = Path.Combine(projectRoot, "ExtentReports");
                        Directory.CreateDirectory(reportsDir);

                        // Create Screenshots directory
                        string screenshotsDir = Path.Combine(reportsDir, "Screenshots");
                        Directory.CreateDirectory(screenshotsDir);

                        // Generate report file name with timestamp
                        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        _reportPath = Path.Combine(reportsDir, $"TestReport_{timestamp}.html");

                        // Initialize ExtentReports
                        var htmlReporter = new ExtentSparkReporter(_reportPath);
                        
                        // Configure HTML Reporter
                        htmlReporter.Config.DocumentTitle = "Intune Security Baseline Test Report";
                        htmlReporter.Config.ReportName = "Windows 365 Security Baseline Tests";
                        htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Dark;
                        htmlReporter.Config.Encoding = "UTF-8";
                        
                        _extent = new ExtentReports();
                        _extent.AttachReporter(htmlReporter);
                        
                        // Record execution start time
                        _executionStartTime = DateTime.Now;
                        
                        // Add system/environment information
                        _extent.AddSystemInfo("Environment", "Production Canary");
                        _extent.AddSystemInfo("User", Environment.UserName);
                        _extent.AddSystemInfo("Machine", Environment.MachineName);
                        _extent.AddSystemInfo("OS", Environment.OSVersion.ToString());
                        _extent.AddSystemInfo(".NET Version", Environment.Version.ToString());
                        _extent.AddSystemInfo("Execution Start Time", _executionStartTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }
            }
            return _extent;
        }

        public static void FlushReport()
        {
            if (_extent != null)
            {
                // Record execution end time and calculate duration
                _executionEndTime = DateTime.Now;
                TimeSpan totalDuration = _executionEndTime - _executionStartTime;
                
                // Add execution time information to the report
                _extent.AddSystemInfo("Execution End Time", _executionEndTime.ToString("yyyy-MM-dd HH:mm:ss"));
                _extent.AddSystemInfo("Total Execution Time", FormatDuration(totalDuration));
                
                _extent.Flush();
                Console.WriteLine($"\n========================================");
                Console.WriteLine($"Extent Report Generated: {_reportPath}");
                Console.WriteLine($"Total Execution Time: {FormatDuration(totalDuration)}");
                Console.WriteLine($"========================================\n");
            }
        }

        public static string GetTotalExecutionTime()
        {
            if (_executionStartTime == default)
                return "N/A";
            
            TimeSpan duration = (_executionEndTime == default ? DateTime.Now : _executionEndTime) - _executionStartTime;
            return FormatDuration(duration);
        }

        private static string FormatDuration(TimeSpan duration)
        {
            if (duration.TotalHours >= 1)
            {
                return $"{(int)duration.TotalHours}h {duration.Minutes}m {duration.Seconds}s";
            }
            else if (duration.TotalMinutes >= 1)
            {
                return $"{(int)duration.TotalMinutes}m {duration.Seconds}s";
            }
            else
            {
                return $"{duration.Seconds}.{duration.Milliseconds:D3}s";
            }
        }

        public static string GetReportPath()
        {
            return _reportPath;
        }

        public static void OpenReport()
        {
            if (!string.IsNullOrEmpty(_reportPath) && File.Exists(_reportPath))
            {
                try
                {
                    Console.WriteLine($"Opening Extent Report in browser...");
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = _reportPath,
                        UseShellExecute = true
                    });
                    Console.WriteLine($"Extent Report opened successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to open report: {ex.Message}");
                }
            }
        }

        public static async Task<string> CaptureScreenshot(IPage page, string testName)
        {
            try
            {
                // Get project root directory (4 levels up from bin/Debug/net10.0-windows)
                string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", ".."));
                string screenshotDir = Path.Combine(projectRoot, "ExtentReports", "Screenshots");
                Directory.CreateDirectory(screenshotDir);

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
                string fileName = $"{testName}_{timestamp}.png";
                string filePath = Path.Combine(screenshotDir, fileName);

                await page.ScreenshotAsync(new PageScreenshotOptions
                {
                    Path = filePath,
                    FullPage = true
                });

                return filePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to capture screenshot: {ex.Message}");
                return string.Empty;
            }
        }
    }
}
