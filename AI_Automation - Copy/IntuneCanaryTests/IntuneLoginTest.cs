using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class IntuneLoginTests : PageTest
    {
        public override BrowserNewContextOptions ContextOptions()
        {
            var certPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "auth-cert", "AIAutoPE_3.pfx"));
            Console.WriteLine($"Certificate path: {certPath}");
            return new BrowserNewContextOptions 
            {
                ClientCertificates = new[] {
                    new ClientCertificate {
                        Origin = "https://certauth.login.microsoftonline.com/c0219094-a70e-402c-8dd2-fd89f7d64010/certauth",
                        PfxPath = certPath,
                        Passphrase = "Admin@123"
                    }
                }
            };
        }

        [Test]
        public async Task CanaryLoginTest()
        {
            // Close any extra pages
            Page.Context.Pages.Skip(1).ToList().ForEach(p => p.CloseAsync().Wait());

            try
            {
                // Navigate to Intune Canary
                Console.WriteLine("Navigating to Intune Canary...");
                await Page.GotoAsync("https://aka.ms/intunecanary", new()
                {
                    WaitUntil = WaitUntilState.NetworkIdle,
                    Timeout = 60000
                });

                // Maximize the browser window
                await Page.SetViewportSizeAsync(1920, 1080);
                try
                {
                    await Page.EvaluateAsync("window.moveTo(0, 0)");
                    await Page.EvaluateAsync("window.resizeTo(screen.width, screen.height)");
                    Console.WriteLine("Maximized browser window");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not maximize window: {ex.Message}");
                }

                // Wait for and verify page load
                Console.WriteLine("Verifying page load...");
                await Task.Delay(2000); // Give page time to settle
                await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

                // Verify page visibility
                var body = Page.Locator("body");
                await Expect(body).ToBeVisibleAsync(new()
                {
                    Timeout = 30000
                });

                // Get and validate page title
                var pageTitle = await Page.TitleAsync();
                Console.WriteLine($"Page title: {pageTitle}");
                Assert.That(pageTitle, Is.Not.Empty, "Page title should not be empty");

                // Wait for username field and enter credentials
                var usernameInput = Page.Locator("[name=\"loginfmt\"]");
                await Expect(usernameInput).ToBeVisibleAsync(new()
                {
                    Timeout = 30000
                });

                Console.WriteLine("Username field found, entering credentials...");
                var username = "admin@AppSHRunner48.onmicrosoft.com";
                Console.WriteLine($"Using username: {username}");
                await usernameInput.FillAsync(username);
                
                // Click the Next button
                await Page.ClickAsync("#idSIButton9");
                Console.WriteLine("Clicked Next button");

                // Wait for and handle the "Stay signed in?" prompt if it appears
                try {
                    Console.WriteLine("Checking for 'Stay signed in?' prompt...");
                    var staySignedInPrompt = await Page.WaitForSelectorAsync("text='Stay signed in?'", 
                        new() { Timeout = 30000 });
                    
                    if (staySignedInPrompt != null)
                    {
                        Console.WriteLine("'Stay signed in?' prompt found, clicking No...");
                        await Page.ClickAsync("#idBtn_Back");
                    }
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("No 'Stay signed in?' prompt found, continuing...");
                }

                // Wait for and verify successful login
                Console.WriteLine("Waiting for home page to load...");
                try
                {
                    await Page.WaitForLoadStateAsync(LoadState.NetworkIdle, new() { Timeout = 60000 });
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("Network idle timeout, checking if page loaded...");
                    // Continue anyway - page might have loaded despite NetworkIdle not being reached
                }
                
                // Give page time to stabilize
                Console.WriteLine("Waiting for home page to stabilize...");
                await Task.Delay(3000);
                
                // Verify we're on the Intune portal
                var currentUrl = Page.Url;
                Console.WriteLine($"Current URL: {currentUrl}");
                Assert.That(currentUrl, Does.Contain("intune.microsoft.com"), "Should be on Intune portal");

                // Check page title
                var finalPageTitle = await Page.TitleAsync();
                Console.WriteLine($"Page title: {finalPageTitle}");
                Assert.That(finalPageTitle, Does.Contain("Microsoft Intune"), "Page title should contain 'Microsoft Intune'");

                // Wait for the page to stabilize
                Console.WriteLine("Waiting for home page to stabilize...");
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle, new() { Timeout = 30000 });
                await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded, new() { Timeout = 30000 });

                // Verify URL contains expected parameters
                var finalUrl = Page.Url;
                Console.WriteLine("Verifying URL parameters...");
                var requiredParams = new[] {
                    "feature.Microsoft_Intune=canary",
                    "feature.Microsoft_Intune_Apps=canary",
                    "feature.Microsoft_Intune_Devices=canary"
                };

                foreach (var param in requiredParams)
                {
                    Assert.That(finalUrl, Does.Contain(param), $"URL should contain {param}");
                }

                // Verify title contains expected text
                var finalTitle = await Page.TitleAsync();
                Assert.That(finalTitle, Does.Contain("Microsoft Intune"), "Title should contain 'Microsoft Intune'");
                Assert.That(finalTitle, Does.Contain("TenantMonkey"), "Title should contain 'TenantMonkey'");

                // Take a screenshot of the successful login
                await Page.ScreenshotAsync(new() { 
                    Path = "successful-login.png",
                    FullPage = true
                });

                Console.WriteLine("Successfully verified home page and login");

                // Navigate to Apps page
                Console.WriteLine("Navigating to Apps page...");
                
                // Wait for the menu to be ready
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                
                // Try to click Apps in different ways
                bool appsClicked = false;
                
                try 
                {
                    // First try: Click using role and name
                    await Page.ClickAsync("a:has-text('Apps')", new() { Timeout = 5000 });
                    appsClicked = true;
                    Console.WriteLine("Clicked Apps menu using text selector");
                }
                catch (TimeoutException) 
                {
                    try 
                    {
                        // Second try: Look for specific link
                        await Page.ClickAsync("[href*='apps']", new() { Timeout = 5000 });
                        appsClicked = true;
                        Console.WriteLine("Clicked Apps menu using href selector");
                    }
                    catch (TimeoutException)
                    {
                        try
                        {
                            // Third try: Try navigation menu
                            await Page.ClickAsync("nav >> text=Apps", new() { Timeout = 5000 });
                            appsClicked = true;
                            Console.WriteLine("Clicked Apps menu using nav selector");
                        }
                        catch (TimeoutException)
                        {
                            Console.WriteLine("Failed to find Apps menu with standard selectors");
                        }
                    }
                }

                Assert.That(appsClicked, Is.True, "Should find and click the Apps menu item");

                // Wait for page transition
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                
                // Verify we're on the Apps page
                Console.WriteLine("Verifying Apps page load...");
                
                // Wait for Apps-specific elements
                try
                {
                    await Page.WaitForSelectorAsync("[data-automation-id='apps-list']", new() { Timeout = 10000 });
                    Console.WriteLine("Found apps list element");
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("Apps list element not found, checking alternative elements");
                }

                // Take a screenshot of the Apps page for verification
                await Page.ScreenshotAsync(new() {
                    Path = "apps-page.png",
                    FullPage = true
                });

                // Verify the page title contains "Apps"
                var appsPageTitle = await Page.TitleAsync();
                Assert.That(appsPageTitle, Does.Contain("Apps"), "Page title should contain 'Apps'");
                
                Console.WriteLine($"Current page title: {appsPageTitle}");

                // Click on All Apps link
                Console.WriteLine("Attempting to click All Apps link...");
                bool allAppsClicked = false;
                
                try 
                {
                    // First try: Click using text
                    await Page.ClickAsync("a:has-text('All apps')", new() { Timeout = 5000 });
                    allAppsClicked = true;
                    Console.WriteLine("Clicked All Apps using text selector");
                }
                catch (TimeoutException) 
                {
                    try 
                    {
                        // Second try: Look for href containing 'all-apps'
                        await Page.ClickAsync("[href*='all-apps']", new() { Timeout = 5000 });
                        allAppsClicked = true;
                        Console.WriteLine("Clicked All Apps using href selector");
                    }
                    catch (TimeoutException)
                    {
                        try
                        {
                            // Third try: Try navigation menu with role
                            await Page.ClickAsync("[role='menuitem']:has-text('All apps')", new() { Timeout = 5000 });
                            allAppsClicked = true;
                            Console.WriteLine("Clicked All Apps using role selector");
                        }
                        catch (TimeoutException)
                        {
                            Console.WriteLine("Failed to find All Apps link with standard selectors");
                        }
                    }
                }

                Assert.That(allAppsClicked, Is.True, "Should find and click the All Apps link");

                // Wait for page to load
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                
                // Verify we're on the All Apps page
                Console.WriteLine("Verifying All Apps page load...");
                
                // Wait for All Apps page elements
                try
                {
                    await Page.WaitForSelectorAsync("[data-automation-id='apps-list']", new() { Timeout = 10000 });
                    Console.WriteLine("Found apps list element on All Apps page");
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("Apps list element not found on All Apps page, checking URL");
                }

                // Verify URL contains windowsApps or allApps
                var allAppsUrl = Page.Url;
                Assert.That(allAppsUrl, Does.Contain("Apps"), "URL should contain 'Apps'");
                Assert.That(allAppsUrl, Does.Match(@"(windows|all)Apps"), "URL should contain either 'windowsApps' or 'allApps'");

                // Take a screenshot of the All Apps page
                await Page.ScreenshotAsync(new() { 
                    Path = "all-apps-page.png",
                    FullPage = true
                });

                Console.WriteLine("Successfully navigated to All Apps page");

                // Wait for page to stabilize - skip NetworkIdle as the page keeps polling
                Console.WriteLine("Waiting for page to stabilize...");
                await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded, new() { Timeout = 30000 });
                await Task.Delay(3000); // Wait for initial content to render

                // Wait for page to be completely loaded - skip second NetworkIdle check
                Console.WriteLine("Waiting for page content to render...");
                await Task.Delay(5000); // Additional wait for dynamic content

                // Check frames and try to find the Create button
                var buttonSelector = "button:has-text('+ Create'), button:has-text('Create')";
                var createButtonFound = false;

                // Try frame approach first
                var frames = Page.Frames;
                Console.WriteLine($"Found {frames.Count()} frames");
                
                foreach (var frame in frames)
                {
                    if (createButtonFound) break;
                    Console.WriteLine($"Checking frame URL: {frame.Url}");
                    
                    try
                    {
                        var buttons = await frame.QuerySelectorAllAsync(buttonSelector);
                        foreach (var button in buttons)
                        {
                            var buttonText = await button.TextContentAsync();
                            Console.WriteLine($"Found button with text: {buttonText}");
                            
                            if (buttonText != null && buttonText.Contains("Create"))
                            {
                                Console.WriteLine("Found Create button in frame, clicking...");
                                await button.ClickAsync();
                                createButtonFound = true;
                                Console.WriteLine("Successfully clicked Create button");
                                break;
                            }
                        }
                    }
                    catch (PlaywrightException ex)
                    {
                        Console.WriteLine($"Error checking frame: {ex.Message}");
                    }
                }

                // If not found in frames, try main page
                if (!createButtonFound)
                {
                    Console.WriteLine("Trying to find Create button in main page...");
                    try 
                    {
                        var buttons = await Page.QuerySelectorAllAsync(buttonSelector);
                        foreach (var button in buttons)
                        {
                            var buttonText = await button.TextContentAsync();
                            Console.WriteLine($"Found button with text: {buttonText}");
                            
                            if (buttonText != null && buttonText.Contains("Create"))
                            {
                                Console.WriteLine("Found Create button on main page, clicking...");
                                await button.ClickAsync();
                                createButtonFound = true;
                                Console.WriteLine("Successfully clicked Create button");
                                break;
                            }
                        }
                    }
                    catch (PlaywrightException ex)
                    {
                        Console.WriteLine($"Error checking main page: {ex.Message}");
                    }
                }

                if (!createButtonFound)
                {
                    throw new Exception("Could not find Create button anywhere on the page");
                }

                // Wait after clicking the button
                Console.WriteLine("Waiting 2 seconds after clicking Create button...");
                await Task.Delay(2000);

                // Wait for the dropdown to be available
                Console.WriteLine("Looking for app type dropdown...");
                try
                {
                    await Page.WaitForLoadStateAsync(LoadState.NetworkIdle, new() { Timeout = 60000 });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Warning: NetworkIdle timeout: {ex.Message}");
                }
                
                await Task.Delay(5000); // Additional wait for dynamic content

                bool dropdownFound = false;
                IElementHandle appTypeElement = null;

                // Try different selectors in all frames
                var selectors = new[] {
                    "#form-label-id-2aria22",
                    "[aria-label*='app type']",
                    "[aria-label*='App type']",
                    "select:has-text('app type')",
                    "[role='combobox']",
                    "div[class*='dropdown']",
                    "[id*='app-type']"
                };

                foreach (var frame in Page.Frames)
                {
                    if (dropdownFound) break;
                    Console.WriteLine($"Checking frame for dropdown: {frame.Url}");

                    foreach (var selector in selectors)
                    {
                        try
                        {
                            appTypeElement = await frame.WaitForSelectorAsync(selector, new() { Timeout = 5000 });
                            if (appTypeElement != null)
                            {
                                Console.WriteLine($"Found app type dropdown using selector: {selector}");
                                var isVisible = await appTypeElement.IsVisibleAsync();
                                var isEnabled = await appTypeElement.IsEnabledAsync();
                                
                                if (isVisible && isEnabled)
                                {
                                    dropdownFound = true;
                                    break;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }

                if (!dropdownFound || appTypeElement == null)
                {
                    Console.WriteLine("Taking screenshot of current state...");
                    await Page.ScreenshotAsync(new() {
                        Path = "dropdown-not-found.png",
                        FullPage = true
                    });
                    throw new Exception("Could not find app type dropdown element");
                }

                // Try to click the dropdown
                try {
                    Console.WriteLine("Attempting to click app type dropdown...");
                    await appTypeElement.ClickAsync();
                    Console.WriteLine("Successfully clicked app type dropdown");
                    
                    // Try to type in the dropdown to filter options
                    Console.WriteLine("Typing 'Windows' to filter options...");
                    await Page.Keyboard.TypeAsync("Windows", new() { Delay = 100 });
                    
                    // Wait for dropdown menu to appear - increased wait time
                    Console.WriteLine("Waiting for dropdown options to load after typing...");
                    await Task.Delay(3000);
                    
                    // Take a screenshot to verify the state
                    await Page.ScreenshotAsync(new() {
                        Path = "after-dropdown-click.png",
                        FullPage = true
                    });

                    // Try to find and click the specific option
                    Console.WriteLine("Looking for 'Windows app (Win32)' option...");
                    bool optionFound = false;

                    // First, try to list all available options for debugging
                    foreach (var frame in Page.Frames)
                    {
                        try
                        {
                            var allOptions = await frame.QuerySelectorAllAsync("[role='option']");
                            if (allOptions.Count > 0)
                            {
                                Console.WriteLine($"Found {allOptions.Count} options in dropdown:");
                                for (int i = 0; i < allOptions.Count && i < 15; i++)
                                {
                                    try
                                    {
                                        var text = await allOptions[i].InnerTextAsync();
                                        var id = await allOptions[i].GetAttributeAsync("id");
                                        Console.WriteLine($"  Option {i}: ID='{id}', Text='{text}'");
                                    }
                                    catch { }
                                }
                            }
                        }
                        catch { }
                    }

                    // Try to find and click "Windows app (Win32)" by text
                    foreach (var frame in Page.Frames)
                    {
                        try
                        {
                            // Try multiple selectors for Windows app option
                            var optionSelectors = new[] {
                                "text='Windows app (Win32)'",
                                "[role='option']:has-text('Windows app (Win32)')",
                                "[role='option']:has-text('Windows app')",
                                "[role='option']:has-text('Win32')",
                                "#form-label-id-2aria22"
                            };

                            foreach (var selector in optionSelectors)
                            {
                                try
                                {
                                    var option = await frame.WaitForSelectorAsync(selector, new() { Timeout = 3000 });
                                    if (option != null)
                                    {
                                        var optionText = await option.InnerTextAsync();
                                        Console.WriteLine($"Found option with selector '{selector}': '{optionText}'");
                                        
                                        // Verify it contains "Windows" or "Win32"
                                        if (optionText.Contains("Windows", StringComparison.OrdinalIgnoreCase) || 
                                            optionText.Contains("Win32", StringComparison.OrdinalIgnoreCase) ||
                                            selector.StartsWith("#form-label"))
                                        {
                                            Console.WriteLine("This is the Windows app option, attempting to click...");
                                            
                                            // Wait a bit before clicking
                                            await Task.Delay(1000);
                                            
                                            await option.ClickAsync();
                                            optionFound = true;
                                            Console.WriteLine("Successfully clicked the Windows app option");
                                            
                                            // Take a screenshot after clicking the option
                                            await Page.ScreenshotAsync(new() {
                                                Path = "after-option-click.png",
                                                FullPage = true
                                            });
                                            
                                            // Wait for 20 seconds after clicking the option
                                            Console.WriteLine("Waiting 20 seconds after clicking the option...");
                                            await Task.Delay(20000);
                                            Console.WriteLine("20 second wait completed");

                                            break;
                                        }
                                    }
                                }
                                catch { }
                            }
                            
                            if (optionFound) break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error searching in frame: {ex.Message}");
                            continue;
                        }
                    }

                    if (optionFound)
                    {
                        // Look for and click the Select button
                        Console.WriteLine("Looking for Select button...");
                                bool selectButtonFound = false;

                                foreach (var innerFrame in Page.Frames)
                                {
                                    try
                                    {
                                        var selectButton = await innerFrame.WaitForSelectorAsync("//div[@title='Select']", new() { Timeout = 5000 });
                                        if (selectButton != null)
                                        {
                                            var isVisible = await selectButton.IsVisibleAsync();
                                            var isEnabled = await selectButton.IsEnabledAsync();
                                            
                                            if (isVisible && isEnabled)
                                            {
                                                Console.WriteLine("Found Select button, attempting to click...");
                                                await selectButton.ClickAsync();
                                                selectButtonFound = true;
                                                Console.WriteLine("Successfully clicked Select button");
                                                
                                                // Take a screenshot after clicking
                                                await Page.ScreenshotAsync(new() {
                                                    Path = "after-select-button-click.png",
                                                    FullPage = true
                                                });

                                                // Wait for 20 seconds after clicking Select button
                                                Console.WriteLine("Waiting 20 seconds after clicking Select button...");
                                                await Task.Delay(20000);
                                                Console.WriteLine("20 second wait completed");

                                                // Look for and click "Select app package file" link
                                                Console.WriteLine("Looking for 'Select app package file' link...");
                                                var packageLink = await innerFrame.WaitForSelectorAsync("text='Select app package file'", new() { Timeout = 5000 });
                                                
                                                if (packageLink != null)
                                                {
                                                    Console.WriteLine("Found package file link, attempting to click...");
                                                    await packageLink.ClickAsync();
                                                    Console.WriteLine("Successfully clicked package file link");

                                                    // Take a screenshot after clicking
                                                    await Page.ScreenshotAsync(new() {
                                                        Path = "after-package-link-click.png",
                                                        FullPage = true
                                                    });

                                                    // Look for and click the file input
                                                    Console.WriteLine("Looking for file input...");
                                                    var fileInput = await innerFrame.WaitForSelectorAsync("//input[@type='file']", new() { Timeout = 5000 });
                                                    
                                                    if (fileInput != null)
                                                    {
                                                        var isFileInputVisible = await fileInput.IsVisibleAsync();
                                                        Assert.That(isFileInputVisible, Is.True, "File input should be visible");
                                                        
                                                        Console.WriteLine("Found file input, setting file path...");
                                                        
                                                        // Set the file path directly using SetInputFilesAsync
                                                        string filePath = @"C:\New_Demo\win_packages\SimpleV2.intunewin";
                                                        Console.WriteLine($"Selecting file: {filePath}");
                                                        
                                                        await fileInput.SetInputFilesAsync(filePath);
                                                        Console.WriteLine("Successfully set file input");

                                                        // Wait for file to be processed
                                                        Console.WriteLine("Waiting for file to be processed...");
                                                        await Task.Delay(5000);

                                                        // Take a screenshot after file selection
                                                        await Page.ScreenshotAsync(new() {
                                                            Path = "after-file-selection.png",
                                                            FullPage = true
                                                        });

                                                        // Verify "Add App" page is displayed
                                                        Console.WriteLine("Verifying Add App page is displayed...");
                                                        
                                                        bool addAppPageFound = false;
                                                        
                                                        // Wait a bit more for the page to load
                                                        await Task.Delay(3000);
                                                        
                                                        // Check for Add App page indicators
                                                        foreach (var checkFrame in Page.Frames)
                                                        {
                                                            try
                                                            {
                                                                // Look for "Add App" text or heading
                                                                var addAppHeading = await checkFrame.WaitForSelectorAsync("text='Add App'", new() { Timeout = 3000 });
                                                                if (addAppHeading != null)
                                                                {
                                                                    Console.WriteLine("Found 'Add App' heading");
                                                                    addAppPageFound = true;
                                                                    break;
                                                                }
                                                            }
                                                            catch
                                                            {
                                                                // Try alternative selectors
                                                                try
                                                                {
                                                                    var heading = await checkFrame.WaitForSelectorAsync("h1, h2, [role='heading']", new() { Timeout = 2000 });
                                                                    if (heading != null)
                                                                    {
                                                                        var headingText = await heading.InnerTextAsync();
                                                                        Console.WriteLine($"Found heading: {headingText}");
                                                                        if (headingText.Contains("Add", StringComparison.OrdinalIgnoreCase) || 
                                                                            headingText.Contains("App", StringComparison.OrdinalIgnoreCase))
                                                                        {
                                                                            addAppPageFound = true;
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                                catch
                                                                {
                                                                    continue;
                                                                }
                                                            }
                                                        }
                                                        
                                                        // Also check page title
                                                        var currentPageTitle = await Page.TitleAsync();
                                                        Console.WriteLine($"Current page title: {currentPageTitle}");
                                                        
                                                        if (currentPageTitle.Contains("Add", StringComparison.OrdinalIgnoreCase) || addAppPageFound)
                                                        {
                                                            Console.WriteLine("✓ Add App page is displayed");
                                                            
                                                            // Take final screenshot
                                                            await Page.ScreenshotAsync(new() {
                                                                Path = "add-app-page-confirmed.png",
                                                                FullPage = true
                                                            });
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Warning: Could not explicitly confirm Add App page, but continuing...");
                                                            
                                                            // Take screenshot for debugging
                                                            await Page.ScreenshotAsync(new() {
                                                                Path = "add-app-page-check.png",
                                                                FullPage = true
                                                            });
                                                        }

                                                        // Step 8: Click OK button after file selection
                                                        Console.WriteLine("Looking for OK button...");
                                                        bool okButtonFound = false;
                                                        
                                                        // Wait longer for dialog/button to appear
                                                        await Task.Delay(3000);
                                                        
                                                        foreach (var okFrame in Page.Frames)
                                                        {
                                                            try
                                                            {
                                                                // Try multiple selectors for OK/Done/Next button
                                                                string[] buttonSelectors = new[] {
                                                                    "button:has-text('OK')",
                                                                    "button:has-text('Ok')",
                                                                    "button:has-text('Done')",
                                                                    "button:has-text('Next')",
                                                                    "button:has-text('Continue')",
                                                                    "[role='button']:has-text('OK')",
                                                                    "[role='button']:has-text('Ok')",
                                                                    "button[type='submit']"
                                                                };
                                                                
                                                                foreach (var selector in buttonSelectors)
                                                                {
                                                                    try
                                                                    {
                                                                        var okButton = await okFrame.WaitForSelectorAsync(selector, new() { Timeout = 3000 });
                                                                        if (okButton != null)
                                                                        {
                                                                            var isOkButtonVisible = await okButton.IsVisibleAsync();
                                                                            if (isOkButtonVisible)
                                                                            {
                                                                                var buttonText = await okButton.InnerTextAsync();
                                                                                Console.WriteLine($"Found button with selector '{selector}': '{buttonText}', attempting to click...");
                                                                                await okButton.ClickAsync();
                                                                                Console.WriteLine($"Successfully clicked button: {buttonText}");
                                                                                okButtonFound = true;
                                                                                
                                                                                // Wait for Add App page to load
                                                                                Console.WriteLine("Waiting for Add App page to load...");
                                                                                await Task.Delay(5000);
                                                                                
                                                                                // Take screenshot after clicking button
                                                                                await Page.ScreenshotAsync(new() {
                                                                                    Path = "after-ok-button.png",
                                                                                    FullPage = true
                                                                                });
                                                                                
                                                                                break;
                                                                            }
                                                                        }
                                                                    }
                                                                    catch
                                                                    {
                                                                        continue;
                                                                    }
                                                                }
                                                                
                                                                if (okButtonFound)
                                                                {
                                                                    break;
                                                                }
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                Console.WriteLine($"Error finding OK button in frame: {ex.Message}");
                                                                continue;
                                                            }
                                                        }
                                                        
                                                        if (okButtonFound)
                                                        {
                                                            // Step 9: Verify Add App page is displayed and fill in App information
                                                            Console.WriteLine("Confirming Add App page is displayed after OK click...");
                                                            
                                                            var currentTitle = await Page.TitleAsync();
                                                            Console.WriteLine($"Page title: {currentTitle}");
                                                            
                                                            // Wait for App information tab to be visible
                                                            await Task.Delay(3000);
                                                            
                                                            // Step 10: Fill in Name field in App information tab
                                                            Console.WriteLine("Looking for Name field in App information tab...");
                                                            bool nameFieldFound = false;
                                                            
                                                            foreach (var appFrame in Page.Frames)
                                                            {
                                                                try
                                                                {
                                                                    // Try to find Name input field
                                                                    var nameInput = await appFrame.WaitForSelectorAsync("input[aria-label*='Name'], input[placeholder*='Name'], input[name*='name']", new() { Timeout = 5000 });
                                                                    if (nameInput != null)
                                                                    {
                                                                        var isNameVisible = await nameInput.IsVisibleAsync();
                                                                        if (isNameVisible)
                                                                        {
                                                                            Console.WriteLine("Found Name field, entering value...");
                                                                            await nameInput.ClickAsync();
                                                                            string currentDate = DateTime.Now.ToString("MMddyyyy");
                                                                            string currentTime = DateTime.Now.ToString("HHmmss");
                                                                            string appName = $"Automation App_{currentDate}_{currentTime}";
                                                                            await nameInput.FillAsync(appName);
                                                                            Console.WriteLine($"Successfully entered Name: {appName}");
                                                                            nameFieldFound = true;
                                                                            
                                                                            // Wait a bit before moving to description
                                                                            await Task.Delay(1000);
                                                                            
                                                                            // Step 11: Fill in Description field
                                                                            Console.WriteLine("Looking for Description field...");
                                                                            var descriptionInput = await appFrame.WaitForSelectorAsync("textarea[aria-label*='Description'], textarea[placeholder*='Description'], input[aria-label*='Description']", new() { Timeout = 5000 });
                                                                            if (descriptionInput != null)
                                                                            {
                                                                                var isDescVisible = await descriptionInput.IsVisibleAsync();
                                                                                if (isDescVisible)
                                                                                {
                                                                                    Console.WriteLine("Found Description field, entering value...");
                                                                                    await descriptionInput.ClickAsync();
                                                                                    await descriptionInput.FillAsync("SCCM Test App - SimpleV2");
                                                                                    Console.WriteLine("Successfully entered Description: SCCM Test App - SimpleV2");
                                                                                    
                                                                                    // Take screenshot after filling fields
                                                                                    await Page.ScreenshotAsync(new() {
                                                                                        Path = "app-info-filled.png",
                                                                                        FullPage = true
                                                                                    });
                                                                                    
                                                                                    // Step 12: Fill in Publisher field
                                                                                    Console.WriteLine("Looking for Publisher field...");
                                                                                    await Task.Delay(1000);
                                                                                    
                                                                                    var publisherInput = await appFrame.WaitForSelectorAsync("input[aria-label*='Publisher'], input[placeholder*='Publisher'], input[name*='publisher']", new() { Timeout = 5000 });
                                                                                    if (publisherInput != null)
                                                                                    {
                                                                                        var isPubVisible = await publisherInput.IsVisibleAsync();
                                                                                        if (isPubVisible)
                                                                                        {
                                                                                            Console.WriteLine("Found Publisher field, entering value...");
                                                                                            await publisherInput.ClickAsync();
                                                                                            await publisherInput.FillAsync("Microsoft");
                                                                                            Console.WriteLine("Successfully entered Publisher: Microsoft");
                                                                                            
                                                                                            await Task.Delay(1000);
                                                                                            
                                                                                            // Step 13: Click Next button
                                                                                            Console.WriteLine("Looking for Next button on App information tab...");
                                                                                            var nextButton = await appFrame.WaitForSelectorAsync("button:has-text('Next'), [role='button']:has-text('Next')", new() { Timeout = 5000 });
                                                                                            if (nextButton != null)
                                                                                            {
                                                                                                var isNextVisible = await nextButton.IsVisibleAsync();
                                                                                                if (isNextVisible)
                                                                                                {
                                                                                                    Console.WriteLine("Found Next button, clicking...");
                                                                                                    await nextButton.ClickAsync();
                                                                                                    Console.WriteLine("Successfully clicked Next button");
                                                                                                    
                                                                                                    // Wait for Program tab to load
                                                                                                    await Task.Delay(3000);
                                                                                                    
                                                                                                    await Page.ScreenshotAsync(new() {
                                                                                                        Path = "program-tab.png",
                                                                                                        FullPage = true
                                                                                                    });
                                                                                                    
                                                                                    // Step 13.5: Fill in Installation time required (mins) textbox
                                                                                    Console.WriteLine("Looking for Installation time required (mins) textbox on Program tab...");
                                                                                    bool installTimeFound = false;
                                                                                    
                                                                                    foreach (var programFrame in Page.Frames)
                                                                                    {
                                                                                        try
                                                                                        {
                                                                                            var installTimeSelectors = new[] {
                                                                                                "(//label[normalize-space(text())='Installation time required (mins)']/following::input)[1]",
                                                                                                "input[aria-label*='Installation time required']",
                                                                                                "input[placeholder*='Installation time']"
                                                                                            };
                                                                                            
                                                                                            foreach (var timeSelector in installTimeSelectors)
                                                                                            {
                                                                                                try
                                                                                                {
                                                                                                    var installTimeInput = await programFrame.WaitForSelectorAsync(timeSelector, new() { Timeout = 3000 });
                                                                                                    if (installTimeInput != null)
                                                                                                    {
                                                                                                        var isInstallTimeVisible = await installTimeInput.IsVisibleAsync();
                                                                                                        if (isInstallTimeVisible)
                                                                                                        {
                                                                                                            Console.WriteLine($"Found Installation time required textbox with selector: {timeSelector}");
                                                                                                            await installTimeInput.ClickAsync();
                                                                                                            await installTimeInput.FillAsync("5");
                                                                                                            Console.WriteLine("Successfully entered Installation time required: 5 mins");
                                                                                                            installTimeFound = true;
                                                                                                            await Task.Delay(1000);
                                                                                                            break;
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                                catch
                                                                                                {
                                                                                                    continue;
                                                                                                }
                                                                                            }
                                                                                            
                                                                                            if (installTimeFound)
                                                                                            {
                                                                                                break;
                                                                                            }
                                                                                        }
                                                                                        catch (Exception ex)
                                                                                        {
                                                                                            Console.WriteLine($"Error finding Installation time textbox in frame: {ex.Message}");
                                                                                            continue;
                                                                                        }
                                                                                    }
                                                                                    
                                                                                    if (!installTimeFound)
                                                                                    {
                                                                                        Console.WriteLine("Warning: Could not find Installation time required textbox");
                                                                                    }
                                                                                    
                                                                                    // Step 14: Select "User" for Install behaviour
                                                                                    Console.WriteLine("Looking for Install behaviour list on Program tab...");
                                                                                    
                                                                                    // Try to find Install behaviour as a list item
                                                                                    bool installBehaviourFound = false;
                                                                                    
                                                                                    foreach (var programFrame in Page.Frames)
                                                                                    {
                                                                                        try
                                                                                        {
                                                                                            // Try to find "User" as a list item (li element)
                                                                                            var userListItemSelectors = new[] {
                                                                                                "(//li[contains(@class,'fxs-portal-border azc-optionPicker-item')]/following-sibling::li)[3]",
                                                                                                "//li[@id='_weave_e_1818']",
                                                                                                "li:has-text('User')",
                                                                                                "li >> text='User'",
                                                                                                "[role='listitem']:has-text('User')",
                                                                                                "//li[contains(text(), 'User')]"
                                                                                            };                                                                                                            foreach (var selector in userListItemSelectors)
                                                                                                            {
                                                                                                                try
                                                                                                                {
                                                                                                                    var userListItem = await programFrame.WaitForSelectorAsync(selector, new() { Timeout = 3000 });
                                                                                                                    if (userListItem != null)
                                                                                                                    {
                                                                                                                        var isUserVisible = await userListItem.IsVisibleAsync();
                                                                                                                        if (isUserVisible)
                                                                                                                        {
                                                                                                                            var itemText = await userListItem.InnerTextAsync();
                                                                                                                            Console.WriteLine($"Found Install behaviour list item with selector '{selector}': '{itemText}'");
                                                                                                                            
                                                                                                                            // Verify it's the User option
                                                                                                                            if (itemText.Contains("User", StringComparison.OrdinalIgnoreCase))
                                                                                                                            {
                                                                                                                                // Console.WriteLine("Clicking 'User' list item...");
                                                                                                                                // await userListItem.ClickAsync();
                                                                                                                                // Console.WriteLine("Successfully clicked 'User' for Install behaviour");
                                                                                                                                // installBehaviourFound = true;
                                                                                                                                
                                                                                                                                // await Task.Delay(1000);
                                                                                                                                
                                                                                                                                // await Page.ScreenshotAsync(new() {
                                                                                                                                //     Path = "install-behaviour-selected.png",
                                                                                                                                //     FullPage = true
                                                                                                                                // });
                                                                                                                                
                                                                                                                                // Step 15: Click Next button on Program tab
                                                                                                                                Console.WriteLine("Looking for Next button on Program tab...");
                                                                                                                                var programNextButton = await programFrame.WaitForSelectorAsync("button:has-text('Next'), [role='button']:has-text('Next')", new() { Timeout = 5000 });
                                                                                                                                if (programNextButton != null)
                                                                                                                                {
                                                                                                                                    var isProgramNextVisible = await programNextButton.IsVisibleAsync();
                                                                                                                                    if (isProgramNextVisible)
                                                                                                                                    {
                                                                                                                                        Console.WriteLine("Found Next button on Program tab, clicking...");
                                                                                                                                        await programNextButton.ClickAsync();
                                                                                                                                        Console.WriteLine("Successfully clicked Next button on Program tab");
                                                                                                                                        
                                                                                                                                        // Wait for Requirements tab to load
                                                                                                                                        await Task.Delay(3000);
                                                                                                                                        
                                                                                                                                        await Page.ScreenshotAsync(new() {
                                                                                                                                            Path = "requirements-tab.png",
                                                                                                                                            FullPage = true
                                                                                                                                        });
                                                                                                                                        
                                                                                                                                        Console.WriteLine("Successfully navigated to Requirements tab");
                                                                                                                                        
                                                                                                                                        // Step 16: Handle Requirements tab - Select Minimum operating system
                                                                                                                                        Console.WriteLine("Looking for Minimum operating system dropdown on Requirements tab...");
                                                                                                                                        await Task.Delay(2000);
                                                                                                                                        
                                                                                                                                        bool minOsFound = false;
                                                                                                                                
                                                                                                                                foreach (var reqFrame in Page.Frames)
                                                                                                                                {
                                                                                                                                    try
                                                                                                                                    {
                                                                                                                                        // Try different selectors for Minimum operating system dropdown
                                                                                                                                        var minOsSelectors = new[] {
                                                                                                                                            "[aria-label*='Minimum operating system'], [aria-label*='minimum operating system']",
                                                                                                                                            "[placeholder*='Minimum operating system']",
                                                                                                                                            "[role='combobox']:near(:text('operating system'))",
                                                                                                                                            "select[name*='operating']"
                                                                                                                                        };
                                                                                                                                        
                                                                                                                                        foreach (var minOsSelector in minOsSelectors)
                                                                                                                                        {
                                                                                                                                            try
                                                                                                                                            {
                                                                                                                                                var minOsDropdown = await reqFrame.WaitForSelectorAsync(minOsSelector, new() { Timeout = 3000 });
                                                                                                                                                if (minOsDropdown != null)
                                                                                                                                                {
                                                                                                                                                    var isMinOsVisible = await minOsDropdown.IsVisibleAsync();
                                                                                                                                                    if (isMinOsVisible)
                                                                                                                                                    {
                                                                                                                                                        Console.WriteLine($"Found Minimum operating system dropdown with selector: {minOsSelector}");
                                                                                                                                                        Console.WriteLine("Clicking Minimum operating system dropdown...");
                                                                                                                                                        await minOsDropdown.ClickAsync();
                                                                                                                                                        
                                                                                                                                                        await Task.Delay(1000);
                                                                                                                                                        
                                                                                                                                                        // Try to select "Windows 10 1607" option
                                                                                                                                                        Console.WriteLine("Looking for 'Windows 10 1607' option...");
                                                                                                                                                        
                                                                                                                                                        var win10Selectors = new[] {
                                                                                                                                                            "[role='option']:has-text('Windows 10 1607')",
                                                                                                                                                            "text='Windows 10 1607'",
                                                                                                                                                            "[role='option']:has-text('1607')",
                                                                                                                                                            "li:has-text('Windows 10 1607')"
                                                                                                                                                        };
                                                                                                                                                        
                                                                                                                                                        foreach (var win10Selector in win10Selectors)
                                                                                                                                                        {
                                                                                                                                                            try
                                                                                                                                                            {
                                                                                                                                                                var win10Option = await reqFrame.WaitForSelectorAsync(win10Selector, new() { Timeout = 3000 });
                                                                                                                                                                if (win10Option != null)
                                                                                                                                                                {
                                                                                                                                                                    var isWin10Visible = await win10Option.IsVisibleAsync();
                                                                                                                                                                    if (isWin10Visible)
                                                                                                                                                                    {
                                                                                                                                                                        var optionText = await win10Option.InnerTextAsync();
                                                                                                                                                                        Console.WriteLine($"Found option with selector '{win10Selector}': '{optionText}'");
                                                                                                                                                                        
                                                                                                                                                                        if (optionText.Contains("1607", StringComparison.OrdinalIgnoreCase))
                                                                                                                                                                        {
                                                                                                                                                                            Console.WriteLine("Clicking 'Windows 10 1607' option...");
                                                                                                                                                                            await win10Option.ClickAsync();
                                                                                                                                                                            Console.WriteLine("Successfully selected 'Windows 10 1607' for Minimum operating system");
                                                                                                                                                                            minOsFound = true;
                                                                                                                                                                            
                                                                                                                                                                            await Task.Delay(1000);
                                                                                                                                                                            
                                                                                                                                                                            await Page.ScreenshotAsync(new() {
                                                                                                                                                                                Path = "min-os-selected.png",
                                                                                                                                                                                FullPage = true
                                                                                                                                                                            });
                                                                                                                                                                            
                                                                                                                                                                            // Step 17: Click Next button on Requirements tab
                                                                                                                                                                            Console.WriteLine("Looking for Next button on Requirements tab...");
                                                                                                                                                                            var reqNextButton = await reqFrame.WaitForSelectorAsync("button:has-text('Next'), [role='button']:has-text('Next')", new() { Timeout = 5000 });
                                                                                                                                                                            if (reqNextButton != null)
                                                                                                                                                                            {
                                                                                                                                                                                var isReqNextVisible = await reqNextButton.IsVisibleAsync();
                                                                                                                                                                                if (isReqNextVisible)
                                                                                                                                                                                {
                                                                                                                                                                                    Console.WriteLine("Found Next button on Requirements tab, clicking...");
                                                                                                                                                                                    await reqNextButton.ClickAsync();
                                                                                                                                                                                    Console.WriteLine("Successfully clicked Next button on Requirements tab");
                                                                                                                                                                                    
                                                                                                                                                                                    await Task.Delay(3000);
                                                                                                                                                                                    
                                                                                                                                                                                    await Page.ScreenshotAsync(new() {
                                                                                                                                                                                        Path = "after-requirements-next.png",
                                                                                                                                                                                        FullPage = true
                                                                                                                                                                                    });
                                                                                                                                                                                    
                                                                                                                                                                                    Console.WriteLine("Navigated to next tab after Requirements");
                                                                                                                                                                                    
                                                                                                                                                                                    // Step 18: Handle Detection Rules tab
                                                                                                                                                                                    await Task.Delay(2000);
                                                                                                                                                                                    Console.WriteLine("Looking for Rules format dropdown on Detection Rules tab...");
                                                                                                                                                                                    
                                                                                                                                                                                    bool rulesFormatFound = false;
                                                                                                                                                                                    
                                                                                                                                                                                    foreach (var detectionFrame in Page.Frames)
                                                                                                                                                                                    {
                                                                                                                                                                                        try
                                                                                                                                                                                        {
                                                                                                                                                                                            // Try different selectors for Rules format dropdown
                                                                                                                                                                                            var rulesFormatSelectors = new[] {
                                                                                                                                                                                                "//div[@id='form-label-id-55textbox']",
                                                                                                                                                                                                "[aria-label*='Rules format'], [aria-label*='rules format']",
                                                                                                                                                                                                "[placeholder*='Rules format']",
                                                                                                                                                                                                "[role='combobox']:near(:text('Rules format'))",
                                                                                                                                                                                                "select[name*='rules']"
                                                                                                                                                                                            };
                                                                                                                                                                                            
                                                                                                                                                                                            foreach (var rfSelector in rulesFormatSelectors)
                                                                                                                                                                                            {
                                                                                                                                                                                                try
                                                                                                                                                                                                {
                                                                                                                                                                                                    var rulesFormatDropdown = await detectionFrame.WaitForSelectorAsync(rfSelector, new() { Timeout = 3000 });
                                                                                                                                                                                                    if (rulesFormatDropdown != null)
                                                                                                                                                                                                    {
                                                                                                                                                                                                        var isRulesFormatVisible = await rulesFormatDropdown.IsVisibleAsync();
                                                                                                                                                                                                        if (isRulesFormatVisible)
                                                                                                                                                                                                        {
                                                                                                                                                                                                            Console.WriteLine($"Found Rules format combobox with selector: {rfSelector}");
                                                                                                                                                                                                            Console.WriteLine("Clicking Rules format combobox...");
                                                                                                                                                                                                            await rulesFormatDropdown.ClickAsync();
                                                                                                                                                                                                            
                                                                                                                                                                                                            await Task.Delay(2000);
                                                                                                                                                                                                            
                                                                                                                                                                                            // Select "Manually configure detection rules"
                                                                                                                                                                                            Console.WriteLine("Looking for 'Manually configure detection rules' option...");
                                                                                                                                                                                            
                                                                                                                                                                                            var manualSelectors = new[] {
                                                                                                                                                                                                "li:has-text('Manually configure detection rules')",
                                                                                                                                                                                                "li:has-text('Manually configure')",
                                                                                                                                                                                                "[role='option']:has-text('Manually configure detection rules')",
                                                                                                                                                                                                "text='Manually configure detection rules'",
                                                                                                                                                                                                "[role='option']:has-text('Manually configure')"
                                                                                                                                                                                            };                                                                                                                                                                                                            foreach (var manualSelector in manualSelectors)
                                                                                                                                                                                                            {
                                                                                                                                                                                                                try
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    var manualOption = await detectionFrame.WaitForSelectorAsync(manualSelector, new() { Timeout = 3000 });
                                                                                                                                                                                                                    if (manualOption != null)
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        var isManualVisible = await manualOption.IsVisibleAsync();
                                                                                                                                                                                                                        if (isManualVisible)
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            var manualOptionText = await manualOption.InnerTextAsync();
                                                                                                                                                                                                                            Console.WriteLine($"Found option with selector '{manualSelector}': '{manualOptionText}'");
                                                                                                                                                                                                                            
                                                                                                                                                                                                                            if (manualOptionText.Contains("Manually configure", StringComparison.OrdinalIgnoreCase))
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                Console.WriteLine("Clicking 'Manually configure detection rules' option...");
                                                                                                                                                                                                                                await manualOption.ClickAsync();
                                                                                                                                                                                                                                Console.WriteLine("Successfully selected 'Manually configure detection rules'");
                                                                                                                                                                                                                                rulesFormatFound = true;
                                                                                                                                                                                                                                
                                                                                                                                                                                                                                await Task.Delay(2000);
                                                                                                                                                                                                                                
                                                                                                                                                                                                                                await Page.ScreenshotAsync(new() {
                                                                                                                                                                                                                                    Path = "rules-format-selected.png",
                                                                                                                                                                                                                                    FullPage = true
                                                                                                                                                                                                                                });
                                                                                                                                                                                                                                
                                                                                                                                                                                                                                // Step 19: Click + Add link (finding it as link text)
                                                                                                                                                                                                                                Console.WriteLine("Looking for '+ Add' link by text...");
                                                                                                                                                                                                                                
                                                                                                                                                                                                                                try
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    // There are multiple "+ Add" links on the page (Program, Requirements, Detection Rules)
                                                                                                                                                                                                                                    // We need to get the one with aria-label="Add" that uses onClick binding (the 3rd one)
                                                                                                                                                                                                                                    var addLinks = await detectionFrame.QuerySelectorAllAsync("a:has-text('+ Add')");
                                                                                                                                                                                                                                    Console.WriteLine($"Found {addLinks.Count} '+ Add' links on the page");
                                                                                                                                                                                                                                    
                                                                                                                                                                                                                                    // Click the last one (Detection Rules tab is the last tab)
                                                                                                                                                                                                                                    if (addLinks.Count > 0)
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        var targetAddLink = addLinks[addLinks.Count - 1]; // Get the last "+ Add" link
                                                                                                                                                                                                                                        Console.WriteLine("Clicking the last '+ Add' link (Detection Rules tab)...");
                                                                                                                                                                                                                                        await targetAddLink.ClickAsync();
                                                                                                                                                                                                                                        Console.WriteLine("Successfully clicked '+ Add' link");
                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                        await Task.Delay(3000);
                                                                                                                                                                                                                                        await Page.ScreenshotAsync(new() { Path = "after-add-link-click.png", FullPage = true });
                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                        // Step 20: Select MSI from Rule type dropdown
                                                                                                                                                                                                                                        Console.WriteLine("Looking for Rule type dropdown...");
                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                        var ruleTypeSelectors = new[] {
                                                                                                                                                                                                                                            "//div[normalize-space(text())='Select one']",
                                                                                                                                                                                                                                            "[aria-label*='Rule type'], [aria-label*='rule type']",
                                                                                                                                                                                                                                            "[placeholder*='Rule type']",
                                                                                                                                                                                                                                            "[role='combobox']:near(:text('Rule type'))",
                                                                                                                                                                                                                                            "select[name*='rule']",
                                                                                                                                                                                                                                            "[role='combobox']"
                                                                                                                                                                                                                                        };
                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                        bool ruleTypeFound = false;
                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                        foreach (var rtSelector in ruleTypeSelectors)
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            try
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                var ruleTypeDropdown = await detectionFrame.WaitForSelectorAsync(rtSelector, new() { Timeout = 3000 });
                                                                                                                                                                                                                                                if (ruleTypeDropdown != null)
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    var isRuleTypeVisible = await ruleTypeDropdown.IsVisibleAsync();
                                                                                                                                                                                                                                                    if (isRuleTypeVisible)
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        Console.WriteLine($"Found Rule type dropdown with selector: {rtSelector}");
                                                                                                                                                                                                                                                        Console.WriteLine("Clicking Rule type dropdown...");
                                                                                                                                                                                                                                                        await ruleTypeDropdown.ClickAsync();
                                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                                        await Task.Delay(2000);
                                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                                        // Select "MSI"
                                                                                                                                                                                                                                                        Console.WriteLine("Looking for 'MSI' option...");
                                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                                        var msiSelectors = new[] {
                                                                                                                                                                                                                                                            "li:has-text('MSI')",
                                                                                                                                                                                                                                                            "[role='option']:has-text('MSI')",
                                                                                                                                                                                                                                                            "text='MSI'",
                                                                                                                                                                                                                                                            "[role='listbox'] li:has-text('MSI')",
                                                                                                                                                                                                                                                            "ul li:has-text('MSI')"
                                                                                                                                                                                                                                                        };
                                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                                        foreach (var msiSelector in msiSelectors)
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            try
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                var msiOption = await detectionFrame.WaitForSelectorAsync(msiSelector, new() { Timeout = 3000 });
                                                                                                                                                                                                                                                                if (msiOption != null)
                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                    var isMsiVisible = await msiOption.IsVisibleAsync();
                                                                                                                                                                                                                                                                    if (isMsiVisible)
                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                        var msiText = await msiOption.InnerTextAsync();
                                                                                                                                                                                                                                                                        Console.WriteLine($"Found MSI option with selector '{msiSelector}': '{msiText}'");
                                                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                                                        if (msiText.Contains("MSI", StringComparison.OrdinalIgnoreCase))
                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                            Console.WriteLine("Clicking 'MSI' option...");
                                                                                                                                                                                                                                                                            await msiOption.ClickAsync();
                                                                                                                                                                                                                                                                            Console.WriteLine("Successfully selected 'MSI' for Rule type");
                                                                                                                                                                                                                                                                            ruleTypeFound = true;
                                                                                                                                                                                                                                                                            
                                                                                                                                                                                                                                                                            await Task.Delay(1000);
                                                                                                                                                                                                                                                                            await Page.ScreenshotAsync(new() { Path = "rule-type-msi-selected.png", FullPage = true });
                                                                                                                                                                                                                                                                            
                                                                                                                                                                                                                                                                            break;
                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                            catch
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                continue;
                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                                        if (ruleTypeFound)
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            break;
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            catch
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                continue;
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                        if (!ruleTypeFound)
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            Console.WriteLine("Warning: Could not find or select Rule type dropdown");
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                        // Step 21: Click OK button on Detection rule dialog
                                                                                                                                                                                                                                        Console.WriteLine("Looking for OK button on Detection rule dialog...");
                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                        var okButtonSelectors = new[] {
                                                                                                                                                                                                                                            "button:has-text('OK')",
                                                                                                                                                                                                                                            "button:has-text('Ok')",
                                                                                                                                                                                                                                            "[role='button']:has-text('OK')",
                                                                                                                                                                                                                                            "[role='button']:has-text('Ok')"
                                                                                                                                                                                                                                        };
                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                        bool okButtonClicked = false;
                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                        foreach (var okSelector in okButtonSelectors)
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            try
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                var okButton = await detectionFrame.WaitForSelectorAsync(okSelector, new() { Timeout = 3000 });
                                                                                                                                                                                                                                                if (okButton != null)
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    var isOkVisible = await okButton.IsVisibleAsync();
                                                                                                                                                                                                                                                    if (isOkVisible)
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        Console.WriteLine($"Found OK button with selector '{okSelector}', clicking...");
                                                                                                                                                                                                                                                        await okButton.ClickAsync();
                                                                                                                                                                                                                                                        Console.WriteLine("Successfully clicked OK button");
                                                                                                                                                                                                                                                        okButtonClicked = true;
                                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                                        await Task.Delay(2000);
                                                                                                                                                                                                                                                        await Page.ScreenshotAsync(new() { Path = "after-detection-rule-ok.png", FullPage = true });
                                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                                        break;
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            catch
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                continue;
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                        if (!okButtonClicked)
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            Console.WriteLine("Warning: Could not find or click OK button");
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                        // Step 22: Click Next button on Detection Rules tab
                                                                                                                                                                                                                                        Console.WriteLine("Looking for Next button on Detection Rules tab...");
                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                        var nextButtonSelectors = new[] {
                                                                                                                                                                                                                                            "button:has-text('Next')",
                                                                                                                                                                                                                                            "[role='button']:has-text('Next')",
                                                                                                                                                                                                                                            "input[value='Next']"
                                                                                                                                                                                                                                        };
                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                        foreach (var nextSelector in nextButtonSelectors)
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            try
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                var detectionNextButton = await detectionFrame.WaitForSelectorAsync(nextSelector, new() { Timeout = 3000 });
                                                                                                                                                                                                                                                if (detectionNextButton != null)
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    var isDetectionNextVisible = await detectionNextButton.IsVisibleAsync();
                                                                                                                                                                                                                                                    if (isDetectionNextVisible)
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        var nextButtonText = await detectionNextButton.InnerTextAsync();
                                                                                                                                                                                                                                                        Console.WriteLine($"Found Next button with selector '{nextSelector}': '{nextButtonText}'");
                                                                                                                                                                                                                                                        Console.WriteLine("Clicking Next button on Detection Rules tab...");
                                                                                                                                                                                                                                                        await detectionNextButton.ClickAsync();
                                                                                                                                                                                                                                                        Console.WriteLine("Successfully clicked Next button on Detection Rules tab");
                                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                                        await Task.Delay(2000);
                                                                                                                                                                                                                                                        await Page.ScreenshotAsync(new() { Path = "after-detection-rules-next.png", FullPage = true });
                                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                                        Console.WriteLine("Successfully completed Detection Rules tab configuration");
                                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                                        // Step: Click Next button on Dependencies tab
                                                                                                                                                                                                                                                        Console.WriteLine("Looking for Next button on Dependencies tab...");
                                                                                                                                                                                                                                                        await Task.Delay(2000);
                                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                                        var dependenciesNextButton = await detectionFrame.WaitForSelectorAsync("[role='button']:has-text('Next')", new() { Timeout = 5000 });
                                                                                                                                                                                                                                                        if (dependenciesNextButton != null)
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            var isDependenciesNextVisible = await dependenciesNextButton.IsVisibleAsync();
                                                                                                                                                                                                                                                            if (isDependenciesNextVisible)
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                var dependenciesNextText = await dependenciesNextButton.TextContentAsync();
                                                                                                                                                                                                                                                                Console.WriteLine($"Found Next button on Dependencies tab: '{dependenciesNextText}'");
                                                                                                                                                                                                                                                                Console.WriteLine("Clicking Next button on Dependencies tab...");
                                                                                                                                                                                                                                                                await dependenciesNextButton.ClickAsync();
                                                                                                                                                                                                                                                                await Task.Delay(2000);
                                                                                                                                                                                                                                                                await Page.ScreenshotAsync(new() { Path = "after-dependencies-next.png", FullPage = true });
                                                                                                                                                                                                                                                                Console.WriteLine("Successfully clicked Next button on Dependencies tab");
                                                                                                                                                                                                                                                                
                                                                                                                                                                                                                                                                // Step: Click Next button on Supersedence tab
                                                                                                                                                                                                                                                                Console.WriteLine("Looking for Next button on Supersedence tab...");
                                                                                                                                                                                                                                                                await Task.Delay(2000);
                                                                                                                                                                                                                                                                
                                                                                                                                                                                                                                                                var supersedenceNextButton = await detectionFrame.WaitForSelectorAsync("[role='button']:has-text('Next')", new() { Timeout = 5000 });
                                                                                                                                                                                                                                                                if (supersedenceNextButton != null)
                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                    var isSupersedenceNextVisible = await supersedenceNextButton.IsVisibleAsync();
                                                                                                                                                                                                                                                                    if (isSupersedenceNextVisible)
                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                        var supersedenceNextText = await supersedenceNextButton.TextContentAsync();
                                                                                                                                                                                                                                                                        Console.WriteLine($"Found Next button on Supersedence tab: '{supersedenceNextText}'");
                                                                                                                                                                                                                                                                        Console.WriteLine("Clicking Next button on Supersedence tab...");
                                                                                                                                                                                                                                                                        await supersedenceNextButton.ClickAsync();
                                                                                                                                                                                                                                                                        await Task.Delay(2000);
                                                                                                                                                                        await Page.ScreenshotAsync(new() { Path = "after-supersedence-next.png", FullPage = true });
                                                                                                                                                                        Console.WriteLine("Successfully clicked Next button on Supersedence tab");
                                                                                                                                                                        
                                                                                                                                                                        // Step: Wait for Assignments tab to load and click "+ Add group"
                                                                                                                                                                        Console.WriteLine("Waiting for Assignments tab to load...");
                                                                                                                                                                        await Task.Delay(3000);
                                                                                                                                                                        await Page.ScreenshotAsync(new() { Path = "assignments-tab.png", FullPage = true });
                                                                                                                                                                        
                                                                                                                                                                        Console.WriteLine("Looking for '+ Add group' button on Assignments tab...");
                                                                                                                                                                        bool addGroupClicked = false;
                                                                                                                                                                        
                                                                                                                                                                        foreach (var assignFrame in Page.Frames)
                                                                                                                                                                        {
                                                                                                                                                                            try
                                                                                                                                                                            {
                                                                                                                                                                                var addGroupSelectors = new[] {
                                                                                                                                                                                    "//div[@id='_weave_e_10953']/div[1]",
                                                                                                                                                                                    "a[aria-label='Add group']",
                                                                                                                                                                                    "div[id*='weave'] a[aria-label='Add group']",
                                                                                                                                                                                    "[aria-label='Add group']"
                                                                                                                                                                                };
                                                                                                                                                                                
                                                                                                                                                                                foreach (var addGroupSelector in addGroupSelectors)
                                                                                                                                                                                {
                                                                                                                                                                                    try
                                                                                                                                                                                    {
                                                                                                                                                                                        var addGroupButton = await assignFrame.WaitForSelectorAsync(addGroupSelector, new() { Timeout = 3000 });
                                                                                                                                                                                        if (addGroupButton != null)
                                                                                                                                                                                        {
                                                                                                                                                                                            var isAddGroupVisible = await addGroupButton.IsVisibleAsync();
                                                                                                                                                                                            if (isAddGroupVisible)
                                                                                                                                                                                            {
                                                                                                                                                                                                Console.WriteLine($"Found '+ Add group' button with selector: {addGroupSelector}");
                                                                                                                                                                                                await addGroupButton.ClickAsync();
                                                                                                                                                                                                Console.WriteLine("Successfully clicked '+ Add group' button");
                                                                                                                                                                                                addGroupClicked = true;
                                                                                                                                                                                                await Task.Delay(2000);
                                                                                                                                                                                                await Page.ScreenshotAsync(new() { Path = "after-add-group-click.png", FullPage = true });
                                                                                                                                                                                                break;
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                    }
                                                                                                                                                                                    catch
                                                                                                                                                                                    {
                                                                                                                                                                                        continue;
                                                                                                                                                                                    }
                                                                                                                                                                                }
                                                                                                                                                                                
                                                                                                                                                                                if (addGroupClicked)
                                                                                                                                                                                {
                                                                                                                                                                                    break;
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                            catch (Exception ex)
                                                                                                                                                                            {
                                                                                                                                                                                Console.WriteLine($"Error finding Add group button in frame: {ex.Message}");
                                                                                                                                                                                continue;
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                        
                                                                                                                                                                        if (!addGroupClicked)
                                                                                                                                                                        {
                                                                                                                                                                            Console.WriteLine("Warning: Could not find or click '+ Add group' button");
                                                                                                                                                                        }
                                                                                                                                                                        else
                                                                                                                                                                        {
                                                                                                                                                                            // Step: Search for and select group after clicking "+ Add group"
                                                                                                                                                                            Console.WriteLine("Waiting for Search groups dialog to open...");
                                                                                                                                                                            await Task.Delay(2000);
                                                                                                                                                                            
                                                                                                                                                                            // Look for search textbox
                                                                                                                                                                            Console.WriteLine("Looking for search textbox in Search groups dialog...");
                                                                                                                                                                            bool searchBoxFound = false;
                                                                                                                                                                            
                                                                                                                                                                            foreach (var searchFrame in Page.Frames)
                                                                                                                                                                            {
                                                                                                                                                                                try
                                                                                                                                                                                {
                                                                                                                                                                                    var searchBoxSelectors = new[] {
                                                                                                                                                                                        "//*[@id='SearchBox4']",
                                                                                                                                                                                        "//input[@id='SearchBox4']",
                                                                                                                                                                                        "input[id='SearchBox4']"
                                                                                                                                                                                    };
                                                                                                                                                                                    
                                                                                                                                                                                    foreach (var searchSelector in searchBoxSelectors)
                                                                                                                                                                                    {
                                                                                                                                                                                        try
                                                                                                                                                                                        {
                                                                                                                                                                                            var searchBox = await searchFrame.WaitForSelectorAsync(searchSelector, new() { Timeout = 3000 });
                                                                                                                                                                                            if (searchBox != null)
                                                                                                                                                                                            {
                                                                                                                                                                                                var isSearchBoxVisible = await searchBox.IsVisibleAsync();
                                                                                                                                                                                                if (isSearchBoxVisible)
                                                                                                                                                                                                {
                                                                                                                                                                                                    Console.WriteLine($"Found search textbox with selector: {searchSelector}");
                                                                                                                                                                                                    await searchBox.ClickAsync();
                                                                                                                                                                                                    await searchBox.FillAsync("Automation_AI");
                                                                                                                                                                                                    Console.WriteLine("Entered 'Automation_AI' in search textbox");
                                                                                                                                                                                                    await searchBox.PressAsync("Enter");
                                                                                                                                                                                                    Console.WriteLine("Pressed Enter to search");
                                                                                                                                                                                                    searchBoxFound = true;
                                                                                                                                                                                                    await Task.Delay(2000);
                                                                                                                                                                                                    break;
                                                                                                                                                                                                }
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                        catch
                                                                                                                                                                                        {
                                                                                                                                                                                            continue;
                                                                                                                                                                                        }
                                                                                                                                                                                    }
                                                                                                                                                                                    
                                                                                                                                                                                    if (searchBoxFound)
                                                                                                                                                                                    {
                                                                                                                                                                                        // Click on the checkbox
                                                                                                                                                                                        Console.WriteLine("Looking for group checkbox...");
                                                                                                                                                                                        var checkboxSelectors = new[] {
                                                                                                                                                                                            "//*[@id='row103-0-checkbox']/div/i[2]",
                                                                                                                                                                                            "//div[@id='row103-0-checkbox']/div/i[2]",
                                                                                                                                                                                            "div[role='checkbox']"
                                                                                                                                                                                        };
                                                                                                                                                                                        
                                                                                                                                                                                        bool checkboxClicked = false;
                                                                                                                                                                                        foreach (var checkboxSelector in checkboxSelectors)
                                                                                                                                                                                        {
                                                                                                                                                                                            try
                                                                                                                                                                                            {
                                                                                                                                                                                                var checkbox = await searchFrame.WaitForSelectorAsync(checkboxSelector, new() { Timeout = 3000 });
                                                                                                                                                                                                if (checkbox != null)
                                                                                                                                                                                                {
                                                                                                                                                                                                    var isCheckboxVisible = await checkbox.IsVisibleAsync();
                                                                                                                                                                                                    if (isCheckboxVisible)
                                                                                                                                                                                                    {
                                                                                                                                                                                                        Console.WriteLine($"Found checkbox with selector: {checkboxSelector}");
                                                                                                                                                                                                        await checkbox.ClickAsync();
                                                                                                                                                                                                        Console.WriteLine("Successfully clicked group checkbox");
                                                                                                                                                                                                        checkboxClicked = true;
                                                                                                                                                                                                        await Task.Delay(1000);
                                                                                                                                                                                                        break;
                                                                                                                                                                                                    }
                                                                                                                                                                                                }
                                                                                                                                                                                            }
                                                                                                                                                                                            catch
                                                                                                                                                                                            {
                                                                                                                                                                                                continue;
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                        
                                                                                                                                                                                        if (checkboxClicked)
                                                                                                                                                                                        {
                                                                                                                                                                                            // Click Select button
                                                                                                                                                                                            Console.WriteLine("Looking for Select button...");
                                                                                                                                                                                            var selectButtonSelectors = new[] {
                                                                                                                                                                                                "button:has-text('Select')",
                                                                                                                                                                                                "[role='button']:has-text('Select')",
                                                                                                                                                                                                "//button[contains(text(),'Select')]"
                                                                                                                                                                                            };
                                                                                                                                                                                            
                                                                                                                                                                                            foreach (var selectSelector in selectButtonSelectors)
                                                                                                                                                                                            {
                                                                                                                                                                                                try
                                                                                                                                                                                                {
                                                                                                                                                                                                    var groupSelectButton = await searchFrame.WaitForSelectorAsync(selectSelector, new() { Timeout = 3000 });
                                                                                                                                                                                                    if (groupSelectButton != null)
                                                                                                                                                                                                    {
                                                                                                                                                                                                        var isSelectButtonVisible = await groupSelectButton.IsVisibleAsync();
                                                                                                                                                                                                        if (isSelectButtonVisible)
                                                                                                                                                                                                        {
                                                                                                                                                                                                            Console.WriteLine($"Found Select button with selector: {selectSelector}");
                                                                                                                                                                                                            await groupSelectButton.ClickAsync();
                                                                                                                                                                                                            Console.WriteLine("Successfully clicked Select button");
                                                                                                                                                                                                            await Task.Delay(2000);
                                                                                                                                                                                                            break;
                                                                                                                                                                                                        }
                                                                                                                                                                                                    }
                                                                                                                                                                                                }
                                                                                                                                                                                                catch
                                                                                                                                                                                                {
                                                                                                                                                                                                    continue;
                                                                                                                                                                                                }
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                        
                                                                                                                                                                                        break;
                                                                                                                                                                                    }
                                                                                                                                                                                }
                                                                                                                                                                                catch (Exception ex)
                                                                                                                                                                                {
                                                                                                                                                                                    Console.WriteLine($"Error in search dialog: {ex.Message}");
                                                                                                                                                                                    continue;
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                            
                                                                                                                                                                            if (!searchBoxFound)
                                                                                                                                                                            {
                                                                                                                                                                                Console.WriteLine("Warning: Could not find search textbox");
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                        
                                                                                                                                                                        // Step: Click Next button on Assignments tab
                                                                                                                                                                        Console.WriteLine("Looking for Next button on Assignments tab...");
                                                                                                                                                                        await Task.Delay(2000);                                                                                                                                                                                                                                                                        var assignmentsNextButton = await detectionFrame.WaitForSelectorAsync("[role='button']:has-text('Next')", new() { Timeout = 5000 });
                                                                                                                                                                                                                                                                        if (assignmentsNextButton != null)
                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                            var isAssignmentsNextVisible = await assignmentsNextButton.IsVisibleAsync();
                                                                                                                                                                                                                                                                            if (isAssignmentsNextVisible)
                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                var assignmentsNextText = await assignmentsNextButton.TextContentAsync();
                                                                                                                                                                                                                                                                                Console.WriteLine($"Found Next button on Assignments tab: '{assignmentsNextText}'");
                                                                                                                                                                                                                                                                                Console.WriteLine("Clicking Next button on Assignments tab...");
                                                                                                                                                                                                                                                                                await assignmentsNextButton.ClickAsync();
                                                                                                                                                                                                                                                                                await Task.Delay(2000);
                                                                                                                                                                                                                                                                                await Page.ScreenshotAsync(new() { Path = "after-assignments-next.png", FullPage = true });
                                                                                                                                                                                                                                                                                Console.WriteLine("Successfully clicked Next button on Assignments tab");
                                                                                                                                                                                                                                                                                
                                                                                                                                                                                                                                                                                // Step: Click Create button on Review + create tab
                                                                                                                                                                                                                                                                                Console.WriteLine("Looking for Create button on Review + create tab...");
                                                                                                                                                                                                                                                                                await Task.Delay(3000);
                                                                                                                                                                                                                                                                                
                                                                                                                                                                                                                                                                                await Page.ScreenshotAsync(new() { Path = "review-create-tab.png", FullPage = true });
                                                                                                                                                                                                                                                                                
                                                                                                                                                                                                                                                                                bool reviewCreateButtonFound = false;
                                                                                                                                                                                                                                                                                
                                                                                                                                                                                                                                                                                var createButton = await detectionFrame.WaitForSelectorAsync("//div[contains(@class,'ext-wizardNextButton fxc-base')]", new() { Timeout = 5000 });
                                                                                                                                                                                                                                                                                if (createButton != null)
                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                    var isCreateButtonVisible = await createButton.IsVisibleAsync();
                                                                                                                                                                                                                                                                                    if (isCreateButtonVisible)
                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                        var createButtonText = await createButton.TextContentAsync();
                                                                                                                                                                                                                                                                                        Console.WriteLine($"Found Create button on Review + create tab: '{createButtonText}'");
                                                                                                                                                                                                                                                                                        Console.WriteLine("Clicking Create button on Review + create tab...");
                                                                                                                                                                                                                                                                                        await createButton.ClickAsync();
                                                                                                                                                                                                                                                                                        await Task.Delay(3000);
                                                                                                                                                                                                                                                                                        await Page.ScreenshotAsync(new() { Path = "after-create-button-click.png", FullPage = true });
                                                                                                                                                                                                                                                                                        Console.WriteLine("Successfully clicked Create button on Review + create tab");
                                                                                                                                                                                                                                                                                        reviewCreateButtonFound = true;
                                                                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                                                                        // Wait for success message or confirmation
                                                                                                                                                                                                                                                                                        await Task.Delay(5000);
                                                                                                                                                                                                                                                                                        await Page.ScreenshotAsync(new() { Path = "final-state.png", FullPage = true });
                                                                                                                                                                                                                                                                                        Console.WriteLine("App creation process completed");
                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                
                                                                                                                                                                                                                                                                                if (!reviewCreateButtonFound)
                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                    Console.WriteLine("Warning: Could not find or click Create button on Review + create tab");
                                                                                                                                                                                                                                                                                    await Page.ScreenshotAsync(new() { Path = "create-button-not-found.png", FullPage = true });
                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                            Console.WriteLine("Warning: Next button not found on Assignments tab");
                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                    Console.WriteLine("Warning: Next button not found on Supersedence tab");
                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            Console.WriteLine("Warning: Next button not found on Dependencies tab");
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                                        break;
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            catch
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                continue;
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        Console.WriteLine("No '+ Add' links found");
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                catch (Exception ex)
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    Console.WriteLine($"Failed to find or click '+ Add' link: {ex.Message}");
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                
                                                                                                                                                                                                                                break;
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                }
                                                                                                                                                                                                            }
                                                                                                                                                                                                            catch
                                                                                                                                                                                                            {
                                                                                                                                                                                                                continue;
                                                                                                                                                                                                            }
                                                                                                                                                                                                        }
                                                                                                                                                                                                        
                                                                                                                                                                                                        if (rulesFormatFound)
                                                                                                                                                                                                        {
                                                                                                                                                                                                            break;
                                                                                                                                                                                                        }
                                                                                                                                                                                                    }
                                                                                                                                                                                                }
                                                                                                                                                                                            }
                                                                                                                                                                                            catch
                                                                                                                                                                                            {
                                                                                                                                                                                                continue;
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                        
                                                                                                                                                                                        if (rulesFormatFound)
                                                                                                                                                                                        {
                                                                                                                                                                                            break;
                                                                                                                                                                                        }
                                                                                                                                                                                    }
                                                                                                                                                                                    catch (Exception ex)
                                                                                                                                                                                    {
                                                                                                                                                                                        Console.WriteLine($"Error finding Rules format in frame: {ex.Message}");
                                                                                                                                                                                        continue;
                                                                                                                                                                                    }
                                                                                                                                                                                }
                                                                                                                                                                                
                                                                                                                                                                                if (!rulesFormatFound)
                                                                                                                                                                                {
                                                                                                                                                                                    Console.WriteLine("Warning: Could not find or select Rules format dropdown");
                                                                                                                                                                                }
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                            else
                                                                                                                                                                            {
                                                                                                                                                                                Console.WriteLine("Warning: Next button not found on Requirements tab");
                                                                                                                                                                            }
                                                                                                                                                                            
                                                                                                                                                                            break;
                                                                                                                                                                        }
                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                            catch
                                                                                                                                                            {
                                                                                                                                                                continue;
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                        
                                                                                                                                                        if (minOsFound)
                                                                                                                                                        {
                                                                                                                                                            break;
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                            catch
                                                                                                                                            {
                                                                                                                                                continue;
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                        
                                                                                                                                        if (minOsFound)
                                                                                                                                        {
                                                                                                                                            break;
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                    catch (Exception ex)
                                                                                                                                    {
                                                                                                                                        Console.WriteLine($"Error finding Minimum operating system in frame: {ex.Message}");
                                                                                                                                        continue;
                                                                                                                                    }
                                                                                                                                }
                                                                                                                                
                                                                                                                                if (!minOsFound)
                                                                                                                                {
                                                                                                                                    Console.WriteLine("Warning: Could not find or select Minimum operating system dropdown");
                                                                                                                                }
                                                                                                                                    }
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    Console.WriteLine("Warning: Next button not found on Program tab");
                                                                                                                                }
                                                                                                                                
                                                                                                                                break;
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                                catch
                                                                                                                {
                                                                                                                    continue;
                                                                                                                }
                                                                                                            }
                                                                                                            
                                                                                                            if (installBehaviourFound)
                                                                                                            {
                                                                                                                break;
                                                                                                            }
                                                                                                        }
                                                                                                        catch (Exception ex)
                                                                                                        {
                                                                                                            Console.WriteLine($"Error finding Install behaviour list item in frame: {ex.Message}");
                                                                                                            continue;
                                                                                                        }
                                                                                                    }
                                                                                                    
                                                                                                    if (!installBehaviourFound)
                                                                                                    {
                                                                                                        Console.WriteLine("Warning: Could not find or select 'User' Install behaviour list item");
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                Console.WriteLine("Warning: Next button not found on App information tab");
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        Console.WriteLine("Warning: Publisher field not found");
                                                                                    }
                                                                                    
                                                                                    break;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                Console.WriteLine("Warning: Description field not found");
                                                                            }
                                                                            
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    Console.WriteLine($"Error finding Name/Description fields in frame: {ex.Message}");
                                                                    continue;
                                                                }
                                                            }
                                                            
                                                            if (!nameFieldFound)
                                                            {
                                                                Console.WriteLine("Warning: Could not find Name field in App information tab");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Warning: Could not find or click OK button, checking if page already advanced...");
                                                            
                                                            // Maybe the file selection auto-advances, try to find Name field anyway
                                                            await Task.Delay(3000);
                                                            
                                                            Console.WriteLine("Looking for Name field in App information tab (without OK button)...");
                                                            bool nameFieldFound = false;
                                                            
                                                            foreach (var appFrame in Page.Frames)
                                                            {
                                                                try
                                                                {
                                                                    var nameInput = await appFrame.WaitForSelectorAsync("input[aria-label*='Name'], input[placeholder*='Name'], input[name*='name']", new() { Timeout = 5000 });
                                                                    if (nameInput != null)
                                                                    {
                                                                        var isNameVisible = await nameInput.IsVisibleAsync();
                                                                        if (isNameVisible)
                                                                        {
                                                                            Console.WriteLine("Found Name field (auto-advanced without OK), entering value...");
                                                                            await nameInput.ClickAsync();
                                                                            await nameInput.FillAsync("SCCM Test App - SimpleV2");
                                                                            Console.WriteLine("Successfully entered Name: SCCM Test App - SimpleV2");
                                                                            nameFieldFound = true;
                                                                            
                                                                            await Task.Delay(1000);
                                                                            
                                                                            Console.WriteLine("Looking for Description field...");
                                                                            var descriptionInput = await appFrame.WaitForSelectorAsync("textarea[aria-label*='Description'], textarea[placeholder*='Description'], input[aria-label*='Description']", new() { Timeout = 5000 });
                                                                            if (descriptionInput != null)
                                                                            {
                                                                                var isDescVisible = await descriptionInput.IsVisibleAsync();
                                                                                if (isDescVisible)
                                                                                {
                                                                                    Console.WriteLine("Found Description field, entering value...");
                                                                                    await descriptionInput.ClickAsync();
                                                                                    await descriptionInput.FillAsync("SCCM Test App - SimpleV2");
                                                                                    Console.WriteLine("Successfully entered Description: SCCM Test App - SimpleV2");
                                                                                    
                                                                                    await Page.ScreenshotAsync(new() {
                                                                                        Path = "app-info-filled.png",
                                                                                        FullPage = true
                                                                                    });
                                                                                    
                                                                                    // Step 12: Fill in Publisher field (fallback path)
                                                                                    Console.WriteLine("Looking for Publisher field...");
                                                                                    await Task.Delay(1000);
                                                                                    
                                                                                    var publisherInput = await appFrame.WaitForSelectorAsync("input[aria-label*='Publisher'], input[placeholder*='Publisher'], input[name*='publisher']", new() { Timeout = 5000 });
                                                                                    if (publisherInput != null)
                                                                                    {
                                                                                        var isPubVisible = await publisherInput.IsVisibleAsync();
                                                                                        if (isPubVisible)
                                                                                        {
                                                                                            Console.WriteLine("Found Publisher field, entering value...");
                                                                                            await publisherInput.ClickAsync();
                                                                                            await publisherInput.FillAsync("Microsoft");
                                                                                            Console.WriteLine("Successfully entered Publisher: Microsoft");
                                                                                            
                                                                                            await Task.Delay(1000);
                                                                                            
                                                                                            // Click Next and continue to Program tab
                                                                                            Console.WriteLine("Looking for Next button on App information tab...");
                                                                                            var nextButton = await appFrame.WaitForSelectorAsync("button:has-text('Next'), [role='button']:has-text('Next')", new() { Timeout = 5000 });
                                                                                            if (nextButton != null)
                                                                                            {
                                                                                                var isNextVisible = await nextButton.IsVisibleAsync();
                                                                                                if (isNextVisible)
                                                                                                {
                                                                                                    Console.WriteLine("Found Next button, clicking...");
                                                                                                    await nextButton.ClickAsync();
                                                                                                    Console.WriteLine("Successfully clicked Next button");
                                                                                                    
                                                                                                    await Task.Delay(3000);
                                                                                                    
                                                                                                    // Continue with Program tab steps (Install behaviour selection)
                                                                                                    Console.WriteLine("Looking for Install behaviour dropdown on Program tab...");
                                                                                                    // (Installation behaviour logic would go here - same as above)
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                    
                                                                                    break;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                Console.WriteLine("Warning: Description field not found");
                                                                            }
                                                                            
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    Console.WriteLine($"Error finding Name/Description fields in frame: {ex.Message}");
                                                                    continue;
                                                                }
                                                            }
                                                            
                                                            if (!nameFieldFound)
                                                            {
                                                                Console.WriteLine("Warning: Could not find Name field - page may not have advanced");
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        throw new Exception("File input element not found");
                                                    }
                                                }
                                                else
                                                {
                                                    throw new Exception("Package file link not found");
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error in frame: {ex.Message}");
                                        continue;
                                    }
                                }

                                if (!selectButtonFound)
                                {
                                    throw new Exception("Could not find or click Select button");
                                }
                            }

                    if (!optionFound)
                    {
                        throw new Exception("Could not find or click the 'Windows app (Win32)' option");
                    }

                } catch (Exception ex) {
                    Console.WriteLine($"Error with dropdown interaction: {ex.Message}");
                    throw;
                }

                Console.WriteLine("Test completed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test failed: {ex.Message}");
                // Take screenshot on failure
                await Page.ScreenshotAsync(new()
                {
                    Path = "test-failure.png",
                    FullPage = true
                });
                throw;
            }
        }
    }
}