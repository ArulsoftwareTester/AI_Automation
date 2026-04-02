using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_4180340A_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"4180340A";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_DependentApp_4180340A";
        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[DependentApp]:[WinClassicApp]:[Available][UserGroup]: A -> B, both applicable to client, deploy A to user";

        [Test]
        public async Task TestMethod_4180340A_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

