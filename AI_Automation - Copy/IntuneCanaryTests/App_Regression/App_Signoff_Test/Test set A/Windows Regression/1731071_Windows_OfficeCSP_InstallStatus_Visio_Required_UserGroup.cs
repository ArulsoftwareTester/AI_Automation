using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1731071_Test_Set_A_Windows_Regression_Windows_OfficeCSP_InstallStatus_Visio_Required_UserGroup : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1731071";

        protected override string RegressionTestName => @"Windows_OfficeCSP_InstallStatus_Visio_Required_UserGroup";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_OfficeCSP_InstallStatus_Visio_Required_UserGroup";

        protected override string TestDescription => @"[IntuneSA]:[Ibiza]:[Windows][Win10RS2+]:[OfficeCSP][InstallStatus]:[Visio]:[Required][UserGroup]:Verify successful Visio installation is reported successfully in the UI for all Users in the Group";

        [Test]
        public async Task TestMethod_1731071_Test_Set_A_Windows_Regression_Windows_OfficeCSP_InstallStatus_Visio_Required_UserGroupAsync()
        {
            await RunTestAsync();
        }
    }
}
