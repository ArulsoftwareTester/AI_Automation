using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_8982222_Test_Set_A_Windows_Regression_Windows_Verify_Edge_Dev_can_be_uninstalled : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_8982222";

        protected override string RegressionTestName => @"Windows_Verify_Edge_Dev_can_be_uninstalled";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Verify_Edge_Dev_can_be_uninstalled";

        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10RS3+]:[Publish][E2E]:[Uninstall]:Verify Edge Dev can be uninstalled";

        [Test]
        public async Task TestMethod_8982222_Test_Set_A_Windows_Regression_Windows_Verify_Edge_Dev_can_be_uninstalledAsync()
        {
            await RunTestAsync();
        }
    }
}