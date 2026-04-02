using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_3778361_Test_Set_A_Android_Regression_Android_LOB_Publish_Apk : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_3778361";

        protected override string RegressionTestName => @"Android_LOB_Publish_Apk";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_LOB_Publish_Apk";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[Publish][Apk]:Verify admin can publish an apk using ibiza";

        protected override string DataFileName => @"Android_LOB_AppRegression.json";

        [Test]
        public async Task TestMethod_3778361_Test_Set_A_Android_Regression_Android_LOB_Publish_ApkAsync()
        {
            await RunTestAsync();
        }
    }
}
