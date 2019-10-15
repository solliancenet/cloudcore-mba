using System.Configuration;

namespace Contoso.Apps.SportsLeague.Web.Models
{
    public class ConfigModel
    {
        public string AppInsightsInstrumentationKey => ConfigurationManager.AppSettings["APPINSIGHTS_INSTRUMENTATIONKEY"];
    }
}