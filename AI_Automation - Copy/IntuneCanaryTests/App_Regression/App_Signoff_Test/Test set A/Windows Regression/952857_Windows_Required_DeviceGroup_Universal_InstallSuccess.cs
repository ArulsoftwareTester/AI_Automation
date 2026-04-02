using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952857_Test_Set_A_Windows_Regression_Windows_Required_DeviceGroup_Universal_InstallSuccess : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952857";

        protected override string RegressionTestName => @"Windows_Required_DeviceGroup_Universal_InstallSuccess";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Required_DeviceGroup_Universal_InstallSuccess";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[Required]:[Win10][DeviceGroup][Universal]:Verify Install is success for all devices in group";

        [Test]
        public async Task TestMethod_952857_Test_Set_A_Windows_Regression_Windows_Required_DeviceGroup_Universal_InstallSuccessAsync()
        {
            await RunTestAsync();
        }
    }
}