using Microsoft.Playwright;
using PlaywrightFramework.Pages;

namespace PlaywrightFramework.Utils
{
    public enum UserRole
    {
        Registrar,
        CaseHandler,
        Manager,
        BoardHandler,
        Admin
    }

    public static class LoginHelper
    {
        public static async Task LoginToApplicationAsync(IPage page, string baseUrl, UserRole role = UserRole.CaseHandler)
        {
            var (username, password) = GetCredentials(role);
            Logger.Info($"🔐 Auto-login starting as {role}...");

            try
            {
                Logger.Info("📄 Navigating to login page...");
                await page.GotoAsync($"{baseUrl}");

                // Wait for login page to load
                Logger.Info("⏳ Waiting for login page to load...");
                await page.WaitForLoadStateAsync(LoadState.NetworkIdle);


                var signInButtonLocator = page.Locator("//a[@id='cantAccessAccount']//preceding::input[2]");
                var useAnotherAccountLocator = page.Locator("#otherTileText");

                var signInButtonCount = await signInButtonLocator.CountAsync();
                var useAnotherAccountCount = await useAnotherAccountLocator.CountAsync();

                if (signInButtonCount > 0)
                {
                    Logger.Info("🔐 Sign in button found, proceeding with login...");

                    Logger.Info("📝 Clicking and entering username...");
                    await signInButtonLocator.ClickAsync();
                    await signInButtonLocator.FillAsync(username);
                    await page.WaitForTimeoutAsync(2000);

                    Logger.Info("➡️ Clicking Next button...");
                    await page.Locator("//input[@value='Next']").ClickAsync();
                    await page.WaitForTimeoutAsync(2000);

                    Logger.Info("🔑 Entering password...");
                    var passwordField = page.Locator("//input[@name='passwd']");
                    await passwordField.ClickAsync();
                    await passwordField.FillAsync(password);
                    await page.WaitForTimeoutAsync(2000);

                    Logger.Info("🚀 Clicking Sign in button...");
                    await page.Locator("//input[@value='Sign in']").ClickAsync();
                    await page.WaitForTimeoutAsync(5000);

                    Logger.Info("✅ Clicking Yes on Stay Signed In...");
                    await page.Locator("//input[@value='Yes']").ClickAsync();
                    await page.WaitForTimeoutAsync(4000);

                    Logger.Info("CLicking on P360 icon to go Homepage");
                    await page.Locator("#header_SiteLogo").ClickAsync();
                    await page.WaitForTimeoutAsync(3000);

                    //Logger.Info("🔙 Clicking 'take me back to 360'...");
                    //await page.Locator("button:has-text('take me back to 360')").ClickAsync();
                   // await page.WaitForTimeoutAsync(3000);

                    Logger.Info("✅ Login completed via signInButton path");
                }
                else if (useAnotherAccountCount > 0)
                {
                    Logger.Info("👤 'Use another account' option found, proceeding...");

                    Logger.Info("🔄 Clicking 'Use another account'...");
                    await useAnotherAccountLocator.ClickAsync();
                    await page.WaitForTimeoutAsync(5000);

                    Logger.Info("📝 Entering username...");
                    await page.Locator("//a[@id='cantAccessAccount']//preceding::input[2]").FillAsync(username);
                    await page.WaitForTimeoutAsync(5000);

                    Logger.Info("➡️ Clicking Next button...");
                    await page.Locator("//input[@value='Next']").ClickAsync();
                    await page.WaitForTimeoutAsync(5000);

                    Logger.Info("🔑 Entering password...");
                    await page.Locator("//input[@name='passwd']").FillAsync(password);
                    await page.WaitForTimeoutAsync(2000);

                    Logger.Info("🚀 Clicking Sign in button...");
                    await page.Locator("//input[@value='Sign in']").ClickAsync();

                    await page.WaitForTimeoutAsync(5000);
                    await page.GotoAsync($"{baseUrl}");
                    Logger.Info("✅ Login completed via useAnotherAccount path");
                   
                }

                Logger.Info("🔐 Auto-login completed ✅");
            }
            catch (Exception ex)
            {
                Logger.Info("ℹ️ Application has been already logged in");
            }
        }

        public static async Task LogoutAsync(IPage page)
        {
            Logger.Info("🔓 Logging out...");

            // Wait for page to be ready
            Logger.Info("⏳ Waiting for page to be ready...");
            await page.WaitForTimeoutAsync(3000);

            // Click user avatar/profile icon
            Logger.Info("👤 Clicking user avatar...");
            var avatarLocator = page.Locator(".fui-Avatar__initials");
            var userInfoLocator = page.Locator("//div[@class='si-user-info']/div/span[1]");

            if (await avatarLocator.IsVisibleAsync())
            {
                await avatarLocator.ClickAsync();
                Logger.Info("✅ Clicked on Avatar initials");
            }
            else
            {
                await userInfoLocator.ClickAsync();
                Logger.Info("✅ Clicked on User info");
            }

            Logger.Info("⏳ Waiting for menu to appear...");
            await page.WaitForTimeoutAsync(500);

            // Click logout button
            Logger.Info("🚪 Clicking logout button...");
            var logoutButtonLocator = page.Locator("//div[@class='user-info-item']/button");
            var logOffButtonLocator = page.Locator("button:has-text('Log off')");

            if (await logoutButtonLocator.IsVisibleAsync())
            {
                await logoutButtonLocator.ClickAsync();
                Logger.Info("✅ Clicked logout button");
            }
            else
            {
                await logOffButtonLocator.ClickAsync();
                Logger.Info("✅ Clicked Log off button");
            }

            // Wait for logout to complete
            Logger.Info("⏳ Waiting for logout to complete...");
            await page.WaitForTimeoutAsync(3000);

            Logger.Info("🔓 Logout completed ✅");
        }

        public static async Task SwitchUserAsync(IPage page, string baseUrl, UserRole role)
        {
            await LogoutAsync(page);
            await LoginToApplicationAsync(page, baseUrl, role);
        }

        private static (string username, string password) GetCredentials(UserRole role) => role switch
        {
            UserRole.Registrar => (TestSettings.CloudRegistrarUserName, TestSettings.CloudRegistrarPassword),
            UserRole.CaseHandler => (TestSettings.CloudCaseHandlerUserName, TestSettings.CloudCaseHandlerPassword),
            UserRole.Manager => (TestSettings.CloudManagerUserName, TestSettings.CloudManagerPassword),
            UserRole.BoardHandler => (TestSettings.CloudBoardHandlerUserName, TestSettings.CloudBoardHandlerPassword),
            UserRole.Admin => (TestSettings.CloudAdminUserName, TestSettings.CloudAdminPassword),
            _ => (TestSettings.CloudRegistrarUserName, TestSettings.CloudRegistrarPassword)
        };
    }
}