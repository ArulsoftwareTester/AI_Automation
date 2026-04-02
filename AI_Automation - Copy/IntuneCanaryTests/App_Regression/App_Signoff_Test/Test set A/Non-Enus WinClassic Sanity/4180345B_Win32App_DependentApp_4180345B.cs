using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_4180345B_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"4180345B";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_DependentApp_4180345B";
        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[DependentApp][Dependencies]:[WinClassicApp]: Create an application with 2 different Win32 app types as dependent apps A -> B (EXE), A -> C (MSI)";

        [Test]
        public async Task TestMethod_4180345B_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

