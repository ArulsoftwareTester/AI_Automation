using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1731081_Test_Set_A_Windows_Regression_Windows_OfficeCSP_Uninstall_ProPlus_Required_DeviceGroup : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1731081";

        protected override string RegressionTestName => @"Windows_OfficeCSP_Uninstall_ProPlus_Required_DeviceGroup";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_OfficeCSP_Uninstall_ProPlus_Required_DeviceGroup";

        protected override string TestDescription => @"[IntuneSA]:[Ibiza]:[Windows][Win10RS2+]:[OfficeCSP][Uninstall]:[ProPlus]:[Required][DeviceGroup]:Verify Project can be uninstalled successfully";

        [Test]
        public async Task TestMethod_1731081_Test_Set_A_Windows_Regression_Windows_OfficeCSP_Uninstall_ProPlus_Required_DeviceGroupAsync()
        {
            await RunTestAsync();
        }
    }
}
