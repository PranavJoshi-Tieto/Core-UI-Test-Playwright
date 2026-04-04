using NUnit.Framework;
using PlaywrightFramework.Fixtures;
using PlaywrightFramework.Pages;
using PlaywrightFramework.src.Pages.ForDistribution;
using PlaywrightFramework.Utils;
using System.Security.Cryptography.X509Certificates;

namespace PlaywrightFramework.Tests
{
    [TestFixture]
    public class ForDistributionTest : BaseTest
    {
        [SetUp]
        public async Task LoginFirst()
        {
            await LoginHelper.LoginToApplicationAsync(Page, TestSettings.BaseUrl_NOR,UserRole.Registrar);
        }

        [Test]
        [Category("ForDistribution")]
        public async Task VerifyForDistributionDesktopIsPresent()
        {
            var forDistributionPOM = new ForDistributionTestPOM(Page, BaseUrl);

            await forDistributionPOM.ClickOnForDistributionDesktopDesktop();
          
        }
    }
}