using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_7365654_Test_Set_A_Windows_Regression_Windows_Windows_Win10RS2_Assignment_Filters_No_Match_E2E_OfficeCSP_ProPlus_Required_UserGroup_Verify_ProPlus_Single_Language_is_not_installed_on_the_device : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_7365654";

        protected override string RegressionTestName => @"Windows_Windows_Win10RS2_Assignment_Filters_No_Match_E2E_OfficeCSP_ProPlus_Required_UserGroup_Verify_ProPlus_Single_Language_is_not_installed_on_the_device";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Windows_Win10RS2_Assignment_Filters_No_Match_E2E_OfficeCSP_ProPlus_Required_UserGroup_Verify_ProPlus_Single_Language_is_not_installed_on_the_device";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[Assignment Filters]:Windows Win10RS2+ Assignment Filters No Match E2E OfficeCSP ProPlus Required UserGroup Verify ProPlus  , Single Language is not installed on the device";

        [Test]
        public async Task TestMethod_7365654_Test_Set_A_Windows_Regression_Windows_Windows_Win10RS2_Assignment_Filters_No_Match_E2E_OfficeCSP_ProPlus_Required_UserGroup_Verify_ProPlus_Single_Language_is_not_installed_on_the_deviceAsync()
        {
            await RunTestAsync();
        }
    }
}