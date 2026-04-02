using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightFramework.Utils
{
    public static class ScreenshotHelper
    {
        public static async Task CaptureScreenshotAsync(IPage page, string testName)
        {
            if (!TestSettings.ScreenshotOnFailure) return;

            var screenshotsDir = Path.Combine(Directory.GetCurrentDirectory(), "screenshots");
            Directory.CreateDirectory(screenshotsDir);

            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var fileName = $"{testName}_{timestamp}.png";
            var filePath = Path.Combine(screenshotsDir, fileName);

            await page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = filePath,
                FullPage = true
            });

            Logger.Info($"📸 Screenshot saved: {filePath}");

            // Attach to NUnit test report
            TestContext.AddTestAttachment(filePath, $"Failure Screenshot - {testName}");
        }
    }
}
