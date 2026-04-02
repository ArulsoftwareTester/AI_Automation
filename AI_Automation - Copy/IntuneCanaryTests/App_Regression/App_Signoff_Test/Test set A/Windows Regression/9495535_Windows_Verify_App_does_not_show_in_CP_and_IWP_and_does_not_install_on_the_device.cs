using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_9495535_Test_Set_A_Windows_Regression_Windows_Verify_App_does_not_show_in_CP_and_IWP_and_does_not_install_on_the_device : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_9495535";

        protected override string RegressionTestName => @"Windows_Verify_App_does_not_show_in_CP_and_IWP_and_does_not_install_on_the_device";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Verify_App_does_not_show_in_CP_and_IWP_and_does_not_install_on_the_device";

        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[Assignment Filters][Exclude][Match]:[Edge]:[Available][UserGroup]:Verify App does not show in CP and IWP and does not install on the device";

        [Test]
        public async Task TestMethod_9495535_Test_Set_A_Windows_Regression_Windows_Verify_App_does_not_show_in_CP_and_IWP_and_does_not_install_on_the_deviceAsync()
        {
            await RunTestAsync();
        }
    }
}