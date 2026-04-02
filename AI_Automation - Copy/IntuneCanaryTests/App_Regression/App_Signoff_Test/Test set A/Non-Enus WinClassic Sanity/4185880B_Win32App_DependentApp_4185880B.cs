using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_4185880B_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"4185880B";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_DependentApp_4185880B";
        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[DependentApp][Dependencies][E2E][Conflict]:[WinClassicApp]:[Available][UserGroup]: A -> B, A is deployed Available, B is deployed Uninstall to same group.  If A is installed, Uninstall should be ignored";

        [Test]
        public async Task TestMethod_4185880B_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

