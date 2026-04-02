using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952837_Test_Set_A_Windows_Regression_Windows_InstallStatus_Required_UserGroup_InstallFailed_AppxBundle : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952837";

        protected override string RegressionTestName => @"Windows_InstallStatus_Required_UserGroup_InstallFailed_AppxBundle";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_InstallStatus_Required_UserGroup_InstallFailed_AppxBundle";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[InstallStatus]:[Required][UserGroup]:[InstallFailed]:[Appx/AppxBundle]:IT Pro can see installation status summary with error code for app deployed for User group as required as failed with error";

        [Test]
        public async Task TestMethod_952837_Test_Set_A_Windows_Regression_Windows_InstallStatus_Required_UserGroup_InstallFailed_AppxBundleAsync()
        {
            await RunTestAsync();
        }
    }
}