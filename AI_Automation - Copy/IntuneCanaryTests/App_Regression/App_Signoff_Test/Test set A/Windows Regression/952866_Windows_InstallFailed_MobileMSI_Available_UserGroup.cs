using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952866_Test_Set_A_Windows_Regression_Windows_InstallFailed_MobileMSI_Available_UserGroup : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952866";

        protected override string RegressionTestName => @"Windows_InstallFailed_MobileMSI_Available_UserGroup";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_InstallFailed_MobileMSI_Available_UserGroup";

        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[InstallStatus][InstallFailed]:[MobileMSI]:[Available][UserGroup]:IT Pro can see installation status summary with error code for app deployed for User group as Available as failed with error";

        [Test]
        public async Task TestMethod_952866_Test_Set_A_Windows_Regression_Windows_InstallFailed_MobileMSI_Available_UserGroupAsync()
        {
            await RunTestAsync();
        }
    }
}
