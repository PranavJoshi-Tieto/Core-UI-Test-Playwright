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
    }
}