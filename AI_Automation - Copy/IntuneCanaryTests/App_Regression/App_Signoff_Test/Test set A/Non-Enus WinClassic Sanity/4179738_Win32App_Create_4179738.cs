using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_4179738_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"4179738";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_Create_4179738";
        protected override string TestDescription => @"[IntuneSA]:[Win10]:[E2E][Create]:[WinClassicApp]:[Available]:[AllUserGroup]:[IntuneWin]:Verify Install is success for all Windows Users in group";

        [Test]
        public async Task TestMethod_4179738_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

