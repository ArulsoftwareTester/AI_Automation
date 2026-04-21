using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_5657693_Test_Set_A_Windows_Regression_Windows_Verify_Edge_Dev_Installed_Version_Status : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_5657693";

        protected override string RegressionTestName => @"Windows_Verify_Edge_Dev_Installed_Version_Status";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Verify_Edge_Dev_Installed_Version_Status";

        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10RS3+]:[Publish][E2E]:[Required]:Verify Edge Dev app is installed and correct version is reported in status";

        [Test]
        public async Task TestMethod_5657693_Test_Set_A_Windows_Regression_Windows_Verify_Edge_Dev_Installed_Version_StatusAsync()
        {
            await RunTestAsync();
        }
    }
}
