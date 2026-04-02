using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_8829109_Test_Set_A_Android_Regression_Conflict_IncludeExclude_DeviceGroup_Required_ExcludeMatch_Uninstall_IncludeNoMatch : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_8829109";

        protected override string RegressionTestName => @"Conflict_IncludeExclude_DeviceGroup_Required_ExcludeMatch_Uninstall_IncludeNoMatch";

        protected override string TestDisplayName => @"Test set A - Android Regression - Conflict_IncludeExclude_DeviceGroup_Required_ExcludeMatch_Uninstall_IncludeNoMatch";

        protected override string TestDescription => @"[IntuneSA]:[Assignment Filters][Conflict]:[Required][Uninstall][Include-Exclude][DeviceGroup]:G1 Required F1 Exclude Match, G2 Uninstall F2 Include No Match, Verify app does not get installed";

        protected override string DataFileName => @"Android_Conflict_Exclude_AppRegression.json";

        [Test]
        public async Task TestMethod_8829109_Test_Set_A_Android_Regression_Conflict_IncludeExclude_DeviceGroup_Required_ExcludeMatch_Uninstall_IncludeNoMatchAsync()
        {
            await RunTestAsync();
        }
    }
}
