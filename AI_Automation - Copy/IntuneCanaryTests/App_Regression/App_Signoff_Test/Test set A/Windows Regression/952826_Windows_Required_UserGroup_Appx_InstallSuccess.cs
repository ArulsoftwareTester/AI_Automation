using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952826_Test_Set_A_Windows_Regression_Windows_Required_UserGroup_Appx_InstallSuccess : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952826";

        protected override string RegressionTestName => @"Windows_Required_UserGroup_Appx_InstallSuccess";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Required_UserGroup_Appx_InstallSuccess";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[Required]:[UserGroup][Appx]:Verify Install is success for all Windows devices of a user.";

        [Test]
        public async Task TestMethod_952826_Test_Set_A_Windows_Regression_Windows_Required_UserGroup_Appx_InstallSuccessAsync()
        {
            await RunTestAsync();
        }
    }
}