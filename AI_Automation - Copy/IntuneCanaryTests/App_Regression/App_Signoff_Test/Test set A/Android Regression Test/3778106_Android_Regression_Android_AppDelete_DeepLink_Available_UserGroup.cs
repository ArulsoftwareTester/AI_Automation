using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_3778106_Test_Set_A_Android_Regression_Android_AppDelete_DeepLink_Available_UserGroup : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_3778106";

        protected override string RegressionTestName => @"Android_AppDelete_DeepLink_Available_UserGroup";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_AppDelete_DeepLink_Available_UserGroup";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[AppDelete]:[Deeplink]:[Available][UserGroup]:IT Pro can delete app from admin console and app is removed from SSP/IWPortal and not uninstalled from device if already installed";

        protected override string DataFileName => @"Android_StoreApp_AppRegression.json";

        [Test]
        public async Task TestMethod_3778106_Test_Set_A_Android_Regression_Android_AppDelete_DeepLink_Available_UserGroupAsync()
        {
            await RunTestAsync();
        }
    }
}
