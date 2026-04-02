using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_952936_Test_Set_A_Android_Regression_Android_WebLink_InstallStatus_Required_DeviceGroup : AndroidRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_952936";

        protected override string RegressionTestName => @"Android_WebLink_InstallStatus_Required_DeviceGroup";

        protected override string TestDisplayName => @"Test set A - Android Regression - Android_WebLink_InstallStatus_Required_DeviceGroup";

        protected override string TestDescription => @"[IntuneSA]:[Android]:[InstallStatus]:[WebApp]:[Required][DeviceGroup]:IT Pro can see installation status summary for app deployed for Device group as required";

        protected override string DataFileName => @"Android_WebLink_AppRegression.json";

        [Test]
        public async Task TestMethod_952936_Test_Set_A_Android_Regression_Android_WebLink_InstallStatus_Required_DeviceGroupAsync()
        {
            await RunTestAsync();
        }
    }
}
