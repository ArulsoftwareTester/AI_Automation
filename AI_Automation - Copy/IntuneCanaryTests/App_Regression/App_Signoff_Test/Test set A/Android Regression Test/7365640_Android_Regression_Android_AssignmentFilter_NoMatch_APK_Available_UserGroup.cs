using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_7365640_Test_Set_A_Android_Regression_Android_AssignmentFilter_NoMatch_APK_Available_UserGroup : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_7365640";

        protected override string RegressionTestName => @"Android_AssignmentFilter_NoMatch_APK_Available_UserGroup";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_AssignmentFilter_NoMatch_APK_Available_UserGroup";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[Assignment Filters][No Match]:[APK]:[Available][UserGroup]:Verify LOB App does not show in CP or IWP";

        protected override string DataFileName => @"Android_AssignmentFilters_AppRegression.json";

        [Test]
        public async Task TestMethod_7365640_Test_Set_A_Android_Regression_Android_AssignmentFilter_NoMatch_APK_Available_UserGroupAsync()
        {
            await RunTestAsync();
        }
    }
}
