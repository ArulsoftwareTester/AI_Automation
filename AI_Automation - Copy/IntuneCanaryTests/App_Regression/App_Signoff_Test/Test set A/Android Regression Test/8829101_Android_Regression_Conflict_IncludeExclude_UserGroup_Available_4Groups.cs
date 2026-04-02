using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_8829101_Test_Set_A_Android_Regression_Conflict_IncludeExclude_UserGroup_Available_4Groups : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_8829101";

        protected override string RegressionTestName => @"Conflict_IncludeExclude_UserGroup_Available_4Groups";

        protected override string TestDisplayName => @"Test set A - Android Regression - Conflict_IncludeExclude_UserGroup_Available_4Groups";

        protected override string TestDescription => @"[IntuneSA]:[Assignment Filters][Conflict]:[Available][Include-Exclude][UserGroup]:G1 Available F1 Exclude No Match, G2 Available F2 Exclude Match, G3 Available F3 Include No Match, G4 Available F4 Include Match, Verify app does not show in CP and IWP";

        protected override string DataFileName => @"Android_Conflict_Exclude_AppRegression.json";

        [Test]
        public async Task TestMethod_8829101_Test_Set_A_Android_Regression_Conflict_IncludeExclude_UserGroup_Available_4GroupsAsync()
        {
            await RunTestAsync();
        }
    }
}
