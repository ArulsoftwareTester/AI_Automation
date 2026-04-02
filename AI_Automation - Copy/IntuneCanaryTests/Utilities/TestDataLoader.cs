using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using IntuneCanaryTests.Models;

namespace IntuneCanaryTests.Utilities
{
    /// <summary>
    /// Singleton class for loading and caching test data from JSON files
    /// Loads data once and reuses across all test executions for performance
    /// </summary>
    public class TestDataLoader
    {
        private static readonly object _lock = new object();
        private static TestDataCollection? _createProfileData;
        private static TestDataCollection? _editProfileData;
        private static TestDataCollection? _vmSyncData;
        private static TestDataCollection? _sampleTestData;
        private static TestDataCollection? _endpointSecurityFirewallData;
        private static TestDataCollection? _endpointSecurityFirewallEditData;

        private static readonly string _testDataPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "..", "..", "..", "..", "TestData");

        /// <summary>
        /// Get all CreateNewProfile test cases
        /// Can use custom JSON file by setting CREATE_PROFILE_JSON_PATH in .env file or environment variable
        /// Example in .env: CREATE_PROFILE_JSON_PATH = C:\MyTests\CustomCreateProfile.json
        /// </summary>
        public static TestDataCollection GetCreateProfileData()
        {
            if (_createProfileData == null)
            {
                lock (_lock)
                {
                    if (_createProfileData == null)
                    {
                        // Load .env file to check for custom JSON path
                        LoadEnvFile();
                        var customFilePath = Environment.GetEnvironmentVariable("CREATE_PROFILE_JSON_PATH");
                        
                        if (!string.IsNullOrEmpty(customFilePath))
                        {
                            // Resolve relative path if needed
                            var resolvedPath = Path.IsPathRooted(customFilePath) 
                                ? customFilePath 
                                : Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", customFilePath));
                            
                            if (File.Exists(resolvedPath))
                            {
                                _createProfileData = LoadJsonFileFromPath(resolvedPath);
                                Console.WriteLine($"[TestDataLoader] ═══════════════════════════════════════");
                                Console.WriteLine($"[TestDataLoader] Loaded {_createProfileData.TestCases.Count} CreateNewProfile test cases from custom file: {resolvedPath}");
                                Console.WriteLine($"[TestDataLoader] Enabled: {_createProfileData.TestCases.Count(tc => tc.Enabled)}");
                                Console.WriteLine($"[TestDataLoader] First test: {_createProfileData.TestCases.FirstOrDefault()?.TestCaseId}");
                                Console.WriteLine($"[TestDataLoader] Last test: {_createProfileData.TestCases.LastOrDefault()?.TestCaseId}");
                                Console.WriteLine($"[TestDataLoader] VMSync tests: {_createProfileData.TestCases.Count(tc => tc.TestCaseId.Contains("VMSync"))}");
                                Console.WriteLine($"[TestDataLoader] ═══════════════════════════════════════");
                            }
                            else
                            {
                                throw new FileNotFoundException($"Custom CREATE_PROFILE_JSON_PATH file not found: {resolvedPath}");
                            }
                        }
                        else
                        {
                            // Use default file
                            _createProfileData = LoadJsonFile("CreateNewProfile_TestData.json");
                            Console.WriteLine($"[TestDataLoader] Loaded {_createProfileData.TestCases.Count} CreateNewProfile test cases from default file");
                        }
                    }
                }
            }
            return _createProfileData;
        }

        /// <summary>
        /// Get all EditNewCreatedPolicy test cases
        /// Can use custom JSON file by setting EDIT_PROFILE_JSON_PATH in .env file
        /// Example in .env: EDIT_PROFILE_JSON_PATH = C:\MyTests\CustomEditProfile.json
        /// </summary>
        public static TestDataCollection GetEditProfileData()
        {
            if (_editProfileData == null)
            {
                lock (_lock)
                {
                    if (_editProfileData == null)
                    {
                        // Load .env file to check for custom JSON path
                        LoadEnvFile();
                        var customFilePath = Environment.GetEnvironmentVariable("EDIT_PROFILE_JSON_PATH");
                        
                        if (!string.IsNullOrEmpty(customFilePath))
                        {
                            // Resolve relative path if needed
                            var resolvedPath = Path.IsPathRooted(customFilePath) 
                                ? customFilePath 
                                : Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", customFilePath));
                            
                            if (File.Exists(resolvedPath))
                            {
                                _editProfileData = LoadJsonFileFromPath(resolvedPath);
                                Console.WriteLine($"[TestDataLoader] Loaded {_editProfileData.TestCases.Count} EditProfile test cases from custom file: {resolvedPath}");
                            }
                            else
                            {
                                throw new FileNotFoundException($"Custom EDIT_PROFILE_JSON_PATH file not found: {resolvedPath}");
                            }
                        }
                        else
                        {
                            // Use default file
                            _editProfileData = LoadJsonFile("EditNewCreatedPolicy_TestData.json");
                            Console.WriteLine($"[TestDataLoader] Loaded {_editProfileData.TestCases.Count} EditProfile test cases from default file");
                        }
                    }
                }
            }
            return _editProfileData;
        }

