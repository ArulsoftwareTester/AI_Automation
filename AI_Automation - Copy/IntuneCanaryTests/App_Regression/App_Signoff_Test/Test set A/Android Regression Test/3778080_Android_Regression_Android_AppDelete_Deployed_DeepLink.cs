using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_3778080_Test_Set_A_Android_Regression_Android_AppDelete_Deployed_DeepLink : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_3778080";

        protected override string RegressionTestName => @"Android_AppDelete_Deployed_DeepLink";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_AppDelete_Deployed_DeepLink";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[AppDelete]:[Deployed][Deeplink]:IT Pro can delete app from admin console which has active deployments";

        protected override string DataFileName => @"Android_StoreApp_AppRegression.json";

        [Test]
        public async Task TestMethod_3778080_Test_Set_A_Android_Regression_Android_AppDelete_Deployed_DeepLinkAsync()
        {
            await RunTestAsync();
        }
    }
}
