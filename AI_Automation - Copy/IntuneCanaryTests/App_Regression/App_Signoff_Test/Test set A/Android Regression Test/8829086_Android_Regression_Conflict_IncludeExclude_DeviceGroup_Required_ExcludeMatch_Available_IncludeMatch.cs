using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_8829086_Test_Set_A_Android_Regression_Conflict_IncludeExclude_DeviceGroup_Required_ExcludeMatch_Available_IncludeMatch : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_8829086";

        protected override string RegressionTestName => @"Conflict_IncludeExclude_DeviceGroup_Required_ExcludeMatch_Available_IncludeMatch";

        protected override string TestDisplayName => @"Test set A - Android Regression - Conflict_IncludeExclude_DeviceGroup_Required_ExcludeMatch_Available_IncludeMatch";

        protected override string TestDescription => @"[IntuneSA]:[Assignment Filters][Conflict]:[Required][Available][Include-Exclude][DeviceGroup]:G1 Required F1 Exclude Match, G2 Available F2 Include Match, Verify does not Install and does not show in IWP or CP";

        protected override string DataFileName => @"Android_Conflict_Exclude_AppRegression.json";

        [Test]
        public async Task TestMethod_8829086_Test_Set_A_Android_Regression_Conflict_IncludeExclude_DeviceGroup_Required_ExcludeMatch_Available_IncludeMatchAsync()
        {
            await RunTestAsync();
        }
    }
}
