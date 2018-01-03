using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationTest.Core.Action;
using OpenQA.Selenium;
// Done
namespace AutomationTest.Core.Extensions
{
    public class ControlExtensions
    {
        private readonly IWebDriver _webDriver;
        private Log _log;

        public ControlExtensions(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _log = new Log();
        }

        public IWebElement FindElement(string controlName)
        {
            try
            {
                IWebElement element = _webDriver.FindElement(By.XPath(controlName));

                return element;
            }
            catch
            {
                return null;
            }
        }

        public IWebElement FindElement(string controlName, string value)
        {
            string control = string.Format(controlName, value);
            IWebElement element = _webDriver.FindElement(By.XPath(control));

            return element;
        }

        public IList<IWebElement> FindElements(string controlName)
        {
            IList<IWebElement> lstElement = null;
            try
            {
                lstElement = _webDriver.FindElements(By.XPath(controlName));
            }
            catch (Exception e)
            {
                BasePage._hasException = true;
                _log.Warn(e.Message.ToString());
            }

            return lstElement;
        }

        public IList<IWebElement> FindElements(string controlName, string value)
        {
            IList<IWebElement> lstElement = null;
            try
            {
                string control = string.Format(controlName, value);
                lstElement = _webDriver.FindElements(By.XPath(control));
            }
            catch (Exception e)
            {
                BasePage._hasException = true;
                _log.Warn(e.Message.ToString());
            }

            return lstElement;
        }

        public By GetBy(string controlName)
        {
            By by = null;
            try
            {
                by = By.XPath(controlName);
            }
            catch(Exception e)
            {
                BasePage._hasException = true;
                _log.Warn(e.Message.ToString());
            }

            return by;
        }

        public By GetBy(string specialControl, string value)
        {
            By by = null;
            try
            {
                string control = string.Format(specialControl, value);
                by = By.XPath(control);
            }
            catch(Exception e)
            {
                BasePage._hasException = true;
                _log.Warn(e.Message.ToString());
            }

            return by;
        }
    }
}
