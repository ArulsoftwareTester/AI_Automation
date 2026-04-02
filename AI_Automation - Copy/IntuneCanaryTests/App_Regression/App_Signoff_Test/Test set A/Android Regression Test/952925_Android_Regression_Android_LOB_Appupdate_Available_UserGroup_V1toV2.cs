using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952925_Test_Set_A_Android_Regression_Android_LOB_Appupdate_Available_UserGroup_V1toV2 : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952925";

        protected override string RegressionTestName => @"Android_LOB_Appupdate_Available_UserGroup_V1toV2";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_LOB_Appupdate_Available_UserGroup_V1toV2";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[Appupdate]:[Apk][Available][UserGroup]:App V1 is installed on device. Now V1 is updated to V2. Verify V1 is updated to V2.";

        protected override string DataFileName => @"Android_LOB_AppRegression.json";

        [Test]
        public async Task TestMethod_952925_Test_Set_A_Android_Regression_Android_LOB_Appupdate_Available_UserGroup_V1toV2Async()
        {
            await RunTestAsync();
        }
    }
}
