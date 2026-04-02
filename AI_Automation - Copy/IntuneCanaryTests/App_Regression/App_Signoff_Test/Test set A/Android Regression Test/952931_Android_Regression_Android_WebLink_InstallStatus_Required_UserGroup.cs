using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952931_Test_Set_A_Android_Regression_Android_WebLink_InstallStatus_Required_UserGroup : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952931";

        protected override string RegressionTestName => @"Android_WebLink_InstallStatus_Required_UserGroup";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_WebLink_InstallStatus_Required_UserGroup";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[InstallStatus]:[WebApp]:[Required][UserGroup]:IT Pro can see installation status summary for app deployed for User group as required";

        protected override string DataFileName => @"Android_WebLink_AppRegression.json";

        [Test]
        public async Task TestMethod_952931_Test_Set_A_Android_Regression_Android_WebLink_InstallStatus_Required_UserGroupAsync()
        {
            await RunTestAsync();
        }
    }
}
