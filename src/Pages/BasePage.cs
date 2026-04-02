using Microsoft.Playwright;
using PlaywrightFramework.Utils;

namespace PlaywrightFramework.Pages
{
    /// <summary>
    /// Base class for all Page Object Model pages.
    /// Provides common page interactions and helpers.
    /// </summary>
    public abstract class BasePage
    {
        protected readonly IPage Page;
        protected readonly string BaseUrl;

        protected BasePage(IPage page, string baseUrl)
        {
            Page = page;
            BaseUrl = baseUrl;
        }

        // ─── Navigation ───────────────────────────────────────────────────────
        public async Task GoToAsync(string path = "")
        {
            var url = $"{BaseUrl}{path}";
            Logger.Info($"🔗 Navigating to: {url}");
            await Page.GotoAsync(url);
        }

        // ─── Element Interactions ─────────────────────────────────────────────
        protected async Task ClickAsync(string selector, string description = "")
        {
            Logger.Debug($"🖱 Click: {description ?? selector}");
            await Page.Locator(selector).WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });
            await Page.Locator(selector).ClickAsync();
        }

        protected async Task FillAsync(string selector, string value, string description = "")
        {
            Logger.Debug($"⌨ Fill [{description ?? selector}]: {value}");
            await Page.Locator(selector).WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });
            await Page.Locator(selector).ClearAsync();
            await Page.Locator(selector).FillAsync(value);
        }

        protected async Task<string> GetTextAsync(string selector)
        {
            await Page.Locator(selector).WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });
            return await Page.Locator(selector).InnerTextAsync() ?? string.Empty;
        }

        protected async Task<bool> IsVisibleAsync(string selector)
        {
            return await Page.Locator(selector).IsVisibleAsync();
        }

        protected async Task WaitForUrlAsync(string urlPattern)
        {
            Logger.Debug($"⏳ Waiting for URL to contain: {urlPattern}");
            await Page.WaitForURLAsync($"**{urlPattern}**");
        }

        protected async Task WaitForElementAsync(string selector)
        {
            await Page.Locator(selector).WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });
        }

        public string GetCurrentUrl() => Page.Url;

        public async Task<string> GetTitleAsync() => await Page.TitleAsync();
    }
}
