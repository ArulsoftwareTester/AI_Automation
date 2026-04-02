using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1731079_Test_Set_A_Windows_Regression_Windows_OfficeCSP_Uninstall_Visio_Required_DeviceAndUserGroup : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1731079";

        protected override string RegressionTestName => @"Windows_OfficeCSP_Uninstall_Visio_Required_DeviceAndUserGroup";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_OfficeCSP_Uninstall_Visio_Required_DeviceAndUserGroup";

        protected override string TestDescription => @"[IntuneSA]:[Ibiza]:[Windows][Win10RS2+]:[OfficeCSP][Uninstall]:[Visio]:[Required][DeviceGroup][UserGroup]:Verify Visio can be uninstalled successfully";

        [Test]
        public async Task TestMethod_1731079_Test_Set_A_Windows_Regression_Windows_OfficeCSP_Uninstall_Visio_Required_DeviceAndUserGroupAsync()
        {
            await RunTestAsync();
        }
    }
}