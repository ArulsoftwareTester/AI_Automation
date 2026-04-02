using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_4180347_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"4180347";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_DependentApp_4180347";
        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[DependentApp][Dependencies][E2E][AutoInstall]:[WinClassicApp]:[Required][UserGroup]: A->B (AutoInst no), A->C (AutoInst yes) and A->D (AutoInst no) A->E (AutoInst yes). B,D already installed. A deployed,  C, E then A installed";

        [Test]
        public async Task TestMethod_4180347_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

