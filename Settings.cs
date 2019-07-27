using System.Configuration;

namespace Icenet.Service.Logging.UI
{
    public class Settings
    {
        public static string LogServiceUrl()
        {
            return ConfigurationManager.AppSettings["LogServiceUrl"];
        }
    }
}