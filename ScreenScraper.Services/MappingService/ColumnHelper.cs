using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenScraper.Domain.Exceptions;
using ScreenScraper.Domain.MappingService;

namespace ScreenScraper.Services.MappingService
{
    /// <summary>
    /// Utilities Class to aid in building objects from the csv deliminated values
    /// </summary>
    public class ColumnHelper
    {
        /// <summary>
        /// Method to get all the column names of the csv delimited object into a 
        /// dictionary where the key the csv column name and property it is on
        /// </summary>
        /// <param name="objectType">The type of object that has all the CsvAttribute marked</param>
        /// <returns>Dictionary with the csv column name and property it is on</returns>
        public static IDictionary<string, string> FindAllRequiredColumns(Type objectType)
        {
            var result = new Dictionary<string, string>();
            //iterate through all properties and find the one(s) that have the csvColumn attribute
            var props = objectType.GetProperties();
            foreach (var prop in props)
            {
                var attrs = prop.GetCustomAttributes(true);
                foreach (var csvAttribute in attrs.OfType<CsvAttribute>())
                {
                    result.Add(csvAttribute.ColumnName, prop.Name);
                }
            }
            return result;
        }
        /// <summary>
        /// Finds what order/position each column is in
        /// </summary>
        /// <param name="headersToPropDict">The headers to look for in the csvObject</param>
        /// <param name="headers">an array of object which are column headers in the csvObject</param>
        /// <returns></returns>
        public static IDictionary<string, int> FindPositionOfColumnHeader(IDictionary<string, string> headersToPropDict, string[] headers)
        {
            var result = new Dictionary<string, int>();
            var notUsedValues = new List<string>();
            for (int ii = 0; ii < headers.Length; ii++)
            {
                if (headersToPropDict.ContainsKey(headers[ii]))
                {
                    result.Add(headersToPropDict[headers[ii]], ii);
                }
                else
                {
                    notUsedValues.Add(headers[ii]);
                }
            }
            //we should have at least the number of 
            if (headersToPropDict.Count() != result.Count)
                throw new MappingServiceException(headersToPropDict.Keys, notUsedValues, result.Count);
            return result;
        }
    }
}
