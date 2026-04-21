using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1731005_Test_Set_A_Windows_Regression_Windows_AppUpdate_V1V2_Available_UserGroup_Appx : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1731005";

        protected override string RegressionTestName => @"Windows_AppUpdate_V1V2_Available_UserGroup_Appx";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_AppUpdate_V1V2_Available_UserGroup_Appx";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[AppUpdate][V1 >>V2]:[Appx/AppxBundle]:[Available][UserGroup]:App V1 is deployed as available and installed on the device. Now V1 is updated to V2. Ensure that V2 is installed on the device.";

        [Test]
        public async Task TestMethod_1731005_Test_Set_A_Windows_Regression_Windows_AppUpdate_V1V2_Available_UserGroup_AppxAsync()
        {
            await RunTestAsync();
        }
    }
}
