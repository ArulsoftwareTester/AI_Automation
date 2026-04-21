using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952829_Test_Set_A_Windows_Regression_Windows_FastApp_Required_UserGroup_Appx : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952829";

        protected override string RegressionTestName => @"Windows_FastApp_Required_UserGroup_Appx";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_FastApp_Required_UserGroup_Appx";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[FastApp]:[Required][UserGroup][Appx/AppxBundle]:Verify app is installed during enrollment within 2 minutes for all Windows devices of user";

        [Test]
        public async Task TestMethod_952829_Test_Set_A_Windows_Regression_Windows_FastApp_Required_UserGroup_AppxAsync()
        {
            await RunTestAsync();
        }
    }
}
