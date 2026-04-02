using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952854_Test_Set_A_Windows_Regression_Windows_Publish_MobileMSI_UserGroup : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952854";

        protected override string RegressionTestName => @"Windows_Publish_MobileMSI_UserGroup";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Publish_MobileMSI_UserGroup";

        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[Publish]:[MobileMSI]:[UserGroup]:Verify admin can create a MobileMSI application and deploy it";

        [Test]
        public async Task TestMethod_952854_Test_Set_A_Windows_Regression_Windows_Publish_MobileMSI_UserGroupAsync()
        {
            await RunTestAsync();
        }
    }
}