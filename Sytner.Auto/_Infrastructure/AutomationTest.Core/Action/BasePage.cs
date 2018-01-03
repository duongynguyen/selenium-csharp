using AutomationTest.Core.Configuration;
using AutomationTest.Core.Extensions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

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
            _webDriver = webDriver;
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

        public By GetBy(string controlName, string value)
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

        protected void UncheckTheCheckbox(string controlName)
        {
            WaitForControl(controlName, _timeWait);
            _element = FindElement(controlName);
            if (_element.Selected)
            {
                Click(controlName);
            }
        }
            
        #endregion Checkbox

        #region Combobox - DropDownList

        protected string GetItemSelectedCombobox(string controlName)
        {
            _element = FindElement(controlName);
            SelectElement select = new SelectElement(_element);
            string itemSelected = select.SelectedOption.ToString();

            return itemSelected;
        }

        protected string GetItemSelectedCombobox(string controlName, string value)
        {
            _element = FindElement(controlName, value);
            SelectElement select = new SelectElement(_element);
            string itemSelected = select.SelectedOption.ToString();

            return itemSelected;
        }

        protected void SelectItemCombobox(string controlName, string item)
        {
            _element = FindElement(controlName);
            SelectElement select = new SelectElement(_element);
            select.SelectByText(item);
        }

        protected void SelectItemCombobox(string controlName, string value, string item)
        {
            _element = FindElement(controlName, value);
            SelectElement select = new SelectElement(_element);
            select.SelectByText(item);
        }

        protected void SelectDropDownListItem(string locator, string item)
        {
            _element = FindElement(locator);

            new SelectElement(_element).SelectByText(item);
        }

        protected void SelectDropDownListItem(string locator, int item)
        {
            _element = FindElement(locator);

            new SelectElement(_element).SelectByIndex(item);
        }

        protected void SelectDropdownList(string element, string controlName, string value)
        {
            try
            {
                Actions action;
                Click(element);
                WaitForControl(controlName, _timeControlWait);
                IList<IWebElement> listElement = FindElements(controlName);
                foreach (IWebElement option in listElement)
                {
                    action = new Actions(_webDriver);
                    if (option.Text == value)
                    {
                        option.Click();
                        break;
                    }
                    else
                    {
                        action.SendKeys(Keys.ArrowDown).Perform();
                    }
                }
            }
            catch (Exception ex)
            {
                string functionName = MethodInfo.GetCurrentMethod().Name;
                HandlException(ex, functionName);
            }
        }

        protected void SelectDropdownListHaveLoadData(string element, string controlName, string value, string elementLoad)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Click(element);
                    Sleep(1);
                    WaitForElementInvisible(elementLoad, 15);
                    Click(element);
                    Sleep(1);
                    IList<IWebElement> listElement = FindElements(controlName);
                    foreach (IWebElement option in listElement)
                    {
                        Actions action = new Actions(_webDriver);
                        if(optrion.Text == value)
                        {
                            option.Click();
                            Sleep(1);
                            break;
                        }
                        else
                        {
                            action.SendKeys(Keys.ArrowDown).Perform();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string functionName = MethodInfo.GetCurrentMethod().Name;
                HandlException(ex, functionName);
            }
        }

        protected void TypeAndSelectDropdownList(string element, string controlName, string typeValue)
        {
            try
            {
                if (!string.IsNullOrEmpty(typeValue))
                {
                    Actions action = new Actions(_webDriver);
                    Type(element, typeValue);
                    Sleep(1);
                    WaitForLoadPage();
                    IList<IWebElement> listElement = FindElements(controlName);
                    foreach (IWebElement option in listElement)
                    {
                       if(options.Text == typeValue) 
                       {
                           options.Click();
                           Sleep(1);
                           break;                           
                       }
                       else
                       {
                           action.SendKeys(Keys.ArrowDown).Perform();
                       }
                    }
                }
            }
            catch (Exception ex)
            {
                string functionName = MethodInfo.GetCurrentMethod().Name;
                HandlException(ex, functionName);
            }
        }

        protected void SelectIndexOnDropdownByJavaScript(string idDropdownList, string controlName)
        {
            WaitForLoadPage();
            var currentValue = GetText(controlName);

            if (currentValue.Contains("Select One"))
            {
                ExecuteJavaScript("$('#" + idDropdownList + "').data('kendoDropDownList').select(" + 1 + ")");
            }
            else
            {
                ExecuteJavaScript("$('#" + idDropdownList + "').data('kendoDropDownList').select(" + 0 + ")");
            }

            ExecuteJavaScript("$('#" + idDropdownList + "').data('kendoDropDownList').trigger('change')");
        }

        protected void SelectIndexOnComboBoxByJavaScript(string idComboBox)
        {
            WaitForLoadPage();
            ExecuteJavaScript("$('#" + idComboBox + "').data('kendoComboBox').select(" + 0 + ")");
        }
            
        #endregion Combobox - DropDownList

        #region Alert

        protected void AcceptJavascriptAlert()
        {
            IAlert alert = _webDriver.SwitchTo().Alert();
            alert.Accept();
        }

        protected void DismissJavascriptAlert()
        {
            IAlert alert = _webDriver.SwitchTo().Alert();
            alert.Dismiss();
        }

        public void HideAlertBox()
        {
            string alertBoxPath = "//as-alert-box[contains(@position,'right')]";

            if (CountElement(alertBoxPath) != 0)
            {
                SetAttribute(alertBoxPath, "style", "display:none");
            }
        }
            
        #endregion Alert

        #endregion Control

        #region Browser

        protected void Refresh()
        {
            _webDriver.Navigate().Refresh();
            Sleep(_timeSleep);
        }

        public void BackOnePage()
        {
            Back();
        }

        protected void Back()
        {
            
        }
            
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
