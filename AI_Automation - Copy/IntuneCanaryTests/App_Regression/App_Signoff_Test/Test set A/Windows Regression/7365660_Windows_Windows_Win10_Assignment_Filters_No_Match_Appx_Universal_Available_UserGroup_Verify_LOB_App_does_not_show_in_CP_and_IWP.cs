using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_7365660_Test_Set_A_Windows_Regression_Windows_Windows_Win10_Assignment_Filters_No_Match_Appx_Universal_Available_UserGroup_Verify_LOB_App_does_not_show_in_CP_and_IWP : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_7365660";

        protected override string RegressionTestName => @"Windows_Windows_Win10_Assignment_Filters_No_Match_Appx_Universal_Available_UserGroup_Verify_LOB_App_does_not_show_in_CP_and_IWP";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Windows_Win10_Assignment_Filters_No_Match_Appx_Universal_Available_UserGroup_Verify_LOB_App_does_not_show_in_CP_and_IWP";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[Assignment Filters]:Windows Win10 Assignment Filters No Match Appx Universal Available UserGroup Verify LOB App does not show in CP and IWP";

        [Test]
        public async Task TestMethod_7365660_Test_Set_A_Windows_Regression_Windows_Windows_Win10_Assignment_Filters_No_Match_Appx_Universal_Available_UserGroup_Verify_LOB_App_does_not_show_in_CP_and_IWPAsync()
        {
            await RunTestAsync();
        }
    }
}