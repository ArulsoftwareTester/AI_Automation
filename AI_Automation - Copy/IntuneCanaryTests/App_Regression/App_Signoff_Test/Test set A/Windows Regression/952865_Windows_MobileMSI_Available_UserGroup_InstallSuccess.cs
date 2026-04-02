using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952865_Test_Set_A_Windows_Regression_Windows_MobileMSI_Available_UserGroup_InstallSuccess : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952865";

        protected override string RegressionTestName => @"Windows_MobileMSI_Available_UserGroup_InstallSuccess";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_MobileMSI_Available_UserGroup_InstallSuccess";

        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[MobileMSI]:[Available][UserGroup]:Verify Install is success for all Windows devices of user";

        [Test]
        public async Task TestMethod_952865_Test_Set_A_Windows_Regression_Windows_MobileMSI_Available_UserGroup_InstallSuccessAsync()
        {
            await RunTestAsync();
        }
    }
}