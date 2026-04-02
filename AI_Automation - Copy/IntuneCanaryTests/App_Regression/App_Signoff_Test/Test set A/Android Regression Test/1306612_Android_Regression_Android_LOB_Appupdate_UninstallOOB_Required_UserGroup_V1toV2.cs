using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1306612_Test_Set_A_Android_Regression_Android_LOB_Appupdate_UninstallOOB_Required_UserGroup_V1toV2 : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1306612";

        protected override string RegressionTestName => @"Android_LOB_Appupdate_UninstallOOB_Required_UserGroup_V1toV2";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_LOB_Appupdate_UninstallOOB_Required_UserGroup_V1toV2";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[Appupdate][UninstallOOB]:[Apk]:[Required][UserGroup]:Verify App V1 is installed on devices for user. Uninstall V1 OOB and update V1 to V2 and Verify that V2 is installed on the device.";

        protected override string DataFileName => @"Android_LOB_AppRegression.json";

        [Test]
        public async Task TestMethod_1306612_Test_Set_A_Android_Regression_Android_LOB_Appupdate_UninstallOOB_Required_UserGroup_V1toV2Async()
        {
            await RunTestAsync();
        }
    }
}
