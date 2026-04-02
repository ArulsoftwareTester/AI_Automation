using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1351438_Test_Set_A_Windows_Regression_Windows_AppUpdate_Appx_Required_UserGroup : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1351438";

        protected override string RegressionTestName => @"Windows_AppUpdate_Appx_Required_UserGroup";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_AppUpdate_Appx_Required_UserGroup";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[AppUpdate]:[Appx]:[Required][UserGroup]:Verify that App V1 deployed as required is installed for all devices of User. When App V1 is updated to V2, V2 is updated on all devices with V1 installed.";

        [Test]
        public async Task TestMethod_1351438_Test_Set_A_Windows_Regression_Windows_AppUpdate_Appx_Required_UserGroupAsync()
        {
            await RunTestAsync();
        }
    }
}