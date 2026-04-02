using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_3778151_Test_Set_A_Android_Regression_Android_LOB_AppDelete_Apk : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_3778151";

        protected override string RegressionTestName => @"Android_LOB_AppDelete_Apk";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_LOB_AppDelete_Apk";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[AppDelete]:[Apk]:IT Pro can delete app from admin console and app is removed from SSP/IWPortal and not uninstalled from device if already installed";

        protected override string DataFileName => @"Android_LOB_AppRegression.json";

        [Test]
        public async Task TestMethod_3778151_Test_Set_A_Android_Regression_Android_LOB_AppDelete_ApkAsync()
        {
            await RunTestAsync();
        }
    }
}
