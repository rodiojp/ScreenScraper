using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenScraper.Services.Interfaces
{
    public interface IScrape
    {
        Tuple<string, Type>[] Scrape();
    }
}
