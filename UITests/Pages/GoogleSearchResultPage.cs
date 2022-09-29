
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using UITests.Utilities.Bases;

namespace UITests.Pages
{
    public class GoogleSearchResultPage
    {
        private IWebDriver webDriver;
        public GoogleSearchResultPage(BaseDriver driver) 
        {
            webDriver = driver.Driver;
        }

        /// <summary>
        /// Find search phrase in search input
        /// </summary>
        /// <returns></returns>
        internal bool FindSearchPhrase(string searchPhrase)
        {
            string inputPhrase = null; 
             
                if (FindSearchForm())
            {
                 inputPhrase = webDriver.FindElement(By.XPath($".//form[contains(@role,'search')]//input[contains(@type,'text')]")).GetAttribute("value");

            }
            return searchPhrase == inputPhrase;
        }

        /// <summary>
        /// Find Results
        /// </summary>
        /// <returns></returns>
        internal bool FindResults()
        {
            var Forms = webDriver.FindElements(By.XPath($".//div[contains(@id,'res') and contains(@role,'main')]"));
            return Forms.Count != 0;
        }

        /// <summary>
        /// Find Search form
        /// </summary>
        /// <returns></returns>
        internal bool FindSearchForm()
        {
            var Forms = webDriver.FindElements(By.XPath($".//form[contains(@role,'search') and .//input[contains(@type,'text')]]"));
            return Forms.Count != 0;
        }

        /// <summary>
        /// Search the phrase
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        internal void Search(string text)
        {
            if (FindSearchForm())
            {
                var searchInput = webDriver.FindElement(By.XPath($".//form[contains(@role,'search')]//input[contains(@type,'text')]"));
                new Actions(webDriver)
                    .MoveToElement(searchInput)
                    .Click()
                    .SendKeys(text)
                    .SendKeys(Keys.Enter)
                    .Build()
                    .Perform();
            }
        }
    }
}
