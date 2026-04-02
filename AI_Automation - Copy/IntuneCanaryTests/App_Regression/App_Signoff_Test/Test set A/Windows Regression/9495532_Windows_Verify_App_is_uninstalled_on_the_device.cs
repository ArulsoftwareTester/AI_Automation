using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_9495532_Test_Set_A_Windows_Regression_Windows_Verify_App_is_uninstalled_on_the_device : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_9495532";

        protected override string RegressionTestName => @"Windows_Verify_App_is_uninstalled_on_the_device";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Verify_App_is_uninstalled_on_the_device";

        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[Assignment Filters][Include][Match][E2E]:[Edge]:[Uninstall][UserGroup]:Verify App is uninstalled on the device";

        [Test]
        public async Task TestMethod_9495532_Test_Set_A_Windows_Regression_Windows_Verify_App_is_uninstalled_on_the_deviceAsync()
        {
            await RunTestAsync();
        }
    }
}