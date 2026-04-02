using NUnit.Framework;
using PlaywrightFramework.Fixtures;
using PlaywrightFramework.Pages;
using PlaywrightFramework.src.Pages.Case;
using PlaywrightFramework.Utils;

namespace PlaywrightFramework.Tests
{
    [TestFixture]
    public class CreateCase : BaseTest
    {
        string caseTitle = "AI agent case " + System.Guid.NewGuid().ToString();
        string firstname = "AI Agent " + System.Guid.NewGuid().ToString().Substring(0, 2);
        string lastname = "Lastname " + System.Guid.NewGuid().ToString().Substring(0, 2);
        [SetUp]
        public async Task LoginFirst()
        {
            await LoginHelper.LoginToApplicationAsync(Page, BaseUrl);
        }
   
        public async Task LoginToApplicationAndSwitchUser()
        {
            // Step 1: Login as Registrar and create a case
            await LoginHelper.LoginToApplicationAsync(Page, BaseUrl, UserRole.Registrar);
            // ... perform action (create case, submit for approval, etc.)
           
            // Step 2: Switch to CaseHandler and approve the action
            await LoginHelper.SwitchUserAsync(Page, BaseUrl, UserRole.CaseHandler);
            
            // Step 3: Switch back to Registrar and verify approval
            await LoginHelper.SwitchUserAsync(Page, BaseUrl, UserRole.Registrar);
           
        }
        [Test]
        [Category("Case")]
        public async Task CreatecaseFromMainMenu()
        {
             // Step 1: Login as Registrar and create a case
            await LoginHelper.LoginToApplicationAsync(Page, BaseUrl, UserRole.CaseHandler);
            var masterpagepom = new MasterPagePOM(Page, BaseUrl);
            // Click on Main Menu button
            await masterpagepom.ClickMenuButton();
            TestContext.WriteLine("Clicked Main Menu button");
            // Step 8: Click on Case
            await masterpagepom.ClickCaseButton();
            TestContext.WriteLine("Clicked Case button under Create new section");

            // Enter Title - Case created from AI agent
            await masterpagepom.EnterCaseTitle(caseTitle);
            TestContext.WriteLine($" Entered case title: {caseTitle}");

            // Enter responsible person as "Caseworker" and hit enter
            await masterpagepom.EnterResponsible(ConstantValues.ResponsibleCH1);
            TestContext.WriteLine($"Entered responsible person: {ConstantValues.ResponsibleCH1}");

            // Verify Finish button should be displayed
            bool isFinishButtonDisplayed = await masterpagepom.IsFinishButtonDisplayed();
            Assert.IsTrue(isFinishButtonDisplayed, "Finish button should be displayed");
            TestContext.WriteLine(" Verified Finish button is displayed");

            // Click on Finish button
            await masterpagepom.ClickFinishButton();
            TestContext.WriteLine(" Clicked Finish button");          

            // Verify "Case created from AI agent" text is displayed on the page
            bool isCaseCreatedTextDisplayed = await masterpagepom.IsCaseCreatedTextDisplayed(caseTitle);
            Assert.IsTrue(isCaseCreatedTextDisplayed, $"The text '{caseTitle}' should be displayed on the page.");
            TestContext.WriteLine($"Verified the text '{caseTitle}' is displayed on the page.");
        }
        [Test]
        [Category("Case")]
        public async Task Case_EditCase()
        {     
              await CreatecaseFromMainMenu();

            //Click on case function option
            var caseDetailsPOM = new CaseDetailsPOM(Page, BaseUrl);
            await caseDetailsPOM.ClickCaseFunctionButtonAndEditcaseTitle();
            TestContext.WriteLine("Clicked on Case function and title updated ");

        }

        [Test]
        [Category("Case")]
        public async Task CreateCaseFromExistingCase()
        {
            //create a Case
            await CreatecaseFromMainMenu();

            //Click on case function and Click on Save as option from list
            var caseDetailsPOM = new CaseDetailsPOM(Page, BaseUrl);
            await caseDetailsPOM.ClickCaseFunctionButtonAndSaveAs();
            TestContext.WriteLine("Clicked on Case function and Save as option from list");
            //Verify Case Title is New 
            await caseDetailsPOM.VerifyCaseTitleIsUpdated();
            TestContext.WriteLine("case Title is updated and New case created from existing Case");
        }

