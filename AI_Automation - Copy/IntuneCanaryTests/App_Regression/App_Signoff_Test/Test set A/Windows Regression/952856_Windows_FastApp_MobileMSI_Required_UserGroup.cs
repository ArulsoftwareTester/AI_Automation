using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952856_Test_Set_A_Windows_Regression_Windows_FastApp_MobileMSI_Required_UserGroup : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952856";

        protected override string RegressionTestName => @"Windows_FastApp_MobileMSI_Required_UserGroup";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_FastApp_MobileMSI_Required_UserGroup";

        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[FastApp]:[MobileMSI]:[Required][UserGroup]:Verify app is installed during enrollment within 2 minutes for all Windows devices of user";

        [Test]
        public async Task TestMethod_952856_Test_Set_A_Windows_Regression_Windows_FastApp_MobileMSI_Required_UserGroupAsync()
        {
            await RunTestAsync();
        }
    }
}