        /// <summary>
        /// Get all VMSync test cases
        /// </summary>
        public static TestDataCollection GetVMSyncData()
        {
            if (_vmSyncData == null)
            {
                lock (_lock)
                {
                    if (_vmSyncData == null)
                    {
                        // Load .env file to check for custom JSON path
                        LoadEnvFile();
                        var customFilePath = Environment.GetEnvironmentVariable("VMSYNC_JSON_PATH");
                        
                        if (!string.IsNullOrEmpty(customFilePath))
                        {
                            // Resolve relative path if needed
                            var resolvedPath = Path.IsPathRooted(customFilePath) 
                                ? customFilePath 
                                : Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", customFilePath));
                            
                            if (File.Exists(resolvedPath))
                            {
                                _vmSyncData = LoadJsonFileFromPath(resolvedPath);
                                Console.WriteLine($"[TestDataLoader] Loaded {_vmSyncData.TestCases.Count} VMSync test cases from custom file: {resolvedPath}");
                            }
                            else
                            {
                                throw new FileNotFoundException($"Custom VMSYNC_JSON_PATH file not found: {resolvedPath}");
                            }
                        }
                        else
                        {
                            // Use default file
                            _vmSyncData = LoadJsonFile("VMSync_TestData.json");
                            Console.WriteLine($"[TestDataLoader] Loaded {_vmSyncData.TestCases.Count} VMSync test cases from default file");
                        }
                    }
                }
            }
            return _vmSyncData;
        }

        /// <summary>
        /// Get sample test data for testing purposes
        /// </summary>
        public static TestDataCollection GetSampleTestData()
        {
            if (_sampleTestData == null)
            {
                lock (_lock)
                {
                    if (_sampleTestData == null)
                    {
                        _sampleTestData = LoadJsonFile("sampleTestData.json");
                        Console.WriteLine($"[TestDataLoader] Loaded {_sampleTestData.TestCases.Count} Sample test cases");
                    }
                }
            }
            return _sampleTestData;
        }

        /// <summary>
        /// Get Endpoint Security Firewall test data
        /// </summary>
        public static TestDataCollection GetEndpointSecurityFirewallTestData()
        {
            if (_endpointSecurityFirewallData == null)
            {
                lock (_lock)
                {
                    if (_endpointSecurityFirewallData == null)
                    {
                        _endpointSecurityFirewallData = LoadJsonFile("EndpointSecurityFirewall_TestData.json");
                        Console.WriteLine($"[TestDataLoader] Loaded {_endpointSecurityFirewallData.TestCases.Count} Endpoint Security Firewall test cases");
                    }
                }
            }
            return _endpointSecurityFirewallData;
        }

        /// <summary>
        /// Get Endpoint Security Firewall Edit test data
        /// </summary>
        public static TestDataCollection GetEndpointSecurityFirewallEditTestData()
        {
            if (_endpointSecurityFirewallEditData == null)
            {
                lock (_lock)
                {
                    if (_endpointSecurityFirewallEditData == null)
                    {
                        _endpointSecurityFirewallEditData = LoadJsonFile("EndpointSecurityFirewallEdit_TestData.json");
                        Console.WriteLine($"[TestDataLoader] Loaded {_endpointSecurityFirewallEditData.TestCases.Count} Endpoint Security Firewall Edit test cases");
                    }
                }
            }
            return _endpointSecurityFirewallEditData;
        }

