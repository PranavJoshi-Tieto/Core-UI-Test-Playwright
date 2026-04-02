using Allure.NUnit;
using Allure.NUnit.Attributes;
using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using PlaywrightFramework.Utils;

namespace PlaywrightFramework.Fixtures
{
    /// <summary>
    /// Base class for all tests.
    /// Handles: browser setup/teardown, screenshot on failure, retry logic, Allure integration.
    /// </summary>
    [AllureNUnit]
    [Parallelizable(ParallelScope.All)]  // ← Enables parallel execution across all test classes
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]  // ← Each test gets its own fresh browser instance
    public abstract class BaseTest
    {
        protected IPlaywright Playwright { get; private set; } = null!;
        protected IBrowser? Browser { get; private set; }
        protected IBrowserContext Context { get; private set; } = null!;
        protected IPage Page { get; private set; } = null!;

        protected string BaseUrl => TestSettings.BaseUrl;
        protected string TestName => TestContext.CurrentContext.Test.Name;

        [SetUp]
        public async Task SetUpAsync()
        {
            Logger.Info($"");
            Logger.Info($"========================================");
            Logger.Info($"▶ Starting Test: {TestName}");
            Logger.Info($"🌍 Environment : {TestSettings.ENV}");
            Logger.Info($"🔗 Base URL    : {TestSettings.BaseUrl}");
            Logger.Info($"🌐 Browser     : {TestSettings.Browser}");
            Logger.Info($"========================================");

            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            // Check if incognito mode is enabled
            if (TestSettings.Incognito)
            {
                Logger.Info("🔒 Incognito Mode: ENABLED");
                
                // Use persistent context for TRUE incognito with visual dark UI
                Context = await BrowserFactory.CreateIncognitoPersistentContextAsync(Playwright);
                
                // Browser is managed by persistent context
                Browser = null;
                
                // Persistent context automatically creates a page
                Page = Context.Pages[0];
            }
            else
            {
                Logger.Info("🔓 Incognito Mode: DISABLED");
                
                // Standard mode - use browser + context
                Browser = await BrowserFactory.CreateBrowserAsync(Playwright);
                Context = await BrowserFactory.CreateContextAsync(Browser);
                Page = await Context.NewPageAsync();
            }

            // Set default timeout from runsettings
            Page.SetDefaultTimeout(TestSettings.Timeout);
            Page.SetDefaultNavigationTimeout(TestSettings.Timeout);
        }

        [TearDown]
        public async Task TearDownAsync()
        {
            var outcome = TestContext.CurrentContext.Result.Outcome.Status;

            if (outcome == TestStatus.Failed)
            {
                Logger.Error($"❌ Test FAILED: {TestName}");
                await ScreenshotHelper.CaptureScreenshotAsync(Page, TestName);
            }
            else
            {
                Logger.Info($"✅ Test PASSED: {TestName}");
            }

            Logger.Info($"========================================");
            Logger.Info($"");

            await Context.CloseAsync();
            
            // Browser is null when using persistent context (incognito mode)
            if (Browser != null)
            {
                await Browser.CloseAsync();
            }
            
            Playwright.Dispose();
        }
    }
}
