using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1351437_Test_Set_A_Windows_Regression_Windows_AppUpdate_Uninstall_Appx_AppxBundle_Required_UserGroup : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1351437";

        protected override string RegressionTestName => @"Windows_AppUpdate_Uninstall_Appx_AppxBundle_Required_UserGroup";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_AppUpdate_Uninstall_Appx_AppxBundle_Required_UserGroup";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[AppUpdate][Uninstall]:[Appx/AppxBundle]:[Required][UserGroup]:Verify App V1 is installed on devices for user. Uninstall V1 OOB and update V1 to V2 and Verify that V2 is installed on the device.";

        [Test]
        public async Task TestMethod_1351437_Test_Set_A_Windows_Regression_Windows_AppUpdate_Uninstall_Appx_AppxBundle_Required_UserGroupAsync()
        {
            await RunTestAsync();
        }
    }
}