        [Test]
        [Category("Case")]
        public async Task Case_SwitchTocaseTabView()
        {
            //create a Case
            await CreatecaseFromMainMenu();

            //Click on case function and Click on Save as option from list
            var caseDetailsPOM = new CaseDetailsPOM(Page, BaseUrl);
            await caseDetailsPOM.ClickOnMoreButton();
            TestContext.WriteLine("Clicked on More button to switch case tab view");

            //Select Option from More button
            await caseDetailsPOM.SelectOptionFromMoreButton("Switch to tab view");

            // Verify "Show overview" text is displayed which is under span
            await Task.Delay(3000);
            bool isShowOverviewTextDisplayed = await caseDetailsPOM.IsShowOverviewTextDisplayed();
            await Task.Delay(3000);
            Assert.IsTrue(isShowOverviewTextDisplayed, "Show overview text should be displayed in tab view");
            TestContext.WriteLine("Verified Show overview text is displayed in tab view");

        }

        [Test]
        [Category("Case")]
        public async Task Case_SearchCaseFromAdvanceSearch()
        {
            await LoginHelper.LoginToApplicationAsync(Page, BaseUrl, UserRole.CaseHandler);
            await Task.Delay(2000);
            var caseDetailsPOM = new CaseDetailsPOM(Page, BaseUrl);
            // Click "Advanced search" button 
            await caseDetailsPOM.ClickAdvancedSearchButtonAndSelectOption("Case");
            await Task.Delay(1000);
            TestContext.WriteLine("Clicked on Advanced search button");

            await caseDetailsPOM.ClickOnSearchButton();
            TestContext.WriteLine("Clicked on search button");

            await caseDetailsPOM.VerifyCaseListIsDisplayed();
            TestContext.WriteLine("Case List is displayed");

        }

        [Test]
        [Category("Case")]
        public async Task Case_VerifyCopyHyperLinkMessageForCase()
        {          
            await CreatecaseFromMainMenu();
         
            var caseDetailsPOM = new CaseDetailsPOM(Page, BaseUrl); 
            // Click on case function option
            await caseDetailsPOM.ClickOnCaseFunctionsButtonAndSelectOption("Copy hyperlink");
            await Task.Delay(1000);
            TestContext.WriteLine("Clicked on Case function and Copy hyperlink option from list");

            // Click on Copy Hyperlink button and verify "Link copied to clipboard" message
            bool isLinkCopiedMessageDisplayed = await caseDetailsPOM.IsLinkCopiedMessageDisplayed();
            Assert.IsTrue(isLinkCopiedMessageDisplayed, "The hyperlink is successfully copied to the clipboard message should be displayed");
            TestContext.WriteLine("Verified 'The hyperlink is successfully copied to the clipboard' message is displayed after clicking Copy Hyperlink option");
        }

        [Test]
        [Category("Case")]
        public async Task Case_UpdateCaseStatusToClosed()
        {
           // await LoginHelper.SwitchUserAsync(Page, BaseUrl, UserRole.Registrar);
            await CreatecaseFromMainMenu();

            var caseDetailsPOM = new CaseDetailsPOM(Page, BaseUrl);
            // Click on Set status button and select Closed option
            await caseDetailsPOM.ClickOnSetStatusButtonAndSelectOption("6");
            TestContext.WriteLine("Clicked on Set status button and selected Closed option");

        }

        [Test]
        [Category("Case")]
        public async Task Case_VerifyProduceCaseSummary()
        {
            await CreatecaseFromMainMenu();

            var caseDetailsPOM = new CaseDetailsPOM(Page, BaseUrl);

            // Click on case function option
            await caseDetailsPOM.ClickOnCaseFunctionsButtonAndSelectOption("Produce case summary");
            await Task.Delay(1000);
            TestContext.WriteLine("Clicked on Case function and Copy hyperlink option from list");

            // Click on Produce button on Popup and verify case summary is generated
            await caseDetailsPOM.ClickOnProduceButtonOfCaseSummaryPopup();
            TestContext.WriteLine("Clicked on Produce button of Case summary popup");

        }
    }
}