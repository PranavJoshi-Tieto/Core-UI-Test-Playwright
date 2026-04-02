using NUnit.Framework;
using PlaywrightFramework.Fixtures;
using PlaywrightFramework.Pages;
using PlaywrightFramework.src.Pages.Contact;
using PlaywrightFramework.Utils;

namespace PlaywrightFramework.Tests
{
    [TestFixture]
    public class CreateContact : BaseTest
    {
        [SetUp]
        public async Task LoginFirst()
        {
            await LoginHelper.LoginToApplicationAsync(Page, BaseUrl);
        }
        // create GUID diffrent all the time for case title with prefix "AI agent case"
        string caseTitle = "AI agent case " + System.Guid.NewGuid().ToString();
        string firstname = "AI Agent " + System.Guid.NewGuid().ToString().Substring(0, 2);
        string lastname = "Lastname " + System.Guid.NewGuid().ToString().Substring(0, 2);


        [Test]
        [Category("Contact")]

        public async Task CreateContactPerson()
        {
            var createContactPOM = new CreateContactPOM(Page, BaseUrl);
            var masterPagePOM = new MasterPagePOM(Page, BaseUrl);

            // Click on Main Menu button
            await masterPagePOM.ClickMenuButton();
            TestContext.WriteLine("Clicked Main Menu button");

            //Click on Conatct Person under menu
            await createContactPOM.SelectContactpersonType("Contact person");
            TestContext.WriteLine("Clicked Contact Person under Contact section");

            // Navigate to iframe and fill contact details
            await createContactPOM.EnterEnterMandatoryDetails(firstname, lastname, "%SI", "Contact person");
            TestContext.WriteLine("All details added on Create Contact page");
            TestContext.WriteLine("Clicked Finish button");

            // Wait for page load after creating contact         
            TestContext.WriteLine("Contact Person creation completed");
        }

        [Test]
        [Category("Contact")]
        public async Task CreatePrivatePerson()
        {

            var createContactPOM = new CreateContactPOM(Page, BaseUrl);
            var masterPagePOM = new MasterPagePOM(Page, BaseUrl);

            // Click on Main Menu button
            await masterPagePOM.ClickMenuButton();
            TestContext.WriteLine("Clicked Main Menu button");

            //Click on Conatct Person under menu
            await createContactPOM.SelectContactpersonType("Private person");
            TestContext.WriteLine("Clicked Private Contact Person under Contact section");

            // Navigate to iframe and fill contact details
            await createContactPOM.EnterEnterMandatoryDetails(firstname, lastname, "%SI", "Private person");
            TestContext.WriteLine("All details added on Create Contact page");
            TestContext.WriteLine("Clicked Finish button");

            // Wait for page load after creating contact         
            TestContext.WriteLine("Contact Person creation completed");
        }

    }


}
