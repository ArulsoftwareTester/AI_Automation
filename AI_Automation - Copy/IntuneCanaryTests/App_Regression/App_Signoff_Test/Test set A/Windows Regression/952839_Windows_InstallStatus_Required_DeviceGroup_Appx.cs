using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952839_Test_Set_A_Windows_Regression_Windows_InstallStatus_Required_DeviceGroup_Appx : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952839";

        protected override string RegressionTestName => @"Windows_InstallStatus_Required_DeviceGroup_Appx";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_InstallStatus_Required_DeviceGroup_Appx";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[InstallStatus]:[Required][DeviceGroup]:[Appx]:IT Pro can see installation status summary for app deployed for Device group as required";

        [Test]
        public async Task TestMethod_952839_Test_Set_A_Windows_Regression_Windows_InstallStatus_Required_DeviceGroup_AppxAsync()
        {
            await RunTestAsync();
        }
    }
}