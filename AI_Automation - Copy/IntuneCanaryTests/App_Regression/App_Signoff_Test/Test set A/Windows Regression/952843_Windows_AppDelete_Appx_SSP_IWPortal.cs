using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952843_Test_Set_A_Windows_Regression_Windows_AppDelete_Appx_SSP_IWPortal : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952843";

        protected override string RegressionTestName => @"Windows_AppDelete_Appx_SSP_IWPortal";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_AppDelete_Appx_SSP_IWPortal";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[AppDelete]:[Appx]:IT Pro can delete app from admin console and app is removed from SSP/IWPortal and not uninstalled from device if already installed";

        [Test]
        public async Task TestMethod_952843_Test_Set_A_Windows_Regression_Windows_AppDelete_Appx_SSP_IWPortalAsync()
        {
            await RunTestAsync();
        }
    }
}
