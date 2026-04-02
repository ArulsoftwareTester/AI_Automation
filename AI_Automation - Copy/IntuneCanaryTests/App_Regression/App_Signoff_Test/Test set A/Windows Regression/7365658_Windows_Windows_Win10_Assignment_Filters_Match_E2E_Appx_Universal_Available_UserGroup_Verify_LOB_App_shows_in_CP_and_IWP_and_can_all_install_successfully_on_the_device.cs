using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_7365658_Test_Set_A_Windows_Regression_Windows_Windows_Win10_Assignment_Filters_Match_E2E_Appx_Universal_Available_UserGroup_Verify_LOB_App_shows_in_CP_and_IWP_and_can_all_install_successfully_on_the_device : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_7365658";

        protected override string RegressionTestName => @"Windows_Windows_Win10_Assignment_Filters_Match_E2E_Appx_Universal_Available_UserGroup_Verify_LOB_App_shows_in_CP_and_IWP_and_can_all_install_successfully_on_the_device";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Windows_Win10_Assignment_Filters_Match_E2E_Appx_Universal_Available_UserGroup_Verify_LOB_App_shows_in_CP_and_IWP_and_can_all_install_successfully_on_the_device";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[Assignment Filters]:Windows Win10 Assignment Filters Match E2E Appx Universal Available UserGroup Verify LOB App shows in CP and IWP and can all install successfully on the device";

        [Test]
        public async Task TestMethod_7365658_Test_Set_A_Windows_Regression_Windows_Windows_Win10_Assignment_Filters_Match_E2E_Appx_Universal_Available_UserGroup_Verify_LOB_App_shows_in_CP_and_IWP_and_can_all_install_successfully_on_the_deviceAsync()
        {
            await RunTestAsync();
        }
    }
}