using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenScraper.Domain;
using ScreenScraper.Domain.DTOs;
using ScreenScraper.Services.Interfaces;

namespace ScreenScraper.Services
{
    /// <summary>
    /// Builds Requests to get Currency Rates as JSON download
    /// </summary>
    public class ScreenScraperRequestProvider : IScreenScraperRequestProvider
    {
        public IEnumerable<ScreenScraperRequest> GetScreenScrapeRequests(RunParameters runParameters)
        {
            //Need this request in first request to force a prompt for Windows Athorization credentials
            Dictionary<string, string> cookies = new Dictionary<string, string>
            {
                {"SMCHALLENGE","YES" }
            };
            //get a string of Currencies IDs delimited by ','
            //int[] currenciesIds = runParameters.CurrenciesList.Select(x => x.ID).ToArray();
            //string strCurrenciesIds = string.Join(",", currenciesIds);

            //parameters for request
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                //{"currenciesId",strCurrenciesIds },
                {"onDate",runParameters.StartDate.ToString("yyyy-MM-dd") },
                {"periodicity","0"}
            };
            // First request is to login
            // Get the JSON response
            ScreenScraperRequest rates = new ScreenScraperRequest($"https://www.nbrb.by/api/ExRates/Rates", 
                runParameters.User, runParameters.Password, null, parameters, null, null, typeof(CurrencyRateShortDto));
            ScreenScraperRequest currencies = new ScreenScraperRequest($"https://www.nbrb.by/api/ExRates/Currencies", 
                runParameters.User, runParameters.Password, null, parameters, null, null, typeof(CurrencyDto));
            IEnumerable<ScreenScraperRequest> screenScraperRequests = new List<ScreenScraperRequest> { currencies, rates };
            return screenScraperRequests;
        }
    }
}
