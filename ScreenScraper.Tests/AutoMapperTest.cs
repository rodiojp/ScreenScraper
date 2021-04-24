using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using System.Globalization;
using ScreenScraper.Domain;
using ScreenScraper.Domain.DTOs;
namespace ScreenScraper.Tests
{
    [TestClass]
    public class AutoMapperTest
    {
        IEnumerable<CurrencyDto> currenciesDto;
        IEnumerable<CurrencyRateShortDto> currencyRatesDto;
        /// <summary>
        /// Constructor logic
        /// </summary>
        /// 
        public AutoMapperTest()
        {
            currenciesDto = new List<CurrencyDto>() { new CurrencyDto {
                    ID = 298,
                    ParentID = 190,
                    Code = "643",
                    Abbreviation = "RUB",
                    Name = "Российский рубль",
                    NameBel = "Расійскі рубель",
                    NameEng = "Russian Ruble",
                    QuotName = "100 Российских рублей",
                    QuotNameBel = "100 Расійскіх рублёў",
                    QuotNameEng = "100 Russian Rubles",
                    NameMulti = "Российских рублей",
                    NameBelMulti = "Расійскіх рублёў",
                    NameEngMulti = "Russian Rubles",
                    Scale = 100,
                    Periodicity = 0,
                    DateStart = new DateTime(2016,07,01),
                    DateEnd = new DateTime(2050,01,01)
                },new CurrencyDto {
                    ID = 23,
                    ParentID = 23,
                    Code = "124",
                    Abbreviation = "CAD",
                    Name = "Канадский доллар",
                    NameBel = "Канадскі долар",
                    NameEng = "Canadian Dollar",
                    QuotName = "1 Канадский доллар",
                    QuotNameBel = "1 Канадскі долар",
                    QuotNameEng = "1 Canadian Dollar",
                    NameMulti = "Канадских долларов",
                    NameBelMulti = "Канадскіх долараў",
                    NameEngMulti = "Canadian Dollars",
                    Scale = 1,
                    Periodicity = 0,
                    DateStart = new DateTime(1991,01,01),
                    DateEnd = new DateTime(2050,01,01)
                },new CurrencyDto {
                    ID = 304,
                    ParentID = 28,
                    Code = "156",
                    Abbreviation = "CNY",
                    Name = "Китайский юань",
                    NameBel = "Кітайскі юань",
                    NameEng = "Yuan Renminbi",
                    QuotName = "10 Китайских юаней",
                    QuotNameBel = "10 Кітайскіх юаняў",
                    QuotNameEng = "10 Yuan Renminbi",
                    NameMulti = "Китайских юаней",
                    NameBelMulti = "Кітайскіх юаняў",
                    NameEngMulti = "Yuan Renminbi",
                    Scale = 10,
                    Periodicity = 0,
                    DateStart = new DateTime(2016,07,01),
                    DateEnd = new DateTime(2050, 01,  01)
                },new CurrencyDto {
                    ID = 145,
                    ParentID = 145,
                    Code = "840",
                    Abbreviation = "USD",
                    Name = "Доллар США",
                    NameBel = "Долар ЗША",
                    NameEng = "US Dollar",
                    QuotName = "1 Доллар США",
                    QuotNameBel = "1 Долар ЗША",
                    QuotNameEng = "1 US Dollar",
                    NameMulti = "Долларов США",
                    NameBelMulti = "Долараў ЗША",
                    NameEngMulti = "US Dollars",
                    Scale = 1,
                    Periodicity = 0,
                    DateStart = new DateTime(1991,01,01),
                    DateEnd = new DateTime(2050,01,01)
                }
            };

            currencyRatesDto = new List<CurrencyRateShortDto>() { new CurrencyRateShortDto()  {
                ID = 298,
                Date = new DateTime(2021,04,19),
                OfficialRate = (decimal?)3.4363
            }, new CurrencyRateShortDto()
            {
                ID = 23,
                Date = new DateTime(2021,04,19),
                OfficialRate = (decimal?)2.0759
            },new CurrencyRateShortDto()
            {
                ID = 304,
                Date = new DateTime(2021,04,19),
                OfficialRate = (decimal?)3.9812
            }
            };

        }

        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
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
            //Assert
            config.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void SimpleMappingOfCurrencyRateDto_Count_Test()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
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
            var tempCurrencyRate = mapper.Map<IEnumerable<CurrencyRateShortDto>, IEnumerable<CurrencyRateShort>>(currencyRatesDto);
            //Assert
            config.AssertConfigurationIsValid();
            Assert.IsTrue(tempCurrencyRate.Count() == 3);
        }
        [TestMethod]
        public void SimpleMappingOfCurrencyDto_Count_Test()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CurrencyDto, Currency>();
            });
            //Act
            var mapper = new Mapper(config);
            var tempCurrency = mapper.Map<IEnumerable<CurrencyDto>, IEnumerable<Currency>>(currenciesDto);
            //Assert
            config.AssertConfigurationIsValid();
            Assert.IsTrue(tempCurrency.Count() == 4);
        }
        [TestMethod]
        public void ComplexMappingOfCurrencyRateDto_Count_Test()
        {
            //Arrange 
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
            var tempCurrencyRate = mapper.Map<IEnumerable<CurrencyRateShortDto>, IEnumerable<CurrencyRateShort>>(currencyRatesDto);
            foreach (var item in tempCurrencyRate)
            {
                item.CurrencySource = mapper.Map<CurrencyDto, Currency>(currenciesDto.FirstOrDefault(x => x.ID == item.CurrencySource.ID));
            }

            //Assert
            Assert.IsTrue(tempCurrencyRate.Count() == 3 && !string.IsNullOrEmpty(tempCurrencyRate.ElementAt(0).CurrencySource.Name));
        }

    }
}