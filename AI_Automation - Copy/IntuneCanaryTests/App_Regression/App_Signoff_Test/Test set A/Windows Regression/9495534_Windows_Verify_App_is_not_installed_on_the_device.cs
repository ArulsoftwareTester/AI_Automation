using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_9495534_Test_Set_A_Windows_Regression_Windows_Verify_App_is_not_installed_on_the_device : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_9495534";

        protected override string RegressionTestName => @"Windows_Verify_App_is_not_installed_on_the_device";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Verify_App_is_not_installed_on_the_device";

        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[Assignment Filters][Exclude][Match][E2E]:[Edge]:[Required][UserGroup]:Verify App is not installed on the device";

        [Test]
        public async Task TestMethod_9495534_Test_Set_A_Windows_Regression_Windows_Verify_App_is_not_installed_on_the_deviceAsync()
        {
            await RunTestAsync();
        }
    }
}