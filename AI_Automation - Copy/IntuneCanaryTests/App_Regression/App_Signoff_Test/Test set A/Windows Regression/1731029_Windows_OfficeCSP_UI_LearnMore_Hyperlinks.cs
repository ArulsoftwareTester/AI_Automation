using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1731029_Test_Set_A_Windows_Regression_Windows_OfficeCSP_UI_LearnMore_Hyperlinks : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1731029";

        protected override string RegressionTestName => @"Windows_OfficeCSP_UI_LearnMore_Hyperlinks";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_OfficeCSP_UI_LearnMore_Hyperlinks";

        protected override string TestDescription => @"[IntuneSA]:[Ibiza]:[Windows][Win10RS2+]:[OfficeCSP][UI]:Verify that ""Learn more"" hyperlinks on the ""Add app"" and ""Configure app suite"" blades work and are directed to the correct place";

        [Test]
        public async Task TestMethod_1731029_Test_Set_A_Windows_Regression_Windows_OfficeCSP_UI_LearnMore_HyperlinksAsync()
        {
            await RunTestAsync();
        }
    }
}