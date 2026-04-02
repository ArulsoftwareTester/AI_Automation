using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_5657696_Test_Set_A_Windows_Regression_Windows_Win10RS3_Publish_E2E_Edge_Dev_Channel_Deployment : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_5657696";

        protected override string RegressionTestName => @"Windows_Win10RS3_Publish_E2E_Edge_Dev_Channel_Deployment";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Win10RS3_Publish_E2E_Edge_Dev_Channel_Deployment";

        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10RS3+]:[Publish][E2E]:Verify Microsoft Edge Dev channel app deployment for user group.";

        [Test]
        public async Task TestMethod_5657696_Test_Set_A_Windows_Regression_Windows_Win10RS3_Publish_E2E_Edge_Dev_Channel_DeploymentAsync()
        {
            await RunTestAsync();
        }
    }
}