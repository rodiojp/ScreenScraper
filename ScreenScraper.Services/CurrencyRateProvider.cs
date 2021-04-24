using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ScreenScraper.Domain;
using ScreenScraper.Domain.DTOs;
using ScreenScraper.Services.Interfaces;
using ScreenScraper.Services.MappingService;

namespace ScreenScraper.Services
{
    /// <summary>
    /// Filters an array of records 
    /// </summary>
    /// <remarks> 
    /// How it works: It first transforms the raw csv to <see cref="CurrencyRateShortDto"/> 
    /// and then builds <see cref="CurrencyReadingBuilder"/> from the <see cref="CurrencyRateShortDto"/> to  <see cref="CurrencyRateShort"/> 
    /// </remarks>
    public class CurrencyRateProvider : ICurrencyRateProvider
    {
        public CurrencyRateProvider()
        { }

        //private CurrencyRateReadingDtoMappingService CurrencyRateReadingDtoMappingService;

        //public CurrencyRateReadingProvider(CurrencyRateReadingDtoMappingService CurrencyRateReadingDtoMappingService)
        //{
        //    this.CurrencyRateReadingDtoMappingService = CurrencyRateReadingDtoMappingService;
        //}
        public IEnumerable<CurrencyRateShort> BuildCurrencyReading(string webContent)
        {
            //var reads = JsonConvert.DeserializeObject<IEnumerable<CurrencyRateShortDto>>(webContent);
            //return reads.Select(x => CurrencyMappingBuilder.FromCurrencyRateReadingDto(x as CurrencyRateShortDto)).ToArray();
            throw new NotImplementedException();
        }
        public IEnumerable<CurrencyRateShort> BuildCurrencyReading(IEnumerable<Tuple<string, Type>> objects)
        {
            IEnumerable<CurrencyRateShortDto> currencyRatesDto = null;
            IEnumerable<CurrencyDto> currenciesDto = null;

            if (objects.Count() == 2)
            {

                foreach (var item in objects)
                {
                    if (item.Item2 == typeof(CurrencyRateShortDto))
                    {
                        currencyRatesDto = JsonConvert.DeserializeObject<IEnumerable<CurrencyRateShortDto>>(item.Item1);
                    }
                    else
                    if (item.Item2 == typeof(CurrencyDto))
                    {
                        currenciesDto = JsonConvert.DeserializeObject<IEnumerable<CurrencyDto>>(item.Item1);
                    }
                }
            }
            else throw new ArgumentException($"Cannot Build Currency Reading from {objects.Count()} paremeters");
            IEnumerable<CurrencyRateShort> currencyRates = CurrencyMappingBuilder.FromCurrencyRateReadingDto(currenciesDto, currencyRatesDto);
            return currencyRates;

        }
        //public override CurrencyRateShort BuildOnDateCurrencyReading(string webContent)
        //{
        //    var read = JsonConvert.DeserializeObject<CurrencyRateShortDto>(webContent);
        //    return CurrencyMappingBuilder.FromCurrencyRateReadingDto(read);
        //}
    }
}
