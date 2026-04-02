using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1731080_Test_Set_A_Windows_Regression_Windows_OfficeCSP_Uninstall_Project_Required_UserGroup : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1731080";

        protected override string RegressionTestName => @"Windows_OfficeCSP_Uninstall_Project_Required_UserGroup";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_OfficeCSP_Uninstall_Project_Required_UserGroup";

        protected override string TestDescription => @"[IntuneSA]:[Ibiza]:[Windows][Win10RS2+]:[OfficeCSP][Uninstall]:[Project]:[Required][UserGroup]:Verify Project can be uninstalled successfully";

        [Test]
        public async Task TestMethod_1731080_Test_Set_A_Windows_Regression_Windows_OfficeCSP_Uninstall_Project_Required_UserGroupAsync()
        {
            await RunTestAsync();
        }
    }
}