using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_7533024_Test_Set_A_Android_Regression_Conflict_Include_DeviceGroup_Required_NoMatch_Available_NoMatch : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_7533024";

        protected override string RegressionTestName => @"Conflict_Include_DeviceGroup_Required_NoMatch_Available_NoMatch";

        protected override string TestDisplayName => @"Test set A - Android Regression - Conflict_Include_DeviceGroup_Required_NoMatch_Available_NoMatch";

        protected override string TestDescription => @"[IntuneSA]:[Assignment Filters][Conflict]:[Required][Available][Include][DeviceGroup]:G1 Required F1 No Match, G2 Available F2 No Match, Verify app does not get Installed and does not show in CP and IWP";

        protected override string DataFileName => @"Android_Conflict_Include_AppRegression.json";

        [Test]
        public async Task TestMethod_7533024_Test_Set_A_Android_Regression_Conflict_Include_DeviceGroup_Required_NoMatch_Available_NoMatchAsync()
        {
            await RunTestAsync();
        }
    }
}
