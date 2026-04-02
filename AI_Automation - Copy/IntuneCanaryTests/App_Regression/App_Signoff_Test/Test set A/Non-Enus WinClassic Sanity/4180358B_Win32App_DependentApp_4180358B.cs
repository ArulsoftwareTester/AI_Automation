using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_4180358B_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"4180358B";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_DependentApp_4180358B";
        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[DependentApp][Dependencies][E2E]:[WinClassicApp]:[Required][UserGroup]:A->B, B->C. C not applicable. When A is deployed, A B not installed (req-not-met). Update C to v2 now applicable. At GRS, Cv2, Bv1 and Av1 installed";

        [Test]
        public async Task TestMethod_4180358B_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

