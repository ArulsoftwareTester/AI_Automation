using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_14716397_Test_Set_A_Windows_Regression_Windows_WebApp_UI_Admin_can_delete_the_application : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_14716397";

        protected override string RegressionTestName => @"Windows_WebApp_UI_Admin_can_delete_the_application";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_WebApp_UI_Admin_can_delete_the_application";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[WebApp][UI]:Admin can delete the application";

        [Test]
        public async Task TestMethod_14716397_Test_Set_A_Windows_Regression_Windows_WebApp_UI_Admin_can_delete_the_applicationAsync()
        {
            await RunTestAsync();
        }
    }
}