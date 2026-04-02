using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_3778549_Test_Set_A_Windows_Regression_Windows_AppUpdate_InstallStatus_Appx_Required_UserGroup : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_3778549";

        protected override string RegressionTestName => @"Windows_AppUpdate_InstallStatus_Appx_Required_UserGroup";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_AppUpdate_InstallStatus_Appx_Required_UserGroup";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[AppUpdate]:[InstallStatus]:[Appx]:[Required]:[UserGroup]:IT Pro can view application status summary change for V1 of app when V2 is updated. Existing status moves to Unknown.";

        [Test]
        public async Task TestMethod_3778549_Test_Set_A_Windows_Regression_Windows_AppUpdate_InstallStatus_Appx_Required_UserGroupAsync()
        {
            await RunTestAsync();
        }
    }
}