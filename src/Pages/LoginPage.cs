using Microsoft.Playwright;
using PlaywrightFramework.Utils;

namespace PlaywrightFramework.Pages
{
    /// <summary>
    /// Page Object Model for the Login page.
    /// Update selectors to match your actual application's HTML.
    /// </summary>
    public class LoginPage : BasePage
    {
        // ─── Selectors ────────────────────────────────────────────────────────
        // 💡 Update these selectors to match your real application
        private const string EmailInput = "input[type='email'], input[name='loginfmt'], input#i0116";
        private const string NextButton = "input[type='submit'], button:has-text('Next'), input#idSIButton9";
        private const string PasswordInput = "input[type='password'], input[name='passwd'], input#i0118";
        private const string SignInButton = "input[type='submit']:has-text('Sign in'), button:has-text('Sign in'), input#idSIButton9";
        private const string StaySignedInButton = "input[type='submit']:has-text('Yes'), button:has-text('Yes'), input#idSIButton9";
        private const string StaySignedInPopup = "div:has-text('Do this to reduce the number of times you are asked to sign in.')";
        private const string WelcomeMessage = "text=Welcome to a new version of 360°";
        private const string TakeBackTo360Button = "button[id='navigate-back-btn']";
        // Add these methods to the LoginPage class

        private const string ErrorMessageSelector = ".error-message, .validation-error, .alert-danger"; // Adjust selector as needed

        // ─── Constructor ──────────────────────────────────────────────────────
        public LoginPage(IPage page, string baseUrl) : base(page, baseUrl) { }

        // ─── Page Actions ─────────────────────────────────────────────────────

        /// <summary>Navigate to the login page</summary>
        public async Task NavigateAsync()
        {
            Logger.Info("📄 Navigating to Login page");
            await GoToAsync("/login");

            
            


        }

       
        public async Task WaitForSuccessfulLoginAsync()
        {
            Logger.Info("⏳ Waiting for dashboard redirect...");
            await WaitForUrlAsync("/dashboard");
        }

        public async Task<bool> IsErrorMessageVisibleAsync()
        {
            return await IsVisibleAsync(ErrorMessageSelector);
        }

        public async Task<string> GetErrorMessageAsync()
        {
            return await GetTextAsync(ErrorMessageSelector);
        }
        public async Task LoginAsync(string username, string password)
        {
            // Fill email/username
            await FillAsync(EmailInput, username, "Enter username/email");
            // Click next if needed
            if (await IsVisibleAsync(NextButton))
            {
                await ClickAsync(NextButton, "Click Next");
            }
            // Fill password
            await FillAsync(PasswordInput, password, "Enter password");
            // Click sign in
            await ClickAsync(SignInButton, "Click Sign In");
            // Handle "Stay signed in?" popup if it appears
            if (await IsVisibleAsync(StaySignedInPopup))
            {
                await ClickAsync(StaySignedInButton, "Click Yes on Stay Signed In");
            }
        }
        public async Task<bool> IsLoginPageLoadedAsync()
        {
            // Check for presence of the email input as a proxy for login page loaded
            return await IsVisibleAsync("input[type='email'], input[name='loginfmt'], input#i0116");
        }
    }
}
