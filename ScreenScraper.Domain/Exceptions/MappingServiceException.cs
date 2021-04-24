using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ScreenScraper.Domain.Extensions;
namespace ScreenScraper.Domain.Exceptions
{
    public class MappingServiceException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="headersToLookFor">These are the expectant header values</param>
        /// <param name="notUsedValues">Values that were in the web request but no actually used</param>
        /// <param name="actualMapped">The number that did get mapped correctly</param>
        public MappingServiceException(ICollection<string> headersToLookFor, List<string> notUsedValues, int actualMapped): 
            base(ModyfyExceptionMessage(headersToLookFor, notUsedValues, actualMapped))
        {

        }
        /// <summary>
        /// Creates a user friendly message to help the customer figure what wasn't mapped correctly
        /// </summary>
        /// <param name="headersToLookFor">These are the expectant header values</param>
        /// <param name="notUsedValues">Values that were in the web request but no actually used</param>
        /// <param name="actualMapped">The number that did get mapped correctly</param>
        /// <returns>User friendly string message</returns>
        private static string ModyfyExceptionMessage(ICollection<string> headersToLookFor, List<string> notUsedValues, int actualMapped)
        {
            var sb = new StringBuilder();
            sb.AppendNewLine($"Expected to map {headersToLookFor.Count} items but mapped {actualMapped}");
            sb.AppendNewLine("These are the headers that were supposed to mapped:");
            foreach (var header in headersToLookFor)
            {
                sb.AppendNewLine(header);
            }
            foreach (var webRequestHeader in notUsedValues)
            {
                sb.AppendNewLine(webRequestHeader);
            }
            return sb.ToString();
        }
    }
}