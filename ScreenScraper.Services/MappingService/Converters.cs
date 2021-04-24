using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ScreenScraper.Services.MappingService
{
    public class CurrencyDateTimeConversion : IValueResolver<string, DateTime, DateTime>
    {
        public DateTime Resolve(string source, DateTime destination, DateTime destMember, ResolutionContext context)
        {
            DateTime result;
            if (DateTime.TryParseExact(source, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }
            return DateTime.ParseExact(source, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
        }
    }

    public class IntTypeConverter : ITypeConverter<string, int>
    {
        public int Convert(string source, int destination, ResolutionContext context)
        {
            return System.Convert.ToInt32(source);
        }
    }
}
