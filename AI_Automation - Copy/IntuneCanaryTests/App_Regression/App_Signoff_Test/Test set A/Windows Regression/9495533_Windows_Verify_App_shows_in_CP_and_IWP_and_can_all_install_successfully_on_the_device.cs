using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_9495533_Test_Set_A_Windows_Regression_Windows_Verify_App_shows_in_CP_and_IWP_and_can_all_install_successfully_on_the_device : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_9495533";

        protected override string RegressionTestName => @"Windows_Verify_App_shows_in_CP_and_IWP_and_can_all_install_successfully_on_the_device";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Verify_App_shows_in_CP_and_IWP_and_can_all_install_successfully_on_the_device";

        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[Assignment Filters][Include][Match][E2E]:[Edge]:[Available][UserGroup]:Verify App shows in CP and IWP and can all install successfully on the device";

        [Test]
        public async Task TestMethod_9495533_Test_Set_A_Windows_Regression_Windows_Verify_App_shows_in_CP_and_IWP_and_can_all_install_successfully_on_the_deviceAsync()
        {
            await RunTestAsync();
        }
    }
}