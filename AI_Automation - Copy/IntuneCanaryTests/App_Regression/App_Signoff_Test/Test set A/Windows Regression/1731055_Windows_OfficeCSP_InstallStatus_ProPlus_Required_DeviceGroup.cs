using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1731055_Test_Set_A_Windows_Regression_Windows_OfficeCSP_InstallStatus_ProPlus_Required_DeviceGroup : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1731055";

        protected override string RegressionTestName => @"Windows_OfficeCSP_InstallStatus_ProPlus_Required_DeviceGroup";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_OfficeCSP_InstallStatus_ProPlus_Required_DeviceGroup";

        protected override string TestDescription => @"[IntuneSA]:[Ibiza]:[Windows][Win10RS2+]:[OfficeCSP][InstallStatus]:[ProPlus]:[Required][DeviceGroup]:Verify successful ProPlus (multiple apps) installation is reported successfully in the UI for all Devices in the Group";

        [Test]
        public async Task TestMethod_1731055_Test_Set_A_Windows_Regression_Windows_OfficeCSP_InstallStatus_ProPlus_Required_DeviceGroupAsync()
        {
            await RunTestAsync();
        }
    }
}