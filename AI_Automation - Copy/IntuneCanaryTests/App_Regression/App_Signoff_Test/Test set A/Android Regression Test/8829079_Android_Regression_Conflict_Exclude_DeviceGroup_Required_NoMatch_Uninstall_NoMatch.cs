using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_8829079_Test_Set_A_Android_Regression_Conflict_Exclude_DeviceGroup_Required_NoMatch_Uninstall_NoMatch : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_8829079";

        protected override string RegressionTestName => @"Conflict_Exclude_DeviceGroup_Required_NoMatch_Uninstall_NoMatch";

        protected override string TestDisplayName => @"Test set A - Android Regression - Conflict_Exclude_DeviceGroup_Required_NoMatch_Uninstall_NoMatch";

        protected override string TestDescription => @"[IntuneSA]:[Assignment Filters][Conflict]:[Required][Uninstall][Exclude][DeviceGroup]:G1 Required F1 NoMatch, G2 Uninstall F2 NoMatch, Verify Successful Install";

        protected override string DataFileName => @"Android_Conflict_Exclude_AppRegression.json";

        [Test]
        public async Task TestMethod_8829079_Test_Set_A_Android_Regression_Conflict_Exclude_DeviceGroup_Required_NoMatch_Uninstall_NoMatchAsync()
        {
            await RunTestAsync();
        }
    }
}
