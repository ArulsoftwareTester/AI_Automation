using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_16135393_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"16135393";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_Create_16135393";
        protected override string TestDescription => @"[IntuneSA]:[Win10]:[E2E][Create]:[WinClassicApp]:[Required]:[DeviceGroup]:[IntuneWin]: Verify Install is success in the next scheduled check-in";

        [Test]
        public async Task TestMethod_16135393_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

