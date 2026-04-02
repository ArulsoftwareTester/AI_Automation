using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using DotNetEnv;

namespace IntuneCanaryTests
{
    public class SecurityBaseline : PageTest
    {
        // Helper function to select dropdown value using keyboard navigation
        private async Task SelectDropdownValueByKeyboard(IPage page, ILocator dropdownElement, string desiredValue, string dropdownLabel)
        {
            try
            {
                Console.WriteLine($"Looking for '{dropdownLabel}' dropdown...");
                await page.WaitForTimeoutAsync(1500);
                
                await dropdownElement.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 20000 });
                await dropdownElement.ScrollIntoViewIfNeededAsync();
                await page.WaitForTimeoutAsync(500);
                
                var currentValue = await dropdownElement.InnerTextAsync();
                Console.WriteLine($"'{dropdownLabel}' - Current value: '{currentValue}'");
                
                // If already at desired value, skip
                if (currentValue.Equals(desiredValue, StringComparison.OrdinalIgnoreCase) || 
                    currentValue.Contains(desiredValue, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"✓ '{dropdownLabel}' already set to '{currentValue}'");
                    return;
                }
                
                Console.WriteLine($"Opening '{dropdownLabel}' dropdown...");
                await dropdownElement.ClickAsync();
                await page.WaitForTimeoutAsync(2000);
                
                Console.WriteLine($"Selecting '{desiredValue}' using keyboard navigation...");
                
                string keyToPress = "";
                string description = "";
                
                if (desiredValue.Equals("Enable", StringComparison.OrdinalIgnoreCase) || 
                    desiredValue.Equals("Enabled", StringComparison.OrdinalIgnoreCase))
                {
                    keyToPress = "ArrowUp";
                    description = "Press ArrowUp to wrap backward from Disable to Enable (last option)";
                }
                else if (desiredValue.Equals("Prompt", StringComparison.OrdinalIgnoreCase))
                {
                    keyToPress = "ArrowDown";
                    description = "Press ArrowDown once to move from Disable to Prompt";
                }
                else if (desiredValue.Equals("Disable", StringComparison.OrdinalIgnoreCase) || 
                         desiredValue.Equals("Disabled", StringComparison.OrdinalIgnoreCase))
                {
                    keyToPress = ""; // No navigation needed if at default
                    description = "Disable is default - close dropdown";
                }
                else
                {
                    Console.WriteLine($"✗ Warning: Unknown value '{desiredValue}' - using ArrowDown as fallback");
                    keyToPress = "ArrowDown";
                    description = $"Attempting to select '{desiredValue}' using ArrowDown";
                }
                
                if (!string.IsNullOrEmpty(keyToPress))
                {
                    Console.WriteLine($"Strategy: {description}");
                    await page.Keyboard.PressAsync(keyToPress);
                    await page.WaitForTimeoutAsync(500);
                }
                
                Console.WriteLine("Pressing Enter to confirm selection...");
                await page.Keyboard.PressAsync("Enter");
                await page.WaitForTimeoutAsync(2000);
                
                var newValue = await dropdownElement.InnerTextAsync();
                Console.WriteLine($"'{dropdownLabel}' after selection: '{newValue}'");
                
                if (newValue.Contains(desiredValue, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"✓ '{dropdownLabel}' selection confirmed");
                }
                else
                {
                    Console.WriteLine($"✗ Warning: '{dropdownLabel}' shows '{newValue}' instead of '{desiredValue}'");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error selecting '{dropdownLabel}': {ex.Message}");
            }
        }

