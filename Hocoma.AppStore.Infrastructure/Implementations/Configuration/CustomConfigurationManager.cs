using System;
using System.Configuration;
using System.Diagnostics;
using Hocoma.AppStore.Infrastructure.Interfaces.Configuration;

namespace Hocoma.AppStore.Infrastructure.Implementations.Configuration
{
    public class CustomConfigurationManager : IConfigurationManager
    {
        public T GetSetting<T>(string key)
        {
            try
            {
                var value = ConfigurationManager.AppSettings[key];
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (InvalidCastException ex)
            {
                Trace.WriteLine("Invalid cast when getting setting from ConfigurationManager: "+ex);
                return default(T);
            }
           
        }
    }
}
