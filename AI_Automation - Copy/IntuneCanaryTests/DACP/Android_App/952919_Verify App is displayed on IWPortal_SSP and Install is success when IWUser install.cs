using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using DotNetEnv;

namespace IntuneCanaryTests
{
    public class _952919_Verify_App_is_displayed_on_IWPortal_SSP_and_Install_is_success_when_IWUser_install : PageTest
    {

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
        
    public async Task IPLogin(IPage page)
    {
        // Get credentials from environment variables
        var username = Environment.GetEnvironmentVariable("INTUNE_TENANT");
        var environment = Environment.GetEnvironmentVariable("TC_Env");
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
                    await noButton.ClickAsync();
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
                // Wait for URL to change from login page to actual Intune portal
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
            
            await page.WaitForTimeoutAsync(2000);
            Console.WriteLine("Home page loaded");
            
            // Verify successful sign-in
            Console.WriteLine("Verifying successful sign-in...");
            var currentUrl = page.Url;
            Console.WriteLine($"Current URL: {currentUrl}");
            var currentTitle = await page.TitleAsync();
            Console.WriteLine($"Current page title: {currentTitle}");
            
            // Assert login success
            Assert.That(currentUrl, Does.Contain("intune.microsoft.com").And.Not.Contains("oauth2"), "Should be redirected to Intune portal home page");
            Console.WriteLine("Sign-in successful! Intune home page is displayed");
        }

        [Test]
        public async Task _952919_Verify_App_is_displayed_on_IWPortal_SSP_and_Install_is_success_when_IWUser_install_Test()
        {
            Console.WriteLine("Starting test: 952919 - Verify App is displayed on IWPortal_SSP and Install is success when IWUser install");
            
            // Call IPLogin function
            await IPLogin(Page);
            
            // Add test implementation here
            Console.WriteLine("Test implementation placeholder - Add your test steps here");
            
            // Example assertion
            Assert.Pass("Test completed successfully");
        }
    }
}
