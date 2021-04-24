using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenScraper.Domain;

namespace ScreenScraper.Services.Interfaces
{
    public interface ICurrencyRateProvider
    {
        IEnumerable<CurrencyRateShort> BuildCurrencyReading(IEnumerable<Tuple<string, Type>> objects);
        IEnumerable<CurrencyRateShort> BuildCurrencyReading(string webContent);
        //CurrencyRateShort BuildOnDateCurrencyReading(string webContent);
    }
}
