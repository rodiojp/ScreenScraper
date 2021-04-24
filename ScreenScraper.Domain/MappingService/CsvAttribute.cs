using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenScraper.Domain.MappingService
{
    [AttributeUsage(AttributeTargets.All)]
    public class CsvAttribute:Attribute
    {
        private readonly string columnName;

        public CsvAttribute(string columnName)
        {
            this.columnName = columnName;
        }
        public virtual string ColumnName => columnName;
    }
}
