using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1363141_Test_Set_A_Android_Regression_Android_GlobalMAMApp_Available_UserGroup_IWPortal : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1363141";

        protected override string RegressionTestName => @"Android_GlobalMAMApp_Available_UserGroup_IWPortal";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_GlobalMAMApp_Available_UserGroup_IWPortal";

        protected override string TestDescription => @"[IntuneSA]:[Ibiza]:[Android]:[GlobalMAMApp]:[Available][UserGroup]:Verify Store App is displayed on IWPortal/SSP and Install is successful when IWUser install from SSP/IWPortal";

        protected override string DataFileName => @"Android_StoreApp_AppRegression.json";

        [Test]
        public async Task TestMethod_1363141_Test_Set_A_Android_Regression_Android_GlobalMAMApp_Available_UserGroup_IWPortalAsync()
        {
            await RunTestAsync();
        }
    }
}
