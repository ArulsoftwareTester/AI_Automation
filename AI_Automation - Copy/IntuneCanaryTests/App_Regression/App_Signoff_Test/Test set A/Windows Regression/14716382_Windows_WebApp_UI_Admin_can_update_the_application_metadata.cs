using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_14716382_Test_Set_A_Windows_Regression_Windows_WebApp_UI_Admin_can_update_the_application_metadata : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_14716382";

        protected override string RegressionTestName => @"Windows_WebApp_UI_Admin_can_update_the_application_metadata";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_WebApp_UI_Admin_can_update_the_application_metadata";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[WebApp][UI]:Admin can update the application metadata";

        [Test]
        public async Task TestMethod_14716382_Test_Set_A_Windows_Regression_Windows_WebApp_UI_Admin_can_update_the_application_metadataAsync()
        {
            await RunTestAsync();
        }
    }
}