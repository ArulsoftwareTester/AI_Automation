using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_3045446_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"3045446";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_Create_Required_AllUsers_InstallSuccess";
        protected override string TestDescription => @"[IntuneSA]:[Win10]:[E2E][Create]:[WinClassicApp]:[Required]:[AllUserGroup]:[IntuneWin]:Verify Install is success for all Windows Users in group";

        [Test]
        public async Task TestMethod_3045446_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

