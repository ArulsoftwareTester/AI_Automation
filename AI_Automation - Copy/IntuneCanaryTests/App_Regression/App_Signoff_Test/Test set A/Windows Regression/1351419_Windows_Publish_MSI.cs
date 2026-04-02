using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1351419_Test_Set_A_Windows_Regression_Windows_Publish_MSI : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1351419";

        protected override string RegressionTestName => @"Windows_Publish_MSI";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Publish_MSI";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[Publish][MSI]:Verify admin can publish an MSI";

        [Test]
        public async Task TestMethod_1351419_Test_Set_A_Windows_Regression_Windows_Publish_MSIAsync()
        {
            await RunTestAsync();
        }
    }
}