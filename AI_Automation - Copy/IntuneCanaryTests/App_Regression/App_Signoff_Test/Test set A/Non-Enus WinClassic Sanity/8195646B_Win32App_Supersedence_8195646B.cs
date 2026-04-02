using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_8195646B_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"8195646B";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_Supersedence_8195646B";
        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[Supersedence][AppReplace]:[WinClassicApp]:[Required][UserGroup]: A<<+B; A & B Required; A not present; Install B; Status: A Not Installed, B Installed";

        [Test]
        public async Task TestMethod_8195646B_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

