using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenScraper.Application.Interfaces
{
    public interface IResultHandler<out T>
    {
        IEnumerable<T> GetAndHandleScreenScrapeResult();
    }
}
