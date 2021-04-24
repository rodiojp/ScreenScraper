using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ScreenScraper.Domain.DTOs
{
    public class CurrencyDto
    {
        [JsonProperty("Cur_ID")]
        public int ID { get; set; }

        [JsonProperty("Cur_ParentID")]
        public int ParentID { get; set; }

        [JsonProperty("Cur_Code")]
        public string Code { get; set; }

        [JsonProperty("Cur_Abbreviation")]
        public string Abbreviation { get; set; }

        [JsonProperty("Cur_Name")]
        public string Name { get; set; }

        [JsonProperty("Cur_Name_Bel")]
        public string NameBel { get; set; }

        [JsonProperty("Cur_Name_Eng")]
        public string NameEng { get; set; }

        [JsonProperty("Cur_QuotName")]
        public string QuotName { get; set; }

        [JsonProperty("Cur_QuotName_Bel")]
        public string QuotNameBel { get; set; }

        [JsonProperty("Cur_QuotName_Eng")]
        public string QuotNameEng { get; set; }

        [JsonProperty("Cur_NameMulti")]
        public string NameMulti { get; set; }

        [JsonProperty("Cur_Name_BelMulti")]
        public string NameBelMulti { get; set; }

        [JsonProperty("Cur_Name_EngMulti")]
        public string NameEngMulti { get; set; }

        [JsonProperty("Cur_Scale")]
        public int Scale { get; set; }

        [JsonProperty("Cur_Periodicity")]
        public int Periodicity { get; set; }

        [JsonProperty("Cur_DateStart")]
        public DateTime DateStart { get; set; }

        [JsonProperty("Cur_DateEnd")]
        public DateTime DateEnd { get; set; }
    }
}
