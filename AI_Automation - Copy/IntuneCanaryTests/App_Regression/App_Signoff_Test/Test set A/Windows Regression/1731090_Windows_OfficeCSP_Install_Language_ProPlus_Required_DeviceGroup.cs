using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1731090_Test_Set_A_Windows_Regression_Windows_OfficeCSP_Install_Language_ProPlus_Required_DeviceGroup : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1731090";

        protected override string RegressionTestName => @"Windows_OfficeCSP_Install_Language_ProPlus_Required_DeviceGroup";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_OfficeCSP_Install_Language_ProPlus_Required_DeviceGroup";

        protected override string TestDescription => @"[IntuneSA]:[Ibiza]:[Windows][Win10RS2+]:[OfficeCSP][Install][Language]:[ProPlus]:[Required][DeviceGroup]:Verify ProPlus (multiple apps), Single Language can all install successfully";

        [Test]
        public async Task TestMethod_1731090_Test_Set_A_Windows_Regression_Windows_OfficeCSP_Install_Language_ProPlus_Required_DeviceGroupAsync()
        {
            await RunTestAsync();
        }
    }
}