        /// <summary>
        /// Get failed test data for rerun (dynamically generated from test failures)
        /// Returns empty collection if file doesn't exist
        /// </summary>
        public static TestDataCollection GetFailedTestsData()
        {
            try
            {
                var failedData = LoadJsonFile("FailedTests_Rerun.json");
                Console.WriteLine($"[TestDataLoader] Loaded {failedData.TestCases.Count} Failed test cases for rerun");
                return failedData;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("[TestDataLoader] No failed tests file found - returning empty collection");
                return new TestDataCollection
                {
                    TestCases = new List<TestDataCase>()
                };
            }
        }

        /// <summary>
        /// Get a specific CreateNewProfile test case by ID
        /// </summary>
        public static TestDataCase GetCreateProfileTestCase(string testCaseId)
        {
            var data = GetCreateProfileData();
            var testCase = data.TestCases.FirstOrDefault(tc => tc.TestCaseId == testCaseId);
            
            if (testCase == null)
                throw new Exception($"CreateNewProfile test case '{testCaseId}' not found in test data");
            
            if (!testCase.Enabled)
                throw new Exception($"Test case '{testCaseId}' is disabled in test data");
            
            return testCase;
        }

        /// <summary>
        /// Get a specific EditNewCreatedPolicy test case by ID
        /// </summary>
        public static TestDataCase GetEditProfileTestCase(string testCaseId)
        {
            var data = GetEditProfileData();
            var testCase = data.TestCases.FirstOrDefault(tc => tc.TestCaseId == testCaseId);
            
            if (testCase == null)
                throw new Exception($"EditNewCreatedPolicy test case '{testCaseId}' not found in test data");
            
            if (!testCase.Enabled)
                throw new Exception($"Test case '{testCaseId}' is disabled in test data");
            
            return testCase;
        }

        /// <summary>
        /// Get a specific VMSync test case by ID
        /// </summary>
        public static TestDataCase GetVMSyncTestCase(string testCaseId)
        {
            var data = GetVMSyncData();
            var testCase = data.TestCases.FirstOrDefault(tc => tc.TestCaseId == testCaseId);
            
            if (testCase == null)
                throw new Exception($"VMSync test case '{testCaseId}' not found in test data");
            
            if (!testCase.Enabled)
                throw new Exception($"Test case '{testCaseId}' is disabled in test data");
            
            return testCase;
        }

        /// <summary>
        /// Get all enabled CreateNewProfile test case IDs (for NUnit TestCaseSource)
        /// </summary>
        public static IEnumerable<string> GetEnabledCreateProfileTestIds()
        {
            return GetCreateProfileData().TestCases
                .Where(tc => tc.Enabled)
                .Select(tc => tc.TestCaseId);
        }

        /// <summary>
        /// Get all enabled EditNewCreatedPolicy test case IDs (for NUnit TestCaseSource)
        /// </summary>
        public static IEnumerable<string> GetEnabledEditProfileTestIds()
        {
            return GetEditProfileData().TestCases
                .Where(tc => tc.Enabled)
                .Select(tc => tc.TestCaseId);
        }

        /// <summary>
        /// Get all enabled VMSync test case IDs (for NUnit TestCaseSource)
        /// </summary>
        public static IEnumerable<string> GetEnabledVMSyncTestIds()
        {
            return GetVMSyncData().TestCases
                .Where(tc => tc.Enabled)
                .Select(tc => tc.TestCaseId);
        }

        /// <summary>
        /// Load JSON file and deserialize to TestDataCollection
        /// </summary>
        private static TestDataCollection LoadJsonFile(string fileName)
        {
            var filePath = Path.GetFullPath(Path.Combine(_testDataPath, fileName));
            
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Test data file not found: {filePath}");

            Console.WriteLine($"[TestDataLoader] Loading test data from: {filePath}");
            
            var jsonContent = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };

            var data = JsonSerializer.Deserialize<TestDataCollection>(jsonContent, options);
            
            if (data == null || data.TestCases == null || data.TestCases.Count == 0)
                throw new Exception($"Failed to load test data from {fileName} or file is empty");

