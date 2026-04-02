using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_8982257_Test_Set_A_Windows_Regression_Windows_Verify_Edge_Stable_app_is_installed_and_correct_version_is_reported_in_status : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_8982257";

        protected override string RegressionTestName => @"Windows_Verify_Edge_Stable_app_is_installed_and_correct_version_is_reported_in_status";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Verify_Edge_Stable_app_is_installed_and_correct_version_is_reported_in_status";

        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10RS3+]:[Publish][E2E]:[Required]:Verify Edge Stable app is installed and correct version is reported in status";

        [Test]
        public async Task TestMethod_8982257_Test_Set_A_Windows_Regression_Windows_Verify_Edge_Stable_app_is_installed_and_correct_version_is_reported_in_statusAsync()
        {
            await RunTestAsync();
        }
    }
}