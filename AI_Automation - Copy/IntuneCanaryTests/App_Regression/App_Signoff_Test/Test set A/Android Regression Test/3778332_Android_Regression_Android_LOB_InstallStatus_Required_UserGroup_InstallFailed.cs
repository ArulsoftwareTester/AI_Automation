using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_3778332_Test_Set_A_Android_Regression_Android_LOB_InstallStatus_Required_UserGroup_InstallFailed : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_3778332";

        protected override string RegressionTestName => @"Android_LOB_InstallStatus_Required_UserGroup_InstallFailed";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_LOB_InstallStatus_Required_UserGroup_InstallFailed";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[InstallStatus]:[Apk]:[Required][UserGroup]:[InstallFailed]:IT Pro can see installation status summary with error code for app deployed for User group as required as failed with error";

        protected override string DataFileName => @"Android_LOB_AppRegression.json";

        [Test]
        public async Task TestMethod_3778332_Test_Set_A_Android_Regression_Android_LOB_InstallStatus_Required_UserGroup_InstallFailedAsync()
        {
            await RunTestAsync();
        }
    }
}
