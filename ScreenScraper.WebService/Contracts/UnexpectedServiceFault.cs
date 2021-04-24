using System;
using System.Runtime.Serialization;
using ScreenScraper.Domain;

namespace ScreenScraper.WebService.Contracts
{
    /// <summary>
    /// POCO for service errors
    /// </summary>
    [DataContract]
    public class UnexpectedServiceFault
    {
        /// <summary>
        /// The error message
        /// </summary>
        [DataMember]
        public string ErrorMessage { get; private set; }
        /// <summary>
        /// The stack trace of the error
        /// </summary>
        [DataMember]
        public string StackTrace { get; private set; }
        /// <summary>
        /// The method that throws the current exception
        /// </summary>
        [DataMember]
        public string Target { get; private set; }
        /// <summary>
        /// The name of the object that causes the error
        /// </summary>
        [DataMember]
        public string Source { get; private set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errorMessage">The error message</param>
        /// <param name="stackTrace">The stack trace of the error</param>
        /// <param name="target">The method that throws the current exception</param>
        /// <param name="source">The name of the object that causes the error</param>
        public UnexpectedServiceFault(string errorMessage, string stackTrace, string target, string source)
        {
            ErrorMessage = errorMessage;
            StackTrace = stackTrace;
            Target = target;
            Source = source;
        }
    }
}