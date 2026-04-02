using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_8829092_Test_Set_A_Android_Regression_Conflict_IncludeExclude_DeviceGroup_Required_ExcludeNoMatch_Available_IncludeMatch : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_8829092";

        protected override string RegressionTestName => @"Conflict_IncludeExclude_DeviceGroup_Required_ExcludeNoMatch_Available_IncludeMatch";

        protected override string TestDisplayName => @"Test set A - Android Regression - Conflict_IncludeExclude_DeviceGroup_Required_ExcludeNoMatch_Available_IncludeMatch";

        protected override string TestDescription => @"[IntuneSA]:[Assignment Filters][Conflict]:[Required][Available][Include-Exclude][DeviceGroup]:G1 Required F1 Exclude No Match, G2 Available F2 Include Match, Verify app will be Installed but it does show in CP and IWP";

        protected override string DataFileName => @"Android_Conflict_Exclude_AppRegression.json";

        [Test]
        public async Task TestMethod_8829092_Test_Set_A_Android_Regression_Conflict_IncludeExclude_DeviceGroup_Required_ExcludeNoMatch_Available_IncludeMatchAsync()
        {
            await RunTestAsync();
        }
    }
}
