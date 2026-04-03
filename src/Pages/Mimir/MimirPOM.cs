using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightFramework.Pages;
using static PlaywrightFramework.src.Pages.Case.CaseDetailsPOM;

namespace PlaywrightFramework.src.Pages.Mimir
{
    public class MimirPOM : BasePage
    {
        public MimirPOM(IPage page, string baseUrl) : base(page, baseUrl) { }


        // Locators
        // Mimir AI Assistant section
        private const string MimirIcon = "button[aria-label*='Mimir'], button[title*='Mimir'], button:has-text('Mimir'), [aria-label*='AI assistant']";
        private const string MimirQuestionInput = "input[placeholder*='Ask your questions here'], input[placeholder*='do not enter sensitive data'], textarea[placeholder*='Ask your questions']";

        // Mimir AI Assistant section

        /// <summary>
        /// Click on 360° Mimir Icon from Homepage
        /// </summary>
        public async Task ClickMimirIcon()
        {
            await Task.Delay(500); // Human-like delay
            var mimirIcon = Page.Locator(MimirIcon);
            await mimirIcon.ClickAsync();
            await Task.Delay(1000);
            TestContext.WriteLine("Clicked on 360° Mimir Icon");
        }

        /// <summary>
        /// Click on Mimir question input box
        /// </summary>
        public async Task ClickMimirQuestionInput()
        {
            await Task.Delay(500); // Human-like delay
            var questionInput = Page.Locator(MimirQuestionInput);
            await questionInput.ClickAsync();
            await Task.Delay(500);
            TestContext.WriteLine("Clicked on Mimir question input box");
        }

        /// <summary>
        /// Enter question in Mimir input box and press Enter
        /// </summary>
        public async Task EnterMimirQuestion(string question)
        {
            await Task.Delay(500);
            var questionInput = Page.Locator(MimirQuestionInput);
            await FieldActions.ClickAndSendKeys(questionInput, question, 50);
            await Task.Delay(500);
            await questionInput.PressAsync("Enter");
            await Task.Delay(2000);
            TestContext.WriteLine($"Entered question in Mimir: {question}");
            await Task.Delay(9000);
            // Verify text from mimir anwer. verify this text- "AI-generated responses can be incorrect."
            // var mimirAnswer = Page.Locator("text=AI-generated responses can be incorrect");
            // bool isAnswerVisible = await mimirAnswer.IsVisibleAsync();

            var mimirAnswer = Page.Locator("text=AI-generated responses can be incorrect");
            await mimirAnswer.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 30000 // 30 seconds timeout
            });
            bool isAnswerVisible = await mimirAnswer.IsVisibleAsync();
            Assert.IsTrue(isAnswerVisible, "Mimir answer is not visible");
            TestContext.WriteLine("Verified Mimir answer is displayed");

            // refresh the page to clear the mimir answer
            await Page.ReloadAsync();

        }

        public async Task ClearChatHistory(string question)
        {
            await Task.Delay(500);
            var questionInput = Page.Locator(MimirQuestionInput);
            await FieldActions.ClickAndSendKeys(questionInput, question, 50);
            await Task.Delay(500);
            await questionInput.PressAsync("Enter");
            await Task.Delay(2000);
            TestContext.WriteLine($"Entered question in Mimir: {question}");

            var mimirAnswer = Page.Locator("text=AI-generated responses can be incorrect");
            await mimirAnswer.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 30000 // 30 seconds timeout
            });
            bool isAnswerVisible = await mimirAnswer.IsVisibleAsync();

            // CLick on Clear chat history button which has aria-label- "Clear chat history"
            var clearChatButton = Page.Locator("button[aria-label*='Clear chat history'], button:has-text('Clear chat history')");
            await clearChatButton.ClickAsync();
            await Task.Delay(4000);

            // Verify text from mimir anwer. verify this text- "AI-generated responses can be incorrect."
            var mimirAnswer1 = Page.Locator("text=AI-generated responses can be incorrect");
            bool isAnswerVisible1 = await mimirAnswer1.IsVisibleAsync();
            Assert.IsFalse(isAnswerVisible1, "Mimir answer is visible");
            TestContext.WriteLine("Verified Mimir answer is clear and History is clear");

        }

        public async Task LikeDislikeAnswer()
        {
            await Task.Delay(500);
            // Click on Like button for the answer. The Like button has aria-label "Like answer"
            var likeButton = Page.Locator("button[aria-label*='Like this answer'], button:has-text('Like this answer')");
            //await likeButton.ClickAsync();
            await Task.Delay(2000);
            TestContext.WriteLine("Clicked on Like button for the answer");
            // Click on Dislike button for the answer. The Dislike button has aria-label "Dislike answer"
            var dislikeButton = Page.Locator("button[aria-label*='Dislike this answer'], button:has-text('Dislike this answer')");
            //await dislikeButton.ClickAsync();
            await Task.Delay(2000);
            TestContext.WriteLine("Clicked on Dislike button for the answer");

        }
    }
}