using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952918_Test_Set_A_Android_Regression_Android_LOB_Uninstall_DeviceGroup : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952918";

        protected override string RegressionTestName => @"Android_LOB_Uninstall_DeviceGroup";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_LOB_Uninstall_DeviceGroup";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[Uninstall]:[Apk]:[DeviceGroup]:Verify Uninstall is success for all Android devices in Group";

        protected override string DataFileName => @"Android_LOB_AppRegression.json";

        [Test]
        public async Task TestMethod_952918_Test_Set_A_Android_Regression_Android_LOB_Uninstall_DeviceGroupAsync()
        {
            await RunTestAsync();
        }
    }
}
