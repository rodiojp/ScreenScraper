using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenScraper.Domain;
using ScreenScraper.Services;
using ScreenScraper.Services.Interfaces;
using ScreenScraper.Services.MappingService;

namespace ScreenScraper.Application
{
    public class WebServiceScrapeManager : ScrapeManager<CurrencyRateShort>
    {
        private readonly RunParameters runParameters;
        public event EventHandler OnRequestBeingProcessed;

        public WebServiceScrapeManager(IScrape scrapeService) : base(scrapeService)
        { }
        public WebServiceScrapeManager(IScrape scrapeService, RunParameters rp) : base(scrapeService)
        {
            runParameters = rp;
            SetupLogging();
        }
        /// <summary>
        /// Setup logging of the screen scrape process
        /// </summary>
        private void SetupLogging()
        {
            WebScraperService webScraperService = ScrapeService as WebScraperService;
            if (webScraperService != null)
            {
                webScraperService.OnRequestBeingMade += WebServiceScrapeManager_LogActivity;
                webScraperService.OnResponseReceived += WebServiceScrapeManager_LogActivity;
            }
        }

        private void WebServiceScrapeManager_LogActivity(object sender, EventArgs e)
        {
            if (OnRequestBeingProcessed != null)
            {
                OnRequestBeingProcessed(this, e);
            }
        }

        /// <summary>
        /// Much of the work is done here where any specific 
        /// manager makes and handles the requests
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<CurrencyRateShort> GetAndHandleScreenScrapeResult()
        {
            ICurrencyRateProvider provider = new CurrencyRateProvider();
            //string result = ScrapeService.Scrape().Result;
            Tuple<string, Type>[] result = ScrapeService.Scrape();
            var reads = provider.BuildCurrencyReading(result);
            return reads;
        }
    }
}
