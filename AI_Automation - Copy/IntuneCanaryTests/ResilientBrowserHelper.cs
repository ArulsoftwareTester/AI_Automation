using Microsoft.Playwright;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Management;
using System.Linq;

namespace IntuneCanaryTests
{
    /// <summary>
    /// Helper class to provide resilient browser operations with retry logic
    /// to handle browser launch failures and process crashes.
    /// </summary>
    public static class ResilientBrowserHelper
    {
        private const int MaxRetries = 3;
        private const int RetryDelayMs = 2000;
        private const int BrowserLaunchTimeoutMs = 60000; // 60 seconds

        /// <summary>
        /// Launches a browser with retry logic to handle failures.
        /// </summary>
        public static async Task<IBrowser> LaunchBrowserWithRetryAsync(
            IBrowserType browserType, 
            BrowserTypeLaunchOptions? options = null,
            int maxRetries = MaxRetries)
        {
            Exception? lastException = null;
            
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    Console.WriteLine($"[BrowserLaunch] Attempt {attempt}/{maxRetries} - Starting browser launch...");
                    
                    // Kill any orphaned browser processes before launching
                    if (attempt > 1)
                    {
                        CleanupOrphanedBrowserProcesses();
                        await Task.Delay(RetryDelayMs * attempt); // Exponential backoff
                    }

                    // Set launch options with timeout
                    var launchOptions = options ?? new BrowserTypeLaunchOptions();
                    launchOptions.Timeout = BrowserLaunchTimeoutMs;
                    
                    // Add stability arguments for headless mode
                    if (launchOptions.Headless == true)
                    {
                        var args = new System.Collections.Generic.List<string>(launchOptions.Args ?? Array.Empty<string>())
                        {
                            "--disable-dev-shm-usage",           // Overcome limited resource problems
                            "--disable-gpu",                     // Disable GPU hardware acceleration
                            "--no-sandbox",                      // Disable sandbox for stability
                            "--disable-setuid-sandbox",          // Disable setuid sandbox
                            "--disable-web-security",            // Disable web security
                            "--disable-features=VizDisplayCompositor", // Reduce memory usage
                            "--disable-software-rasterizer"      // Disable software rasterizer
                        };
                        launchOptions.Args = args.ToArray();
                    }

                    var browser = await browserType.LaunchAsync(launchOptions);
                    Console.WriteLine($"[BrowserLaunch] ✓ Browser launched successfully on attempt {attempt}");
                    return browser;
                }
                catch (Exception ex) when (ex is Microsoft.Playwright.PlaywrightException ||
                                          ex is TimeoutException)
                {
                    lastException = ex;
                    Console.WriteLine($"[BrowserLaunch] ✗ Attempt {attempt}/{maxRetries} failed: {ex.Message}");
                    
                    if (attempt == maxRetries)
                    {
                        Console.WriteLine($"[BrowserLaunch] All {maxRetries} attempts failed. Giving up.");
                        break;
                    }
                    
                    Console.WriteLine($"[BrowserLaunch] Waiting {RetryDelayMs * attempt}ms before retry...");
                }
            }

