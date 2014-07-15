using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppStoreFramework.Logging.Interfaces;

namespace AppStoreFramework.AutomatedTests
{
    public class TextLogger : ILogger
    {
        public void Log(string category, string message, string sender, string details, string tags)
        {
            Debugger.Log(0,category,message);
        }

        public void Debug(string message, string sender = "", string tags = "")
        {
            Debugger.Log(0, "Debug", message);
        }

        public void Info(string message, string sender = "", string tags = "")
        {
            Debugger.Log(1, "Info", message);
        }

        public void Warn(string message, string sender = "", string tags = "")
        {
            Debugger.Log(2, "Warn", message);
        }

        public void Error(string message, string details, string sender = "", string tag = "")
        {
            Debugger.Log(3, "Error", message +": " + details);
        }
    }
}
