using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_4237714C_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"4237714C";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_DependentApp_4237714C";
        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[DependentApp]:[WinClassicApp]:[Available][UserGroup]:A -> B. A is deployed, B is installed then A. Then dependency is changed to A -> C. At global re-eval, C is installed";

        [Test]
        public async Task TestMethod_4237714C_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

