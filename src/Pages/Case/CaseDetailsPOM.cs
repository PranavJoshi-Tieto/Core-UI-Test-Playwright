using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightFramework.Pages;

namespace PlaywrightFramework.src.Pages.Case
{
    public class CaseDetailsPOM : BasePage
    {
        public CaseDetailsPOM(IPage page, string baseUrl) : base(page, baseUrl) { }


        //Locator's

        private const string CaseEditIframe = "iframe[title*='Edit properties: Case'], iframe[title*='Edit properties: Case']";    
        private const string CaseFunctionButton = "//span[contains(text(),'Case functions')]";      
        private const string EditPropertiesButton = "//span[contains(text(),'Edit properties')]";
        private const string EditCaseIframe = "iframe[src*='/locator/DMS/Case/Edit']";
        private const string TakeBackTo360Button = "button[id='navigate-back-btn']";
        private const string CopyHyperlinkButton = "//span[text()='Copy']";
        private const string SetArchiveStatusIframe = "iframe[src^='/locator/DMS/Dialog/EditCaseStatus']";
        private const string CaseSummaryIframe = "iframe[src^='/locator/Common/Dialog/CaseSummaryDialog']";
        private const string SetatusDropdown = "PlaceHolderMain_MainView_CaseStatusComboControl_div";
        private const string OkButton = "PlaceHolderMain_MainView_Finish-Button";


        public async Task ClickCaseFunctionButtonAndEditcaseTitle()
        {
            var caseFunctionButton = Page.Locator(CaseFunctionButton);
            await caseFunctionButton.ClickAsync();
            await Task.Delay(1000); // Add a small delay to allow the dropdown to appear
            TestContext.WriteLine("Clicked Case functions button");

            var editPropertiesButton = Page.Locator(EditPropertiesButton);
            await editPropertiesButton.ClickAsync();
            await Task.Delay(1000); // Add a small delay to allow the edit properties page to load
            TestContext.WriteLine("Clicked Edit properties button");

            var frame = Page.FrameLocator(CaseEditIframe).First;
            await Task.Delay(500);

            var titleField = frame.GetByRole(AriaRole.Textbox, new() { Name = "Title" });
            // Get textbox value
            var titleValue = await titleField.InputValueAsync();
            TestContext.WriteLine($"Current case title is: {titleValue}");

            //Clear the existing title and enter a new title
            await titleField.FillAsync("");
            await titleField.TypeAsync("Updated case title from AI agent");
            TestContext.WriteLine("Entered new case title: Updated case title from AI agent");

            // Clcik on Finish button
            var finishButton = frame.GetByRole(AriaRole.Button, new() { Name = "Finish" });
            var updatedTitleValue = await titleField.InputValueAsync();
            await finishButton.ClickAsync();

            // switch back to home page from iframe
            await Page.MainFrame.WaitForLoadStateAsync(LoadState.NetworkIdle);

            // verify titleField value is not same as Updated case title from AI agent
            await Task.Delay(1000); // Add a small delay to allow the changes to be saved

            Assert.AreNotEqual("Updated case title from AI agent", titleValue, "Case title should be updated and not same as the new title entered");

        }

