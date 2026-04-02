using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952844_Test_Set_A_Windows_Regression_Windows_Targeting_DeviceGroup_Required_Uninstall_AppxBundle : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952844";

        protected override string RegressionTestName => @"Windows_Targeting_DeviceGroup_Required_Uninstall_AppxBundle";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Targeting_DeviceGroup_Required_Uninstall_AppxBundle";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[Targeting]:[DeviceGroup]:[Required][Uninstall]:[AppX/AppxBundle]:Deploy App as required and make sure app is installed on device, Now change targeting to Uninstall and verify app is uninstalled from device";

        [Test]
        public async Task TestMethod_952844_Test_Set_A_Windows_Regression_Windows_Targeting_DeviceGroup_Required_Uninstall_AppxBundleAsync()
        {
            await RunTestAsync();
        }
    }
}