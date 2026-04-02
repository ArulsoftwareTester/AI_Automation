using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952842_Test_Set_A_Windows_Regression_Windows_AppDelete_Appx_Deployed : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952842";

        protected override string RegressionTestName => @"Windows_AppDelete_Appx_Deployed";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_AppDelete_Appx_Deployed";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[AppDelete]:[Appx][Deployed]:IT Pro can delete app from admin console which has active deployments";

        [Test]
        public async Task TestMethod_952842_Test_Set_A_Windows_Regression_Windows_AppDelete_Appx_DeployedAsync()
        {
            await RunTestAsync();
        }
    }
}