using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IntuneCanaryTests.Models
{
    /// <summary>
    /// Root collection for test data JSON files
    /// </summary>
    public class TestDataCollection
    {
        [JsonPropertyName("testCases")]
        public List<TestDataCase> TestCases { get; set; } = new List<TestDataCase>();
    }

    /// <summary>
    /// Represents a single test case with all its metadata and parameters
    /// </summary>
    public class TestDataCase
    {
        [JsonPropertyName("testCaseId")]
        public string TestCaseId { get; set; } = string.Empty;

        [JsonPropertyName("testName")]
        public string TestName { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        [JsonPropertyName("priority")]
        public string Priority { get; set; } = string.Empty;

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; } = true;

        [JsonPropertyName("parameters")]
        public TestParameters Parameters { get; set; } = new TestParameters();
    }

    /// <summary>
    /// Base class for test parameters - can be extended for specific test types
    /// </summary>
    public class TestParameters
    {
        // Common parameters for CreateNewProfile and EditNewCreatedPolicy
        [JsonPropertyName("firstLink")]
        public string FirstLink { get; set; } = string.Empty;

        [JsonPropertyName("secondLink")]
        public string SecondLink { get; set; } = string.Empty;

        [JsonPropertyName("secBaselineName")]
        public string SecBaselineName { get; set; } = string.Empty;

        [JsonPropertyName("configurationSettings")]
        public string ConfigurationSettings { get; set; } = string.Empty;

        [JsonPropertyName("parentDropDown")]
        public string ParentDropDown { get; set; } = string.Empty;

        [JsonPropertyName("parentDropDownOption")]
        public string ParentDropDownOption { get; set; } = string.Empty;

        [JsonPropertyName("childDropDown")]
        public string ChildDropDown { get; set; } = string.Empty;

        [JsonPropertyName("childDropDownOption")]
        public string ChildDropDownOption { get; set; } = string.Empty;

        [JsonPropertyName("parentSectionPath")]
        public string ParentSectionPath { get; set; } = string.Empty;

        [JsonPropertyName("childSectionPath")]
        public string ChildSectionPath { get; set; } = string.Empty;

        // Endpoint Security specific parameters
        [JsonPropertyName("platform")]
        public string Platform { get; set; } = string.Empty;

        [JsonPropertyName("profile")]
        public string Profile { get; set; } = string.Empty;

        [JsonPropertyName("assignmentGroup")]
        public string AssignmentGroup { get; set; } = string.Empty;

        [JsonPropertyName("settingsValue")]
        public string SettingsValue { get; set; } = string.Empty;

        [JsonPropertyName("dropDownValue")]
        public string DropDownValue { get; set; } = string.Empty;

        [JsonPropertyName("numericValue")]
        public string NumericValue { get; set; } = string.Empty;

        [JsonPropertyName("checkboxValues")]
        public List<string> CheckboxValues { get; set; } = new List<string>();

        [JsonPropertyName("listValues")]
        public List<string> ListValues { get; set; } = new List<string>();

        /// <summary>
        /// When set, drives only the toggle switch (no fill).
        /// "Not configured" → turn toggle OFF; "Configured" → turn toggle ON.
        /// </summary>
        [JsonPropertyName("toggleValue")]
        public string ToggleValue { get; set; } = string.Empty;

        [JsonPropertyName("additionalSettings")]
        public List<AdditionalSetting> AdditionalSettings { get; set; } = new List<AdditionalSetting>();

        // VMSync specific parameters
        [JsonPropertyName("searchTerm")]
        public string SearchTerm { get; set; } = string.Empty;

        [JsonPropertyName("expectedValue")]
        public string ExpectedValue { get; set; } = string.Empty;
    }

    /// <summary>
    /// Represents a created policy record from NewPolicyList JSON files
    /// Used to lookup PolicyName by TestCaseId for Edit tests
    /// </summary>
    public class CreatedPolicyRecord
    {
        [JsonPropertyName("PolicyName")]
        public string PolicyName { get; set; } = string.Empty;

        [JsonPropertyName("BaselineType")]
        public string BaselineType { get; set; } = string.Empty;

        [JsonPropertyName("CreatedDateTime")]
        public string CreatedDateTime { get; set; } = string.Empty;

        [JsonPropertyName("TestName")]
        public string TestName { get; set; } = string.Empty;

        [JsonPropertyName("ParentDropDown")]
        public string ParentDropDown { get; set; } = string.Empty;

        [JsonPropertyName("ParentDropDownOption")]
        public string ParentDropDownOption { get; set; } = string.Empty;

        [JsonPropertyName("ChildDropDown")]
        public string ChildDropDown { get; set; } = string.Empty;

        [JsonPropertyName("ChildDropDownOption")]
        public string ChildDropDownOption { get; set; } = string.Empty;
    }

    /// <summary>
    /// Represents one additional setting to configure on the same Configuration Settings page
    /// after the primary setting, before navigating to the next wizard step.
    /// </summary>
    public class AdditionalSetting
    {
        [JsonPropertyName("settingsValue")]
        public string SettingsValue { get; set; } = string.Empty;

        /// <summary>Single-select dropdown option value</summary>
        [JsonPropertyName("dropDownValue")]
        public string DropDownValue { get; set; } = string.Empty;

        /// <summary>
        /// Toggle-to-Configured then fill text/numeric input.
        /// Also used for plain text inputs (Log File Path etc.)
        /// </summary>
        [JsonPropertyName("numericValue")]
        public string NumericValue { get; set; } = string.Empty;

        /// <summary>Click '+ Add' for each value and type into the new row input.</summary>
        [JsonPropertyName("listValues")]
        public List<string> ListValues { get; set; } = new List<string>();

        /// <summary>
        /// When set, drives only the toggle switch (no fill).
        /// "Not configured" → turn toggle OFF; "Configured" → turn toggle ON.
        /// </summary>
        [JsonPropertyName("toggleValue")]
        public string ToggleValue { get; set; } = string.Empty;
    }
}
