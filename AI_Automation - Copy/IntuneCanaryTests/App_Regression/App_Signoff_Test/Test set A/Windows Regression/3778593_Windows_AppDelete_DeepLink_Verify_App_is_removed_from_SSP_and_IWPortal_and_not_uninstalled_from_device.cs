using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_3778593_Test_Set_A_Windows_Regression_Windows_AppDelete_DeepLink_Verify_App_is_removed_from_SSP_and_IWPortal_and_not_uninstalled_from_device : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_3778593";

        protected override string RegressionTestName => @"Windows_AppDelete_DeepLink_Verify_App_is_removed_from_SSP_and_IWPortal_and_not_uninstalled_from_device";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_AppDelete_DeepLink_Verify_App_is_removed_from_SSP_and_IWPortal_and_not_uninstalled_from_device";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[AppDelete]:[Deeplink]:IT Pro can delete app from admin console and app is removed from SSP/IWPortal and not uninstalled from device if already installed";

        [Test]
        public async Task TestMethod_3778593_Test_Set_A_Windows_Regression_Windows_AppDelete_DeepLink_Verify_App_is_removed_from_SSP_and_IWPortal_and_not_uninstalled_from_deviceAsync()
        {
            await RunTestAsync();
        }
    }
}