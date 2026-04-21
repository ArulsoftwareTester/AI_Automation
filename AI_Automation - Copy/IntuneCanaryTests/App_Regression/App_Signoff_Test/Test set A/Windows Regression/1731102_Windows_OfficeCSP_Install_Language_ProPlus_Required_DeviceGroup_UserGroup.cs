using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_1731102_Test_Set_A_Windows_Regression_Windows_OfficeCSP_Install_Language_ProPlus_Required_DeviceGroup_UserGroup : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_1731102";

        protected override string RegressionTestName => @"Windows_OfficeCSP_Install_Language_ProPlus_Required_DeviceGroup_UserGroup";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_OfficeCSP_Install_Language_ProPlus_Required_DeviceGroup_UserGroup";

        protected override string TestDescription => @"[IntuneSA]:[Ibiza]:[Windows][Win10RS2+]:[OfficeCSP][Install][Language]:[ProPlus]:[Required][DeviceGroup][UserGroup]:Verify ProPlus (multiple apps) Multiple Languages can all install successfully";

        [Test]
        public async Task TestMethod_1731102_Test_Set_A_Windows_Regression_Windows_OfficeCSP_Install_Language_ProPlus_Required_DeviceGroup_UserGroupAsync()
        {
            await RunTestAsync();
        }
    }
}
