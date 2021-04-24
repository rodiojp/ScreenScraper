using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenScraper.Application.Interfaces;
using ScreenScraper.Services.Interfaces;

namespace ScreenScraper.Application
{
    /// <summary>
    /// Abstract class to handle scrapes
    /// </summary>
    public abstract class ScrapeManager<T> : IResultHandler<T>
    {
        protected readonly IScrape ScrapeService;
        protected readonly IDictionary<string, string> Mapping;

        protected ScrapeManager(IScrape scrapeService)
        {
            ScrapeService = scrapeService;
        }

        public virtual IEnumerable<T> GetAndHandleScreenScrapeResult()
        {
            return null;
        }
    }
}
