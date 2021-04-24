using System;

namespace ScreenScraper.Domain
{
    public class CurrencyRateShort
    {
        public Currency CurrencySource { get; set; }
        public DateTime Date { get; set; }
        public decimal OfficialRate { get; set; }
    }

}