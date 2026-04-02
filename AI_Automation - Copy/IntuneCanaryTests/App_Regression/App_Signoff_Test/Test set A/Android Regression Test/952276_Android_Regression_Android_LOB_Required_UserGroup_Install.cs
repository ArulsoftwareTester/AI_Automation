using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952276_Test_Set_A_Android_Regression_Android_LOB_Required_UserGroup_Install : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952276";

        protected override string RegressionTestName => @"Android_LOB_Required_UserGroup_Install";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_LOB_Required_UserGroup_Install";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[Required]:[UserGroup][Apk]:Verify Install is success for all Android devices of user";

        protected override string DataFileName => @"Android_LOB_AppRegression.json";

        [Test]
        public async Task TestMethod_952276_Test_Set_A_Android_Regression_Android_LOB_Required_UserGroup_InstallAsync()
        {
            await RunTestAsync();
        }
    }
}
