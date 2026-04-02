using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_7365635_Test_Set_A_Android_Regression_Android_AssignmentFilter_Match_APK_Available_UserGroup : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_7365635";

        protected override string RegressionTestName => @"Android_AssignmentFilter_Match_APK_Available_UserGroup";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_AssignmentFilter_Match_APK_Available_UserGroup";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[Assignment Filters][Match][E2E]:[APK]:[Available][UserGroup]:Verify LOB App shows in CP and IWP and can be installed successfully on the device";

        protected override string DataFileName => @"Android_AssignmentFilters_AppRegression.json";

        [Test]
        public async Task TestMethod_7365635_Test_Set_A_Android_Regression_Android_AssignmentFilter_Match_APK_Available_UserGroupAsync()
        {
            await RunTestAsync();
        }
    }
}