            throw new Exception($"Failed to launch browser after {maxRetries} attempts. Last error: {lastException?.Message}", lastException);
        }

        /// <summary>
        /// Executes a browser operation with retry logic.
        /// </summary>
        public static async Task<T> ExecuteWithRetryAsync<T>(
            Func<Task<T>> operation,
            string operationName,
            int maxRetries = 2)
        {
            Exception? lastException = null;
            
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    Console.WriteLine($"[{operationName}] Attempt {attempt}/{maxRetries}");
                    var result = await operation();
                    Console.WriteLine($"[{operationName}] ✓ Succeeded on attempt {attempt}");
                    return result;
                }
                catch (Exception ex) when (ex is Microsoft.Playwright.PlaywrightException)
                {
                    lastException = ex;
                    Console.WriteLine($"[{operationName}] ✗ Attempt {attempt}/{maxRetries} failed: {ex.Message}");
                    
                    if (attempt < maxRetries)
                    {
                        await Task.Delay(RetryDelayMs);
                    }
                }
            }

            throw new Exception($"{operationName} failed after {maxRetries} attempts. Last error: {lastException?.Message}", lastException);
        }

        /// <summary>
        /// Cleans up orphaned browser processes (chromium, msedge, etc.)
        /// Excludes Microsoft Teams and only targets Playwright-launched browsers
        /// </summary>
        public static void CleanupOrphanedBrowserProcesses()
        {
            try
            {
                Console.WriteLine("[Cleanup] Checking for orphaned browser processes...");
                var processesToKill = new[] { "chrome", "chromium", "msedge", "msedge.exe", "chrome.exe", "chromium.exe" };
                int killedCount = 0;
                int skippedTeamsCount = 0;
                int skippedNonPlaywrightCount = 0;

                foreach (var processName in processesToKill)
                {
                    try
                    {
                        var processes = Process.GetProcessesByName(processName);
                        foreach (var process in processes)
                        {
                            try
                            {
                                // Only kill processes older than 10 minutes (likely orphaned)
                                var processAge = DateTime.Now - process.StartTime;
                                if (processAge.TotalMinutes > 10)
                                {
                                    // OPTION 2: Skip if this is a Teams-related process
                                    if (IsTeamsRelatedProcess(process))
                                    {
                                        Console.WriteLine($"[Cleanup] Skipping Teams process: {process.ProcessName} (PID: {process.Id})");
                                        skippedTeamsCount++;
                                        continue;
                                    }

                                    // OPTION 1: Only kill if it's a Playwright/test browser
                                    if (!IsPlaywrightBrowserProcess(process))
                                    {
                                        Console.WriteLine($"[Cleanup] Skipping non-Playwright process: {process.ProcessName} (PID: {process.Id})");
                                        skippedNonPlaywrightCount++;
                                        continue;
                                    }

                                    Console.WriteLine($"[Cleanup] Killing orphaned Playwright process: {process.ProcessName} (PID: {process.Id}, Age: {processAge.TotalMinutes:F1} min)");
                                    process.Kill(true);
                                    killedCount++;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"[Cleanup] Could not process {processName} (PID: {process.Id}): {ex.Message}");
                            }
                        }
                    }
                    catch { /* Process not found - that's OK */ }
                }

                Console.WriteLine($"[Cleanup] Summary: Killed={killedCount}, Skipped Teams={skippedTeamsCount}, Skipped Non-Playwright={skippedNonPlaywrightCount}");
                
                if (killedCount > 0)
                {
                    System.Threading.Thread.Sleep(1000); // Wait for cleanup
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Cleanup] Error during cleanup: {ex.Message}");
            }
        }

        /// <summary>
        /// Checks if a process is related to Microsoft Teams
        /// </summary>
        private static bool IsTeamsRelatedProcess(Process process)
        {
            try
            {
                // Check the process path
                string processPath = process.MainModule?.FileName ?? "";
                if (processPath.Contains("Teams", StringComparison.OrdinalIgnoreCase) ||
                    processPath.Contains("ms-teams", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                // Check parent process
                var parentProcess = GetParentProcess(process);
                if (parentProcess != null)
                {
                    string parentName = parentProcess.ProcessName;
                    if (parentName.Contains("Teams", StringComparison.OrdinalIgnoreCase) ||
                        parentName.Contains("ms-teams", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }

                // Check command line for Teams-related parameters
                string commandLine = GetProcessCommandLine(process.Id);
                if (commandLine.Contains("Teams", StringComparison.OrdinalIgnoreCase) ||
                    commandLine.Contains("ms-teams", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            catch
            {
                // If we can't determine, be safe and don't kill it
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if a process is a Playwright-launched browser
        /// </summary>
        private static bool IsPlaywrightBrowserProcess(Process process)
        {
            try
            {
                string commandLine = GetProcessCommandLine(process.Id);
                
                // Playwright browsers have specific command-line arguments
                // Check for remote debugging port (used by Playwright for automation)
                if (commandLine.Contains("--remote-debugging-port") ||
                    commandLine.Contains("--remote-debugging-pipe"))
                {
                    // Exclude child renderer/utility processes (only kill main browser process)
                    if (!commandLine.Contains("--type=renderer") &&
                        !commandLine.Contains("--type=gpu-process") &&
                        !commandLine.Contains("--type=utility"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                // If we can't determine, be safe and don't kill it
                return false;
            }

            return false;
        }

        /// <summary>
        /// Gets the command line arguments of a process
        /// </summary>
        private static string GetProcessCommandLine(int processId)
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher(
                    $"SELECT CommandLine FROM Win32_Process WHERE ProcessId = {processId}"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        return obj["CommandLine"]?.ToString() ?? "";
                    }
                }
            }
            catch
            {
                // Fall back to empty string if we can't get command line
            }

            return "";
        }

        /// <summary>
        /// Gets the parent process of a given process
        /// </summary>
        private static Process? GetParentProcess(Process process)
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher(
                    $"SELECT ParentProcessId FROM Win32_Process WHERE ProcessId = {process.Id}"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        int parentId = Convert.ToInt32(obj["ParentProcessId"]);
                        return Process.GetProcessById(parentId);
                    }
                }
            }
            catch
            {
                // Parent process may not exist or be accessible
            }

            return null;
        }

        /// <summary>
        /// Gets recommended browser launch options for stable execution
        /// </summary>
        public static BrowserTypeLaunchOptions GetStableLaunchOptions(bool headless = false)
        {
            var options = new BrowserTypeLaunchOptions
            {
                Headless = headless,
                Timeout = BrowserLaunchTimeoutMs,
                SlowMo = 100, // Add 100ms delay between operations for stability
            };

            if (headless)
            {
                options.Args = new[]
                {
                    "--disable-dev-shm-usage",
                    "--disable-gpu",
                    "--no-sandbox",
                    "--disable-setuid-sandbox",
                    "--disable-web-security",
                    "--disable-features=VizDisplayCompositor",
                    "--disable-software-rasterizer",
                    "--disable-background-networking",
                    "--disable-default-apps",
                    "--disable-extensions",
                    "--disable-sync",
                    "--disable-translate",
                    "--hide-scrollbars",
                    "--metrics-recording-only",
                    "--mute-audio",
                    "--no-first-run"
                };
            }

            return options;
        }
    }
}
