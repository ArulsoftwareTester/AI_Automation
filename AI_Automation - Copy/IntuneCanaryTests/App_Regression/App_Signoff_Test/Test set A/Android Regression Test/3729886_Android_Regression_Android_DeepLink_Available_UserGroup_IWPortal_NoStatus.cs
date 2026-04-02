using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_3729886_Test_Set_A_Android_Regression_Android_DeepLink_Available_UserGroup_IWPortal_NoStatus : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_3729886";

        protected override string RegressionTestName => @"Android_DeepLink_Available_UserGroup_IWPortal_NoStatus";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_DeepLink_Available_UserGroup_IWPortal_NoStatus";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[Deeplink]:[Available][UserGroup]:Verify Store App is displayed on IWPortal/SSP and Install is successful when IWUser install from SSP/IWPortal (No Status in Admin UI)";

        protected override string DataFileName => @"Android_StoreApp_AppRegression.json";

        [Test]
        public async Task TestMethod_3729886_Test_Set_A_Android_Regression_Android_DeepLink_Available_UserGroup_IWPortal_NoStatusAsync()
        {
            await RunTestAsync();
        }
    }
}
