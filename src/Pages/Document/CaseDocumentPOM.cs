using Microsoft.Playwright;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework.Interfaces;
using PlaywrightFramework.Pages;
using System;

namespace PlaywrightFramework.src.Pages.Document
{
    public class CaseDocumentPOM : BasePage
    {
        public CaseDocumentPOM(IPage page, string baseUrl) : base(page, baseUrl) { }
        

            // Locators
        private const string DocumentIframe = "iframe[src*='/locator/DMS/Dialog/TemplateFilterDialog']";
        private const string SelectDocumentType = "//a[contains(text(),'Internal memo without follow-up, Case document')]";
        private const string OKButton = "//span[contains(text(),'OK')]";
        private const string FileUploadControl = "#PlaceHolderMain_MainView_DocumentMultiFileUploadControl_dragdropContainer input[type='file']";
        // create a same like Fileuploadcontrol with with =  //*[starts-with(@class, 'si-dropzone-container')]
        private const string DragAndDropFileUnregisteredDocument = "//*[starts-with(@class, 'si-dropzone-container')]//input[@type='file']";
        private const string CaseDocumentButtonMainMenu = "//div[text()='Case document']";


        // Actions

        public async Task ClickDocumentButton()
        {
            await Task.Delay(500); // Human-like delay
            var documentButton = Page.Locator("//div[text()='Select template']");
            await documentButton.ClickAsync();
            await Task.Delay(1000);
        }

        public async Task ClickOnCaseDocumentButtonRegistrar()
        {
            await Task.Delay(500); // Human-like delay
            var documentButton = Page.Locator(CaseDocumentButtonMainMenu);
            await documentButton.ClickAsync();
            await Task.Delay(1000);
        }

        public async Task SelectDocumentTemplate()
        {
            var frame = Page.FrameLocator(DocumentIframe).First;
            await Task.Delay(500);

            //Click on Common radio button span has text "Common"
            var commonRadioButton = frame.GetByRole(AriaRole.Radio, new() { Name = "Common" });
            await commonRadioButton.ClickAsync();
            await Task.Delay(800);

            // I want to click on index 0 SelectDocumentType element
            var detailsText = frame.Locator(SelectDocumentType).Nth(0);
            //var detailsText = frame.Locator(SelectDocumentType);
            await detailsText.ClickAsync();
            await Task.Delay(500);

            var okButton = frame.Locator(OKButton);
            await okButton.ClickAsync();
            await Task.Delay(1000);
        }

        public async Task EnterDocumentDetails(string documentTitle)
        {
            var frame = Page.FrameLocator(DocumentIframe).First;
            await Task.Delay(500);

            var titleField = frame.GetByRole(AriaRole.Textbox, new() { Name = "Title" });
            await titleField.ClickAsync();
            await Task.Delay(500);
            await titleField.FillAsync(documentTitle);
            await Task.Delay(500);
            await titleField.PressAsync("Enter");
            await Task.Delay(1000);

            // Enter Case title in Details section
            var caseField = frame.GetByRole(AriaRole.Textbox, new() { Name = "Case" });
            await caseField.ClickAsync();
            await Task.Delay(500);
            await caseField.FillAsync("%");
            // press Enter key from Keyboard to select a value from list
            await caseField.PressAsync("Enter");
            await Task.Delay(500);
            // Arrow down key to select first value from list
            await caseField.PressAsync("ArrowDown");
            await Task.Delay(500);
            await caseField.PressAsync("ArrowDown");
            await caseField.PressAsync("Enter");
            await Task.Delay(1000);

            // Click on Files tab 
            var filesTab = frame.GetByRole(AriaRole.Tab, new() { Name = "Files" });
            await filesTab.ClickAsync();
            await Task.Delay(500);         

        }

        public async Task ClickOnFishishButton()
        {
            var frame = Page.FrameLocator(DocumentIframe).First;
            await Task.Delay(500);
            // Click on Finish button
            var finishButton = frame.GetByRole(AriaRole.Button, new() { Name = "Finish" });
            await finishButton.ClickAsync();
            await Task.Delay(1000);
        }

        public async Task SelectOrDragFilesHere(string fileName)
        {
            var frame = Page.FrameLocator(DocumentIframe).First;
            await Task.Delay(500);

            var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
            var uploadFilePath = Path.Combine(projectRoot, "src", "TestData", fileName);

            if (!File.Exists(uploadFilePath))
            {
                throw new FileNotFoundException($"Upload file not found: {uploadFilePath}");
            }
           
            var fileInput = frame.Locator(FileUploadControl);

            await fileInput.SetInputFilesAsync(uploadFilePath);

            await Task.Delay(2000);
        }     
    }
}
    


