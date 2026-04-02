using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_997399_Test_Set_A_Android_Regression_Android_LOB_Requirements_Required_DeviceGroup_OSVersion : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_997399";

        protected override string RegressionTestName => @"Android_LOB_Requirements_Required_DeviceGroup_OSVersion";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_LOB_Requirements_Required_DeviceGroup_OSVersion";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[Requirements]:[Apk][Required][DeviceGroup]:Verify Install is success for all Android devices of group which are compliant/meet requirements. Requirements- OS Version";

        protected override string DataFileName => @"Android_LOB_AppRegression.json";

        [Test]
        public async Task TestMethod_997399_Test_Set_A_Android_Regression_Android_LOB_Requirements_Required_DeviceGroup_OSVersionAsync()
        {
            await RunTestAsync();
        }
    }
}
