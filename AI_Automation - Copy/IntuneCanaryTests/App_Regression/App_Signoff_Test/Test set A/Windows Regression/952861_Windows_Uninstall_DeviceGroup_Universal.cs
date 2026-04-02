using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952861_Test_Set_A_Windows_Regression_Windows_Uninstall_DeviceGroup_Universal : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952861";

        protected override string RegressionTestName => @"Windows_Uninstall_DeviceGroup_Universal";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Uninstall_DeviceGroup_Universal";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[Uninstall]:[Win10]:[DeviceGroup]:[Universal]:Verify Uninstall is success for all devices in a group";

        [Test]
        public async Task TestMethod_952861_Test_Set_A_Windows_Regression_Windows_Uninstall_DeviceGroup_UniversalAsync()
        {
            await RunTestAsync();
        }
    }
}