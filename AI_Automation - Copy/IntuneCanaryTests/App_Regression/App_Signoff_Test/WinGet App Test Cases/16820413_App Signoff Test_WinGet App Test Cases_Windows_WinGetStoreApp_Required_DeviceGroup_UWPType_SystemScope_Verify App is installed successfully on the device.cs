using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_16820413_App_Signoff_Test_WinGet_App_Test_Cases : WinGetStoreAppRegressionTestBase
    {
        protected override string RegressionTestCaseId => "TC_16820413";

        protected override string TestDisplayName => "App Signoff Test - WinGet App Test Cases - Windows_WinGetStoreApp_Required_DeviceGroup_UWPType_SystemScope_Verify App is installed successfully on the device";

        [Test]
        public async Task TestMethod_16820413_App_Signoff_Test_WinGet_App_Test_CasesAsync()
        {
            await RunTestAsync();
        }
    }
}
