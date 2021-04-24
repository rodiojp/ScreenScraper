using ScreenScraper.Domain;
using ScreenScraper.Services.Interfaces;
using ScreenScraper.Domain.Utilities;
using System.Net.Http;
using System.Collections.Generic;
using System;
using System.Net.Http.Headers;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ScreenScraper.Services
{
    /// <summary>
    /// Makes the appropriate Web Reqeusts
    /// </summary>
    /// <remarks>
    /// How it works: Will use the <see cref="WebRequesterUtility"/>
    /// </remarks>
    public class WebScraperService : IScrape
    {
        private static HttpClient client = new HttpClient();
        private readonly IEnumerable<ScreenScraperRequest> ScreenScraperRequests;
        public event EventHandler OnRequestBeingMade;
        public event EventHandler OnResponseReceived;
        /// <summary>
        /// Constructor for the Scraper service
        /// </summary>
        /// <param name="screenScraperRequests"></param>
        public WebScraperService(IEnumerable<ScreenScraperRequest> screenScraperRequests)
        {
            ScreenScraperRequests = screenScraperRequests;
        }
        /// <summary>
        /// Constructor that will call the providers method to get the Screen Scrape Requests
        /// </summary>
        /// <param name="rp"><see cref="RunParameters"/> object</param>
        /// <param name="provider">An implementation of <see cref="IScreenScraperRequestProvider"/> that will translate
        /// run parameters into actual web requests 
        /// </param>
        public WebScraperService(RunParameters rp, IScreenScraperRequestProvider provider)
        {
            if (rp is null)
            {
                throw new ArgumentNullException(nameof(rp));
            }

            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            ScreenScraperRequests = provider.GetScreenScrapeRequests(rp);
        }
        /// <summary>
        /// Returns the final request
        /// </summary>
        /// <returns>The output of the final request</returns>
        public Tuple<string,Type>[] Scrape()
        {
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken().Result);
            if (ScreenScraperRequests.ElementAt(0).ContentType != null)
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ScreenScraperRequests.ElementAt(0).ContentType));
            }
            //var dates = SplitDateRange(DateTime.Parse(ScreenScraperRequests.ElementAt(1).Parameters["startDate"]),
            //    DateTime.Parse(ScreenScraperRequests.ElementAt(1).Parameters["endDate"]),
            //    ScreenScraperRequests.ElementAt(1).Parameters["periodicity"] == "0" ? 1 : 7);
            var tasks = new List<Task<Tuple<string, Type>>>();
            //using (var date = dates.GetEnumerator())
            //{
            //    //Create new tasks
            //    while (date.MoveNext())
            //    {
            //        ScreenScraperRequests.ElementAt(1).Parameters["startDate"] = date.Current.Item1.ToString("yyyy-MM-dd");
            //        ScreenScraperRequests.ElementAt(1).Parameters["endDate"] = date.Current.Item2.ToString("yyyy-MM-dd");
            //        tasks.Add(MakeRequest($"{ScreenScraperRequests.ElementAt(1).Url}?{BuildPostParameters(ScreenScraperRequests.ElementAt(1).Parameters)}"));
            //    }
            //}
            foreach (var request in ScreenScraperRequests)
            {
                string url = $"{request.Url}?{BuildPostParameters(request.Parameters)}";
                var tuple = new Tuple<string, Type>(url, request.TypeExpected);
                tasks.Add(MakeRequest(tuple));
            }

            //Wait for the tasks to complete and return
            return Task.WhenAll(tasks).Result;
        }

        private string BuildPostParameters(Dictionary<string, string> parameters)
        {
            StringBuilder sb = new StringBuilder();
            const char concatenator = '&';
            foreach (KeyValuePair<string, string> item in parameters)
            {
                sb.Append($"{concatenator}{item.Key}={item.Value}");
            }
            //strip the initial ampersand
            return sb.ToString().TrimStart(new char[] { concatenator });
        }
        private IEnumerable<Tuple<DateTime, DateTime>> SplitDateRange(DateTime start, DateTime end, int dayChunkSize)
        {
            DateTime chunkEnd;
            while ((chunkEnd = start.AddDays(dayChunkSize)) < end)
            {
                yield return Tuple.Create(start, chunkEnd.AddDays(-1));
                start = chunkEnd;
            }
            yield return Tuple.Create(start, end);
        }
        private async Task<Tuple<string, Type>> MakeRequest(Tuple<string,Type> request)
        {
            HttpResponseMessage response = await client.GetAsync(request.Item1);
            using (Stream stream = await response.Content.ReadAsStreamAsync())
            using (StreamReader reader = new StreamReader(stream))
            {
                string text = reader.ReadToEnd();
                if (text.Substring(0, 6) == @")]}',\n")
                {
                    text.Substring(6);
                }
                if (!response.StatusCode.ToString().Equals("OK"))
                {
                    throw new ArgumentException($"Failed to get data from Web API, Error: '{text}'");
                }
                else
                {
                    return new Tuple<string,Type>(text, request.Item2);
                }
            }
        }
        private async Task<string> GetToken()
        {
            //Web API requires JWT tocken before getting data
            //string authParam = $"{{'username': '{ScreenScraperRequests.ElementAt(0).User}','password': '{ScreenScraperRequests.ElementAt(0).Password}'}}";
            string authParam = JObject.FromObject(new { username = ScreenScraperRequests.ElementAt(0).User, password = ScreenScraperRequests.ElementAt(0).Password }).ToString();
            HttpResponseMessage response = await client.PostAsync(ScreenScraperRequests.ElementAt(0).Url,
                new StringContent(authParam, Encoding.UTF8, ScreenScraperRequests.ElementAt(0).ContentType));
            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException("Authentication failure logging into Web API");
            }
            string bearerData = await response.Content.ReadAsStringAsync();
            string token = JObject.Parse(bearerData)["accessToken"].ToString();
            return token;
        }
        private void RequestBeingMade(EventArgs e)
        {
            if (OnRequestBeingMade != null)
            {
                OnRequestBeingMade(this, e);
            }
        }
        private void ResponseReceived(EventArgs e)
        {
            if (OnResponseReceived != null)
            {
                OnResponseReceived(this, e);
            }
        }

    }
}