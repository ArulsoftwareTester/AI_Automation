using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_7365639_Test_Set_A_Android_Regression_Android_AssignmentFilter_NoMatch_APK_Required_UserGroup : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_7365639";

        protected override string RegressionTestName => @"Android_AssignmentFilter_NoMatch_APK_Required_UserGroup";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_AssignmentFilter_NoMatch_APK_Required_UserGroup";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[Assignment Filters][No Match][E2E]:[APK]:[Required][UserGroup]: Verify LOB App is not installed on the device";

        protected override string DataFileName => @"Android_AssignmentFilters_AppRegression.json";

        [Test]
        public async Task TestMethod_7365639_Test_Set_A_Android_Regression_Android_AssignmentFilter_NoMatch_APK_Required_UserGroupAsync()
        {
            await RunTestAsync();
        }
    }
}
