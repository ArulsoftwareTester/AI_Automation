using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_4185926A_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"4185926A";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_Delete_4185926A";
        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[DependentApp][UI][Delete]:[WinClassicApp]: Can delete a dependency";

        [Test]
        public async Task TestMethod_4185926A_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

