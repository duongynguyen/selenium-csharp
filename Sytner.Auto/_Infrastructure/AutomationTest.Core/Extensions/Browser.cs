using System;
using System.Linq;
using AutomationTest.Core.Action;
using AutomationTest.Core.Configuration;
using AutomationTest.Core.Report;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using AutomationTest.Share.Utils;
// Done
namespace AutomationTest.Core.Extensions
{
    public class Browser
    {
        public Log log { get; set; }
        private AppConfiguration _appConfiguration;

        public Browser()
        {
            Log = new Log();
            _appConfiguration = new AppConfiguration();
        }

        public IWebDriver Launch(string browser, string site)
        {
            IWebDriver driver = null;
            try
            {
                string pathDriver = CommonHelper.GetPathFolder(_appConfiguration.PathDriver);

                if(browser.ToLower().Equals("firefox"))
                {
                    var options = new FirefoxOptions();
                    options.BrowserExecutableLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe";
                    var service = FirefoxDriverService.CreateDefaultService(pathDriver, "geckodriver.exe");
                    service.HideCommandPromptWindow = true;
                    driver = new FirefoxDriver(service, options, TimeSpan.FromSeconds(15));

                    if(VersionBrowsers.Firefox == null)
                    {
                        ICapabilities cap = ((RemoteWebDriver)driver).Capabilities;
                        string uAgent = (string)((IJavaScriptExecutor)driver).ExecuteScript("return navigator.userAgent;");
                        VersionBrowsers.Firefox = uAgent.Split('/').Last().Split('.')[0];
                    }
                }
                else if(browser.ToLower().Equals("chrome"))
                {
                    ChromeOptions options = new ChromeOptions();
                    options.AddArguments("--disable-extensions", "--disable-infobars");
                    var service = ChromeDriverService.CreateDefaultService(pathDriver);
                    service.HideCommandPromptWindow = true;
                    driver = new ChromeDriver(service, options);

                    if(VersionBrowsers.Chrome == null)
                    {
                        ICapabilities cap = ((RemoteWebDriver)driver).Capabilities;
                        VersionBrowsers.Chrome = cap.Version.Split('.')[0];
                    }
                }
                else if(browser.ToLower().Equals("ie"))
                {
                    InternetExplorerOptions options = new InternetExplorerOptions();
                    options.EnableNativeEvents = false;
                    options.EnableFullPageScreenshot = true;
                    var service = InternetExplorerDriverService.CreateDefaultService(pathDriver);
                    service.HideCommandPromptWindow = true;
                    driver = new InternetExplorerDriver(service, options);

                    if(VersionBrowsers.IE == null)
                    {
                        ICapabilities cap = ((RemoteWebDriver)driver).Capabilities;
                        VersionBrowsers.IE = cap.Version.Split('.')[0];
                    }
                }

                driver.Navigate().GoToUrl(site);

                if(driver.GetType() == typeof(InternetExplorerDriver) && driver.Title.Contains("Certificate"))
                {
                    driver.Navigate().GoToUrl("javascript:document.getElementById('overridelink').click()");
                }
                else if(driver.GetType() == typeof(FirefoxDriver) && driver.Title.Contains("Insecure Connection"))
                {
                    driver.Navigate().GoToUrl("javascript:document.getElementById('advanceButton').click()");
                    Thread.Sleep(1000);
                    driver.FindElement(By.Id("exceptionDialogButton")).Click();
                    Thread.Sleep(1000);
                    SendKeys.SendWait("{TAB}"); 
                    SendKeys.SendWait("{TAB}");                    
                    SendKeys.SendWait("{TAB}");                    
                    SendKeys.SendWait("{TAB}");                    
                    SendKeys.SendWait("{ENTER}");                 
                    Thread.Sleep(5000);
                }
            }
            catch(Exception e)
            {
                BasePage._hasException = true;
                Log.Warn(e.Message.ToString());
            }

            return driver;
        }
    }
}
