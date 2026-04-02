using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_8829095_Test_Set_A_Android_Regression_Conflict_IncludeExclude_DeviceGroup_Required_ExcludeNoMatch_Available_IncludeNoMatch : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_8829095";

        protected override string RegressionTestName => @"Conflict_IncludeExclude_DeviceGroup_Required_ExcludeNoMatch_Available_IncludeNoMatch";

        protected override string TestDisplayName => @"Test set A - Android Regression - Conflict_IncludeExclude_DeviceGroup_Required_ExcludeNoMatch_Available_IncludeNoMatch";

        protected override string TestDescription => @"[IntuneSA]:[Assignment Filters][Conflict]:[Required][Available][Include-Exclude][DeviceGroup]:G1 Required F1 Exclude No Match, G2 Available F2 Include No Match, Verify Successful Install and will not show in CP and IWP";

        protected override string DataFileName => @"Android_Conflict_Exclude_AppRegression.json";

        [Test]
        public async Task TestMethod_8829095_Test_Set_A_Android_Regression_Conflict_IncludeExclude_DeviceGroup_Required_ExcludeNoMatch_Available_IncludeNoMatchAsync()
        {
            await RunTestAsync();
        }
    }
}
