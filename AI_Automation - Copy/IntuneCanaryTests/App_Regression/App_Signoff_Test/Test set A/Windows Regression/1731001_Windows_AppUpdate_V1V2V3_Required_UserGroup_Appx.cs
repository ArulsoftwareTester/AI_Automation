using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1731001_Test_Set_A_Windows_Regression_Windows_AppUpdate_V1V2V3_Required_UserGroup_Appx : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1731001";

        protected override string RegressionTestName => @"Windows_AppUpdate_V1V2V3_Required_UserGroup_Appx";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_AppUpdate_V1V2V3_Required_UserGroup_Appx";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[AppUpdate][V1>>V2>>V3]:[Appx/AppxBundle]:[Required][UserGroup]:Verify App update for different versions is updated for all devices of user. [V1 installed, V1 updated by V2 : V2 installed, V2 updated by V3: V3 installed]";

        [Test]
        public async Task TestMethod_1731001_Test_Set_A_Windows_Regression_Windows_AppUpdate_V1V2V3_Required_UserGroup_AppxAsync()
        {
            await RunTestAsync();
        }
    }
}
