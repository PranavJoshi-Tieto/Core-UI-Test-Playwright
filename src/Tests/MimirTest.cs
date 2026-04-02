using NUnit.Framework;
using PlaywrightFramework.Fixtures;
using PlaywrightFramework.Pages;
using PlaywrightFramework.src.Pages.Mimir;
using PlaywrightFramework.Utils;

namespace PlaywrightFramework.Tests
{
    [TestFixture]
    public class MimirTest : BaseTest
    {
        [SetUp]
        public async Task LoginFirst()
        {
            await LoginHelper.LoginToApplicationAsync(Page, BaseUrl);
        }

        [Test]
        [Category("Mimir")]

        public async Task Mimir_AskQuestionAndVerify()
        {
            var mimirPOM = new MimirPOM(Page, BaseUrl);

            // Click on 360° Mimir Icon from Homepage
            await mimirPOM.ClickMimirIcon();
            TestContext.WriteLine(" Clicked on 360° Mimir Icon");

            // Click on "Ask your questions here (do not enter sensitive data)" input box
            await mimirPOM.ClickMimirQuestionInput();
            TestContext.WriteLine(" Clicked on Mimir question input box");

            // Send keys and press Enter
            await mimirPOM.EnterMimirQuestion("How to create a Case?");
            TestContext.WriteLine(" Entered question 'How to create a Case?' and pressed Enter");

            // Wait for response         
            TestContext.WriteLine("Mimir AI Assistant interaction completed");
        }

        [Test]
        [Category("Mimir")]
        public async Task Mimir_VerifyClearChatHistory()
        {
            var mimirPOM = new MimirPOM(Page, BaseUrl);

            // Click on 360° Mimir Icon from Homepage
            await mimirPOM.ClickMimirIcon();
            TestContext.WriteLine(" Clicked on 360° Mimir Icon");

            // Click on "Ask your questions here (do not enter sensitive data)" input box
            await mimirPOM.ClickMimirQuestionInput();
            TestContext.WriteLine(" Clicked on Mimir question input box");

            // Send keys and press Enter
            await mimirPOM.ClearChatHistory("How to create a Case?");
            TestContext.WriteLine(" Entered question 'How to create a Case?' and pressed Enter");
            TestContext.WriteLine(" Clicked on Clear chat history");

            // Wait for response          
            TestContext.WriteLine("Mimir AI Assistant interaction completed");
        }

        [Test]
        [Category("Mimir")]
        public async Task Mimir_VerifyLikeDislikeAnswer()
        {
            var mimirPOM = new MimirPOM(Page, BaseUrl);
            // Click on 360° Mimir Icon from Homepage
            await mimirPOM.ClickMimirIcon();
            TestContext.WriteLine(" Clicked on 360° Mimir Icon");

            // Click on "Ask your questions here (do not enter sensitive data)" input box
            await mimirPOM.ClickMimirQuestionInput();
            TestContext.WriteLine(" Clicked on Mimir question input box");

            // Send keys and press Enter
            await mimirPOM.EnterMimirQuestion("How to create a Case?");
            TestContext.WriteLine(" Entered question 'How to create a Case?' and pressed Enter");

            // Verify Like and Dislike button for the answer
            await mimirPOM.LikeDislikeAnswer();
            TestContext.WriteLine(" Entered question 'How to create a Case?' and pressed Enter");
            TestContext.WriteLine(" Clicked on Like and Dislike button for the answer");
            // Wait for response          
            TestContext.WriteLine("Mimir AI Assistant interaction completed");
        }
    }
}