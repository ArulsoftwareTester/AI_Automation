using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1351430_Test_Set_A_Windows_Regression_Windows_Publish_AppxBundle : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1351430";

        protected override string RegressionTestName => @"Windows_Publish_AppxBundle";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Publish_AppxBundle";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[Publish][Appx Bundle]:Verify admin can publish an appx bundle";

        [Test]
        public async Task TestMethod_1351430_Test_Set_A_Windows_Regression_Windows_Publish_AppxBundleAsync()
        {
            await RunTestAsync();
        }
    }
}