            return data;
        }

        /// <summary>
        /// Load test data from absolute file path (for custom user-provided JSON files)
        /// </summary>
        private static TestDataCollection LoadJsonFileFromPath(string absolutePath)
        {
            if (!File.Exists(absolutePath))
                throw new FileNotFoundException($"Custom test data file not found: {absolutePath}");

            Console.WriteLine($"[TestDataLoader] Loading test data from custom file: {absolutePath}");
            
            var jsonContent = File.ReadAllText(absolutePath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };

            var data = JsonSerializer.Deserialize<TestDataCollection>(jsonContent, options);
            
            if (data == null || data.TestCases == null || data.TestCases.Count == 0)
                throw new Exception($"Failed to load test data from {absolutePath} or file is empty");

            return data;
        }

        /// <summary>
        /// Load .env file to set environment variables
        /// </summary>
        public static void LoadEnvFile()
        {
            try
            {
                var envPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", ".env"));
                if (File.Exists(envPath))
                {
                    foreach (var line in File.ReadAllLines(envPath))
                    {
                        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                            continue;
                        
                        var parts = line.Split('=', 2);
                        if (parts.Length == 2)
                        {
                            var key = parts[0].Trim();
                            var value = parts[1].Trim();
                            Environment.SetEnvironmentVariable(key, value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TestDataLoader] Warning: Could not load .env file: {ex.Message}");
            }
        }

        /// <summary>
        /// Clear all cached data (useful for testing or data refresh)
        /// </summary>
        public static void ClearCache()
        {
            lock (_lock)
            {
                _createProfileData = null;
                _editProfileData = null;
                _vmSyncData = null;
                Console.WriteLine("[TestDataLoader] Cache cleared");
            }
        }

        /// <summary>
        /// Get test data summary for logging/debugging
        /// </summary>
        public static string GetDataSummary()
        {
            var createCount = GetCreateProfileData().TestCases.Count(tc => tc.Enabled);
            var editCount = GetEditProfileData().TestCases.Count(tc => tc.Enabled);
            var vmSyncCount = GetVMSyncData().TestCases.Count(tc => tc.Enabled);

            return $"Test Data Summary - CreateProfile: {createCount}, EditProfile: {editCount}, VMSync: {vmSyncCount}";
        }

        /// <summary>
        /// Find and return the PolicyName for a given testCaseId from the NewPolicyList JSON file
        /// Uses CREATED_POLICY_LIST_JSON_PATH from .env if specified, otherwise searches CreateNewPolicy folder
        /// Matches TestName field with pattern "Execute({testCaseId})"
        /// </summary>
        /// <param name="testCaseId">The test case ID (e.g., "TC_27979778")</param>
        /// <returns>The PolicyName if found, otherwise null</returns>
        public static string? GetPolicyNameForTestCase(string testCaseId)
        {
            try
            {
                // Load .env to check for custom policy list path
                LoadEnvFile();
                var customPolicyListPath = Environment.GetEnvironmentVariable("CREATED_POLICY_LIST_JSON_PATH");

                List<string> policyFilesToSearch = new List<string>();

                // If custom path is provided, support semicolon-separated multiple paths
                if (!string.IsNullOrEmpty(customPolicyListPath))
                {
                    var rawPaths = customPolicyListPath.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    foreach (var rawPath in rawPaths)
                    {
                        var resolvedPath = Path.IsPathRooted(rawPath)
                            ? rawPath
                            : Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", rawPath));

                        if (File.Exists(resolvedPath))
                        {
                            policyFilesToSearch.Add(resolvedPath);
                            Console.WriteLine($"[TestDataLoader] Using custom CREATED_POLICY_LIST_JSON_PATH: {resolvedPath}");
                        }
                        else
                        {
                            Console.WriteLine($"[TestDataLoader] WARNING: Custom CREATED_POLICY_LIST_JSON_PATH not found: {resolvedPath}");
                        }
                    }
                    if (policyFilesToSearch.Count == 0)
                        Console.WriteLine($"[TestDataLoader] Falling back to auto-search in CreateNewPolicy folder");
                }

                // If no custom path or custom file not found, search CreateNewPolicy folder
                if (policyFilesToSearch.Count == 0)
                {
                    var createPolicyFolder = Path.GetFullPath(Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "..", "..", "..", "..", "CreateNewPolicy"));

                    if (!Directory.Exists(createPolicyFolder))
                    {
                        Console.WriteLine($"[TestDataLoader] WARNING: CreateNewPolicy folder not found at: {createPolicyFolder}");
                        return null;
                    }

                    // Find all NewPolicyList JSON files
                    policyFilesToSearch = Directory.GetFiles(createPolicyFolder, "NewPolicyList_*.json")
                        .OrderByDescending(f => f) // Sort by filename (descending) to get the latest
                        .ToList();

                    if (!policyFilesToSearch.Any())
                    {
                        Console.WriteLine($"[TestDataLoader] WARNING: No NewPolicyList_*.json files found in {createPolicyFolder}");
                        return null;
                    }

                    Console.WriteLine($"[TestDataLoader] Auto-searching {policyFilesToSearch.Count} NewPolicyList files (latest first)");
                }

                // Try each file (starting with latest) until we find a match
                foreach (var policyFile in policyFilesToSearch)
                {
                    try
                    {
                        var jsonContent = File.ReadAllText(policyFile);
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                            ReadCommentHandling = JsonCommentHandling.Skip,
                            AllowTrailingCommas = true
                        };

                        var policyRecords = JsonSerializer.Deserialize<List<CreatedPolicyRecord>>(jsonContent, options);
                        
                        if (policyRecords != null && policyRecords.Count > 0)
                        {
                            // Search for matching TestName by extracting value inside brackets
                            // TestName format: "Execute(TC_XXXXXXXX)" - we need to extract "TC_XXXXXXXX"
                            var matchingRecord = policyRecords.FirstOrDefault(r => 
                            {
                                if (string.IsNullOrEmpty(r.TestName))
                                    return false;
                                
                                // Extract value inside brackets: "Execute(TC_27979778)" -> "TC_27979778"
                                var startIndex = r.TestName.IndexOf('(');
                                var endIndex = r.TestName.IndexOf(')');
                                
                                if (startIndex >= 0 && endIndex > startIndex)
                                {
                                    var extractedTestId = r.TestName.Substring(startIndex + 1, endIndex - startIndex - 1).Trim();
                                    return extractedTestId.Equals(testCaseId, StringComparison.OrdinalIgnoreCase);
                                }
                                
                                return false;
                            });

                            if (matchingRecord != null)
                            {
                                Console.WriteLine($"[TestDataLoader] ✓ Found PolicyName for {testCaseId}: '{matchingRecord.PolicyName}' in {Path.GetFileName(policyFile)}");
                                return matchingRecord.PolicyName;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[TestDataLoader] WARNING: Error reading {Path.GetFileName(policyFile)}: {ex.Message}");
                        continue; // Try next file
                    }
                }

                Console.WriteLine($"[TestDataLoader] WARNING: No PolicyName found for testCaseId '{testCaseId}' in any NewPolicyList JSON file");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TestDataLoader] ERROR in GetPolicyNameForTestCase: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Get PolicyName for a test case, with fallback to default secBaselineName if not found
        /// </summary>
        /// <param name="testCaseId">The test case ID</param>
        /// <param name="defaultSecBaselineName">Fallback value from test data JSON</param>
        /// <returns>PolicyName if found, otherwise the default value</returns>
        public static string GetPolicyNameOrDefault(string testCaseId, string defaultSecBaselineName)
        {
            var policyName = GetPolicyNameForTestCase(testCaseId);
            
            if (!string.IsNullOrEmpty(policyName))
            {
                return policyName;
            }
            
            Console.WriteLine($"[TestDataLoader] Using default secBaselineName: '{defaultSecBaselineName}' for {testCaseId}");
            return defaultSecBaselineName;
        }

        /// <summary>
        /// Get detailed test case statistics
        /// </summary>
        public static TestDataStatistics GetStatistics()
        {
            var createData = GetCreateProfileData();
            var editData = GetEditProfileData();
            var vmSyncData = GetVMSyncData();

            return new TestDataStatistics
            {
                CreateProfile = new TestCategoryStats
                {
                    TotalTestCases = createData.TestCases.Count,
                    EnabledTestCases = createData.TestCases.Count(tc => tc.Enabled),
                    DisabledTestCases = createData.TestCases.Count(tc => !tc.Enabled),
                    HighPriority = createData.TestCases.Count(tc => tc.Priority == "High" || tc.Priority == "Critical"),
                    MediumPriority = createData.TestCases.Count(tc => tc.Priority == "Medium"),
                    LowPriority = createData.TestCases.Count(tc => tc.Priority == "Low")
                },
                EditProfile = new TestCategoryStats
                {
                    TotalTestCases = editData.TestCases.Count,
                    EnabledTestCases = editData.TestCases.Count(tc => tc.Enabled),
                    DisabledTestCases = editData.TestCases.Count(tc => !tc.Enabled),
                    HighPriority = editData.TestCases.Count(tc => tc.Priority == "High" || tc.Priority == "Critical"),
                    MediumPriority = editData.TestCases.Count(tc => tc.Priority == "Medium"),
                    LowPriority = editData.TestCases.Count(tc => tc.Priority == "Low")
                },
                VMSync = new TestCategoryStats
                {
                    TotalTestCases = vmSyncData.TestCases.Count,
                    EnabledTestCases = vmSyncData.TestCases.Count(tc => tc.Enabled),
                    DisabledTestCases = vmSyncData.TestCases.Count(tc => !tc.Enabled),
                    HighPriority = vmSyncData.TestCases.Count(tc => tc.Priority == "High" || tc.Priority == "Critical"),
                    MediumPriority = vmSyncData.TestCases.Count(tc => tc.Priority == "Medium"),
                    LowPriority = vmSyncData.TestCases.Count(tc => tc.Priority == "Low")
                }
            };
        }

        /// <summary>
        /// Print detailed statistics to console
        /// </summary>
        public static void PrintStatistics()
        {
            var stats = GetStatistics();
            
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║           DATA-DRIVEN TEST SUITE STATISTICS                   ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝\n");

            PrintCategoryStats("CreateNewProfile", stats.CreateProfile);
            PrintCategoryStats("EditNewCreatedPolicy", stats.EditProfile);
            PrintCategoryStats("VMSync", stats.VMSync);

            Console.WriteLine("─────────────────────────────────────────────────────────────────");
            Console.WriteLine($"GRAND TOTAL: {stats.GrandTotal} test cases");
            Console.WriteLine($"  ✓ Enabled:  {stats.TotalEnabled}");
            Console.WriteLine($"  ✗ Disabled: {stats.TotalDisabled}");
            Console.WriteLine($"  ⚠ High/Critical Priority: {stats.TotalHighPriority}");
            Console.WriteLine($"  ⚡ Medium Priority: {stats.TotalMediumPriority}");
            Console.WriteLine($"  ⬇ Low Priority: {stats.TotalLowPriority}");
            Console.WriteLine("─────────────────────────────────────────────────────────────────\n");
        }

        private static void PrintCategoryStats(string categoryName, TestCategoryStats stats)
        {
            Console.WriteLine($"📋 {categoryName}");
            Console.WriteLine($"   Total:    {stats.TotalTestCases}");
            Console.WriteLine($"   Enabled:  {stats.EnabledTestCases}");
            Console.WriteLine($"   Disabled: {stats.DisabledTestCases}");
            Console.WriteLine($"   Priority: High/Critical={stats.HighPriority}, Medium={stats.MediumPriority}, Low={stats.LowPriority}");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Test data statistics container
    /// </summary>
    public class TestDataStatistics
    {
        public TestCategoryStats CreateProfile { get; set; } = new TestCategoryStats();
        public TestCategoryStats EditProfile { get; set; } = new TestCategoryStats();
        public TestCategoryStats VMSync { get; set; } = new TestCategoryStats();

        public int GrandTotal => CreateProfile.TotalTestCases + EditProfile.TotalTestCases + VMSync.TotalTestCases;
        public int TotalEnabled => CreateProfile.EnabledTestCases + EditProfile.EnabledTestCases + VMSync.EnabledTestCases;
        public int TotalDisabled => CreateProfile.DisabledTestCases + EditProfile.DisabledTestCases + VMSync.DisabledTestCases;
        public int TotalHighPriority => CreateProfile.HighPriority + EditProfile.HighPriority + VMSync.HighPriority;
        public int TotalMediumPriority => CreateProfile.MediumPriority + EditProfile.MediumPriority + VMSync.MediumPriority;
        public int TotalLowPriority => CreateProfile.LowPriority + EditProfile.LowPriority + VMSync.LowPriority;
    }

    /// <summary>
    /// Statistics for a test category
    /// </summary>
    public class TestCategoryStats
    {
        public int TotalTestCases { get; set; }
        public int EnabledTestCases { get; set; }
        public int DisabledTestCases { get; set; }
        public int HighPriority { get; set; }
        public int MediumPriority { get; set; }
        public int LowPriority { get; set; }
    }
}
