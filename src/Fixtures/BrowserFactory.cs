using Microsoft.Playwright;
using PlaywrightFramework.Utils;

namespace PlaywrightFramework.Fixtures
{
    public static class BrowserFactory
    {
        public static async Task<IBrowser> CreateBrowserAsync(IPlaywright playwright, string? browserName = null)
        {
            var browser = browserName ?? TestSettings.Browser;
            var headless = TestSettings.Headless;
            var slowMo = TestSettings.SlowMo;

            Logger.Info($"🌐 Launching browser: {browser} | Headless: {headless} | SlowMo: {slowMo}ms");

            return browser.ToLower() switch
            {
                "chromium" or "chrome" => await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = headless,
                    SlowMo = slowMo,
                    Channel = "chrome",
                    Args = new[] { "--start-maximized", "--disable-dev-shm-usage" }
                }),

                "firefox" => await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = headless,
                    SlowMo = slowMo
                }),

                "edge" => await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = headless,
                    SlowMo = slowMo,
                    Channel = "msedge",
                    Args = new[] { "--start-maximized", "--disable-dev-shm-usage" }
                }),

                "webkit" or "safari" => await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = headless,
                    SlowMo = slowMo
                }),

                _ => throw new ArgumentException($"Unsupported browser: {browser}. Use: chromium, firefox, edge, webkit")
            };
        }

        public static async Task<IBrowserContext> CreateContextAsync(IBrowser browser)
        {
            return await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport,
                IgnoreHTTPSErrors = true,
                RecordVideoDir = TestSettings.VideoOnFailure
                    ? Path.Combine(Directory.GetCurrentDirectory(), "videos")
                    : null
            });
        }

        /// <summary>
        /// Launch browser with persistent context for TRUE visual incognito mode.
        /// This will show the dark incognito UI in Chrome with maximized window.
        /// </summary>
        public static async Task<IBrowserContext> CreateIncognitoPersistentContextAsync(IPlaywright playwright, string? browserName = null)
        {
            var browser = browserName ?? TestSettings.Browser;
            var headless = TestSettings.Headless;
            var slowMo = TestSettings.SlowMo;

            Logger.Info($"🎭 Launching browser with INCOGNITO MODE");
            Logger.Info($"Browser: {browser} | Headless: {headless} | SlowMo: {slowMo}ms");

            // Create a temporary user data directory (will be cleaned up automatically)
            var tempUserDataDir = Path.Combine(Path.GetTempPath(), $"pw-incognito-{Guid.NewGuid()}");
            Directory.CreateDirectory(tempUserDataDir);

            var args = new List<string>
            {
                "--start-maximized",
                "--disable-dev-shm-usage",
                "--incognito"
            };

            var contextOptions = new BrowserTypeLaunchPersistentContextOptions
            {
                Headless = headless,
                Channel = browser.ToLower() == "edge" ? "msedge" : "chrome",
                Args = args.ToArray(),
                ViewportSize = ViewportSize.NoViewport,
                IgnoreHTTPSErrors = true,
                AcceptDownloads = true,
                SlowMo = slowMo,
                RecordVideoDir = TestSettings.VideoOnFailure
                    ? Path.Combine(Directory.GetCurrentDirectory(), "videos")
                    : null
            };

            Logger.Info($"📂 Temporary profile: {tempUserDataDir}");
            Logger.Info("🔒 INCOGNITO MODE ENABLED - Dark UI will appear!");

            return browser.ToLower() switch
            {
                "chromium" or "chrome" => await playwright.Chromium.LaunchPersistentContextAsync(tempUserDataDir, contextOptions),
                "edge" => await playwright.Chromium.LaunchPersistentContextAsync(tempUserDataDir, contextOptions),
                _ => throw new ArgumentException($"Incognito persistent context only supported for Chrome/Edge. Use: chrome, edge")
            };
        }
    }
}
