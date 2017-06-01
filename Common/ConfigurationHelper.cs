using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace Common
{
    public static class ConfigurationHelper
    {
        public static string AppSetting(string key)
        {
            return System.Configuration.ConfigurationSettings.AppSettings[key];
        }
    }
}