        public async Task ClickCaseFunctionButtonAndSaveAs()
        {
            var caseFunctionButton = Page.Locator(CaseFunctionButton);
            await caseFunctionButton.ClickAsync();
            await Task.Delay(1000); // Add a small delay to allow the dropdown to appear
            TestContext.WriteLine("Clicked Case functions button");

            // Click on Save as button
            var saveAsButton = Page.Locator("//span[contains(text(),'Save as')]");
            await saveAsButton.ClickAsync();
            await Task.Delay(1000); // Add a small delay to allow the save as page to load

            var frame = Page.FrameLocator(EditCaseIframe).First;
            var titleField = frame.GetByRole(AriaRole.Textbox, new() { Name = "Title" });

            // Enter title for the new case
            string newCaseTitle = "New case created from Save as - AI agent " + Guid.NewGuid().ToString();
            await titleField.FillAsync("");
            await titleField.TypeAsync(newCaseTitle);
            TestContext.WriteLine($"Entered new case title: {newCaseTitle}");

            // Click on Finish button
            var finishButton = frame.GetByRole(AriaRole.Button, new() { Name = "Finish" });
            await finishButton.ClickAsync();
            TestContext.WriteLine("Clicked Finish button to create new case from Save as");

            // switch back to home page from iframe
            await Page.MainFrame.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        public async Task VerifyCaseTitleIsUpdated()
        {

            var caseTitleLocator = Page.Locator("h1[id*='PlaceHolderMain_MainView_DetailTitle_generic']");
            var caseTitle = await caseTitleLocator.TextContentAsync();
            TestContext.WriteLine($"Current case title displayed on page: {caseTitle}");
            Assert.AreNotEqual(" ", caseTitle, "Case title should be updated and not same as the new title entered");

        }

        public async Task ClickOnMoreButton()
        {
            var moreButton = Page.Locator("//span[contains(text(),'More')]");
            await moreButton.ClickAsync();
            TestContext.WriteLine("Clicked More button");
        }

        public async Task SelectOptionFromMoreButton(string option)
        {
            var optionLocator = $"//span[contains(text(),'{option}')]";
            var optionElement = Page.Locator(optionLocator);
            await optionElement.ClickAsync();
            TestContext.WriteLine($"Clicked {option} option from More button");
        }

        public async Task<bool> IsShowOverviewTextDisplayed()
        {
            // Add delay to allow the overview section to load
            await Task.Delay(2000);
            var showOverviewLocator = Page.Locator("//span[contains(text(),'Show overview')]");
            return showOverviewLocator.IsVisibleAsync().Result;
        }

        public async Task ClickAdvancedSearchButtonAndSelectOption(string option)
        {
            var advancedSearchButton = Page.Locator("//button[contains(text(),'Advanced search')]");
            await advancedSearchButton.ClickAsync();
            TestContext.WriteLine("Clicked Advanced search button");

            // Select option from dropdown
            var optionLocator = $"//span[contains(text(),'{option}')]";
            var optionElement = Page.Locator(optionLocator);
            await optionElement.ClickAsync();
            TestContext.WriteLine($"Clicked {option} option from Advanced search dropdown");

            try
            {
                // Wait for the button to appear (with timeout)
                await Page.WaitForSelectorAsync(TakeBackTo360Button, new PageWaitForSelectorOptions { Timeout = 5000 });

                var takeBackButton = Page.Locator(TakeBackTo360Button);
                await takeBackButton.ClickAsync();

                // Wait for the navigation to complete
                await Task.Delay(2000);
            }
            catch
            {
                // Button not displayed within timeout, continue without error
            }
            // Click on "extended search" button 
            await Task.Delay(2000);
            // Click on "Extended search " span 
            var extendedSearchButton = Page.Locator("//span[contains(text(),'Extended search')]");
            await FieldActions.ClickElement(extendedSearchButton);
            //await extendedSearchButton.ClickAsync();
            TestContext.WriteLine("Clicked Extended search button to open advanced search panel");

        }

        public async Task ClickOnSearchButton()
        {
            await Task.Delay(2000);
            var searchButton = Page.Locator("//span[text()='Search']");
            await searchButton.ClickAsync();
            TestContext.WriteLine("Clicked Search button to perform search action");
        }

        public async Task VerifyCaseListIsDisplayed()
        {
            // Wait for search results to load by waiting for the table row to appear
            await Page.WaitForSelectorAsync("//tr[contains(@class,'ms-itmhover')]", new PageWaitForSelectorOptions
            {
                Timeout = 10000,
                State = WaitForSelectorState.Visible
            });

            // Verify List with class "ms-alternating ms-itmhover ms-itmHoverEnabled" is displayed in search results
            var caseListLocator = Page.Locator("//tr[contains(@class,'ms-alternating ms-itmhover ms-itmHoverEnabled')]");

            // Check if at least one row is visible
            var count = await caseListLocator.CountAsync();
            Assert.IsTrue(count > 0, $"Case list should be displayed in search results. Found {count} items.");
            Assert.IsTrue(await caseListLocator.First.IsVisibleAsync(), "First case in the list should be visible");

            TestContext.WriteLine($"Verified case list is displayed in search results with {count} items");

        }

        public async Task SelectCaseStatusFromDropdown()
        {
            await Task.Delay(2000);
            // Click on case status dropdown which has title="Status" and textbox is next to it
            var caseStatusDropdown = Page.Locator("//span[@title='Status']/following-sibling::div//input");
            await caseStatusDropdown.ClickAsync();
            TestContext.WriteLine("Clicked on Case status dropdown");

            // Click on Status dropdown conatins ID "PlaceHolderMain_MainView_StatusControl_div" 
            var statusOption = Page.Locator("//div[contains(@id,'PlaceHolderMain_MainView_StatusControl_div')]");

            // Set dropdown value to "5" which is Inprogress status
            await statusOption.SelectOptionAsync(new[] { "5" });
            TestContext.WriteLine("Selected Inprogress status with value '5'");


        }

        public async Task ClickOnCaseFunctionsButtonAndSelectOption(string option)
        {
            var caseFunctionButton = Page.Locator(CaseFunctionButton);
            await caseFunctionButton.ClickAsync();
            await Task.Delay(1000); // Add a small delay to allow the dropdown to appear
            TestContext.WriteLine("Clicked Case functions button");
            var optionLocator = $"//span[contains(text(),'{option}')]";
            var optionElement = Page.Locator(optionLocator);
            await optionElement.ClickAsync();
            await Task.Delay(1000); // Add a small delay to allow the action to complete
            TestContext.WriteLine($"Clicked {option} option from Case functions button");
        }

        public async Task<bool>  IsLinkCopiedMessageDisplayed()
        {   //Click on Copy Hyperlink Button
            var copyHyperlinkButton = Page.Locator(CopyHyperlinkButton);
            copyHyperlinkButton.ClickAsync().Wait();
            TestContext.WriteLine("Clicked on Copy Hyperlink button");

            var linkCopiedMessage = Page.Locator("text=The hyperlink is successfully copied to the clipboard");
            return linkCopiedMessage.IsVisibleAsync().Result;
        }

        public async Task ClickOnSetStatusButtonAndSelectOption(string statusValue)
        {
            // Click on set status Button which ID "PlaceHolderMain_MainView_SetStatusButton_DetailFunctionControl"
            var setStatusButton = Page.Locator("#PlaceHolderMain_MainView_SetStatusButton_DetailFunctionControl");
            await setStatusButton.ClickAsync();
            TestContext.WriteLine("Clicked on Set status button");

            // Nvaigate to iframe with src contains "EditCaseStatus" and click on Closed status which has value "6"
            var frame = Page.FrameLocator(SetArchiveStatusIframe).First;
            // Click on dropdown and set value to "6" which is Closed status. create a parameter for status value
            var statusDropdown = frame.Locator($"#{SetatusDropdown}");
            await statusDropdown.ClickAsync();
            await Task.Delay(500); // Wait for dropdown to open
            await frame.Locator($"#{SetatusDropdown} option[value='{statusValue}']").ClickAsync();


            // Click on Ok button element is OkButton
            // Click on Ok button - it's inside the iframe, so use frame locator with # prefix
            var okButtonClick = frame.Locator($"#{OkButton}");
            await okButtonClick.ClickAsync();
            TestContext.WriteLine("Ok Button is clicked");         
        }

        public async Task ClickOnProduceButtonOfCaseSummaryPopup()
        {
            // Navigate to Iframe CaseSummaryIframe
            var frame = Page.FrameLocator(CaseSummaryIframe).First;

            // CLick on Produce button which has text "Produce" remove contains it should match exact text
            var produceButton = frame.Locator("//button[text()='Produce']");
            await produceButton.ClickAsync();
            TestContext.WriteLine("Clicked on Produce button in Case summary popup");

            // Verify that the iframe is no longer visible
            await Task.Delay(2000); // Wait for the popup to close
           if (await frame.Locator(CaseSummaryIframe).IsVisibleAsync())
            {
                Assert.Fail("Case summary iframe should be closed after clicking Produce button");
            }
            else
            {
                TestContext.WriteLine("Verified that Case summary iframe is closed after clicking Produce button");
            }
        }



        /// <summary>
        /// Actions methos
        /// </summary>
        public static class FieldActions
        {
            /// <summary>
            /// Clicks on a field, clears it, and types text character by character
            /// </summary>
            public static async Task ClickAndSendKeys(ILocator field, string text, int delayBetweenChars = 50)
            {
                // Click on the field to focus
                await field.ClickAsync();
                await Task.Delay(300);

                // Clear existing content
                await field.PressAsync("Control+A");
                await Task.Delay(100);
                await field.PressAsync("Backspace");
                await Task.Delay(200);

                // Type text sequentially
                await field.PressSequentiallyAsync(text, new() { Delay = delayBetweenChars });
                await Task.Delay(300);
            }
            public static async Task ClickElement(ILocator element, int delayBefore = 500, int delayAfter = 1000)
            {
                await Task.Delay(delayBefore);
                await element.ClickAsync();
                await Task.Delay(delayAfter);
            }

            /// <summary>
            /// Clicks, sends keys, and presses Enter
            /// </summary>
            public static async Task ClickSendKeysAndEnter(ILocator field, string text, int delayBetweenChars = 50)
            {
                await ClickAndSendKeys(field, text, delayBetweenChars);
                await field.PressAsync("Enter");
                await Task.Delay(500);
            }
        


        }

    }
}