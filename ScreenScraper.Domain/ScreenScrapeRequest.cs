using System;
using System.Collections.Generic;
using System.Text;
using ScreenScraper.Domain.Extensions;
using ScreenScraper.Domain.Utilities;

namespace ScreenScraper.Domain
{
    /// <summary>
    /// Holder for Request Parameters for the Web API site
    /// </summary>
    public class ScreenScraperRequest
    {
        /// <summary>
        /// <see cref="Dictionary"/> to hold any cookie and values needed for a request
        /// </summary>
        public Dictionary<string, string> Cookies { get; private set; }

        /// <summary>
        /// <see cref="Dictionary"/> to hold any parameters to post
        /// </summary>
        public Dictionary<string, string> Parameters { get; private set; }
        /// <summary>
        /// The URL to request from site
        /// </summary>
        public string Url { get; private set; }
        /// <summary>
        /// What content to accept
        /// </summary>
        public string AcceptOnly { get; private set; }
        /// <summary>
        /// The media type of the request
        /// </summary>
        public string ContentType { get; private set; }
        /// <summary>
        /// The User for authentication
        /// </summary>
        public string User { get; private set; }
        /// <summary>
        /// The Password for authentication
        /// </summary>
        public string Password { get; private set; }
        /// <summary>
        /// Expected Data Type
        /// </summary>
        public Type TypeExpected { get; private set; }
        /// <summary>
        /// Whether to use Windows Authentication
        /// </summary>
        public bool UseWindowsAuthentication
        {
            get { return !string.IsNullOrEmpty(User) && !string.IsNullOrEmpty(Password); }
        }
        /// <summary>
        /// GET or POST Request Method
        /// </summary>
        public RequestMethod RequestType
        {
            get { return Parameters != null ? RequestMethod.Post : RequestMethod.Get; }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="cookies"></param>
        /// <param name="parameters"></param>
        /// <param name="acceptOnly"></param>
        /// <param name="contentType"></param>
        public ScreenScraperRequest(string url, string user = "", string password = "",
                                    Dictionary<string, string> cookies = null,
                                    Dictionary<string, string> parameters = null,
                                    string acceptOnly = "", string contentType = "", Type typeExpected = null)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new System.ArgumentException($"'{nameof(url)}' cannot be null or empty.", nameof(url));
            }

            Url = url;
            User = user;
            Password = password;
            Cookies = cookies;
            Parameters = parameters;
            AcceptOnly = acceptOnly;
            ContentType = contentType;
            TypeExpected = typeExpected;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendNewLine("ScreenScraperRequest parameters:");
            sb.AppendNewLine($"URL: '{Url}'");
            sb.AppendNewLine($"User Name: '{User}', Password: '{Password}'");
            sb.AppendNewLine($"AcceptOnly: '{AcceptOnly}'");
            sb.AppendNewLine($"ContentType: '{ContentType}'");
            if (Cookies != null)
            {
                sb.AppendNewLine("List of Cookies:");
                foreach (var cookie in Cookies)
                {
                    sb.AppendNewLine($"key:  '{cookie.Key}', value: {cookie.Value}");
                }
            }
            if (Parameters != null)
            {
                sb.AppendNewLine("List of Parameters:");
                foreach (var item in Parameters)
                {
                    sb.AppendNewLine($"key:  '{item.Key}', value: {item.Value}");
                }
            }
            if (TypeExpected !=null)
            {
                sb.AppendNewLine($"TypeExpected: '{TypeExpected}'");
            }
            return sb.ToString();
        }
    }
}