using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1731030_Test_Set_A_Windows_Regression_Windows_OfficeCSP_UI_AddApp_Blade : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1731030";

        protected override string RegressionTestName => @"Windows_OfficeCSP_UI_AddApp_Blade";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_OfficeCSP_UI_AddApp_Blade";

        protected override string TestDescription => @"[IntuneSA]:[Ibiza]:[Windows][Win10RS2+]:[OfficeCSP][UI]:Verify that the text of the options on the ""Add app"" blade changes when the respective blade is opened filled out and saved";

        [Test]
        public async Task TestMethod_1731030_Test_Set_A_Windows_Regression_Windows_OfficeCSP_UI_AddApp_BladeAsync()
        {
            await RunTestAsync();
        }
    }
}
