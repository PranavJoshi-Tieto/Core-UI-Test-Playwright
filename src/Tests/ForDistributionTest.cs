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
            await LoginHelper.LoginToApplicationAsync(Page, TestSettings.BaseUrl_NOR, UserRole.Registrar);
        }

        [Test]
        [Category("ForDistribution")]
        public async Task VerifyForDistributionDesktopIsPresent()
        {
            var forDistributionPOM = new ForDistributionTestPOM(Page, BaseUrl);

            await forDistributionPOM.ClickOnForDistributionDesktopDesktop();

        }

        [Test]
        [Category("ForDistribution")]
        public async Task SignOffDocumentAndAddSignOffRemark()
        {
            var forDistributionPOM = new ForDistributionTestPOM(Page, BaseUrl);
            await forDistributionPOM.ClickOnForDistributionDesktopDesktop();
            //Click on Documents Tab
            await forDistributionPOM.ClickOnTab("Documents");

            //Click on Sign off button
            await forDistributionPOM.ClickOnSignOffButtonAndAddNote("AP");

        }
        [Test]
        [Category("ForDistribution")]
        public async Task VerifyUserIsAbleToDistributeDocument()
        {
            var forDistributionPOM = new ForDistributionTestPOM(Page, BaseUrl);
            await forDistributionPOM.ClickOnForDistributionDesktopDesktop();

            //Click on Documents Tab
            await forDistributionPOM.ClickOnTab("Documents");

            // Select Document from the list
            await forDistributionPOM.SelectItemFromList();

            // Get document title BEFORE distribution
            var documentTitleBeforeDistribution = await forDistributionPOM.GetDocumentTitle();
            TestContext.WriteLine($"Document selected for distribution: {documentTitleBeforeDistribution}");

            // Click on Distribute Button
            await forDistributionPOM.ClickOnTabButton("Distribute");

            // Add mandatory details and click on Distribute button in popup
            await forDistributionPOM.AddMandatoryDetailsInDistribute();
          
            // Get document title After distribution
            var documentTitleAfterDistribution = await forDistributionPOM.GetDocumentTitle();
            TestContext.WriteLine($"Document selected for distribution: {documentTitleAfterDistribution}");

            // Verify documentTitleBeforeDistribution and documentTitleAfterDistribution are not same
            Assert.AreNotEqual(documentTitleBeforeDistribution, documentTitleAfterDistribution, $"Document title before distribution '{documentTitleBeforeDistribution}' should not be the same as document title after distribution '{documentTitleAfterDistribution}'.");
        }

        [Test]
        [Category("ForDistribution")]
        public async Task VerifyuserIsAbleToBookMarkDocument()
        {
            var forDistributionPOM = new ForDistributionTestPOM(Page, BaseUrl);
            await forDistributionPOM.ClickOnForDistributionDesktopDesktop();

            //Click on Documents Tab
            await forDistributionPOM.ClickOnTab("Documents");

            // Select Document from the list
            await forDistributionPOM.SelectItemFromList();

            // Click on Distribute Button
            await forDistributionPOM.ClickOnTabButton("Bookmark");
                             
        }

        [Test]
        [Category("ForDistribution")]
        public async Task VerifyUserIsAbleToDistributeCase()
        {
            var forDistributionPOM = new ForDistributionTestPOM(Page, BaseUrl);
            await forDistributionPOM.ClickOnForDistributionDesktopDesktop();
            //Click on Documents Tab
            await forDistributionPOM.ClickOnTab("Cases");
            // Select Case from the list
            await forDistributionPOM.SelectItemFromList();
            // Click on Distribute Button
            await forDistributionPOM.ClickOnTabButton("Distribute");
            // Add mandatory details and click on Distribute button in popup
            await forDistributionPOM.AddMandatoryDetailsInDistribute();
        }
    }
}