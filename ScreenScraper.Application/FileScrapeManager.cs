using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenScraper.Domain;
using ScreenScraper.Domain.Utilities;
using ScreenScraper.Services.Interfaces;

namespace ScreenScraper.Application
{
    public class FileScrapeManager : ScrapeManager<string>
    {
        private readonly string runParameterId = "Unidentified";

        public FileScrapeManager(IScrape scrapeService): base (scrapeService)
        { }
        public FileScrapeManager(IScrape scrapeService, string identifier) : base(scrapeService)
        {
            runParameterId = identifier;
        }
        public override IEnumerable<string> GetAndHandleScreenScrapeResult()
        {
            //string result = ScrapeService.Scrape().Result;
            string result = string.Empty;
            FileGenerator.GenerateFile(runParameterId, result);
            return new string[] { result };
        }
    }
}
