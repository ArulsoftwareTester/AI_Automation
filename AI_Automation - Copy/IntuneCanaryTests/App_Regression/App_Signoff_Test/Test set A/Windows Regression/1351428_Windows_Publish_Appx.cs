using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1351428_Test_Set_A_Windows_Regression_Windows_Publish_Appx : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1351428";

        protected override string RegressionTestName => @"Windows_Publish_Appx";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Publish_Appx";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[Publish][Appx]:Verify admin can publish an appx";

        [Test]
        public async Task TestMethod_1351428_Test_Set_A_Windows_Regression_Windows_Publish_AppxAsync()
        {
            await RunTestAsync();
        }
    }
}