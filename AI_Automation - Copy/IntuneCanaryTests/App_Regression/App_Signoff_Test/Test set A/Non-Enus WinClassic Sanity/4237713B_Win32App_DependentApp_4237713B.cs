using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_4237713B_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"4237713B";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_DependentApp_4237713B";
        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[DependentApp]:[WinClassicApp]:[Required][DeviceGroup]: A -> B, both applicable to client, deploy A to device group";

        [Test]
        public async Task TestMethod_4237713B_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

