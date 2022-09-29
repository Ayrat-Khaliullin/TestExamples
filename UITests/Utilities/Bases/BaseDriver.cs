using System;
using System.IO;
using System.Text;
using Allure.Commons;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace UITests.Utilities.Bases
{
    public class BaseDriver
    { 
        public readonly IWebDriver Driver;
        public readonly string DownloadPath;
        public readonly string RemoteWdUri;
        private static int _screenShotCount = 0;

        public BaseDriver()
        {
            var platform = Environment.OSVersion.Platform;
            if (platform == PlatformID.Win32NT)
                DownloadPath = Environment.CurrentDirectory + "\\downloads";
            else
                DownloadPath = Environment.CurrentDirectory + "/downloads";
            RemoteWdUri = new ConfigReader().GetValue("RemoteWdUri");
            Driver = WebDriver;
        }
        
        public void GoToUrl(string url)
        {
            Driver.Url = url;
        }

        public string GetUrl()
        {
            return Driver.Url;
        }
        

        public bool IsClickable(IWebElement element)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(1));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
                return true;
            }
            catch (Exception e)
            {
                AttachScreenToReport();
                AttachResToReport(e.Message, "text/html", "html");
                return false;
            }
        }


        /// <summary>
        /// Attaching response log to Allure Report
        /// </summary>
        /// <param name="res">log data</param>
        /// <param name="type">MIME-type</param>
        /// <param name="extension">Log file extension</param>
        /// <param name="name">filename</param>
        public void AttachResToReport(string res, string type, string extension, string name = "Response_Log")
        {
            var log = Encoding.UTF8.GetBytes(res);
            try
            {
                AllureLifecycle.Instance?.AddAttachment(name, type, log, extension);
            }
            catch
            {
            }
        }

        
        #region Private
        private RemoteWebDriver WebDriver => _webDriver ?? StartWebDriver();

        private RemoteWebDriver _webDriver;

        private RemoteWebDriver StartWebDriver()
        {
            if (_webDriver != null)
                return _webDriver;
            
                try
                {
                    _webDriver = StartBrowser();
                    return _webDriver;
                }
                catch (Exception e)
                {
                        throw new Exception($"Browser launching is failed; {e.Message}"); 
                }
        }

        private RemoteWebDriver StartBrowser()
        {
            var reader = new ConfigReader();
            ChromeOptions options;
            
                options = new ChromeOptions();
                options.AddArgument("--no-sandbox");
                options.AddAdditionalCapability("version", "80.0", true);
            options.AddUserProfilePreference("download.default_directory", "/home/selenium/Downloads");
            options.AddArgument("start-maximized");
            options.AddArgument("ignore-certificate-errors");
            options.AddAdditionalCapability("platform", "Any", true);
            options.AddAdditionalCapability("enableVNC", true, true);
            options.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name+" | Started at "+DateTime.Now.ToString("HH:mm:ss"), true);
            
            var driver = new RemoteWebDriver(new Uri(RemoteWdUri + "wd/hub"), options.ToCapabilities(), TimeSpan.FromMinutes(9));
            
            return driver;
        }
        #endregion

        #region Public

        public void WaitUntilElementVisible(By by, int time = 600)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(time));
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
            }
            catch (Exception e)
            {
                AttachScreenToReport();
                AttachResToReport(e.Message, "text/html", "html");
                throw;
            }
        }
        #endregion

        #region Screenshots
        /// <summary>
        /// Attaching screenshot to Allure Report
        /// </summary>
        /// <returns>screenshot name</returns>
        public void AttachScreenToReport()
        {
            byte[] img = TakeScreenshot();

            try
            {
                var screenshotName = $"ScreenShot _ {DateTime.Now.Hour}:{DateTime.Now.Minute} _ id: {_screenShotCount++}";
                AllureLifecycle.Instance.AddAttachment(screenshotName, "image/png", img, "png");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Makes a screenshot
        /// </summary>
        /// <returns>screenshot as a byte array</returns>
        private byte[] TakeScreenshot()
        {
            IWebDriver driver = WebDriver;
            try
            {
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                byte[] screenshotAsByteArray = ss.AsByteArray;
                return screenshotAsByteArray;
            }
            catch (Exception e)
            {
                throw new Exception($"Can't take a screenshot; {e.Message}");
            }
        }
        #endregion
        public void Dispose()
        {
            Driver.Quit();
            Driver.Dispose();
        }

    }
}