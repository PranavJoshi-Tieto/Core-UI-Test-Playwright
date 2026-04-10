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
        Admin,
        // Add 10 new user roles for parallel testing
        User1,
        User2,
        User3,
        User4,
        User5,
        User6,
        User7,
        User8,
        User9,
        User10
    }

    public static class LoginHelper
    {
        // ========== OVERLOAD 1: Login with custom username/password ==========
        public static async Task LoginToApplicationAsync(IPage page, string baseUrl, string username, string password)
        {
            Logger.Info($"🔐 Auto-login starting with user: {username}...");

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

                    // wait until #header_SiteLogo is visible and click to go to homepage
                    await page.Locator("#header_SiteLogo").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
                    Logger.Info("Clicking on P360 icon to go Homepage");
                    await page.Locator("#header_SiteLogo").ClickAsync();
                    await page.WaitForTimeoutAsync(3000);

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

                    Logger.Info("✅ Clicking Yes on Stay Signed In...");
                    await page.Locator("//input[@value='Yes']").ClickAsync();
                    await page.WaitForTimeoutAsync(4000);

                    Logger.Info("✅ Login completed via useAnotherAccount path");

                    // wait until #header_SiteLogo is visible and click to go to homepage
                    await page.Locator("#header_SiteLogo").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
                    Logger.Info("Clicking on P360 icon to go Homepage");
                    await page.Locator("#header_SiteLogo").ClickAsync();
                    await page.WaitForTimeoutAsync(3000);
                }

                Logger.Info("🔐 Auto-login completed ✅");
            }
            catch (Exception ex)
            {
                Logger.Info($"⚠️ Login exception: {ex.Message}");
            }
        }

        // ========== OVERLOAD 2: Login with UserRole enum ==========
        public static async Task LoginToApplicationAsync(IPage page, string baseUrl, UserRole role = UserRole.CaseHandler)
        {
            var (username, password) = GetCredentials(role);
            Logger.Info($"🔐 Auto-login starting as {role}...");
            await LoginToApplicationAsync(page, baseUrl, username, password);
        }

        public static async Task LogoutAsync(IPage page)
        {
            Logger.Info("🔓 Logging out...");

            // Wait for page to be ready
            Logger.Info("⏳ Waiting for page to be ready...");
            await page.WaitForTimeoutAsync(3000);

            // Click user avatar/profile icon
            Logger.Info("👤 Clicking user avatar...");
            var avatarLocator = page.Locator(".fui-Avatar__initials").First;
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

            // Add 10 new user mappings
            UserRole.User1 => (TestSettings.CloudUser1UserName, TestSettings.CloudUser1Password),
            UserRole.User2 => (TestSettings.CloudUser2UserName, TestSettings.CloudUser2Password),
            UserRole.User3 => (TestSettings.CloudUser3UserName, TestSettings.CloudUser3Password),
            UserRole.User4 => (TestSettings.CloudUser4UserName, TestSettings.CloudUser4Password),
            UserRole.User5 => (TestSettings.CloudUser5UserName, TestSettings.CloudUser5Password),
            UserRole.User6 => (TestSettings.CloudUser6UserName, TestSettings.CloudUser6Password),
            UserRole.User7 => (TestSettings.CloudUser7UserName, TestSettings.CloudUser7Password),
            UserRole.User8 => (TestSettings.CloudUser8UserName, TestSettings.CloudUser8Password),
            UserRole.User9 => (TestSettings.CloudUser9UserName, TestSettings.CloudUser9Password),
            UserRole.User10 => (TestSettings.CloudUser10UserName, TestSettings.CloudUser10Password),

            _ => (TestSettings.CloudRegistrarUserName, TestSettings.CloudRegistrarPassword)
        };
    }
}