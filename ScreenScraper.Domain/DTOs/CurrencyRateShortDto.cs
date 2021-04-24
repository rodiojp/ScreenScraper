using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ScreenScraper.Domain.DTOs
{
    public class CurrencyRateShortDto
    {
        [JsonProperty("Cur_ID")]
        public int ID { get; set; }

        [JsonProperty("Date")]
        public DateTime Date { get; set; }

        [JsonProperty("Cur_OfficialRate")]
        public decimal? OfficialRate { get; set; }

        public CurrencyDto CurrencySource { get; set; }
    }
}
