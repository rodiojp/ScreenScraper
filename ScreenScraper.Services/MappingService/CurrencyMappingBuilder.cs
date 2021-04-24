using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using System.Globalization;
using ScreenScraper.Domain;
using ScreenScraper.Domain.DTOs;

namespace ScreenScraper.Services.MappingService
{
    public class CurrencyMappingBuilder
    {
        public static IEnumerable<CurrencyRateShort> FromCurrencyRateReadingDto(IEnumerable<CurrencyDto> currenciesDto,
                                                                                IEnumerable<CurrencyRateShortDto> currencyRatesDto)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CurrencyDto, Currency>();
                cfg.CreateMap<CurrencyDto, CurrencyRateShort>()
                   .ForMember(dest => dest.CurrencySource, opt => opt.MapFrom(src => src));

                cfg.CreateMap<CurrencyRateShortDto, CurrencyRateShort>()
                    .ForMember(dest => dest.CurrencySource, opt =>
                    {
                        //opt.PreCondition(src => (src.ID >= 0));
                        opt.MapFrom(src =>
                            new Currency()
                            {
                                ID = src.ID,
                            });
                    })
                    .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                    .ForMember(dest => dest.OfficialRate, opt => opt.MapFrom(src => src.OfficialRate));
            });
            //Act
            var mapper = new Mapper(config);
            var tempCurrencyRates = mapper.Map<IEnumerable<CurrencyRateShortDto>, IEnumerable<CurrencyRateShort>>(currencyRatesDto);
            foreach (var item in tempCurrencyRates)
            {
                item.CurrencySource = mapper.Map<CurrencyDto, Currency>(currenciesDto.FirstOrDefault(x => x.ID == item.CurrencySource.ID));
            }
            return tempCurrencyRates;
        }
    }
}
