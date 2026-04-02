using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1306608_Test_Set_A_Android_Regression_Android_LOB_Appupdate_Required_DeviceGroup_V1toV2 : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1306608";

        protected override string RegressionTestName => @"Android_LOB_Appupdate_Required_DeviceGroup_V1toV2";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_LOB_Appupdate_Required_DeviceGroup_V1toV2";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[Appupdate]:[Apk]:[Required][DeviceGroup]:Verify that App V1 deployed as required is installed for all devices of group. When App V1 is updated to V2, V2 is updated on all devices with V1 installed.";

        protected override string DataFileName => @"Android_LOB_AppRegression.json";

        [Test]
        public async Task TestMethod_1306608_Test_Set_A_Android_Regression_Android_LOB_Appupdate_Required_DeviceGroup_V1toV2Async()
        {
            await RunTestAsync();
        }
    }
}
