using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_8829068_Test_Set_A_Android_Regression_Conflict_Exclude_UserGroup_Required_NoMatch_Available_NoMatch : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_8829068";

        protected override string RegressionTestName => @"Conflict_Exclude_UserGroup_Required_NoMatch_Available_NoMatch";

        protected override string TestDisplayName => @"Test set A - Android Regression - Conflict_Exclude_UserGroup_Required_NoMatch_Available_NoMatch";

        protected override string TestDescription => @"[IntuneSA]:[Assignment Filters][Conflict]:[Required][Available][Exclude][UserGroup]:G1 Required F1 No Match, G2 Available F2 No Match, Verify Successfully Installs and shows in CP and IWP";

        protected override string DataFileName => @"Android_Conflict_Exclude_AppRegression.json";

        [Test]
        public async Task TestMethod_8829068_Test_Set_A_Android_Regression_Conflict_Exclude_UserGroup_Required_NoMatch_Available_NoMatchAsync()
        {
            await RunTestAsync();
        }
    }
}
