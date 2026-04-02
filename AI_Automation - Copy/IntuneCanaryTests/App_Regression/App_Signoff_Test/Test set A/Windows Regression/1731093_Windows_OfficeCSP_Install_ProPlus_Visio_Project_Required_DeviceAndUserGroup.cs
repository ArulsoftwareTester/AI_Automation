using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1731093_Test_Set_A_Windows_Regression_Windows_OfficeCSP_Install_ProPlus_Visio_Project_Required_DeviceAndUserGroup : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1731093";

        protected override string RegressionTestName => @"Windows_OfficeCSP_Install_ProPlus_Visio_Project_Required_DeviceAndUserGroup";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_OfficeCSP_Install_ProPlus_Visio_Project_Required_DeviceAndUserGroup";

        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10RS2+]:[OfficeCSP][Install]:[ProPlus][Visio][Project]:[Required][DeviceGroup][UserGroup]:Verify ProPlus (multiple apps), Visio and Project can all install successfully";

        [Test]
        public async Task TestMethod_1731093_Test_Set_A_Windows_Regression_Windows_OfficeCSP_Install_ProPlus_Visio_Project_Required_DeviceAndUserGroupAsync()
        {
            await RunTestAsync();
        }
    }
}