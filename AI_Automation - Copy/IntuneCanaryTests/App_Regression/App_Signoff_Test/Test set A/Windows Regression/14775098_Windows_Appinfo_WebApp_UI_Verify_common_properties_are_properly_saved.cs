using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_14775098_Test_Set_A_Windows_Regression_Windows_Appinfo_WebApp_UI_Verify_common_properties_are_properly_saved : WindowsRegressionTestSetABase
    {
        protected override string RegressionTestCaseId => @"TC_14775098";

        protected override string RegressionTestName => @"Windows_Appinfo_WebApp_UI_Verify_common_properties_are_properly_saved";

        protected override string TestDisplayName => @"Test set A - Windows Regression - Windows_Appinfo_WebApp_UI_Verify_common_properties_are_properly_saved";

        protected override string TestDescription => @"[IntuneSA]:[Windows]:[Appinfo]:[WebApp][UI]:Verify common properties (Description/Publisher/Category/etc) are properly saved";

        [Test]
        public async Task TestMethod_14775098_Test_Set_A_Windows_Regression_Windows_Appinfo_WebApp_UI_Verify_common_properties_are_properly_savedAsync()
        {
            await RunTestAsync();
        }
    }
}