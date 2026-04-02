using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_7532819_Test_Set_A_Android_Regression_Conflict_Include_UserGroup_Required_NoMatch_Available_Match : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_7532819";

        protected override string RegressionTestName => @"Conflict_Include_UserGroup_Required_NoMatch_Available_Match";

        protected override string TestDisplayName => @"Test set A - Android Regression - Conflict_Include_UserGroup_Required_NoMatch_Available_Match";

        protected override string TestDescription => @"[IntuneSA]:[Assignment Filters][Conflict]:[Required][Available][Include][UserGroup]:G1 Required F1 No Match, G2 Available F2 Match, Verify app gets Installed and it shows in CP and IWP";

        protected override string DataFileName => @"Android_Conflict_Include_AppRegression.json";

        [Test]
        public async Task TestMethod_7532819_Test_Set_A_Android_Regression_Conflict_Include_UserGroup_Required_NoMatch_Available_MatchAsync()
        {
            await RunTestAsync();
        }
    }
}
