using Microsoft.Playwright;

namespace PlaywrightFramework.Pages
{
    public class MasterPagePOM : BasePage
    {
        public MasterPagePOM(IPage page, string baseUrl) : base(page, baseUrl) { }

        // Locators
        private const string MenuButton = "button[aria-label*='Menu'], button[title*='Menu'], button:has([data-testid*='menu']), [role='button']:near([aria-label*='avatar']):visible";
        private const string ContactPersonButton = "//div[contains(text(),'Contact person')]";
        private const string ImportFromCitizenRegisterButton = "//span[contains(text(),'Import from citizen register')]";
        private const string SearchInPeopleRegisterDropdown = "input[placeholder*='Search in people register'], input[aria-label*='Search in people register'], input[name*='search']";
        private const string ImportContactButton = "button:has-text('Import contact')";
        private const string FinishButton = "#PlaceHolderMain_MainView_WizardFinishButton";
        private const string SaveAsNewButton = "button:has-text('Save as new')";
        private const string PossibleDuplicatePopup = "text=Possible duplicate in the contact list";
        private const string ContactIframe = "iframe[src*='/locator/CRM/Contact/New']";
        private const string ContactPersonIframe = "iframe[src*='/locator/CRM/Contact/New']";
        private const string EmailInput = "input[type='email'], input[name='loginfmt'], input#i0116";
        private const string NextButton = "input[type='submit'], button:has-text('Next'), input#idSIButton9";
        private const string PasswordInput = "input[type='password'], input[name='passwd'], input#i0118";
        private const string SignInButton = "input[type='submit']:has-text('Sign in'), button:has-text('Sign in'), input#idSIButton9";
        private const string StaySignedInButton = "input[type='submit']:has-text('Yes'), button:has-text('Yes'), input#idSIButton9";
        private const string StaySignedInPopup = "div:has-text('Do this to reduce the number of times you are asked to sign in.')";
        private const string WelcomeMessage = "text=Welcome to a new version of 360°";
        private const string TakeBackTo360Button = "button:has-text('I'm done, take me back to 360°')";
        private const string CaseButton = "//button[@type='button']//div[text()='Case']";
        private const string CaseIframe = "iframe[title*='New case'], iframe[title*='Case']";
        private const string DocumentIframe = "iframe[src*='/locator/DMS/Dialog/TemplateFilterDialog']";
        private const string StandInDesktopButton = "iframe[src *= '/DesktopManagement/AddDesktopShortCut']";
        private const string DetailsText = "text=Details";
        private const string GlobalSearchButton = "button[aria-label*='Global search'], button[title*='Global search'], button:has-text('Global search')";
        private const string SelectDocumentType = "//a[contains(text(),'Internal memo without follow-up, Case document')]";
        private const string OKButton = "//span[contains(text(),'OK')]";
        private const string AdvanceSearchButton = "//button[contains(text(),'Advanced search')]";


       


        //Actions 

        public async Task ClickMenuButton()
        {
            await Task.Delay(500); // Human-like delay
            var menuButton = Page.Locator(MenuButton);
            await menuButton.ClickAsync();
            await Task.Delay(1000);
        }

        public async Task ClickCaseButton()
        {
            await Task.Delay(500); // Human-like delay
            var caseButton = Page.Locator(CaseButton);
            await caseButton.ClickAsync();
            await Task.Delay(1000);
        }

        public async Task EnterCaseTitle(string title)
        {
            var frame = Page.FrameLocator(CaseIframe).First;
            await Task.Delay(500);

            var titleField = frame.GetByRole(AriaRole.Textbox, new() { Name = "Title" });
            await titleField.ClickAsync();
            await Task.Delay(300);
            await titleField.FillAsync(title);
            await Task.Delay(500);
        }
        public async Task EnterResponsible(string responsible)
        {
            var frame = Page.FrameLocator(CaseIframe).First;
            await Task.Delay(500);

            var responsibleField = frame.GetByRole(AriaRole.Textbox, new() { Name = "Responsible" });
            await responsibleField.ClickAsync();
            await Task.Delay(300);
            await responsibleField.FillAsync(responsible);
            await Task.Delay(500);
            await responsibleField.PressAsync("Enter");
            await Task.Delay(1000);

            // If Class field is displayed then add value in class field as well and hit enter elese continue with next step
            var classField = frame.GetByRole(AriaRole.Textbox, new() { Name = "Class" });
            if (await classField.IsVisibleAsync())
            {
                await classField.ClickAsync();
                await Task.Delay(300);
                await classField.FillAsync("000");
                await Task.Delay(500);
                await classField.PressAsync("Enter");
                await Task.Delay(1000);
                await classField.PressAsync("Enter");              
            }




        }
        public async Task<bool> IsFinishButtonDisplayed()
        {
            try
            {
                var frame = Page.FrameLocator(CaseIframe).First;
                var finishButton = frame.GetByRole(AriaRole.Button, new() { Name = "Finish" });
                await finishButton.WaitForAsync(new() { Timeout = 10000 });
                return await finishButton.IsVisibleAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task ClickFinishButton()
        {
            var frame = Page.FrameLocator(CaseIframe).First;
            await Task.Delay(500);

            var finishButton = frame.GetByRole(AriaRole.Button, new() { Name = "Finish" });
            await finishButton.ClickAsync();
            await Task.Delay(1000);
        }
        public async Task<bool> IsCaseCreatedTextDisplayed(string caseTitle)
        {
            // Focus on main page content
            await Task.Delay(2000);
            try
            {
                var caseTitleSelector = $"text={caseTitle}";
                // Use Page directly to ensure main content search, not iframe
                var mainPageLocator = Page.Locator(caseTitleSelector).First;

                await mainPageLocator.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 15000
                });

                return await mainPageLocator.IsVisibleAsync();
            }
            catch
            {
                return false;
            }
        }


    }
}