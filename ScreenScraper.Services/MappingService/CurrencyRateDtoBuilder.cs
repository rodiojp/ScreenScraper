using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenScraper.Domain.DTOs;

namespace ScreenScraper.Services.MappingService
{
    /// <summary>
    /// Builds Currency Reading Dto
    /// </summary>
    public class CurrencyRateDtoBuilder
    {
        /// <summary>
        /// Builds a CurrencyRateReadingDto from a record of values and a mapping between
        /// the property name and what position it is in the csv
        /// </summary>
        /// <param name="values">a record of values</param>
        /// <param name="columnMapping">Dictionary where the key is the property name and position/order it is in the values supplied</param>
        /// <param name="type"></param>
        /// <returns>a <see cref="CurrencyRateShortDto"/> object</returns>
        public static CurrencyRateShortDto Create(string[] values, IDictionary<string,int> columnMapping, Type type)
        {
            //create a new dictionary with property name and its values 
            var mapValues = columnMapping.ToDictionary(item => item.Key, item => values[item.Value].ToString());
            CurrencyRateShortDto result = null;
            if (type == typeof(CurrencyRateShortDto))
            {
                result = (CurrencyRateShortDto)Activator.CreateInstance(type);
            }

            foreach (var pair in mapValues)
            {
                type.GetProperty(pair.Key).SetValue(result, pair.Value);
            }
            return result;
        }
    }
}
