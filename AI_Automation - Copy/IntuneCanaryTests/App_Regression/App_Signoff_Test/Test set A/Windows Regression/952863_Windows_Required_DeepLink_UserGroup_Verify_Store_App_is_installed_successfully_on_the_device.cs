using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952863_Test_Set_A_Windows_Regression_Windows_Required_DeepLink_UserGroup_Verify_Store_App_is_installed_successfully_on_the_device : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952863";

        protected override string RegressionTestName => @"Windows_Required_DeepLink_UserGroup_Verify_Store_App_is_installed_successfully_on_the_device";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Required_DeepLink_UserGroup_Verify_Store_App_is_installed_successfully_on_the_device";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[Required]:[DeepLink][UserGroup]:Verify Store App is installed successfully on the device";

        [Test]
        public async Task TestMethod_952863_Test_Set_A_Windows_Regression_Windows_Required_DeepLink_UserGroup_Verify_Store_App_is_installed_successfully_on_the_deviceAsync()
        {
            await RunTestAsync();
        }
    }
}