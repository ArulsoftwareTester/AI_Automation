using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952831_Test_Set_A_Windows_Regression_Windows_ReEnroll_Required_DeviceGroup_AppxBundle : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952831";

        protected override string RegressionTestName => @"Windows_ReEnroll_Required_DeviceGroup_AppxBundle";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_ReEnroll_Required_DeviceGroup_AppxBundle";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[ReEnroll]:[Appx/AppxBundle]:[Required][DeviceGroup]:Verify Install is success after requried deployment and app is removed during unenroll and app is pushed back after reenroll for all Windows devices in group";

        [Test]
        public async Task TestMethod_952831_Test_Set_A_Windows_Regression_Windows_ReEnroll_Required_DeviceGroup_AppxBundleAsync()
        {
            await RunTestAsync();
        }
    }
}
