using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenScraper.Domain;

namespace ScreenScraper.Services.Interfaces
{
    public interface IScreenScraperRequestProvider
    {
        IEnumerable<ScreenScraperRequest> GetScreenScrapeRequests(RunParameters runParameters);
    }
}
