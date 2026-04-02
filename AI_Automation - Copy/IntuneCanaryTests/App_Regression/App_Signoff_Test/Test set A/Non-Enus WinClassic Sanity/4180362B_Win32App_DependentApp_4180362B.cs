using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_4180362B_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"4180362B";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_DependentApp_4180362B";
        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[DependentApp][Dependencies][E2E]:[WinClassicApp]:[Required][UserGroup]: A -> B. When A is deployed B should be installed first. If B fails, A should not be attempted";

        [Test]
        public async Task TestMethod_4180362B_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

