using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_3778608_Test_Set_A_Windows_Regression_Windows_GRS_Required_UserGroup_Appx : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_3778608";

        protected override string RegressionTestName => @"Windows_GRS_Required_UserGroup_Appx";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_GRS_Required_UserGroup_Appx";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[GRS]:[Win10][Required][UserGroup][Appx/AppxBundle]:Verify Install is success after requried deployment and app is evaluated after GRS is expired. All apps including failed apps will attempt to install";

        [Test]
        public async Task TestMethod_3778608_Test_Set_A_Windows_Regression_Windows_GRS_Required_UserGroup_AppxAsync()
        {
            await RunTestAsync();
        }
    }
}
