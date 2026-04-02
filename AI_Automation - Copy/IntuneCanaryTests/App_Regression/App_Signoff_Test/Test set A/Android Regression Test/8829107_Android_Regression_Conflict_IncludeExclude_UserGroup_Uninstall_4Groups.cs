using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_8829107_Test_Set_A_Android_Regression_Conflict_IncludeExclude_UserGroup_Uninstall_4Groups : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_8829107";

        protected override string RegressionTestName => @"Conflict_IncludeExclude_UserGroup_Uninstall_4Groups";

        protected override string TestDisplayName => @"Test set A - Android Regression - Conflict_IncludeExclude_UserGroup_Uninstall_4Groups";

        protected override string TestDescription => @"[IntuneSA]:[Assignment Filters][Conflict]:[Uninstall][Include-Exclude][UserGroup]:G1 Uninstall F1 Exclude No Match, G2 Uninstall F2 Exclude No Match, G3 Uninstall F3 Include Match, G4 Uninstall F4 Include No Match, Verify Successful Uninstall";

        protected override string DataFileName => @"Android_Conflict_Exclude_AppRegression.json";

        [Test]
        public async Task TestMethod_8829107_Test_Set_A_Android_Regression_Conflict_IncludeExclude_UserGroup_Uninstall_4GroupsAsync()
        {
            await RunTestAsync();
        }
    }
}
