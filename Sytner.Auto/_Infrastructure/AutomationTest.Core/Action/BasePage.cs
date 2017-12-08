using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AutomationTest.Core.Action
{
    public class BasePage
    {
        #region Var, Properties, Constructure
        
        protected readonly AppConfiguration _appConfiguration;
        protected readonly int _timeWait;
        protected readonly int _timeControlWait;
        public readonly int _timeSleep;

        protected IWebElement _element;
        
        public static bool _isFailed = false;
        public static bool _hasException = false;

        protected Log log {get; set;}
        protected readonly IWebElement _webDriver;

        protected BasePage(IWebElement webDriver)
        {
            Log = new Log();
            _webDriver = webDriverl
            _appConfiguration = new AppConfiguration();

            // Set time
            _timeWait = _appConfiguration.TimeWait;
            _timeControlWait = _appConfiguration.TimeControlWait;
            _timeSleep = _appConfiguration.TimeSleep;
        }

        #endregion

        #region FindElement

        public IWebElement FindElement(string controlName)
        {
            IWebElement element = null;
            try
            {
                element = _webDriver.FindElement(By.XPath(controlName))  ;
            }
            catch (Exception e)
            {
                HandlException(e);
            }

            return element;
        }

        public IWebElement FindElement(string controlName, string value)
        {
            IWebElement element = null;
            try
            {
                string control = string.Format(controlName, value);
                element = _webDriver.FindElement(By.XPath(control))  ;
            }
            catch (Exception e)
            {
                HandlException(e);
            }

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
                HandlException(e);
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
                HandlException(e);
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
            catch (Exception e)
            {
                HandlException(e);
            }

            return by;
        }

        public By GetBy(string controlName, value)
        {
            By by = null;
            try
            {
                string control = string.Format(controlName, value);
                by = By.XPath(control);
            }
            catch (Exception e)
            {
                HandlException(e);
            }

            return by;
        }
            
        #endregion

        #region Control

        #region Click

        public void Click(string controlName)
        {
            WaitForControl(controlName, _timeWait);
            _element = FindElement(controlName);
            try
            {
                _element.Click();
            }
            catch (Exception ex)
            {
                string functionName = MethodInfo.GetCurrentMethod().Name;
                HandlException(ex, functionName);
            }
        }

        protected void Click(string controlName, string value)
        {
            WaitForControl(controlName, value, _timeWait);
            _element = FindElement(controlName, value);
            try
            {
                _element.Click();
            }
            catch (Exception ex)
            {
                string functionName = MethodInfo.GetCurrentMethod().Name;
                HandlException(ex, functionName);
            }
        }

        protected void DoubleClick(string controlName)
        {
            WaitForControl(controlName, _timeWait);
            Actions action = new Actions(_webDriver);
            _element = FindElement(controlName);
            action.DoubleClick(_element).Perform();
        }

        protected void ClickAndHoldOnElement(string controlName)
        {
            WaitForControl(controlName, _timeWait);
            Actions action = new Actions(_webDriver);
            _element = FindElement(controlName);
            action.ClickAndHold(_element).Perform();
        }

        protected void ClickAndHoldOnElement(string controlName, string value)
        {
            WaitForControl(controlName, value, _timeWait);
            Actions action = new Actions(_webDriver);
            _element = FindElement(controlName, value);
            action.ClickAndHold(_element).Perform();
        }
            
        #endregion Click
            
        protected void Clear(string controlName)    
        {
            WaitForControl(controlName, _timeWait);
            _element = FindElement(controlName);
            try
            {
                _element.Clear();
            }
            catch (Exception ex)
            {
                string functionName = MethodInfo.GetCurrentMethod().Name;
                HandlException(ex, functionName);
            }
        }

        protected void Type(string controlName, string value)
        {
            WaitForControl(controlName, _timeWait);
            _element = FindElement(controlName);
            try
            {
                if (value.Contains(Keys.Enter) || value.Contains(Keys.Space))
                {
                    _element.SendKeys(value);
                }
                else
                {
                    _element.Clear();
                    _element.SendKeys(value);
                }
            }
            catch (Exception ex)
            {
                string functionName = MethodInfo.GetCurrentMethod().Name;
                HandlException(ex, functionName);
            }
        }

        protected void Type(string specialcontrol, string value, string inputValue)
        {
            WaitForControl(specialcontrol, value, _timeWait);
            _element = FindElement(specialcontrol, value);
            try
            {
                if (inputValue.Contains(Keys.Enter) || inputValue.Contains(Keys.Space))
                {
                    _element.SendKeys(inputValue);
                }
                else
                {
                    _element.Clear();
                    _element.SendKeys("\u0008" + inputValue);
                }
            }
            catch (Exception ex)
            {
                string functionName = MethodInfo.GetCurrentMethod().Name;
                HandlException(ex, functionName);
            }
        }

        protected void DragAndDrop(string sourceControl, string targetControl)
        {
            IWebElement source = FindElement(sourceControl);
            IWebElement target = FindElement(targetControl);
            Actions action = new Actions(_webDriver);
            action.DragAndDrop(source, target);
            actins.Perform();
        }

        protected void MoveMouseToElement(string controlName)
        {
            WaitForControl(controlName, _timeWait);
            Actinos actions = new Actions(_webDriver);
            _element = FindElement(controlName);
            action.MoveToElement(_element).Build().Perform();
        }

        protected void MoveMouseToElement(string controlName, string value)
        {
            WaitForControl(controlName, value, _timeWait);
            Actinos actions = new Actions(_webDriver);
            _element = FindElement(controlName, value);
            action.MoveToElement(_element).Build().Perform();
        }

        #region Checkbox

        protected void CheckTheCheckbox(string controlName)
        {
            WaitForControl(controlName, _timeWait);
            _element = FindElement(controlName);
            if (!_element.Selected)
            {
                Click(controlName);
            }
        }

        protected void CheckTheCheckbox(string controlName, string value)
        {
            WaitForControl(controlName, value, _timeWait);
            _element = FindElement(controlName, value);
            if (!_element.Selected)
            {
                Click(controlName, value);
            }
        }
            
        #endregion

        #endregion

        #region Element

        protected IWebElement GetElement(string controlName)
        {
            return FindElement(controlName);
        }

        protected int CountElement(string controlName)
        {
            IList<IWebElement> element = null;
            try
            {
                element = FindElements(controlName);
            }
            catch (Exception ex)
            {
                string functionName = MethodInfo.GetCurrentMethod().Name;
                HandlException(ex, functionName);
            }

            return element.Count;
        }

        protected int CountElement(string controlName, string value)
        {
            IList<IWebElement> element = null;
            try
            {
                element = FindElements(controlName, value);
            }
            catch (Exception ex)
            {
                string functionName = MethodInfo.GetCurrentMethod().Name;
                HandlException(ex, functionName);
            }

            return element.Count;
        }
        
        #endregion

        #region Private function
        
        protected void HandlException(Exception ex, string functionName = "")
        {
            _hasException = true;
            string message = string.Join(" - ", this.GetType().Name, functionName, ex.Message);
            Log.Warn(message);
        }

        #endregion

    }
}
