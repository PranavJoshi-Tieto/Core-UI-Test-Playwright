using Microsoft.Playwright;
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
        private const string OKButtonText = "text=OK";


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

        public async Task ClickOnDocumentsTab()
        {
            // Click on Documents Tab
            await Page.Locator(DocumentsTabText).WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });
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
    }
    }