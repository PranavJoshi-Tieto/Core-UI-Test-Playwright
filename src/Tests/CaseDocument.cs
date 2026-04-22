using NUnit.Framework;
using PlaywrightFramework.Fixtures;
using PlaywrightFramework.Pages;
using PlaywrightFramework.src.Pages.Document;
using PlaywrightFramework.Utils;

namespace PlaywrightFramework.Tests
{
    [TestFixture]
    public class CaseDocument : BaseTest
    {
        public async Task LoginFirst()
        {
            await LoginHelper.LoginToApplicationAsync(Page, TestSettings.BaseUrl_NOR);
        }
        string documentTitle = "AI Generated Document " + System.Guid.NewGuid().ToString().Substring(0, 2);

        [Test]
        [Category("Document")]
        public async Task CreateCaseDocumentFromMainMenu()
        {
            // Login as a CaseHandler
            await LoginHelper.LoginToApplicationAsync(Page, TestSettings.BaseUrl_NOR, UserRole.CaseHandler);
            var masterpagepom = new MasterPagePOM(Page, BaseUrl);
            // Click on Main Menu button
            await masterpagepom.ClickMenuButton();
            TestContext.WriteLine("Clicked Main Menu button");

            // Click on Document button
            var caseDocumentPOM = new CaseDocumentPOM(Page, BaseUrl);
            await caseDocumentPOM.ClickDocumentButton();
            TestContext.WriteLine("Clicked Document button under Create new section");
           
            // Enter Document Title
            string documentTitle = "AI Generated Document " + System.Guid.NewGuid().ToString().Substring(0, 2);
            await caseDocumentPOM.SelectDocumentTemplate();
            TestContext.WriteLine("Clicked OK button for Document creation");
           

            await caseDocumentPOM.EnterDocumentDetails(documentTitle, "Caseworker");
            await caseDocumentPOM.SelectOrDragFilesHere(ConstantValues.TestWordFile , "Caseworker");
            await caseDocumentPOM.ClickOnFishishButton();
            TestContext.WriteLine($"Entered Document Details: {documentTitle}");
           
            // Verify case title matches
            string actualTitle = await Page.Locator("//h1[@id='PlaceHolderMain_MainView_DetailTitle_generic']").TextContentAsync();
            TestContext.WriteLine($"Actual Document Title on Page: '{actualTitle?.Trim()}'");
            Assert.AreEqual(documentTitle, actualTitle?.Trim(), $"Document title mismatch. Expected: '{documentTitle}', Actual: '{actualTitle?.Trim()}'");
            TestContext.WriteLine($"Verified case title '{documentTitle}' matches on the page");
        }

        [Test]
        [Category("Document")]
        public async Task CreateDocumentAsRegistrarWithFile()
        {
            // Login as a Registrar
            await LoginHelper.LoginToApplicationAsync(Page, TestSettings.BaseUrl_NOR, UserRole.Registrar);

            var masterpagepom = new MasterPagePOM(Page, BaseUrl);
            // Click on Main Menu button
            await masterpagepom.ClickMenuButton();
            TestContext.WriteLine("Clicked Main Menu button");

            var caseDocumentPOM = new CaseDocumentPOM(Page, BaseUrl);
            // Click on case Document button
            await caseDocumentPOM.ClickOnCaseDocumentButtonRegistrar();
            TestContext.WriteLine("Clicked Case Document button under Create new section");

            // Enter Document Title              
            await caseDocumentPOM.EnterDocumentDetails(documentTitle ,"Registrar");
            await caseDocumentPOM.SelectOrDragFilesHere(ConstantValues.TestWordFile, "Registrar");
            await caseDocumentPOM.ClickOnFishishButtonRegistrar();
            TestContext.WriteLine($"Entered Document Details: {documentTitle}");

            // Verify case title matches
            string actualTitle = await Page.Locator("/h1[@id='PlaceHolderMain_MainView_DetailTitle_generic']").TextContentAsync();
            TestContext.WriteLine($"Actual Document Title on Page: '{actualTitle?.Trim()}'");
            Assert.AreEqual(documentTitle, actualTitle?.Trim(), $"Document title mismatch. Expected: '{documentTitle}', Actual: '{actualTitle?.Trim()}'");
            TestContext.WriteLine($"Verified case title '{documentTitle}' matches on the page");






        }

    }
}
