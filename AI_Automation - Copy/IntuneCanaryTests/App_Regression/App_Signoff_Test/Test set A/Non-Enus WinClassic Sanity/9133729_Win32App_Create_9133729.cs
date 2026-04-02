using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_9133729_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"9133729";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_Create_9133729";
        protected override string TestDescription => @"[IntuneSA]:[Win10]:[E2E][Create]:[WinClassicApp]:[Required]:[AllDeviceGroup]:[IntuneWin]:Verify app is Installed for all Windows devices in group";

        [Test]
        public async Task TestMethod_9133729_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

