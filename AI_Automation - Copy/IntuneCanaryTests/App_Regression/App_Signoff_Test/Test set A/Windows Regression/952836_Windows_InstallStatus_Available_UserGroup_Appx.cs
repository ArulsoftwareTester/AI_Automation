using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952836_Test_Set_A_Windows_Regression_Windows_InstallStatus_Available_UserGroup_Appx : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952836";

        protected override string RegressionTestName => @"Windows_InstallStatus_Available_UserGroup_Appx";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_InstallStatus_Available_UserGroup_Appx";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[InstallStatus]:[Available][UserGroup]:[Appx]:IT Pro can see installation status summary for app deployed for User group as available";

        [Test]
        public async Task TestMethod_952836_Test_Set_A_Windows_Regression_Windows_InstallStatus_Available_UserGroup_AppxAsync()
        {
            await RunTestAsync();
        }
    }
}