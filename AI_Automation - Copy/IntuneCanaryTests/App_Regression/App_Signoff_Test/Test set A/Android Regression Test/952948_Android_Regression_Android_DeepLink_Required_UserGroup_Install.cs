using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952948_Test_Set_A_Android_Regression_Android_DeepLink_Required_UserGroup_Install : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952948";

        protected override string RegressionTestName => @"Android_DeepLink_Required_UserGroup_Install";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_DeepLink_Required_UserGroup_Install";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[DeepLink]:[Required][UserGroup]:Verify Store App is installed successfully on the device";

        protected override string DataFileName => @"Android_StoreApp_AppRegression.json";

        [Test]
        public async Task TestMethod_952948_Test_Set_A_Android_Regression_Android_DeepLink_Required_UserGroup_InstallAsync()
        {
            await RunTestAsync();
        }
    }
}
