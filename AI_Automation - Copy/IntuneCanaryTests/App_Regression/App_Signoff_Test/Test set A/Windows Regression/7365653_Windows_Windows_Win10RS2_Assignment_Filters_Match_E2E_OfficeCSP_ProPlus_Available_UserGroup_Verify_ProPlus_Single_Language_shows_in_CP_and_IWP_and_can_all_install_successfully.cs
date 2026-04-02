using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_7365653_Test_Set_A_Windows_Regression_Windows_Windows_Win10RS2_Assignment_Filters_Match_E2E_OfficeCSP_ProPlus_Available_UserGroup_Verify_ProPlus_Single_Language_shows_in_CP_and_IWP_and_can_all_install_successfully : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_7365653";

        protected override string RegressionTestName => @"Windows_Windows_Win10RS2_Assignment_Filters_Match_E2E_OfficeCSP_ProPlus_Available_UserGroup_Verify_ProPlus_Single_Language_shows_in_CP_and_IWP_and_can_all_install_successfully";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Windows_Win10RS2_Assignment_Filters_Match_E2E_OfficeCSP_ProPlus_Available_UserGroup_Verify_ProPlus_Single_Language_shows_in_CP_and_IWP_and_can_all_install_successfully";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[Assignment Filters]:Windows Win10RS2+ Assignment Filters Match E2E OfficeCSP ProPlus Available UserGroup Verify ProPlus  , Single Language shows in CP and IWP and can all install successfully";

        [Test]
        public async Task TestMethod_7365653_Test_Set_A_Windows_Regression_Windows_Windows_Win10RS2_Assignment_Filters_Match_E2E_OfficeCSP_ProPlus_Available_UserGroup_Verify_ProPlus_Single_Language_shows_in_CP_and_IWP_and_can_all_install_successfullyAsync()
        {
            await RunTestAsync();
        }
    }
}