using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1731088_Test_Set_A_Windows_Regression_Windows_OfficeCSP_Uninstall_ProPlus_Visio_Project_Required_UserGroup : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1731088";

        protected override string RegressionTestName => @"Windows_OfficeCSP_Uninstall_ProPlus_Visio_Project_Required_UserGroup";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_OfficeCSP_Uninstall_ProPlus_Visio_Project_Required_UserGroup";

        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10RS2+]:[OfficeCSP][Uninstall]:[ProPlus][Visio][Project]:[Required][UserGroup]:Verify ProPlus (multiple apps), Visio and Project can all be uninstall successfully";

        [Test]
        public async Task TestMethod_1731088_Test_Set_A_Windows_Regression_Windows_OfficeCSP_Uninstall_ProPlus_Visio_Project_Required_UserGroupAsync()
        {
            await RunTestAsync();
        }
    }
}