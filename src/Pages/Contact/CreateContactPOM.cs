using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightFramework.Pages;

namespace PlaywrightFramework.src.Pages.Contact
{
    public class CreateContactPOM : BasePage
    {
        public CreateContactPOM(IPage page, string baseUrl) : base(page, baseUrl) { }

        // Locators
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

        // Actions

        public async Task ClickContactPerson()
        {
            await Task.Delay(500); // Human-like delay
            var contactPersonButton = Page.Locator(ContactPersonButton);
            await contactPersonButton.ClickAsync();
            await Task.Delay(1000);
        }

        public async Task SelectContactpersonType( string contactType)
        { 
            await Task.Delay(500); // Human-like delay
            // I want to parameterize this method to select any contact person type based on the input
            var contactTypeButton = Page.Locator($"//div[contains(text(),'{contactType}')]");
            await contactTypeButton.ClickAsync();
            TestContext.WriteLine($"Selected contact type: {contactType}");
        }

        /// <summary>
        /// Enters first name in the Contact Person iframe
        /// </summary>
        public async Task EnterEnterMandatoryDetails(string firstName , string lastName , string enterprise, string contactType)
        {
            var frame = Page.FrameLocator(ContactPersonIframe).First;
            await Task.Delay(500);

            var firstNameField = frame.GetByRole(AriaRole.Textbox, new() { Name = "First name" });
            await firstNameField.ClickAsync();
            await Task.Delay(500);
            await firstNameField.FillAsync(firstName);
           // await firstNameField.PressAsync("Enter");
            await Task.Delay(500);        

            var lastNameField = frame.GetByRole(AriaRole.Textbox, new() { Name = "Last name" });
            await lastNameField.ClickAsync();
            await Task.Delay(500);
            await lastNameField.FillAsync(lastName);
           // await lastNameField.PressAsync("Enter");
            await Task.Delay(500);

            if (contactType == "Contact person")
            {
                var enterpriseField = frame.GetByRole(AriaRole.Textbox, new() { Name = "Enterprise" });
                await enterpriseField.ClickAsync();
                await Task.Delay(500);
                await enterpriseField.FillAsync(enterprise);
                await Task.Delay(500);
                await enterpriseField.PressAsync("Enter");
                await Task.Delay(500);
                await enterpriseField.PressAsync("Enter");
                await Task.Delay(1000);
            }             
            var finishButton = frame.GetByRole(AriaRole.Button, new() { Name = "Finish" });
            await finishButton.ClickAsync();
            await Task.Delay(1000);
        }
    }
}
