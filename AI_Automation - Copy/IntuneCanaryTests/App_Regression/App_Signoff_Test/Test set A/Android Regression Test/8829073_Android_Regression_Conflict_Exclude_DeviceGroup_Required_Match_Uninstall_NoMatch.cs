using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_8829073_Test_Set_A_Android_Regression_Conflict_Exclude_DeviceGroup_Required_Match_Uninstall_NoMatch : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_8829073";

        protected override string RegressionTestName => @"Conflict_Exclude_DeviceGroup_Required_Match_Uninstall_NoMatch";

        protected override string TestDisplayName => @"Test set A - Android Regression - Conflict_Exclude_DeviceGroup_Required_Match_Uninstall_NoMatch";

        protected override string TestDescription => @"[IntuneSA]:[Assignment Filters][Conflict]:[Required][Uninstall][Exclude][DeviceGroup]:G1 Required F1 Match, G2 Uninstall F2 NoMatch, Verify app does not Install";

        protected override string DataFileName => @"Android_Conflict_Exclude_AppRegression.json";

        [Test]
        public async Task TestMethod_8829073_Test_Set_A_Android_Regression_Conflict_Exclude_DeviceGroup_Required_Match_Uninstall_NoMatchAsync()
        {
            await RunTestAsync();
        }
    }
}
