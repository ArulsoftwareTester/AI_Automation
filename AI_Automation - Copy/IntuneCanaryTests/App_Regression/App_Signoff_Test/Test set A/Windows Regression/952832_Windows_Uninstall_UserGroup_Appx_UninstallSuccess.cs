using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952832_Test_Set_A_Windows_Regression_Windows_Uninstall_UserGroup_Appx_UninstallSuccess : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952832";

        protected override string RegressionTestName => @"Windows_Uninstall_UserGroup_Appx_UninstallSuccess";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Uninstall_UserGroup_Appx_UninstallSuccess";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[Uninstall]:[UserGroup]:[Appx]:Verify Uninstall is success for all Windows devices in Group";

        [Test]
        public async Task TestMethod_952832_Test_Set_A_Windows_Regression_Windows_Uninstall_UserGroup_Appx_UninstallSuccessAsync()
        {
            await RunTestAsync();
        }
    }
}