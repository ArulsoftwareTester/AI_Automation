using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_8829104_Test_Set_A_Android_Regression_Conflict_IncludeExclude_UserGroup_Required_4Groups : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_8829104";

        protected override string RegressionTestName => @"Conflict_IncludeExclude_UserGroup_Required_4Groups";

        protected override string TestDisplayName => @"Test set A - Android Regression - Conflict_IncludeExclude_UserGroup_Required_4Groups";

        protected override string TestDescription => @"[IntuneSA]:[Assignment Filters][Conflict]:[Required][Include-Exclude][UserGroup]:G1 Required F1 Exclude No Match, G2 Required F2 Exclude No Match, G3 Required F3 Include Match, G4 Required F4 Include No Match, Verify Successful Install";

        protected override string DataFileName => @"Android_Conflict_Exclude_AppRegression.json";

        [Test]
        public async Task TestMethod_8829104_Test_Set_A_Android_Regression_Conflict_IncludeExclude_UserGroup_Required_4GroupsAsync()
        {
            await RunTestAsync();
        }
    }
}
