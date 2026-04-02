using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_8195617B_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"8195617B";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_Delete_8195617B";
        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[Supersedence][UI][Delete]:[WinClassicApp]: Can delete a superseded app after removing related relationships";

        [Test]
        public async Task TestMethod_8195617B_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

