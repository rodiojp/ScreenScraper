using System;
using System.Runtime.Serialization;
using ScreenScraper.Domain;

namespace ScreenScraper.WebService.Contracts
{
    /// <summary>
    /// Basic POCO for results
    /// </summary>
    /// <remarks>Use a data contract to add MesurementRading types to service operations.</remarks>
    [DataContract]
    public class MeasurementReading
    {
        [DataMember]
        public int CurrencyID { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public string CurrencyAbbreviation { get; set; }
        [DataMember]
        public int CurrencyScale { get; set; }
        [DataMember]
        public string CurrencyName { get; set; }
        [DataMember]
        public decimal CurrencyOfficialRate { get; set; }

        public MeasurementReading(int currencyID, DateTime date, string currencyAbbreviation, int currencyScale,
                                  string currencyName, decimal currencyOfficialRate)
        {
            CurrencyID = currencyID;
            Date = date;
            CurrencyAbbreviation = currencyAbbreviation;
            CurrencyScale = currencyScale;
            CurrencyName = currencyName;
            CurrencyOfficialRate = currencyOfficialRate;
        }

        public static MeasurementReading FromCurrencyReading(CurrencyRateShort cr)
        {
            return new MeasurementReading(cr.CurrencySource.ID, cr.Date, cr.CurrencySource.Abbreviation, cr.CurrencySource.Scale, cr.CurrencySource.Name, cr.OfficialRate);
        }
    }

}