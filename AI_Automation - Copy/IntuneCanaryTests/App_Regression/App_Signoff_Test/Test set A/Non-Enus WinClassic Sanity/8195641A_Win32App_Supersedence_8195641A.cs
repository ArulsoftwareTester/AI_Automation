using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_8195641A_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"8195641A";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_Supersedence_8195641A";
        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[Supersedence][AppUpdate]:[WinClassicApp]:[Required][UserGroup]: A<<-B; A & B Required; A not present, B is present; no action; Status: A Not Installed, B Installed";

        [Test]
        public async Task TestMethod_8195641A_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

