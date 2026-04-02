using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using NUnit.Framework;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Principal;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class VM_ValidationFixed
    {
        private Runspace runspace;
        private PowerShell powerShell;
        private bool isElevated;
        private const string VM_NAME = "New Virtual Machine";
        private const string VM_USERNAME = "Admin";
        private const string VM_PASSWORD = "Admin";

        [SetUp]
        public void Setup()
        {
            using (var identity = WindowsIdentity.GetCurrent())
            {
                var principal = new WindowsPrincipal(identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            var initialSessionState = InitialSessionState.CreateDefault();
            initialSessionState.ExecutionPolicy = Microsoft.PowerShell.ExecutionPolicy.Unrestricted;
            
            runspace = RunspaceFactory.CreateRunspace(initialSessionState);
            runspace.Open();
            powerShell = PowerShell.Create();
            powerShell.Runspace = runspace;
            
            // Set error action to continue to avoid stopping on non-critical errors
            powerShell.AddCommand("Set-Variable").AddParameter("Name", "ErrorActionPreference").AddParameter("Value", "Continue");
            powerShell.Invoke();
            powerShell.Commands.Clear();
        }

        [Test]
        public async Task RunVMTests()
        {
            try
            {
                if (!isElevated)
                {
                    Assert.Ignore("This test requires elevated (administrator) privileges to run.");
                    return;
                }

                // Step 1: Check and Start VM
                Console.WriteLine("=== Checking VM Status ===");
                powerShell.Commands.Clear();
                powerShell.AddCommand("Get-VM").AddParameter("Name", VM_NAME);
                var vmInfo = powerShell.Invoke();
                
                Assert.That(vmInfo.Count > 0, $"VM '{VM_NAME}' not found in Hyper-V");
                var vm = vmInfo[0];
                var vmState = vm.Properties["State"].Value.ToString();
                Console.WriteLine($"VM State: {vmState}");

                // Start VM if it's not running
                if (!vmState.Equals("Running", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Starting VM...");
                    powerShell.Commands.Clear();
                    powerShell.AddCommand("Start-VM").AddParameter("Name", VM_NAME);
                    powerShell.Invoke();

                    // Wait for VM to fully boot
                    Console.WriteLine("Waiting for VM to boot (45 seconds)...");
                    await Task.Delay(TimeSpan.FromSeconds(45));
                    
                    Console.WriteLine("✓ VM started successfully");
                }
                else
                {
                    Console.WriteLine("✓ VM already running");
                }

                // Step 2: Configure WinRM (if needed)
                Console.WriteLine("\n=== Configuring WinRM ===");
                powerShell.Commands.Clear();
                powerShell.AddScript(@"
                    try {
                        Enable-PSRemoting -Force -SkipNetworkProfileCheck -ErrorAction SilentlyContinue
                        Set-Item WSMan:\localhost\Client\TrustedHosts -Value '*' -Force -ErrorAction SilentlyContinue
                    } catch {
                        # Ignore errors in WinRM setup
                    }
                ");
                powerShell.Invoke();
                powerShell.Streams.Error.Clear();
                Console.WriteLine("✓ WinRM configured");

                // Step 3: Create remote session
                Console.WriteLine("\n=== Connecting to VM ===");
                powerShell.Commands.Clear();
                powerShell.AddScript($@"
                    $securePassword = ConvertTo-SecureString '{VM_PASSWORD}' -AsPlainText -Force
                    $credential = New-Object System.Management.Automation.PSCredential('{VM_USERNAME}', $securePassword)
                    New-PSSession -VMName '{VM_NAME}' -Credential $credential
                ");

                var sessionResult = powerShell.Invoke();
                Assert.That(sessionResult.Count > 0, "Failed to create remote session to VM");
                var sessionId = sessionResult[0].Properties["Id"].Value;
                Console.WriteLine($"✓ Remote session created (ID: {sessionId})");

                // Step 4: Copy test files to VM (if not already there)
                Console.WriteLine("\n=== Copying Test Files to VM ===");
                powerShell.Commands.Clear();
                powerShell.AddScript($@"
                    $session = Get-PSSession -Id {sessionId}
                    
                    # Check if files already exist
                    $filesExist = Invoke-Command -Session $session -ScriptBlock {{
                        Test-Path 'C:\Tests\IntuneCanaryTests\IntuneLoginTests.cs'
                    }}
                    
                    if (-not $filesExist) {{
                        Write-Host 'Copying files to VM...'
                        Invoke-Command -Session $session -ScriptBlock {{
                            if (-not (Test-Path 'C:\Tests')) {{
                                New-Item -Path 'C:\Tests' -ItemType Directory -Force | Out-Null
                            }}
                        }}
                        
                        Copy-Item -Path 'C:\New_Demo\IntuneCanaryTests' -Destination 'C:\Tests' -ToSession $session -Recurse -Force
                        Copy-Item -Path 'C:\New_Demo\*.sln' -Destination 'C:\Tests' -ToSession $session -Force
                        Copy-Item -Path 'C:\New_Demo\auth-cert' -Destination 'C:\Tests' -ToSession $session -Recurse -Force
                        Copy-Item -Path 'C:\New_Demo\config' -Destination 'C:\Tests' -ToSession $session -Recurse -Force -ErrorAction SilentlyContinue
                        Write-Host '✓ Files copied to VM'
                    }} else {{
                        Write-Host '✓ Test files already exist in VM'
                    }}
                ");
                powerShell.Invoke();

                // Step 5: Install dependencies in VM
                Console.WriteLine("\n=== Installing Dependencies in VM ===");
                powerShell.Commands.Clear();
                powerShell.AddScript($@"
                    $session = Get-PSSession -Id {sessionId}
                    
                    Invoke-Command -Session $session -ScriptBlock {{
                        # Check .NET
                        $dotnetInstalled = $false
                        try {{
                            $dotnetVersion = dotnet --version
                            Write-Host ""✓ .NET already installed: $dotnetVersion""
                            $dotnetInstalled = $true
                        }} catch {{
                            Write-Host '✗ .NET not found, installing...'
                        }}
                        
                        # Check Playwright browsers
                        $playwrightInstalled = Test-Path 'C:\Users\Admin\AppData\Local\ms-playwright\chromium-*'
                        
                        if (-not $playwrightInstalled) {{
                            Write-Host 'Installing Playwright browsers...'
                            Set-Location 'C:\Tests\IntuneCanaryTests\bin\Debug\net9.0'
                            if (Test-Path 'playwright.ps1') {{
                                powershell -File playwright.ps1 install chromium
                                Write-Host '✓ Playwright browsers installed'
                            }} else {{
                                Write-Host '✗ playwright.ps1 not found, building project first...'
                                Set-Location 'C:\Tests'
                                dotnet build New_Demo.sln
                                Set-Location 'C:\Tests\IntuneCanaryTests\bin\Debug\net9.0'
                                if (Test-Path 'playwright.ps1') {{
                                    powershell -File playwright.ps1 install chromium
                                    Write-Host '✓ Playwright browsers installed'
                                }}
                            }}
                        }} else {{
                            Write-Host '✓ Playwright browsers already installed'
                        }}
                    }}
                ");
                powerShell.Invoke();

                // Step 6: Run tests in VM with visible Chrome browser
                Console.WriteLine("\n=== Running Tests in VM (Headed Mode) ===");
                powerShell.Commands.Clear();
                powerShell.AddScript($@"
                    $session = Get-PSSession -Id {sessionId}
                    
                    Invoke-Command -Session $session -ScriptBlock {{
                        Set-Location C:\Tests
                        
                        # Set environment for headed mode
                        $env:HEADED = '1'
                        
                        Write-Host 'Executing IntuneLoginTests in headed mode...'
                        Write-Host 'Chrome browser will open visibly in the VM'
                        Write-Host ''
                        
                        # Run the test
                        dotnet test 'New_Demo.sln' ``
                            --filter 'FullyQualifiedName~IntuneLoginTests' ``
                            --logger 'console;verbosity=detailed' ``
                            -- NUnit.NumberOfTestWorkers=1
                        
                        return $LASTEXITCODE
                    }}
                ");

                var testResults = powerShell.Invoke();
                var errors = powerShell.Streams.Error.ReadAll();

                // Display any errors
                if (errors.Any())
                {
                    Console.WriteLine("\n=== Errors Encountered ===");
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Error: {error.Exception?.Message ?? error.ToString()}");
                    }
                }

                // Display output
                Console.WriteLine("\n=== Test Output ===");
                foreach (var line in powerShell.Streams.Information)
                {
                    Console.WriteLine(line.MessageData);
                }

                // Check results
                if (testResults.Count > 0)
                {
                    var exitCode = testResults[0].BaseObject as int?;
                    Console.WriteLine($"\n=== Test Exit Code: {exitCode} ===");
                    
                    if (exitCode.HasValue && exitCode.Value == 0)
                    {
                        Console.WriteLine("✓ All tests passed successfully in VM!");
                    }
                    else
                    {
                        Console.WriteLine("✗ Tests failed in VM");
                        Assert.Fail($"Tests failed in VM with exit code: {exitCode}");
                    }
                }
                else
                {
                    Assert.Fail("No test results returned from VM");
                }

                // Step 7: Cleanup session
                powerShell.Commands.Clear();
                powerShell.AddScript($"Remove-PSSession -Id {sessionId}");
                powerShell.Invoke();
                Console.WriteLine("\n✓ Remote session closed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Test failed: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner error: {ex.InnerException.Message}");
                }
                throw;
            }
        }

        [TearDown]
        public void Cleanup()
        {
            if (powerShell != null)
            {
                powerShell.Dispose();
            }
            if (runspace != null)
            {
                runspace.Dispose();
            }
        }
    }
}