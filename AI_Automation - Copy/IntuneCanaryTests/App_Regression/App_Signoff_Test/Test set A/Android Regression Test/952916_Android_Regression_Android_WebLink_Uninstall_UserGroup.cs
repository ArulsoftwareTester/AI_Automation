using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952916_Test_Set_A_Android_Regression_Android_WebLink_Uninstall_UserGroup : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952916";

        protected override string RegressionTestName => @"Android_WebLink_Uninstall_UserGroup";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_WebLink_Uninstall_UserGroup";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[Uninstall]:[WebApp]:[UserGroup]:[WebApp]:Verify Uninstall is success for all Android devices of user";

        protected override string DataFileName => @"Android_WebLink_AppRegression.json";

        [Test]
        public async Task TestMethod_952916_Test_Set_A_Android_Regression_Android_WebLink_Uninstall_UserGroupAsync()
        {
            await RunTestAsync();
        }
    }
}
