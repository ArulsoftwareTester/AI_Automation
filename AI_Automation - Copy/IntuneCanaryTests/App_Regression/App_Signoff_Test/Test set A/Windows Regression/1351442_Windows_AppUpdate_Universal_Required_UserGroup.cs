using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1351442_Test_Set_A_Windows_Regression_Windows_AppUpdate_Universal_Required_UserGroup : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1351442";

        protected override string RegressionTestName => @"Windows_AppUpdate_Universal_Required_UserGroup";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_AppUpdate_Universal_Required_UserGroup";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[Appupdate]:[Universal]:[Required][UserGroup]:App V1 is installed on device. Now V1 is updated to V2. Verify V1 is updated to V2";

        [Test]
        public async Task TestMethod_1351442_Test_Set_A_Windows_Regression_Windows_AppUpdate_Universal_Required_UserGroupAsync()
        {
            await RunTestAsync();
        }
    }
}