using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_25572285_Test_Set_A_Android_Regression_Android_MinOS_DeepLink_Required_UserGroup : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_25572285";

        protected override string RegressionTestName => @"Android_MinOS_DeepLink_Required_UserGroup";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_MinOS_DeepLink_Required_UserGroup";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[MinOS]:[DeepLink]:[Required][UserGroup]: MinOS is honored";

        protected override string DataFileName => @"Android_StoreApp_AppRegression.json";

        [Test]
        public async Task TestMethod_25572285_Test_Set_A_Android_Regression_Android_MinOS_DeepLink_Required_UserGroupAsync()
        {
            await RunTestAsync();
        }
    }
}
