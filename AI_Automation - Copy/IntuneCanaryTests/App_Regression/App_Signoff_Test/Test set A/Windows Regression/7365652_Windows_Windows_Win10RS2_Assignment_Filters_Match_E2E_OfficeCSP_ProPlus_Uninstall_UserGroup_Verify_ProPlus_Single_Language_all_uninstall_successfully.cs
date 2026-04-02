using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_7365652_Test_Set_A_Windows_Regression_Windows_Windows_Win10RS2_Assignment_Filters_Match_E2E_OfficeCSP_ProPlus_Uninstall_UserGroup_Verify_ProPlus_Single_Language_all_uninstall_successfully : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_7365652";

        protected override string RegressionTestName => @"Windows_Windows_Win10RS2_Assignment_Filters_Match_E2E_OfficeCSP_ProPlus_Uninstall_UserGroup_Verify_ProPlus_Single_Language_all_uninstall_successfully";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Windows_Win10RS2_Assignment_Filters_Match_E2E_OfficeCSP_ProPlus_Uninstall_UserGroup_Verify_ProPlus_Single_Language_all_uninstall_successfully";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[Assignment Filters]:Windows Win10RS2+ Assignment Filters Match E2E OfficeCSP ProPlus Uninstall UserGroup Verify ProPlus  , Single Language all uninstall successfully";

        [Test]
        public async Task TestMethod_7365652_Test_Set_A_Windows_Regression_Windows_Windows_Win10RS2_Assignment_Filters_Match_E2E_OfficeCSP_ProPlus_Uninstall_UserGroup_Verify_ProPlus_Single_Language_all_uninstall_successfullyAsync()
        {
            await RunTestAsync();
        }
    }
}