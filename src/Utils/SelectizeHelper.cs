using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace SI.AutomatedUITest.src.utils
{
    /// <summary>
    /// WebElementHelper - Updated for Playwright
    /// These are template methods that you should implement based on your existing logic
    /// </summary>
    public static class SelectizeHelper
    {
        /// <summary>
        /// Checks if an element is clickable (visible, enabled, not hidden by other elements)
        /// </summary>
        public static async Task<bool> IsElementClickable(ILocator locator)
        {
            try
            {
                // Check if element is visible
                bool isVisible = await locator.IsVisibleAsync();

                // Check if element is enabled
                bool isEnabled = await locator.IsEnabledAsync();

                return isVisible && isEnabled;
            }
            catch (PlaywrightException)
            {
                return false;
            }
        }

        /// <summary>
        /// Scrolls the page so the element is in view
        /// </summary>
        public static async Task ScrollToElement(IPage page, ILocator locator)
        {
            try
            {
                await locator.ScrollIntoViewIfNeededAsync();
            }
            catch (PlaywrightException ex)
            {
                await LogHelper.LogErrorAsync(ex);
            }
        }

        /// <summary>
        /// Waits until post-back/AJAX call completes
        /// This is a template - adjust based on your actual post-back detection logic
        /// </summary>
        public static async Task WaitUntilPostBackComplete(IPage page, int timeout = 60000)
        {
            try
            {
                // Option 1: Wait for network to be idle (if using modern web framework)
                await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = timeout });
            }
            catch (PlaywrightException)
            {
                // If network idle fails, just wait a bit
                await Task.Delay(500);
            }
        }

        /// <summary>
        /// Alternative: Wait for DOM to be loaded (if post-back uses DOM updates)
        /// </summary>
        public static async Task WaitUntilPostBackCompleteDOMReady(IPage page, int timeout = 60000)
        {
            try
            {
                await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded, new PageWaitForLoadStateOptions { Timeout = timeout });
            }
            catch (PlaywrightException ex)
            {
                await LogHelper.LogErrorAsync(ex);
            }
        }

        /// <summary>
        /// Wait for specific element to be ready
        /// </summary>
        public static async Task WaitForElement(ILocator locator, int timeout = 30000)
        {
            try
            {
                await locator.WaitForAsync(new LocatorWaitForOptions { Timeout = timeout });
            }
            catch (PlaywrightException ex)
            {
                await LogHelper.LogErrorAsync(ex);
            }
        }

        /// <summary>
        /// Wait for element to be visible
        /// </summary>
        public static async Task WaitForElementVisible(ILocator locator, int timeout = 30000)
        {
            try
            {
                await locator.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = timeout
                });
            }
            catch (PlaywrightException ex)
            {
                await LogHelper.LogErrorAsync(ex);
            }
        }
    }

    /// <summary>
    /// LogHelper - Updated for Playwright
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// Logs an error asynchronously
        /// Implement based on your logging framework (NLog, Serilog, etc.)
        /// </summary>
        public static async Task LogErrorAsync(Exception ex)
        {
            // Example: Using simple console logging
            await Task.Run(() => Console.WriteLine($"ERROR: {ex.Message}\n{ex.StackTrace}"));

            // Example: Using a logging framework like NLog
            // var logger = LogManager.GetCurrentClassLogger();
            // logger.Error(ex, ex.Message);

            // Example: Using Serilog
            // Log.Error(ex, "An error occurred");
        }

        /// <summary>
        /// Logs a warning message
        /// </summary>
        public static async Task LogWarningAsync(string message)
        {
            await Task.Run(() => Console.WriteLine($"WARNING: {message}"));
        }

        /// <summary>
        /// Logs an info message
        /// </summary>
        public static async Task LogInfoAsync(string message)
        {
            await Task.Run(() => Console.WriteLine($"INFO: {message}"));
        }
    }

    /// <summary>
    /// Additional Helper Methods for Common Playwright Operations
    /// </summary>
    public static class PlaywrightHelpers
    {
        /// <summary>
        /// Waits for a specific URL to load
        /// </summary>
        public static async Task WaitForUrl(IPage page, string urlPattern, int timeout = 30000)
        {
            await page.WaitForURLAsync(urlPattern, new PageWaitForURLOptions { Timeout = timeout });
        }

        /// <summary>
        /// Fills a text input field with value
        /// </summary>
        public static async Task FillInputField(ILocator locator, string value, int timeout = 30000)
        {
            await locator.FillAsync(value, new LocatorFillOptions { Timeout = timeout });
        }

        /// <summary>
        /// Gets text content from an element
        /// </summary>
        public static async Task<string> GetElementText(ILocator locator)
        {
            return await locator.TextContentAsync();
        }

        /// <summary>
        /// Selects an option from a dropdown by text
        /// </summary>
        public static async Task SelectDropdownByText(ILocator selectLocator, string text, int timeout = 30000)
        {
            await selectLocator.SelectOptionAsync(text, new LocatorSelectOptionOptions { Timeout = timeout });
        }

        /// <summary>
        /// Selects an option from a dropdown by value attribute
        /// </summary>
        public static async Task SelectDropdownByValue(ILocator selectLocator, string value, int timeout = 30000)
        {
            await selectLocator.SelectOptionAsync(value, new LocatorSelectOptionOptions { Timeout = timeout });
        }

        /// <summary>
        /// Waits for element and clicks it
        /// </summary>
        public static async Task WaitAndClick(ILocator locator, int timeout = 30000)
        {
            await locator.WaitForAsync(new LocatorWaitForOptions { Timeout = timeout });
            await locator.ClickAsync();
        }

        /// <summary>
        /// Double-clicks an element
        /// </summary>
        public static async Task DoubleClick(ILocator locator)
        {
            await locator.DblClickAsync();
        }

        /// <summary>
        /// Right-clicks (context menu) on an element
        /// </summary>
        public static async Task RightClick(ILocator locator)
        {
            await locator.ClickAsync(new LocatorClickOptions { Button = MouseButton.Right });
        }

        /// <summary>
        /// Hovers over an element
        /// </summary>
        public static async Task HoverElement(ILocator locator)
        {
            await locator.HoverAsync();
        }

        /// <summary>
        /// Checks if element exists in DOM
        /// </summary>
        public static async Task<bool> ElementExists(ILocator locator)
        {
            try
            {
                await locator.WaitForAsync(new LocatorWaitForOptions { Timeout = 1000 });
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets count of elements matching locator
        /// </summary>
        public static async Task<int> GetElementCount(ILocator locator)
        {
            return await locator.CountAsync();
        }
    }
}