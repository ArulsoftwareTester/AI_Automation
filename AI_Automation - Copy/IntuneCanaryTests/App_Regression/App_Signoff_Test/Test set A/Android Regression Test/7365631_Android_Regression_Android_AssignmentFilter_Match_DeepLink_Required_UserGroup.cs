using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_7365631_Test_Set_A_Android_Regression_Android_AssignmentFilter_Match_DeepLink_Required_UserGroup : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_7365631";

        protected override string RegressionTestName => @"Android_AssignmentFilter_Match_DeepLink_Required_UserGroup";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_AssignmentFilter_Match_DeepLink_Required_UserGroup";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[Assignment Filters][Match][E2E]:[DeepLink]:[Required][UserGroup]: Verify Store App is installed successfully on the device";

        protected override string DataFileName => @"Android_AssignmentFilters_AppRegression.json";

        [Test]
        public async Task TestMethod_7365631_Test_Set_A_Android_Regression_Android_AssignmentFilter_Match_DeepLink_Required_UserGroupAsync()
        {
            await RunTestAsync();
        }
    }
}
