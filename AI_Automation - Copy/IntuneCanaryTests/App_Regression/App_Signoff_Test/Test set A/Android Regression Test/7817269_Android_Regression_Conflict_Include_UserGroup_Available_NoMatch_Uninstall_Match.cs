using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_7817269_Test_Set_A_Android_Regression_Conflict_Include_UserGroup_Available_NoMatch_Uninstall_Match : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_7817269";

        protected override string RegressionTestName => @"Conflict_Include_UserGroup_Available_NoMatch_Uninstall_Match";

        protected override string TestDisplayName => @"Test set A - Android Regression - Conflict_Include_UserGroup_Available_NoMatch_Uninstall_Match";

        protected override string TestDescription => @"[IntuneSA]:[Assignment Filters][Conflict]:[Available][Uninstall][Include][UserGroup]:G1 Available F1 NoMatch, G2 Uninstall F2 Match, Verify app gets uninstalled. App does not show in IWP or CP";

        protected override string DataFileName => @"Android_Conflict_Include_AppRegression.json";

        [Test]
        public async Task TestMethod_7817269_Test_Set_A_Android_Regression_Conflict_Include_UserGroup_Available_NoMatch_Uninstall_MatchAsync()
        {
            await RunTestAsync();
        }
    }
}
