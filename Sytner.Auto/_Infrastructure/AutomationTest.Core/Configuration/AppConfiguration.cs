using System.Configuration;
using Sytner.Auto.Share.Utils;
// Done
namespace AutomationTest.Core.Configuration
{
    public class AppConfiguration
    {
        private string _controlConfigMapping;
        private string _dataPath;
        private string _environment;
        private string _client;
        private string _userName;
        private string _password;
        private string _testDataSheetPath;

        public string _controlConfigMapping
        {
            get
            {
                if(string.IsNullOrEmpty(_controlConfigMapping))
                {
                    _controlConfigMapping = GetValue("Control_ConfigMappingPath");
                }
                return CommonHelper.GetPathFolder(_controlConfigMapping);
            }
        }

        public string DataPath
        {
            get
            {
                if(string.IsNullOrEmpty(_dataPath))
                {
                    _dataPath = GetValue("Data_Path");
                }
                return CommonHelper.GetPathFolder(_dataPath);
            }
        }

        public string TestDataSheetPath
        {
            get
            {
                if(string.IsNullOrEmpty(_testDataSheetPath))
                {
                    _testDataSheetPath = GetValue("TestDataSheetPath");
                }

                return CommonHelper.GetPathFolder(_testDataSheetPath);
            }
        }

        public string Client
        {
            get
            {
                if(string.IsNullOrEmpty(_client))
                {
                    _client = GetValue("Selected_Client");
                }

                return _client;
            }
        }

        public string Environment
        {
            get
            {
                if(string.IsNullOrEmpty(_environment))
                {
                    _environment = GetValue("Selected_Environment");
                }

                return _environment;
            }
        }

        public string UserName
        {
            get
            {
                if(string.IsNullOrEmpty(_userName))
                {
                    string key = Environment + ".Username";
                    _userName = GetValue(key);
                }

                return _userName;
            }
        }

        public string Password
        {
            get
            {
                if(string.IsNullOrEmpty(_password))
                {
                    string key = Environment + ".Password";
                    _password = GetValue(key);
                }

                return _password;
            }
        }

        public string FolderReport
        {
            get
            {
                return GetValue("FolderReport");
            }
        }

        public string Browsers
        {
            get
            {
                return GetValue("Browsers");
            }
        }

        public string PathContent
        {
            get
            {
                return GetValue("PathContent");
            }
        }

        public string PathDriver
        {
            get
            {
                return GetValue("PathDriver");
            }
        }

        public string PathApplication
        {
            get
            {
                return GetValue("PathApplication");
            }
        }

        public string PathTestResult
        {
            get
            {
                return GetValue("PathTestResult");
            }
        }

        public string PathTemplatesHtml
        {
            get
            {
                return GetValue("PathTemplatesHtml");
            }
        }

        public string DefaultGenerateReport
        {
            get
            {
                return GetValue("DefaultGenerateReport");
            }
        }

        #region Time

        public int TimeWait
        {
            get
            {
                return GetIntValue("TimeWait");
            }
        }

        public int TImeControlWait
        {
            get
            {
                return GetIntValue("TimeControlWait");
            }
        }

        public int TimeSleep
        {
            get
            {
                return GetIntValue("TimeSleep");
            }
        }

        public int TimeWaitForTest
        {
            get
            {
                return GetIntValue("TimeWaitForTest");
            }
        }

        #endregion

        #region GetValue

        public string GetValue(string key) 
        {
            string value = ConfigurationManager.AppSettings[key];

            return value;
        }

        public int GetIntValue(string key)
        {
            string value = GetValue(key);
            int result = value.ToInt();

            return result;
        }

        #endregion
    }
}
