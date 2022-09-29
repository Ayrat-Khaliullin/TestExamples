using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using UITests.Utilities.Bases;

namespace UITests.Pages
{
    public class GoogleMainPage : AbstractPage
    {
        private IWebDriver webDriver;
        public GoogleMainPage(BaseDriver driver) : base(driver)
        {
            webDriver = driver.Driver;
            driver.GoToUrl("https://www.google.com/");
        }

        /// <summary>
        /// Find Google logo
        /// </summary>
        /// <returns></returns>
        internal bool FindLogo()
        {
            var logoElements = webDriver.FindElements(By.XPath($".//img[contains(@alt,'Google')]"));
            return logoElements.Count != 0;
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
            try
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
                    webDriverInstance.WaitUntilElementVisible(By.CssSelector("body"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught", ex);
            }
        }
    }
}
