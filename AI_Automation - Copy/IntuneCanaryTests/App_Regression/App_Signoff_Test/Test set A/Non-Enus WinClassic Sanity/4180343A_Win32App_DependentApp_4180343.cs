using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_4180343A_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"4180343A";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_DependentApp_4180343";
        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[DependentApp]:[WinClassicApp]:[Required][UserGroup]: Dependency Loop";

        [Test]
        public async Task TestMethod_4180343A_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

