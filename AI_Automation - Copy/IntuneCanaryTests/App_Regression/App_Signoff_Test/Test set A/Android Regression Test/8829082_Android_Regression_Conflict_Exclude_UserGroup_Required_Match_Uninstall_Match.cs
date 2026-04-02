using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_8829082_Test_Set_A_Android_Regression_Conflict_Exclude_UserGroup_Required_Match_Uninstall_Match : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_8829082";

        protected override string RegressionTestName => @"Conflict_Exclude_UserGroup_Required_Match_Uninstall_Match";

        protected override string TestDisplayName => @"Test set A - Android Regression - Conflict_Exclude_UserGroup_Required_Match_Uninstall_Match";

        protected override string TestDescription => @"[IntuneSA]:[Assignment Filters][Conflict]:[Required][Uninstall][Exclude][UserGroup]:G1 Required F1 Match, G2 Uninstall F2 Match, Verify app does not get Installed";

        protected override string DataFileName => @"Android_Conflict_Exclude_AppRegression.json";

        [Test]
        public async Task TestMethod_8829082_Test_Set_A_Android_Regression_Conflict_Exclude_UserGroup_Required_Match_Uninstall_MatchAsync()
        {
            await RunTestAsync();
        }
    }
}
