using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952852_Test_Set_A_Windows_Regression_Windows_Targeting_MobileMSI_Uninstall_DeviceGroup : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952852";

        protected override string RegressionTestName => @"Windows_Targeting_MobileMSI_Uninstall_DeviceGroup";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Targeting_MobileMSI_Uninstall_DeviceGroup";

        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[Targeting]:[MobileMSI]:[Uninstall][DeviceGroup]:Deploy App as required and make sure app is installed on device, Now change targeting to Uninstall and verify app is uninstalled from device";

        [Test]
        public async Task TestMethod_952852_Test_Set_A_Windows_Regression_Windows_Targeting_MobileMSI_Uninstall_DeviceGroupAsync()
        {
            await RunTestAsync();
        }
    }
}