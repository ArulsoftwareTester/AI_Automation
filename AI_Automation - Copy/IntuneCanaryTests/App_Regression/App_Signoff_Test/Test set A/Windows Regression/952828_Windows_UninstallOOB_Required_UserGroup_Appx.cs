using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952828_Test_Set_A_Windows_Regression_Windows_UninstallOOB_Required_UserGroup_Appx : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952828";

        protected override string RegressionTestName => @"Windows_UninstallOOB_Required_UserGroup_Appx";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_UninstallOOB_Required_UserGroup_Appx";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[UninstallOOB]:[Required][UserGroup]:[Appx]:Verify Install is success for required deployment and app is  pushed back after user uninstalled app OOB for all Windows devices of user";

        [Test]
        public async Task TestMethod_952828_Test_Set_A_Windows_Regression_Windows_UninstallOOB_Required_UserGroup_AppxAsync()
        {
            await RunTestAsync();
        }
    }
}
