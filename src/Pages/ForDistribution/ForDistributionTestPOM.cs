using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightFramework.Pages;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaywrightFramework.src.Pages.ForDistribution
{
    public class ForDistributionTestPOM : BasePage
    {
        public ForDistributionTestPOM(IPage page, string baseUrl) : base(page, baseUrl) { }

        // Locators
        private const string ForDistributionText = "text=For distribution";
        private const string ManageDesktopText = "text=Manage desktop";
        private const string AddDesktopTabText = "text=Add desktop tab";
        private const string AddDesktopTabToDesktopIframe = "iframe[src*='/DesktopManagement/AddDesktopShortCut']";
        private const string AddAsDesktopTabButton = "[id*='PlaceHolderMain_AddDesktopShortcut']";
        private const string DocumentsTabText = "//span[contains(text(),'Documents')]";
        private const string SignOffButtonText = "text=Sign-off";
        private const string SignOffCodeDropdown = "div[id*='PlaceHolderMain_MainView_WriteOffCodeComboControl_div']";
        private const string SignOffDocumentIframe = "iframe[src*='/locator/DMS/Dialog/WriteOffSingleDocumentDialog']";
        private const string ShareIframe = "iframe[src*='/locator/Common/Dialog/ShareWith']";
        private const string OKButtonText = "text=OK";
        private const string DistributeButtonText = "text=Distribute";
        private const string DistributeDocumentIframe = "iframe[src*='/locator/DMS/Dialog']";
        private const string DistributionForTaksIframeReminder= "iframe[src*='/locator/CRM/Activity/Edit']";
        private const string DistributionForTaksIframeReminderIFAR = "iframe[src*='/locator/CRM/Activity/Dialog/DistributePetitionDialog']";
        private const string ResponsibleTextbox = "//input[@placeholder='Enter name for responsible unit or person.']";
        private const string BookmarkButtonText = "//button[@title=\"Bookmark\"]";     
        private const string BookmarkSuccessPopup = "[role='dialog']";
        private const string BookmarkSuccessMessage = "text=Items bookmarked successfully";
        private const string BookmarkOKButton = "button:has-text('OK')";
        private const string SaveButton= "text=Save";
        private const string ShareWithAddContactTextbox = "//input[@name='ctl00$PlaceHolderMain$MainView$ToContactQuickSearchControl_DISPLAY']";




        // Actions
        // Add your page action methods here

        public async Task ClickOnForDistributionDesktopDesktop()
        {
            // Verify For distribution is available or not.
            if (await IsVisibleAsync(ForDistributionText))
            {
                await ClickAsync(ForDistributionText, "For distribution");
                TestContext.WriteLine("✅ 'For distribution' Desktop is already visible on the page.");
            }
            else
            {
                TestContext.WriteLine("⚠️ 'For distribution' Desktop is not visible on the page.");
                // Click on Manage desktop button
                await ClickAsync(ManageDesktopText, "Manage desktop");
                // Click on Add desktop tab
                // wait until Add desktop tab is visible and click to add desktop tab
                await Page.Locator(AddDesktopTabText).WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible

                });
                await ClickAsync(AddDesktopTabText, "Add desktop tab");

                // Wait for the Add Desktop Shortcut iframe to be visible and switch to it
                var addDesktopIframe = Page.FrameLocator(AddDesktopTabToDesktopIframe).First;

                // Click on For distribution from the list of available desktop (inside iframe)
                await addDesktopIframe.Locator(ForDistributionText).ClickAsync();

                // Click on Add as desktop tab 
                await addDesktopIframe.Locator(AddAsDesktopTabButton).ClickAsync();

                // exit from iframe to main page
                await Page.MainFrame.WaitForLoadStateAsync(LoadState.NetworkIdle);
                await Task.Delay(2000);

                // Verify For distribution is available or not after adding desktop.
                if (await IsVisibleAsync(ForDistributionText))
                {
                    TestContext.WriteLine("✅ 'For distribution' Desktop is now visible on the page after adding it.");
                }
                else
                {
                    Assert.Fail("❌ 'For distribution' Desktop is still not visible on the page after adding it.");
                    TestContext.WriteLine("❌ 'For distribution' Desktop is still not visible on the page after adding it.");
                }
            }
        }

        public async Task ClickOnTab(string tabName)
        {
            // Click on Documents Tab
            await Page.Locator("//span[contains(text(),'"+tabName+ "')]").WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });
            await Page.Locator("//span[contains(text(),'"+tabName+"')]").ClickAsync();
        }

        public async Task ClickOnSignOffButtonAndAddNote(string signoffcode)
        {
            // Select a first document from list index of 1 

            await Page.Locator("//*[starts-with(@class,'si-icon')]").Nth(2).ClickAsync();

            // Click on Sign off button
            await ClickAsync(SignOffButtonText, "Sign-off button");

            // Wait for the Sign off document iframe to be visible and switch to it
            var signOffIframe = Page.FrameLocator(SignOffDocumentIframe).First;
            // Click on Sign-off code dropdown inside the iframe
            await signOffIframe.Locator(SignOffCodeDropdown).ClickAsync();

            TestContext.WriteLine("⚠️ Sign-off code dropdown is clicked.");

            await signOffIframe.Locator(SignOffCodeDropdown).Locator("input").PressSequentiallyAsync(signoffcode, new() { Delay = 100 });
            // Press Enter to select the sign off code
            await signOffIframe.Locator(SignOffCodeDropdown).Locator("input").PressAsync("Enter");
            TestContext.WriteLine($"⚠️ Sign-off code '{signoffcode}' is selected from dropdown.");
            // Click on textarea to add a remark for sign off
            await signOffIframe.Locator("td[id*='PlaceHolderMain_MainView_YellowNoteTextBoxControl_inputCell']").ClickAsync();
            // Type a remark for sign off in textarea
            var SignoffTextRemark = Page.Locator("td[id*='PlaceHolderMain_MainView_YellowNoteTextBoxControl_inputCell']");
            await signOffIframe.Locator(SignoffTextRemark).PressSequentiallyAsync("This document is signed off for testing purpose.", new() { Delay = 100 });
            TestContext.WriteLine("⚠️ Remark is added for sign off in textarea.");
            // Click on OK button to complete the sign off action
            await signOffIframe.Locator(OKButtonText).ClickAsync();
            TestContext.WriteLine("⚠️ OK button is clicked to complete the sign off action.");
            await Page.MainFrame.WaitForLoadStateAsync(LoadState.NetworkIdle);

            TestContext.WriteLine("✅ Document is signed off successfully.");
        }

        public async Task SelectItemFromList()
        {
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            // Select a first document from list index of 1 
            await Page.Locator("//*[starts-with(@class,'si-icon')]").Nth(2).ClickAsync();
            TestContext.WriteLine("Document is selected from the list");
        }

        public async Task<string> GetDocumentTitle()
        {
            // There are list of documents in document tab, get the title of first document from list index of 2
            // Document has a href link ='/locator/DMS/Document/Details/Simplified/'
            var documentTitle = await Page.Locator("a[href*='/locator/DMS/Document/Details/Simplified/']").Nth(1).InnerTextAsync();
            return documentTitle;
        }

        public async Task ClickOnTabButton(string buttonName)
        {
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            if (buttonName.Equals("Bookmark"))
            {
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                await ClickAsync(BookmarkButtonText, "Bookmark button");
                TestContext.WriteLine("Clicked on Bookmark Button");
                return;
            }
            if (buttonName.Equals("Distribute"))
            {
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                // wait until the button is visible and click on it
                await Page.Locator($"//span[text()='{buttonName}']").WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible
                });
                var SelectTab1 = Page.Locator("//span[text()='" + buttonName + "']");
                await SelectTab1.ClickAsync();
                return;
            }
            if (buttonName.Equals("Share"))
            {
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                await Page.Locator("//button[@aria-label='Share']").WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible
                });
                await Page.Locator("//button[@aria-label='Share']").ClickAsync();
                return;
            }
            // wait until the button is visible and click on it
            await Page.Locator($"//span[text()='{buttonName}']").WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });
            var SelectTab = Page.Locator("//span[text()='" + buttonName + "']");
            await SelectTab.ClickAsync();
            TestContext.WriteLine("Clicked on Distribute Button");
        }

        public async Task AddMandatoryDetailsInDistribute(string tabName)
        {
            await Task.Delay(2000);
            if (tabName.Equals("Tasks") &&
             (await Page.Locator(DistributionForTaksIframeReminder).IsVisibleAsync() ||
             await Page.Locator(DistributionForTaksIframeReminderIFAR).IsVisibleAsync()))
            {
                // If either iframe is displayed, go inside the appropriate iframe
                string visibleIframe = await Page.Locator(DistributionForTaksIframeReminder).IsVisibleAsync()
                    ? DistributionForTaksIframeReminder
                    : DistributionForTaksIframeReminderIFAR;

                await Page.Locator(visibleIframe).WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000 // 10 seconds timeout
                });

                if (visibleIframe == DistributionForTaksIframeReminderIFAR)
                {
                    var responsibleField1 = Page.FrameLocator(visibleIframe).First.Locator(ResponsibleTextbox);
                    await responsibleField1.ClickAsync();
                    await responsibleField1.FillAsync("%Ch1");
                    await responsibleField1.PressAsync("Enter");
                    var okButton1 = Page.FrameLocator(visibleIframe).First.Locator(OKButtonText);
                    await okButton1.ClickAsync();
                    return;
                }
                var selectReminder = Page.FrameLocator(visibleIframe).First.Locator("//label[text()='No reminder']");
                await selectReminder.ClickAsync();
                var saveButtonClick = Page.FrameLocator(visibleIframe).First.Locator(SaveButton);
                await saveButtonClick.ClickAsync();
                return;
            }

            var DistributeIframe = Page.FrameLocator(DistributeDocumentIframe).First;
            var responsibleField = DistributeIframe.Locator(ResponsibleTextbox);
           
            await responsibleField.ClickAsync();
            await responsibleField.FillAsync("%Ch1");
            await responsibleField.PressAsync("Enter");

            // Wait for the frame to be in NetworkIdle state (ensures dropdown processed the selection)
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            // Now click OK button
            var okButton = DistributeIframe.Locator(OKButtonText);
            await okButton.ClickAsync();
            TestContext.WriteLine("Mandatory details are added in distribute document popup and clicked on OK button");

            // Wait for the iframe to close/disappear after clicking OK
            await Page.Locator(DistributeDocumentIframe).WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Detached,
                Timeout = 10000 // 10 seconds timeout
            });

            TestContext.WriteLine("✅ Distribute iframe closed successfully");
            // Switch back to main page and wait for network idle
            await Page.MainFrame.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await Task.Delay(4000); 

        }

        public async Task AddMandatoryDetailsInShare(string contactPerson)
        {
            // Navigate to Share document iframe
            await Task.Delay(2000);
            var shareIframe = Page.FrameLocator(ShareIframe).First;
        // Click on Add contact textbox and add contact 
            var addContactTextbox = shareIframe.Locator(ShareWithAddContactTextbox);
            await addContactTextbox.ClickAsync();
            await addContactTextbox.FillAsync(contactPerson);
            await addContactTextbox.PressAsync("Enter");
            TestContext.WriteLine("Contact is added in Share document popup");
            // Wait for the frame to be in NetworkIdle state (ensures dropdown processed the selection)
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            // Now click OK button
            var okButton = shareIframe.Locator(OKButtonText);
            await okButton.ClickAsync();
            TestContext.WriteLine("Clicked on OK button in Share document popup");
            // Wait for the iframe to close/disappear after clicking OK
            await Page.Locator(ShareIframe).WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Detached,
                Timeout = 10000 // 10 seconds timeout
            });
            TestContext.WriteLine("✅ Share iframe closed successfully");
             // Switch back to main page and wait for network idle
             await Page.MainFrame.WaitForLoadStateAsync(LoadState.NetworkIdle);
             await Task.Delay(4000);

        }




    }
}