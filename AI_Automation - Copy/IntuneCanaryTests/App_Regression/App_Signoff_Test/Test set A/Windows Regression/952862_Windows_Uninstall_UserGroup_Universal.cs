using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952862_Test_Set_A_Windows_Regression_Windows_Uninstall_UserGroup_Universal : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952862";

        protected override string RegressionTestName => @"Windows_Uninstall_UserGroup_Universal";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Uninstall_UserGroup_Universal";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[Uninstall]:[Win10]:[UserGroup]:[Universal]:Verify Uninstall is success for all devices of a user";

        [Test]
        public async Task TestMethod_952862_Test_Set_A_Windows_Regression_Windows_Uninstall_UserGroup_UniversalAsync()
        {
            await RunTestAsync();
        }
    }
}