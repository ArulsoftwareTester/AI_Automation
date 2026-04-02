using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_7817274_Test_Set_A_Android_Regression_Conflict_Include_DeviceGroup_Required_Match_Uninstall_NoMatch : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_7817274";

        protected override string RegressionTestName => @"Conflict_Include_DeviceGroup_Required_Match_Uninstall_NoMatch";

        protected override string TestDisplayName => @"Test set A - Android Regression - Conflict_Include_DeviceGroup_Required_Match_Uninstall_NoMatch";

        protected override string TestDescription => @"[IntuneSA]:[Assignment Filters][Conflict]:[Required][Uninstall][Include][DeviceGroup]:G1 Required F1 Match, G2 Uninstall F2 NoMatch, Verify Successful Install";

        protected override string DataFileName => @"Android_Conflict_Include_AppRegression.json";

        [Test]
        public async Task TestMethod_7817274_Test_Set_A_Android_Regression_Conflict_Include_DeviceGroup_Required_Match_Uninstall_NoMatchAsync()
        {
            await RunTestAsync();
        }
    }
}