        public override BrowserNewContextOptions ContextOptions()
        {
            var certPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "auth-cert", "AIAutoPE_3.pfx"));
            Console.WriteLine($"Certificate path: {certPath}");
            return new BrowserNewContextOptions 
            {
                ClientCertificates = new[] {
                    new ClientCertificate {
                        Origin = "https://certauth.login.microsoftonline.com",
                        PfxPath = certPath,
                        Passphrase = "Admin@123"
                    }
                }
            };
        }
        
        public async Task IPLogin(IPage page, string username, string environment)
        {
            Console.WriteLine($"IPLogin called with username: {username}, environment: {environment}");
            
            // Load .env file
            var envPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", ".env"));
            Env.Load(envPath);
            
            // Determine URL based on environment
            string intuneUrl;
            if (environment.Equals("PE", StringComparison.OrdinalIgnoreCase))
            {
                intuneUrl = Environment.GetEnvironmentVariable("INTUNE_URL");
                Console.WriteLine($"Environment 'PE' selected, using INTUNE_URL from .env: {intuneUrl}");
            }
            else if (environment.Equals("SH", StringComparison.OrdinalIgnoreCase))
            {
                intuneUrl = "https://aka.ms/IntuneSH";
                Console.WriteLine($"Environment 'SH' selected, using URL: {intuneUrl}");
            }
            else
            {
                throw new ArgumentException($"Invalid environment '{environment}'. Expected 'SH' or 'PE'.");
            }
            
            Console.WriteLine($"Navigating to Intune URL: {intuneUrl}");
            
            // Navigate to the URL
            await page.GotoAsync(intuneUrl, new PageGotoOptions { Timeout = 60000, WaitUntil = WaitUntilState.Load });
            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            Console.WriteLine("Page navigation completed");
            
            // Maximize the browser and wait for page to be ready
            await page.Context.Pages[0].SetViewportSizeAsync(1920, 1080);
            await page.Locator("body").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
            Console.WriteLine("Browser maximized and page ready");
            
            // Validate the browser title
            var pageTitle = await page.TitleAsync();
            Console.WriteLine($"Page title: {pageTitle}");
            Assert.That(pageTitle, Is.Not.Empty, "Page title should not be empty");
            
            // Use the specified username for PEAI1
            var intuneTenant = "admin@a830edad9050849PEAIUser1.onmicrosoft.com";
            Console.WriteLine($"Using tenant username: {intuneTenant}");
            
            var usernameField = page.Locator("input[type='email'], input[name='loginfmt'], input[placeholder*='Email']").First;
            await usernameField.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 30000 });
            await usernameField.FillAsync(intuneTenant);
            Console.WriteLine("Username entered");
            
            // Click Next button
            var nextButton = page.Locator("input[type='submit'], button[type='submit']").First;
            await nextButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 30000 });
            await nextButton.ClickAsync();
            Console.WriteLine("Clicked Next button");
            
            // Wait for password page and click on "Use a certificate or smart card" link
            Console.WriteLine("Waiting for certificate link...");
            await page.WaitForTimeoutAsync(2000); // Wait for page to load
            
            // Try to find and click the certificate link
            var certificateLinkSelectors = new[]
            {
                "a:has-text('Use a certificate or smart card')",
                "a:has-text('Sign in with a certificate')",
                "a:has-text('certificate')",
                "div[id='certLink']",
                "[data-bind*='certLink']"
            };
            
            bool certificateLinkClicked = false;
            foreach (var selector in certificateLinkSelectors)
            {
                try
                {
                    var certLink = page.Locator(selector).First;
                    await certLink.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
                    Console.WriteLine($"Certificate link found using selector: {selector}, clicking...");
                    await certLink.ClickAsync();
                    Console.WriteLine("Clicked certificate link");
                    certificateLinkClicked = true;
                    break;
                }
                catch
                {
                    // Try next selector
                }
            }
            
            if (!certificateLinkClicked)
            {
                Console.WriteLine("Certificate link not found - may already be on certificate authentication page");
            }
            
            // Wait for certificate authentication to complete
            Console.WriteLine("Waiting for certificate authentication...");
            await page.WaitForTimeoutAsync(3000);
            
            // Wait for URL to change indicating authentication progress
            try
            {
                await page.WaitForURLAsync(url => !url.Contains("login.microsoftonline.com/common/login"), new PageWaitForURLOptions { Timeout = 30000 });
                Console.WriteLine("URL changed after authentication");
            }
            catch
            {
                Console.WriteLine("Authentication may still be in progress");
            }
            
            // Check for 'Stay signed in?' prompt
            Console.WriteLine("Checking for 'Stay signed in?' prompt...");
            
            // Try multiple selectors for the No button
            var noButtonSelectors = new[]
            {
                "input[type='submit'][value='No']",
                "input[id='idBtn_Back']",
                "button:has-text('No')",
                "input[value='No']",
                "#idBtn_Back"
            };
            
            bool noButtonClicked = false;
            foreach (var selector in noButtonSelectors)
            {
                try
                {
                    var noButton = page.Locator(selector).First;
                    await noButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
                    Console.WriteLine($"'Stay signed in?' prompt found using selector: {selector}, clicking No...");
                    await noButton.ClickAsync(); // Click immediately when visible
                    Console.WriteLine("Clicked No button");
                    noButtonClicked = true;
                    break;
                }
                catch
                {
                    // Try next selector
                }
            }
            
            if (!noButtonClicked)
            {
                Console.WriteLine("'Stay signed in?' prompt not found or already dismissed");
            }
            
            // Wait for redirect to complete and Intune home page to load
            Console.WriteLine("Waiting for Intune home page to load...");
            try
            {
                await page.WaitForURLAsync(url => url.Contains("intune.microsoft.com") && !url.Contains("oauth2"), new PageWaitForURLOptions { Timeout = 60000 });
                Console.WriteLine("Redirected to Intune portal");
            }
            catch
            {
                Console.WriteLine("Redirect timeout - checking if already on Intune portal");
            }
            
            // Wait for page to be in a loaded state
            try
            {
                await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded, new PageWaitForLoadStateOptions { Timeout = 30000 });
                Console.WriteLine("Page DOM loaded");
            }
            catch
            {
                Console.WriteLine("DOM load timeout - continuing anyway");
            }
            
            await page.WaitForTimeoutAsync(2000); // Additional wait for page elements to render
            Console.WriteLine("Home page loaded");
            
            // Verify successful sign-in
            Console.WriteLine("Verifying successful sign-in...");
            var currentUrl = page.Url;
            Console.WriteLine($"Current URL: {currentUrl}");
            var currentTitle = await page.TitleAsync();
            Console.WriteLine($"Current page title: {currentTitle}");
            
            // Assert login success by checking if we're on the actual Intune portal (not OAuth page)
            Assert.That(currentUrl, Does.Contain("intune.microsoft.com").And.Not.Contains("oauth2"), "Should be redirected to Intune portal home page");
            Console.WriteLine("Sign-in successful! Intune home page is displayed");
        }

        public async Task createProfileAdminTemplate(IPage page, string securityBaseline, string dropDownOption)
        {
            // This method is copied and adapted from the original project - it uses Playwright selectors and flows.
            Console.WriteLine($"createProfileAdminTemplate called with securityBaseline: {securityBaseline}, dropdownOption: {dropDownOption}");
            
            var endpointSecurityLink = page.Locator("a:has-text('Endpoint security'), button:has-text('Endpoint security')").First;
            await endpointSecurityLink.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 30000 });
            await endpointSecurityLink.ClickAsync();
            Console.WriteLine("Clicked Endpoint security link");
            
            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            var securityBaselinesLink = page.Locator("a:has-text('Security baselines'), button:has-text('Security baselines')").First;
            await securityBaselinesLink.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 30000 });
            await securityBaselinesLink.ClickAsync();
            Console.WriteLine("Clicked Security baselines link");
            
            await page.WaitForTimeoutAsync(1500);
            Console.WriteLine("Security Baselines page loaded");

            string baselineLinkText = securityBaseline switch
            {
                var s when s.Equals("Windows 365", StringComparison.OrdinalIgnoreCase) || s.Equals("Windows365", StringComparison.OrdinalIgnoreCase) || s.Equals("Win365", StringComparison.OrdinalIgnoreCase) => "Windows 365 Security Baseline",
                var s when s.Equals("HoloLens 2", StringComparison.OrdinalIgnoreCase) || s.Equals("HoloLens2", StringComparison.OrdinalIgnoreCase) || s.Equals("Standard HoloLens", StringComparison.OrdinalIgnoreCase) => "Standard Security Baseline for HoloLens 2",
                var s when s.Equals("Windows 10", StringComparison.OrdinalIgnoreCase) || s.Equals("Windows10", StringComparison.OrdinalIgnoreCase) || s.Equals("Win10", StringComparison.OrdinalIgnoreCase) => "Security Baseline for Windows 10 and later",
                var s when s.Equals("Edge", StringComparison.OrdinalIgnoreCase) || s.Equals("Microsoft Edge", StringComparison.OrdinalIgnoreCase) || s.Equals("MS Edge", StringComparison.OrdinalIgnoreCase) => "Security Baseline for Microsoft Edge",
                var s when s.Equals("Defender", StringComparison.OrdinalIgnoreCase) || s.Equals("MDE", StringComparison.OrdinalIgnoreCase) || s.Equals("Microsoft Defender", StringComparison.OrdinalIgnoreCase) => "Microsoft Defender for Endpoint Security Baseline",
                var s when s.Equals("M365 Apps", StringComparison.OrdinalIgnoreCase) || s.Equals("Microsoft 365 Apps", StringComparison.OrdinalIgnoreCase) || s.Equals("Office 365", StringComparison.OrdinalIgnoreCase) => "Microsoft 365 Apps for Enterprise Security Baseline",
                var s when s.Equals("Advanced HoloLens", StringComparison.OrdinalIgnoreCase) || s.Equals("Advanced HoloLens 2", StringComparison.OrdinalIgnoreCase) || s.Equals("HoloLens2 Advanced", StringComparison.OrdinalIgnoreCase) => "Advanced Security Baseline for HoloLens 2",
                _ => securityBaseline,
            };

            Console.WriteLine($"Looking for '{baselineLinkText}' link inside iframe...");
            var baselineLink = page.FrameLocator("iframe[name='SecurityBaselineTemplateSummary.ReactView']").Locator("a").Filter(new LocatorFilterOptions { HasText = baselineLinkText });
            await baselineLink.First.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached, Timeout = 30000 });
            await baselineLink.First.ClickAsync(new LocatorClickOptions { Force = true });
            Console.WriteLine($"Clicked '{baselineLinkText}' link");

            await page.WaitForTimeoutAsync(4000);

            var createPolicyButton = page.FrameLocator("iframe[id='_react_frame_3']").Locator("xpath=//*[@id='root']/div/div/div[2]/div/div/div/div/div[1]/button");
            await createPolicyButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 30000 });
            await createPolicyButton.ClickAsync();
            Console.WriteLine("Clicked '+ Create Policy' button");

            // Find and click Create in panel (several fallbacks)
            ILocator? createButton = null;
            try
            {
                createButton = page.FrameLocator("iframe[id='_react_frame_3']").Locator("button:has-text('Create')").Filter(new LocatorFilterOptions { HasNotText = "+ Create" }).First;
                await createButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
            }
            catch
            {
                try
                {
                    createButton = page.FrameLocator("iframe[id='_react_frame_4']").Locator("button:has-text('Create')").Filter(new LocatorFilterOptions { HasNotText = "+ Create" }).First;
                    await createButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
                }
                catch
                {
                    createButton = page.Locator("button:has-text('Create')").Filter(new LocatorFilterOptions { HasNotText = "+ Create" }).First;
                    await createButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
                }
            }

            if (createButton != null)
            {
                await createButton.ClickAsync();
                Console.WriteLine("Clicked 'Create' button in Create a profile panel");
            }

            // Wait and proceed through wizard - use the robust dropdown helper where needed
            await page.WaitForTimeoutAsync(2000);

            // Enter Name into Basics tab (best-effort selectors)
            var currentDateTime = DateTime.Now;
            var nameValue = $"Automation_{currentDateTime:MMddyyyy}_{currentDateTime:HHmm}";
            try
            {
                var nameFieldWrapper = page.Locator("xpath=(//div[@class='azc-inputbox-wrapper azc-textBox-wrapper'])[3]");
                await nameFieldWrapper.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 15000 });
                var nameField = nameFieldWrapper.Locator("input").First;
                await nameField.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
                await nameField.FillAsync(nameValue);
                Console.WriteLine($"Entered name: {nameValue}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not enter name: {ex.Message}");
            }

            // Try to click Next and navigate configuration
            try
            {
                var nextButton = page.Locator("xpath=//div[contains(@class,'ext-wizardNextButton fxc-base')]");
                await nextButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 15000 });
                await nextButton.ClickAsync();
                Console.WriteLine("Clicked Next button in Basics tab");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not click Next: {ex.Message}");
            }

            // Configure Administrative Templates and set dropdowns (best-effort)
            try
            {
                var adminTemplatesElement = page.Locator("xpath=//div[normalize-space(text())='Administrative Templates']");
                await adminTemplatesElement.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 15000 });
                await adminTemplatesElement.ClickAsync();
                Console.WriteLine("Clicked 'Administrative Templates' element");

                var allowUnencryptedDropdown = page.Locator("xpath=//div[@role='combobox' and @aria-label='Allow unencrypted traffic']").First;
                await SelectDropdownValueByKeyboard(page, allowUnencryptedDropdown, dropDownOption, "Allow unencrypted traffic");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during configuration steps: {ex.Message}");
            }

            // Finalize and attempt to create profile
            try
            {
                var reviewCreateButton = page.Locator("xpath=//div[@class='ext-wizardNextButton fxc-base fxc-simplebutton']");
                await reviewCreateButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 15000 });
                await reviewCreateButton.ClickAsync();
                Console.WriteLine("Clicked Create button in Review + create tab");
            }
            catch { }

            // Wait for profile creation to complete
            await page.WaitForTimeoutAsync(2000);
            Console.WriteLine("createProfileAdminTemplate finished (best-effort)");
        }

        public async Task VMSync(IPage page)
        {
            Console.WriteLine("VMSync stub on macOS - skipping Windows-only VM UI automation");
            await Task.CompletedTask;
        }
    }
}
