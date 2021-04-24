using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenScraper.Domain.Extensions
{ 
    public static class StringBuilderExtensions
    {

        /// <summary>
        /// Add Environment.NewLine after each line
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="value"></param>
        public static StringBuilder AppendNewLine(this StringBuilder sb, string value)
        {
            sb.Append(value).Append(Environment.NewLine);
            return sb;
        }
    }
}
