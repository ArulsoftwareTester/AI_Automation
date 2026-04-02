using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_7532990_Test_Set_A_Android_Regression_Conflict_Include_DeviceGroup_Required_Match_Available_Match : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_7532990";

        protected override string RegressionTestName => @"Conflict_Include_DeviceGroup_Required_Match_Available_Match";

        protected override string TestDisplayName => @"Test set A - Android Regression - Conflict_Include_DeviceGroup_Required_Match_Available_Match";

        protected override string TestDescription => @"[IntuneSA]:[Assignment Filters][Conflict]:[Required][Available][Include][DeviceGroup]:G1 Required F1 Match, G2 Available F2 Match, Verify Successful Install and does not show in IWP. CP will show only if affiliated user";

        protected override string DataFileName => @"Android_Conflict_Include_AppRegression.json";

        [Test]
        public async Task TestMethod_7532990_Test_Set_A_Android_Regression_Conflict_Include_DeviceGroup_Required_Match_Available_MatchAsync()
        {
            await RunTestAsync();
        }
    }
}
