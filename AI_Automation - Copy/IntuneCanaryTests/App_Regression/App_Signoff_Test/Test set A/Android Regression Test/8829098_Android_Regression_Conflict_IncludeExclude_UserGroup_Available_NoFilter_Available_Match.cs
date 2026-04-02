using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_8829098_Test_Set_A_Android_Regression_Conflict_IncludeExclude_UserGroup_Available_NoFilter_Available_Match : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_8829098";

        protected override string RegressionTestName => @"Conflict_IncludeExclude_UserGroup_Available_NoFilter_Available_Match";

        protected override string TestDisplayName => @"Test set A - Android Regression - Conflict_IncludeExclude_UserGroup_Available_NoFilter_Available_Match";

        protected override string TestDescription => @"[IntuneSA]:[Assignment Filters][Conflict]:[Available][Include-Exclude][UserGroup]:G1 Available no filter, G2 Available F1 Match, Verify app shows in CP and IWP";

        protected override string DataFileName => @"Android_Conflict_Exclude_AppRegression.json";

        [Test]
        public async Task TestMethod_8829098_Test_Set_A_Android_Regression_Conflict_IncludeExclude_UserGroup_Available_NoFilter_Available_MatchAsync()
        {
            await RunTestAsync();
        }
    }
}
