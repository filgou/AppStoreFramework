using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppStoreFramework.Logging.Interfaces;

namespace AppStoreFramework.Logging.Implementations
{
    public class Logger : ILogger
    {
        public void Log(string category, string message, string sender, string details, string tags)
        {
            var s = String.Format("{0} - {1} : {2} {3} {4}  tags: {5}  sender: {6}", DateTime.UtcNow, category, message, details, tags, sender);
            Trace.WriteLine(s);
        }

        public void Debug(string message, string sender = "", string tags = "")
        {
            Log("Debug", message, sender, "", tags);
        }

        public void Info(string message, string sender = "", string tags = "")
        {
            Log("Info", message, sender, "", tags);
        }

        public void Warn(string message, string sender = "", string tags = "")
        {
            Log("Warn", message, sender, "", tags);
        }

        public void Error(string message, string details, string sender = "", string tags = "")
        {
            Log("Error", message, sender, details, tags);
        }
    }
}
