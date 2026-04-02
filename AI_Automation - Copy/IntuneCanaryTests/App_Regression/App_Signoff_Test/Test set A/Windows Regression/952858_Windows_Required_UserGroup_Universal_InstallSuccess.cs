using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952858_Test_Set_A_Windows_Regression_Windows_Required_UserGroup_Universal_InstallSuccess : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952858";

        protected override string RegressionTestName => @"Windows_Required_UserGroup_Universal_InstallSuccess";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Required_UserGroup_Universal_InstallSuccess";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[Required]:[Win10][UserGroup][Universal]:Verify Install is success for all devices of a user";

        [Test]
        public async Task TestMethod_952858_Test_Set_A_Windows_Regression_Windows_Required_UserGroup_Universal_InstallSuccessAsync()
        {
            await RunTestAsync();
        }
    }
}