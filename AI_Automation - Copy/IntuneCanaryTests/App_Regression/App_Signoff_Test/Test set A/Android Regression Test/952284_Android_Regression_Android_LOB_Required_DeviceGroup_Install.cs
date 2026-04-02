using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952284_Test_Set_A_Android_Regression_Android_LOB_Required_DeviceGroup_Install : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952284";

        protected override string RegressionTestName => @"Android_LOB_Required_DeviceGroup_Install";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_LOB_Required_DeviceGroup_Install";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[Required]:[DeviceGroup][Apk]:Verify Install is success for all Android devices in group";

        protected override string DataFileName => @"Android_LOB_AppRegression.json";

        [Test]
        public async Task TestMethod_952284_Test_Set_A_Android_Regression_Android_LOB_Required_DeviceGroup_InstallAsync()
        {
            await RunTestAsync();
        }
    }
}
