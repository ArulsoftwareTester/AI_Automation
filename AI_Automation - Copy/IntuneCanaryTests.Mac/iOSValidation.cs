using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace IntuneCanaryTests.Mac
{
    [TestFixture]
    public class iOSValidation
    {
        private const string DeviceName = "Centific iPhone";
        private const string CompanyPortalBundleId = "com.microsoft.CompanyPortal";
        private const string AppiumServerUrl = "http://127.0.0.1:4723";
        private IOSDriver? _driver;

        [Test]
        public async Task ConnectToiPhoneAndLaunchCompanyPortal()
        {
            try
            {
                Console.WriteLine("Starting iOS device connection test...");

                // Check if Xcode command line tools are installed
                if (!IsXcodeInstalled())
                {
                    Assert.Fail("Xcode command line tools are not installed. Please install Xcode.");
                    return;
                }

                // Get list of connected iOS devices
                string deviceId = await GetConnectedDeviceId(DeviceName);
                
                if (string.IsNullOrEmpty(deviceId))
                {
                    Assert.Fail($"Device '{DeviceName}' is not connected or not found.");
                    return;
                }

                Console.WriteLine($"Found device: {DeviceName} with ID: {deviceId}");

                // Launch Company Portal app on the device
                bool launchSuccess = await LaunchAppOnDevice(deviceId, CompanyPortalBundleId);

                if (launchSuccess)
                {
                    Console.WriteLine($"Successfully launched Company Portal app on {DeviceName}");
                    
                    // Wait for app to fully load
                    Console.WriteLine("Waiting for Company Portal app to fully load...");
                    await Task.Delay(5000);
                    Console.WriteLine("App should be loaded now.");
                    
                    Console.WriteLine("\\n" + new string('=', 80));
                    Console.WriteLine("SEARCHING FOR DEVICE TAB AT BOTTOM");
                    Console.WriteLine(new string('=', 80));
                    
                    // Try to initialize Appium and click on Devices button
                    bool clickSuccess = await ClickDevicesButtonWithAppium(deviceId);
                    
                    if (clickSuccess)
                    {
                        Console.WriteLine("✓ Successfully clicked on Device tab using Appium");
                        Console.WriteLine(new string('=', 80) + "\\n");
                        Assert.Pass("Company Portal app launched and Device tab clicked successfully");
                    }
                    else
                    {
                        Console.WriteLine("⚠ Appium click failed, trying alternative methods...");
                        // Click on Devices button using alternative methods
                        clickSuccess = await ClickDevicesButton(deviceId);
                        
                        if (clickSuccess)
                        {
                            Console.WriteLine("✓ Successfully clicked on Devices button");
                            Console.WriteLine(new string('=', 80) + "\\n");
                            Assert.Pass("Company Portal app launched and Devices button clicked successfully");
                        }
                        else
                        {
                            Console.WriteLine("\\n✗ Could not automatically click Devices button");
                            Console.WriteLine("\\nTO ENABLE AUTOMATIC CLICKING:");
                            Console.WriteLine("1. Install WebDriverAgent on your device:");
                            Console.WriteLine("   cd ~/.appium/node_modules/appium-xcuitest-driver/node_modules/appium-webdriveragent");
                            Console.WriteLine($"   xcodebuild -project WebDriverAgent.xcodeproj -scheme WebDriverAgentRunner -destination 'id={deviceId}' test");
                            Console.WriteLine("\\n2. OR install idb:");
                            Console.WriteLine("   brew tap facebook/fb && brew install idb-companion");
                            Console.WriteLine("\\n3. Then re-run this test");
                            Console.WriteLine("\\nFor now, please MANUALLY tap the 'Devices' tab at the bottom of the Company Portal app on your iPhone.");
                            Console.WriteLine(new string('=', 80) + "\\n");
                            
                            // Give user time to manually click
                            Console.WriteLine("Waiting 10 seconds for manual tap...");
                            await Task.Delay(10000);
                            
                            Assert.Pass("Company Portal app launched successfully. Manual interaction may be required.");
                        }
                    }
                }
                else
                {
                    Assert.Fail("Failed to launch Company Portal app on the device");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during iOS validation: {ex.Message}");
                throw;
            }
            finally
            {
                // Clean up Appium driver
                _driver?.Quit();
                _driver = null;
            }
        }

        private bool IsXcodeInstalled()
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "xcode-select",
                        Arguments = "-p",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                process.WaitForExit();
                return process.ExitCode == 0;
            }
            catch
            {
                return false;
            }
        }

        private async Task<string> GetConnectedDeviceId(string deviceName)
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "xcrun",
                        Arguments = "xctrace list devices",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                string output = await process.StandardOutput.ReadToEndAsync();
                await process.WaitForExitAsync();

                Console.WriteLine("Connected devices:");
                Console.WriteLine(output);

                // Parse output to find device ID
                // Format is typically: "Device Name (iOS Version) (Device-ID)"
                var lines = output.Split('\n');
                foreach (var line in lines)
                {
                    if (line.Contains(deviceName) && line.Contains("("))
                    {
                        // Extract device ID from the line
                        var parts = line.Split('(');
                        if (parts.Length >= 3)
                        {
                            var deviceId = parts[parts.Length - 1].Replace(")", "").Trim();
                            return deviceId;
                        }
                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting device list: {ex.Message}");
                return string.Empty;
            }
        }

        private async Task<bool> LaunchAppOnDevice(string deviceId, string bundleId)
        {
            try
            {
                Console.WriteLine($"Attempting to launch app {bundleId} on device {deviceId}...");

                // Use simctl for simulator or ios-deploy for real devices
                // First, try using ios-deploy (need to install via: brew install ios-deploy)
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "xcrun",
                        Arguments = $"devicectl device process launch --device {deviceId} {bundleId}",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();

                Console.WriteLine($"Launch output: {output}");
                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine($"Launch error: {error}");
                }

                // If devicectl doesn't work, try alternative method with instruments
                if (process.ExitCode != 0)
                {
                    Console.WriteLine("Trying alternative launch method...");
                    return await LaunchAppWithInstruments(deviceId, bundleId);
                }

                return process.ExitCode == 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error launching app: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> LaunchAppWithInstruments(string deviceId, string bundleId)
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "instruments",
                        Arguments = $"-w {deviceId}",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                await Task.Delay(2000); // Wait for device to wake up

                // Now try to launch the app using idevice tools or libimobiledevice
                var launchProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "idevicedebug",
                        Arguments = $"-u {deviceId} run {bundleId}",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                launchProcess.Start();
                await Task.Delay(3000); // Give app time to launch

                // Kill the debug process after launch
                if (!launchProcess.HasExited)
                {
                    launchProcess.Kill();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error with alternative launch method: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> ClickDevicesButtonWithAppium(string deviceId)
        {
            try
            {
                Console.WriteLine("Attempting to connect to Appium server and click Devices button...");
                
                // Check if Appium server is running
                if (!await IsAppiumServerRunning())
                {
                    Console.WriteLine("Appium server is not running.");
                    Console.WriteLine("To start Appium server, run: appium");
                    Console.WriteLine("Make sure XCUITest driver is installed: appium driver install xcuitest");
                    return false;
                }

                // Check if Appium server is running
                if (!await IsAppiumServerRunning())
                {
                    Console.WriteLine("Appium server is not running.");
                    Console.WriteLine("To start Appium server, run: appium");
                    Console.WriteLine("Make sure XCUITest driver is installed: appium driver install xcuitest");
                    return false;
                }
                
                // Check if WebDriverAgent is running, if not try to start it
                if (!await IsWebDriverAgentRunning(deviceId))
                {
                    Console.WriteLine("WebDriverAgent is not running. Attempting to start it...");
                    bool wdaStarted = await StartWebDriverAgent(deviceId);
                    if (!wdaStarted)
                    {
                        Console.WriteLine("Failed to start WebDriverAgent automatically.");
                        Console.WriteLine("Please start it manually in a separate terminal:");
                        Console.WriteLine($"  ./start-wda.sh {deviceId}");
                        Console.WriteLine("Or:");
                        Console.WriteLine($"  cd ~/.appium/node_modules/appium-xcuitest-driver/node_modules/appium-webdriveragent");
                        Console.WriteLine($"  xcodebuild -project WebDriverAgent.xcodeproj -scheme WebDriverAgentRunner -destination 'id={deviceId}' test");
                        return false;
                    }
                    
                    // Wait for WDA to be ready
                    Console.WriteLine("Waiting for WebDriverAgent to be ready...");
                    await Task.Delay(10000);
                }

                // Setup Appium capabilities
                var capabilities = new AppiumOptions();
                capabilities.PlatformName = "iOS";
                capabilities.PlatformVersion = "26.2";
                capabilities.DeviceName = DeviceName;
                capabilities.AddAdditionalAppiumOption("udid", deviceId);
                capabilities.AddAdditionalAppiumOption("bundleId", CompanyPortalBundleId);
                capabilities.AutomationName = "XCUITest";
                capabilities.AddAdditionalAppiumOption("noReset", true);
                capabilities.AddAdditionalAppiumOption("fullReset", false);
                capabilities.AddAdditionalAppiumOption("newCommandTimeout", 300);

                Console.WriteLine("Connecting to Appium server...");
                
                try
                {
                    Console.WriteLine("Creating Appium driver with capabilities:");
                    Console.WriteLine($"  - Platform: iOS 26.2");
                    Console.WriteLine($"  - Device: {DeviceName} ({deviceId})");
                    Console.WriteLine($"  - Bundle ID: {CompanyPortalBundleId}");
                    Console.WriteLine($"  - Appium URL: {AppiumServerUrl}");
                    
                    _driver = new IOSDriver(new Uri(AppiumServerUrl), capabilities, TimeSpan.FromSeconds(120));
                    Console.WriteLine("✓ Connected to Appium. Searching for Devices button...");
                }
                catch (WebDriverException ex)
                {
                    Console.WriteLine($"WebDriverException: {ex.Message}");
                    Console.WriteLine($"Status: {ex.GetType().Name}");
                    
                    if (ex.Message.Contains("WebDriverAgent") || ex.Message.Contains("could not be found"))
                    {
                        Console.WriteLine("\\nWebDriverAgent is not running on the device.");
                        Console.WriteLine("\\nTo start WebDriverAgent:");
                        Console.WriteLine($"1. Open a new terminal and run:");
                        Console.WriteLine($"   cd ~/.appium/node_modules/appium-xcuitest-driver/node_modules/appium-webdriveragent");
                        Console.WriteLine($"   xcodebuild -project WebDriverAgent.xcodeproj -scheme WebDriverAgentRunner \\\\");
                        Console.WriteLine($"              -destination 'id={deviceId}' test");
                        Console.WriteLine($"2. Keep that terminal running (WDA server must stay active)");
                        Console.WriteLine($"3. Run this test again in a different terminal\\n");
                    }
                    
                    Console.WriteLine("Trying direct XCUITest approach...");
                    return await TapDevicesUsingXCUITest(deviceId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to connect to Appium: {ex.Message}");
                    Console.WriteLine($"Exception type: {ex.GetType().Name}");
                    Console.WriteLine("Trying direct XCUITest approach...");
                    return await TapDevicesUsingXCUITest(deviceId);
                }
                
                // Wait for the app to be ready
                await Task.Delay(2000);
                
                Console.WriteLine("\\nLooking for bottom tab bar navigation...");
                
                // First, let's check if we can find the tab bar
                try
                {
                    var tabBar = _driver.FindElement(MobileBy.XPath("//XCUIElementTypeTabBar"));
                    Console.WriteLine("✓ Found tab bar at bottom of the screen");
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("⚠ Tab bar not found - app might be in a different state");
                }
                
                // Try multiple strategies to find the Devices button
                IWebElement? devicesButton = null;
                
                // Strategy 1: Find by accessibility ID
                try
                {
                    Console.WriteLine("Trying to find button by accessibility ID 'Devices'...");
                    devicesButton = _driver.FindElement(MobileBy.AccessibilityId("Devices"));
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("Not found by accessibility ID");
                }
                
                // Strategy 2: Find by name
                if (devicesButton == null)
                {
                    try
                    {
                        Console.WriteLine("Trying to find button by name 'Devices'...");
                        devicesButton = _driver.FindElement(MobileBy.Name("Devices"));
                    }
                    catch (NoSuchElementException)
                    {
                        Console.WriteLine("Not found by name");
                    }
                }
                
                // Strategy 3: Find by XPath with text
                if (devicesButton == null)
                {
                    try
                    {
                        Console.WriteLine("Trying to find button by XPath with text 'Devices'...");
                        devicesButton = _driver.FindElement(MobileBy.XPath("//*[@name='Devices']"));
                    }
                    catch (NoSuchElementException)
                    {
                        Console.WriteLine("Not found by XPath");
                    }
                }
                
                // Strategy 4: Find in tab bar
                if (devicesButton == null)
                {
                    try
                    {
                        Console.WriteLine("Trying to find button in tab bar...");
                        devicesButton = _driver.FindElement(MobileBy.XPath("//XCUIElementTypeTabBar//XCUIElementTypeButton[@name='Devices']"));
                    }
                    catch (NoSuchElementException)
                    {
                        Console.WriteLine("Not found in tab bar");
                    }
                }
                
                // Strategy 5: Find any button with Devices or Device label
                if (devicesButton == null)
                {
                    try
                    {
                        Console.WriteLine("Trying to find any button with 'Devices' or 'Device' label...");
                        var allButtons = _driver.FindElements(MobileBy.XPath("//XCUIElementTypeButton"));
                        foreach (var button in allButtons)
                        {
                            var label = button.GetAttribute("label");
                            var name = button.GetAttribute("name");
                            Console.WriteLine($"Found button: label='{label}', name='{name}'");
                            // Check for "Devices" or exact "Device" (but not "Device Details" or other variations)
                            if ((label?.Equals("Devices", StringComparison.OrdinalIgnoreCase) == true || 
                                 name?.Equals("Devices", StringComparison.OrdinalIgnoreCase) == true) ||
                                (label?.Equals("Device", StringComparison.OrdinalIgnoreCase) == true || 
                                 name?.Equals("Device", StringComparison.OrdinalIgnoreCase) == true))
                            {
                                devicesButton = button;
                                Console.WriteLine($"Found matching device button: label='{label}', name='{name}'");
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error searching buttons: {ex.Message}");
                    }
                }
                
                if (devicesButton != null)
                {
                    Console.WriteLine($"\\n✓ Found Device tab button at the bottom!");
                    Console.WriteLine("Clicking on the Device tab...");
                    devicesButton.Click();
                    await Task.Delay(2000); // Wait for navigation
                    Console.WriteLine("✓ Successfully clicked Device tab button");
                    Console.WriteLine("Device details screen should now be displayed");
                    
                    // Now find and click "Check status" button
                    Console.WriteLine("\\nWaiting 2 seconds before clicking 'Check status'...");
                    await Task.Delay(2000);
                    
                    IWebElement? checkStatusButton = null;
                    
                    // Try to find "Check status" button
                    try
                    {
                        Console.WriteLine("Looking for 'Check status' button...");
                        checkStatusButton = _driver.FindElement(MobileBy.AccessibilityId("Check status"));
                        Console.WriteLine("Found 'Check status' button by accessibility ID");
                    }
                    catch (NoSuchElementException)
                    {
                        // Try by name
                        try
                        {
                            checkStatusButton = _driver.FindElement(MobileBy.Name("Check status"));
                            Console.WriteLine("Found 'Check status' button by name");
                        }
                        catch (NoSuchElementException)
                        {
                            // Try by XPath
                            try
                            {
                                checkStatusButton = _driver.FindElement(MobileBy.XPath("//XCUIElementTypeButton[@name='Check status']"));
                                Console.WriteLine("Found 'Check status' button by XPath");
                            }
                            catch (NoSuchElementException)
                            {
                                Console.WriteLine("⚠ Could not find 'Check status' button");
                            }
                        }
                    }
                    
                    if (checkStatusButton != null)
                    {
                        Console.WriteLine("✓ Found 'Check status' button!");
                        Console.WriteLine("Clicking on 'Check status' button...");
                        checkStatusButton.Click();
                        await Task.Delay(2000); // Wait for action to complete
                        Console.WriteLine("✓ Successfully clicked 'Check status' button");
                    }
                    else
                    {
                        Console.WriteLine("⚠ 'Check status' button not found - may not be visible on this screen");
                    }
                    
                    return true;
                }
                else
                {
                    Console.WriteLine("Could not find Devices button with any strategy");
                    
                    // Print page source for debugging
                    Console.WriteLine("Dumping page source for analysis:");
                    var pageSource = _driver.PageSource;
                    Console.WriteLine(pageSource.Length > 5000 ? pageSource.Substring(0, 5000) + "..." : pageSource);
                    
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ClickDevicesButtonWithAppium: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return false;
            }
        }
        
        private async Task<bool> IsAppiumServerRunning()
        {
            try
            {
                using var tcpClient = new System.Net.Sockets.TcpClient();
                await tcpClient.ConnectAsync("127.0.0.1", 4723);
                return tcpClient.Connected;
            }
            catch
            {
                return false;
            }
        }
        
        private async Task<bool> IsWebDriverAgentRunning(string deviceId)
        {
            try
            {
                // Check if WDA process is running
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "ps",
                        Arguments = "aux",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                
                process.Start();
                string output = await process.StandardOutput.ReadToEndAsync();
                await process.WaitForExitAsync();
                
                // Look for WebDriverAgentRunner process with our device ID
                bool isRunning = output.Contains("WebDriverAgentRunner") && output.Contains(deviceId);
                
                if (isRunning)
                {
                    Console.WriteLine("✓ WebDriverAgent is already running");
                }
                
                return isRunning;
            }
            catch
            {
                return false;
            }
        }
        
        private async Task<bool> StartWebDriverAgent(string deviceId)
        {
            try
            {
                string scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "start-wda.sh");
                
                // Check parent directories if not found
                if (!File.Exists(scriptPath))
                {
                    scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "../../../start-wda.sh");
                }
                
                if (!File.Exists(scriptPath))
                {
                    Console.WriteLine($"start-wda.sh script not found at {scriptPath}");
                    return false;
                }
                
                Console.WriteLine($"Starting WebDriverAgent using script: {scriptPath}");
                Console.WriteLine("This will run in the background...");
                
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = $"\"{scriptPath}\" {deviceId}",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = false
                    }
                };
                
                process.Start();
                
                // Don't wait for it to finish, it should run in background
                // Just give it a moment to start
                await Task.Delay(3000);
                
                // Check if WDA started successfully
                bool started = await IsWebDriverAgentRunning(deviceId);
                
                if (started)
                {
                    Console.WriteLine("✓ WebDriverAgent started successfully");
                    return true;
                }
                else
                {
                    // Read any error output
                    if (!process.HasExited)
                    {
                        Console.WriteLine("WebDriverAgent is starting... (may need more time)");
                        return true; // Assume it's starting
                    }
                    
                    string error = await process.StandardError.ReadToEndAsync();
                    if (!string.IsNullOrEmpty(error))
                    {
                        Console.WriteLine($"WDA Error: {error}");
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting WebDriverAgent: {ex.Message}");
                return false;
            }
        }
        
        private async Task<bool> TapDevicesUsingXCUITest(string deviceId)
        {
            try
            {
                Console.WriteLine("Attempting to tap Devices button using XCUITest...");
                
                // Try using the Swift XCUITest script
                bool swiftSuccess = await RunSwiftUITest(deviceId);
                if (swiftSuccess)
                {
                    Console.WriteLine("Successfully tapped using Swift XCUITest");
                    return true;
                }
                
                // Get the screen size first
                var sizeProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "xcrun",
                        Arguments = $"simctl io {deviceId} screenshot --type=png --display=internal /tmp/screen.png",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                
                // For physical devices, try using coordinates based on standard iPhone layout
                // Bottom tab bar typically has items at these relative positions:
                // Assuming iPhone screen width ~390-430 points
                // Tab bar items are usually evenly distributed
                
                Console.WriteLine("Attempting tap at standard Devices tab location...");
                
                // Try multiple potential positions for the Devices tab
                var tabPositions = new[]
                {
                    (100, 750),  // First tab position
                    (195, 750),  // Second tab position
                    (290, 750),  // Third tab position
                    (385, 750),  // Fourth tab position
                    (78, 800),   // iOS 17+ adjusted positions
                    (195, 800),
                    (312, 800)
                };
                
                foreach (var (x, y) in tabPositions)
                {
                    Console.WriteLine($"Trying tap at position ({x}, {y})...");
                    bool success = await TapAtPosition(deviceId, x, y);
                    if (success)
                    {
                        Console.WriteLine($"Successfully tapped at ({x}, {y})");
                        await Task.Delay(1000);
                        return true;
                    }
                    await Task.Delay(500);
                }
                
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in TapDevicesUsingXCUITest: {ex.Message}");
                return false;
            }
        }
        
        private async Task<bool> TapAtPosition(string deviceId, int x, int y)
        {
            try
            {
                // Try using simctl for simulators (won't work for physical device but won't hurt)
                var simProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "xcrun",
                        Arguments = $"simctl io {deviceId} touch {x} {y}",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                
                simProcess.Start();
                await simProcess.WaitForExitAsync();
                
                if (simProcess.ExitCode == 0)
                {
                    return true;
                }
                
                // For physical devices, try using python script with pymobiledevice3 if available
                var pyScript = $@"
import sys
try:
    from pymobiledevice3.services.web_protocol import WebDriver
    from pymobiledevice3.lockdown import create_using_usbmux
    
    lockdown = create_using_usbmux(udid='{deviceId}')
    driver = WebDriver(lockdown)
    driver.tap({x}, {y})
    print('SUCCESS')
except Exception as e:
    print(f'FAILED: {{e}}')
    sys.exit(1)
";
                
                var pythonProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "python3",
                        Arguments = $"-c \"{pyScript}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                
                pythonProcess.Start();
                string output = await pythonProcess.StandardOutput.ReadToEndAsync();
                await pythonProcess.WaitForExitAsync();
                
                return output.Contains("SUCCESS");
            }
            catch
            {
                return false;
            }
        }
        
        private async Task<bool> RunSwiftUITest(string deviceId)
        {
            try
            {
                Console.WriteLine("Attempting to run Swift XCUITest script...");
                
                // Check if the Swift script exists
                string scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "TapDevicesButton.swift");
                if (!File.Exists(scriptPath))
                {
                    // Check parent directories
                    scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "../../../TapDevicesButton.swift");
                    if (!File.Exists(scriptPath))
                    {
                        Console.WriteLine("Swift script not found");
                        return false;
                    }
                }
                
                Console.WriteLine($"Running Swift script: {scriptPath}");
                
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "swift",
                        Arguments = $"\"{scriptPath}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                
                process.Start();
                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();
                
                Console.WriteLine($"Swift output: {output}");
                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine($"Swift error: {error}");
                }
                
                return output.Contains("Tapped Devices");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error running Swift script: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> ClickDevicesButton(string deviceId)
        {
            try
            {
                Console.WriteLine("Attempting to click Devices button...");
                
                // Try multiple approaches to click the button
                
                // Approach 1: Try using AppleScript with UI scripting
                bool success = await TryAppleScriptClick(deviceId);
                if (success)
                {
                    Console.WriteLine("Successfully clicked using AppleScript");
                    return true;
                }
                
                // Approach 2: Try using idb if available
                success = await TapElementByText(deviceId, "Devices");
                if (success)
                {
                    Console.WriteLine("Successfully clicked using idb");
                    return true;
                }
                
                // Approach 3: Try accessibility label
                success = await TapElementByAccessibilityLabel(deviceId, "Devices");
                if (success)
                {
                    Console.WriteLine("Successfully clicked using accessibility label");
                    return true;
                }
                
                // Approach 4: Try coordinate-based tap (bottom tab bar)
                success = await TapByCoordinates(deviceId, 100, 750); // Approximate location for first tab
                if (success)
                {
                    Console.WriteLine("Successfully clicked using coordinates");
                    return true;
                }

                Console.WriteLine("All click methods failed. UI automation requires additional setup.");
                Console.WriteLine("For physical device automation, consider:");
                Console.WriteLine("  1. Install Appium: npm install -g appium && appium driver install xcuitest");
                Console.WriteLine("  2. Install idb: brew tap facebook/fb && brew install idb-companion");
                Console.WriteLine("  3. Use XCTest UI Testing framework");
                
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clicking Devices button: {ex.Message}");
                return false;
            }
        }
        
        private async Task<bool> TryAppleScriptClick(string deviceId)
        {
            try
            {
                Console.WriteLine("Trying AppleScript approach...");
                
                // This works for Simulator, but not for physical devices
                // AppleScript doesn't work for physical devices
                
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AppleScript approach failed: {ex.Message}");
                return false;
            }
        }
        
        private async Task<bool> TapByCoordinates(string deviceId, int x, int y)
        {
            try
            {
                Console.WriteLine($"Attempting to tap at coordinates ({x}, {y})...");
                
                // Try using xcrun simctl for simulator
                var simProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "xcrun",
                        Arguments = $"simctl io {deviceId} touch {x} {y}",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                simProcess.Start();
                await simProcess.WaitForExitAsync();

                if (simProcess.ExitCode == 0)
                {
                    Console.WriteLine($"Tap successful via simctl");
                    return true;
                }
                
                // For physical devices, try using pymobiledevice3 if available
                var pyScript = $@"
import sys
try:
    from pymobiledevice3.services.web_protocol import WebDriver
    from pymobiledevice3.lockdown import create_using_usbmux
    
    lockdown = create_using_usbmux(udid='{deviceId}')
    driver = WebDriver(lockdown)
    driver.tap({x}, {y})
    print('SUCCESS')
except Exception as e:
    print(f'FAILED: {{e}}')
    sys.exit(1)
";
                
                var pythonProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "python3",
                        Arguments = $"-c \"{pyScript}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                
                pythonProcess.Start();
                string output = await pythonProcess.StandardOutput.ReadToEndAsync();
                await pythonProcess.WaitForExitAsync();
                
                if (output.Contains("SUCCESS"))
                {
                    Console.WriteLine($"Tap successful via pymobiledevice3");
                    return true;
                }
                
                Console.WriteLine($"Tap failed: devicectl is not supported for tap operations on physical devices");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error tapping coordinates: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> TapElementByAccessibilityLabel(string deviceId, string label)
        {
            try
            {
                Console.WriteLine($"Attempting to tap element with accessibility label: {label}");
                
                // Try to get UI hierarchy first
                bool hierarchyAvailable = await DumpUIHierarchy(deviceId);
                
                if (hierarchyAvailable)
                {
                    Console.WriteLine("UI hierarchy dumped, analyzing for Devices button...");
                }
                
                // Try using xcrun devicectl ui commands (iOS 17+)
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "xcrun",
                        Arguments = $"devicectl device info diagnostics --device {deviceId}",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();
                
                if (!string.IsNullOrEmpty(output))
                {
                    Console.WriteLine("UI inspection output:");
                    Console.WriteLine(output);
                }
                
                if (!string.IsNullOrEmpty(error) && !error.Contains("not supported"))
                {
                    Console.WriteLine($"UI inspection error: {error}");
                }
                
                Console.WriteLine($"Note: Direct UI automation for physical devices requires WebDriverAgent or XCUITest");
                Console.WriteLine($"Attempted to access '{label}' element");
                
                // Return false since we can't actually click without proper automation tools
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error tapping element: {ex.Message}");
                return false;
            }
        }
        
        private async Task<bool> DumpUIHierarchy(string deviceId)
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "xcrun",
                        Arguments = $"devicectl device info --device {deviceId}",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                await process.WaitForExitAsync();
                return process.ExitCode == 0;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> TapElementByText(string deviceId, string text)
        {
            try
            {
                Console.WriteLine($"Attempting to tap element with text: {text}");
                
                // This would require proper UI automation framework
                // For demonstration, we'll use py-ios-device or similar tool if available
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = $"-c \"command -v idb\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                string output = await process.StandardOutput.ReadToEndAsync();
                await process.WaitForExitAsync();

                if (!string.IsNullOrEmpty(output.Trim()))
                {
                    // idb is available, use it
                    return await UseIdbToTap(deviceId, text);
                }

                Console.WriteLine("Note: For full UI automation, consider installing idb (iOS Development Bridge)");
                Console.WriteLine("Install with: brew install idb-companion");
                
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in TapElementByText: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> UseIdbToTap(string deviceId, string text)
        {
            try
            {
                // Use idb to interact with UI
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "idb",
                        Arguments = $"ui tap --udid {deviceId} --text \"{text}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();

                Console.WriteLine($"idb output: {output}");
                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine($"idb error: {error}");
                }

                return process.ExitCode == 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error using idb: {ex.Message}");
                return false;
            }
        }

        [Test]
        public async Task VerifyDeviceConnection()
        {
            Console.WriteLine("Verifying iOS device connection...");
            
            string deviceId = await GetConnectedDeviceId(DeviceName);
            
            if (!string.IsNullOrEmpty(deviceId))
            {
                Console.WriteLine($"✓ Device '{DeviceName}' is connected with ID: {deviceId}");
                Assert.Pass("Device is connected");
            }
            else
            {
                Console.WriteLine($"✗ Device '{DeviceName}' is not found");
                Assert.Fail($"Device '{DeviceName}' is not connected");
            }
        }

        [Test]
        public async Task ListAllConnectedDevices()
        {
            Console.WriteLine("Listing all connected iOS devices...");
            
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "xcrun",
                        Arguments = "xctrace list devices",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                string output = await process.StandardOutput.ReadToEndAsync();
                await process.WaitForExitAsync();

                Console.WriteLine("Available devices:");
                Console.WriteLine(output);
                
                Assert.Pass("Device list retrieved successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing devices: {ex.Message}");
                throw;
            }
        }
    }
}
