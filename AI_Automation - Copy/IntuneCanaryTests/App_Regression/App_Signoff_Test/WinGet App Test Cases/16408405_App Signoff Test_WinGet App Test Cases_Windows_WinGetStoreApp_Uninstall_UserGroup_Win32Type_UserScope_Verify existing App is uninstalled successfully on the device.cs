using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_16408405_App_Signoff_Test_WinGet_App_Test_Cases : WinGetStoreAppRegressionTestBase
    {
        protected override string RegressionTestCaseId => "TC_16408405";

        protected override string TestDisplayName => "App Signoff Test - WinGet App Test Cases - Windows_WinGetStoreApp_Uninstall_UserGroup_Win32Type_UserScope_Verify existing App is uninstalled successfully on the device";

        [Test]
        public async Task TestMethod_16408405_App_Signoff_Test_WinGet_App_Test_CasesAsync()
        {
            await RunTestAsync();
        }
    }
}
