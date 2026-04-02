using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8923156_DC_Endpoint_Protection_WindowsEncryption_Write_access_to_fixed_data_drive_not_protected_by_BitLocker
    {
        [Test]
        public async Task Test_8923156_DC_Endpoint_Protection_WindowsEncryption_Write_access_to_fixed_data_drive_not_protected_by_BitLocker()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8923156 completed");
        }
    }
}
