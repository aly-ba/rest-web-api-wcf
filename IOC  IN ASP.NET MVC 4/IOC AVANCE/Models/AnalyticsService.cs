using MvcIoC.Pages;

namespace MvcIoC.Models
{
    public class AnalyticsService : IAnalyticsService
    {
        public string Code
        {
            get { return "Tracking you!"; }
        }
    }
}