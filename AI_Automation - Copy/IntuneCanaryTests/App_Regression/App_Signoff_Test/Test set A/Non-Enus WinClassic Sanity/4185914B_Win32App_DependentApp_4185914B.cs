using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_4185914B_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"4185914B";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_DependentApp_4185914B";
        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[DependentApp][UI]:[WinClassicApp]: Can add a dependency";

        [Test]
        public async Task TestMethod_4185914B_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

