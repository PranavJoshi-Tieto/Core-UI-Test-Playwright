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
        // ForDistribution Document Tab 
        [Test]
        [Category("ForDistribution")]
        public async Task Doucument_SignOffDocumentAndAddSignOffRemark()
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
        public async Task Doucument_VerifyUserIsAbleToDistributeDocument()
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
            await forDistributionPOM.AddMandatoryDetailsInDistribute("");

            // Get document title After distribution
            var documentTitleAfterDistribution = await forDistributionPOM.GetDocumentTitle();
            TestContext.WriteLine($"Document selected for distribution: {documentTitleAfterDistribution}");

            // Verify documentTitleBeforeDistribution and documentTitleAfterDistribution are not same
            Assert.AreNotEqual(documentTitleBeforeDistribution, documentTitleAfterDistribution, $"Document title before distribution '{documentTitleBeforeDistribution}' should not be the same as document title after distribution '{documentTitleAfterDistribution}'.");
        }

        [Test]
        [Category("ForDistribution")]
        public async Task Doucument_VerifyuserIsAbleToBookMarkDocument()
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
        public async Task Doucument_VerifyUserIsAbleToShareDocument()
        {
            var forDistributionPOM = new ForDistributionTestPOM(Page, BaseUrl);
            await forDistributionPOM.ClickOnForDistributionDesktopDesktop();
            //Click on Documents Tab
            await forDistributionPOM.ClickOnTab("Documents");
            // Select Document from the list
            await forDistributionPOM.SelectItemFromList();
            // Click on Distribute Button
            await forDistributionPOM.ClickOnTabButton("Share");
            // Add mandatory details and click on Share button in popup
            await forDistributionPOM.AddMandatoryDetailsInShare(ConstantValues.ResponsibleCH1);
        }

        // ForDistribution Case Tab 
        [Test]
        [Category("ForDistribution")]
        public async Task Case_VerifyUserIsAbleToDistributeCase()
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
            await forDistributionPOM.AddMandatoryDetailsInDistribute("");
        }

        [Test]
        [Category("ForDistribution")]
        public async Task Case_VerifyUserIsAbleToBookMarkCase()
        {
            var forDistributionPOM = new ForDistributionTestPOM(Page, BaseUrl);
            await forDistributionPOM.ClickOnForDistributionDesktopDesktop();
            //Click on Documents Tab
            await forDistributionPOM.ClickOnTab("Cases");
            // Select Case from the list
            await forDistributionPOM.SelectItemFromList();
            // Click on Distribute Button
            await forDistributionPOM.ClickOnTabButton("Bookmark");
        }

        [Test]
        [Category("ForDistribution")]
        public async Task Case_VerifyUserIsAbleToShareCase()
        {
            var forDistributionPOM = new ForDistributionTestPOM(Page, BaseUrl);
            await forDistributionPOM.ClickOnForDistributionDesktopDesktop();
            //Click on Documents Tab
            await forDistributionPOM.ClickOnTab("Cases");
            // Select Document from the list
            await forDistributionPOM.SelectItemFromList();
            // Click on Distribute Button
            await forDistributionPOM.ClickOnTabButton("Share");
            // Add mandatory details and click on Share button in popup
            await forDistributionPOM.AddMandatoryDetailsInShare(ConstantValues.ResponsibleCH1);
        }

        // ForDistribution Task Tab 

        [Test]
        [Category("ForDistribution")]
        public async Task Tasks_VerifyUserIsAbleToDistributeTask()
        {
            var forDistributionPOM = new ForDistributionTestPOM(Page, BaseUrl);
            await forDistributionPOM.ClickOnForDistributionDesktopDesktop();
            //Click on Documents Tab
            await forDistributionPOM.ClickOnTab("Tasks");
            // Select Case from the list
            await forDistributionPOM.SelectItemFromList();
            // Click on Distribute Button
            await forDistributionPOM.ClickOnTabButton("Distribute");
            // Add mandatory details and click on Distribute button in popup
            await forDistributionPOM.AddMandatoryDetailsInDistribute("Tasks");
        }

        [Test]
        [Category("ForDistribution")]
        public async Task Tasks_VerifyUserIsAbleToBookMarkTask()
        {
            var forDistributionPOM = new ForDistributionTestPOM(Page, BaseUrl);
            await forDistributionPOM.ClickOnForDistributionDesktopDesktop();
            //Click on Documents Tab
            await forDistributionPOM.ClickOnTab("Tasks");
            // Select Case from the list
            await forDistributionPOM.SelectItemFromList();
            // Click on Distribute Button
            await forDistributionPOM.ClickOnTabButton("Bookmark");
        }

        [Test]
        [Category("ForDistribution")]
        public async Task Tasks_VerifyUserIsAbleToShareTask()
        {
            var forDistributionPOM = new ForDistributionTestPOM(Page, BaseUrl);
            await forDistributionPOM.ClickOnForDistributionDesktopDesktop();
            //Click on Documents Tab
            await forDistributionPOM.ClickOnTab("Tasks");
            // Select Document from the list
            await forDistributionPOM.SelectItemFromList();
            // Click on Distribute Button
            await forDistributionPOM.ClickOnTabButton("Share");
            // Add mandatory details and click on Share button in popup
            await forDistributionPOM.AddMandatoryDetailsInShare(ConstantValues.ResponsibleCH1);
        }
    }
}