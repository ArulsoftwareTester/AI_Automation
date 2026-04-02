using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_7365659_Test_Set_A_Windows_Regression_Windows_Windows_Win10_Assignment_Filters_No_Match_E2E_Appx_Universal_Required_UserGroup_Verify_LOB_App_is_not_installed_on_the_device : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_7365659";

        protected override string RegressionTestName => @"Windows_Windows_Win10_Assignment_Filters_No_Match_E2E_Appx_Universal_Required_UserGroup_Verify_LOB_App_is_not_installed_on_the_device";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Windows_Win10_Assignment_Filters_No_Match_E2E_Appx_Universal_Required_UserGroup_Verify_LOB_App_is_not_installed_on_the_device";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[Assignment Filters]:Windows Win10 Assignment Filters No Match E2E Appx Universal Required UserGroup Verify LOB App is not installed on the device";

        [Test]
        public async Task TestMethod_7365659_Test_Set_A_Windows_Regression_Windows_Windows_Win10_Assignment_Filters_No_Match_E2E_Appx_Universal_Required_UserGroup_Verify_LOB_App_is_not_installed_on_the_deviceAsync()
        {
            await RunTestAsync();
        }
    }
}