using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_8829053_Test_Set_A_Android_Regression_Conflict_Exclude_DeviceGroup_Required_NoMatch_Available_Match : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_8829053";

        protected override string RegressionTestName => @"Conflict_Exclude_DeviceGroup_Required_NoMatch_Available_Match";

        protected override string TestDisplayName => @"Test set A - Android Regression - Conflict_Exclude_DeviceGroup_Required_NoMatch_Available_Match";

        protected override string TestDescription => @"[IntuneSA]:[Assignment Filters][Conflict]:[Required][Available][Exclude][DeviceGroup]:G1 Required F1 No Match, G2 Available F2 Match, Verify not Installed and does not show in CP and IWP";

        protected override string DataFileName => @"Android_Conflict_Exclude_AppRegression.json";

        [Test]
        public async Task TestMethod_8829053_Test_Set_A_Android_Regression_Conflict_Exclude_DeviceGroup_Required_NoMatch_Available_MatchAsync()
        {
            await RunTestAsync();
        }
    }
}
