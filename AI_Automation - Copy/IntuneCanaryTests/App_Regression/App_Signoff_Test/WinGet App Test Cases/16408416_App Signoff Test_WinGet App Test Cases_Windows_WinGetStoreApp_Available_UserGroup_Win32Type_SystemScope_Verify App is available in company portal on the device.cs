using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_16408416_App_Signoff_Test_WinGet_App_Test_Cases : WinGetStoreAppRegressionTestBase
    {
        protected override string RegressionTestCaseId => "TC_16408416";

        protected override string TestDisplayName => "App Signoff Test - WinGet App Test Cases - Windows_WinGetStoreApp_Available_UserGroup_Win32Type_SystemScope_Verify App is available in company portal on the device";

        [Test]
        public async Task TestMethod_16408416_App_Signoff_Test_WinGet_App_Test_CasesAsync()
        {
            await RunTestAsync();
        }
    }
}
