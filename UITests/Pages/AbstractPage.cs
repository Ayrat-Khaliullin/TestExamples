using OpenQA.Selenium.Support.UI;
using System;
using UITests.Utilities.Bases;
using UITests.Utilities;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace UITests.Pages
{
   
    public abstract class AbstractPage 
    {

        public BaseDriver webDriverInstance;

        public  WebDriverWait wait { get; set; }
        public AbstractPage(BaseDriver webDriverInstance)
        {
            this.webDriverInstance = webDriverInstance;
            webDriverInstance.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            wait = new WebDriverWait(webDriverInstance.Driver, TimeSpan.FromSeconds(60));

        }
        public void ScreenshotMakingStep()
        {
                webDriverInstance.AttachScreenToReport();
        }
    }
}
