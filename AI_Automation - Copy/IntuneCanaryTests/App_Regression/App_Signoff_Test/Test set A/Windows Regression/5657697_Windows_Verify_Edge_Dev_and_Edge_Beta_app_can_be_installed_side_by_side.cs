using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_5657697_Test_Set_A_Windows_Regression_Windows_Verify_Edge_Dev_and_Edge_Beta_app_can_be_installed_side_by_side : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_5657697";

        protected override string RegressionTestName => @"Windows_Verify_Edge_Dev_and_Edge_Beta_app_can_be_installed_side_by_side";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Verify_Edge_Dev_and_Edge_Beta_app_can_be_installed_side_by_side";

        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10RS3+]:[Publish][E2E]:Verify Edge Dev and Edge Beta app can be installed side by side";

        [Test]
        public async Task TestMethod_5657697_Test_Set_A_Windows_Regression_Windows_Verify_Edge_Dev_and_Edge_Beta_app_can_be_installed_side_by_sideAsync()
        {
            await RunTestAsync();
        }
    }
}