using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using log4net;
using ScreenScraper.Domain;
using ScreenScraper.Domain.Extensions;
using ScreenScraper.WebService.Contracts;
using ScreenScraper.Services.Interfaces;
using ScreenScraper.Services;
using ScreenScraper.Application;

namespace ScreenScraper.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    /// <summary>
    /// Implementation of IMeasurementFetch
    /// </summary>
    public class MeasurementFetchService : IMeasurementFetch
    {
        //Log4Net logger
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string GetData(int value)
        {
            Log.Info($"GetData. You entered: {value}");
            return string.Format("You entered: {0}", value);
        }

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    Log.Info($"GetDataUsingDataContract {composite.StringValue}");
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}


        /// <summary>
        /// a less coupled request where all parameters are supplied by the client
        /// </summary>
        /// <param name="user">The User Name to login into the Web API site</param>
        /// <param name="password">The Password needet to enter the Web API</param>
        /// <param name="startDate">The day to start from</param>
        /// <param name="endDate">The end date</param>
        /// <param name="currencies">An Array of integers representing the currency in the Web IPI site</param>
        /// <returns>an array of <see cref="MeasurementReading"/> objects</returns>
        public IEnumerable<MeasurementReading> GetMesurementsFromParameters(string user, string password, DateTime startDate, DateTime endDate,
           IEnumerable<int> currencies)
        {
            Log.Debug("Running GetMesurementsFromParameters");
            var result = new List<MeasurementReading>();
            try
            {
                var currenciesList = currencies.Select(x => new Currency(x)).ToArray(); // new int[] { currency }; 
                var rp = new RunParameters(user, password, startDate, endDate, currenciesList);
                Log.Debug(rp.ToString());
                BuildMesurementReadings(rp, result);
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToLogString());
                throw new FaultException<UnexpectedServiceFault>(
                    new UnexpectedServiceFault(ex.Message, ex.Source, ex.StackTrace, ex.TargetSite.ToString()),
                    new FaultReason(string.Format(CultureInfo.InvariantCulture, "{0}", "Service fault exception GetMesurementsFromParameters:" + ex.InnerException.Message))
                    );
            }
        }
        /// <summary>
        /// Creates a manager and uses the manager to do 
        /// the Screen Scraping and returns the result which
        /// is then covered to our contracted result
        /// </summary>
        /// <param name="rp"></param>
        /// <param name="result"></param>
        private void BuildMesurementReadings(RunParameters rp, List<MeasurementReading> result)
        {
            IScreenScraperRequestProvider provider = new ScreenScraperRequestProvider();
            var manager = new WebServiceScrapeManager(new WebScraperService(rp, provider), rp);
            manager.OnRequestBeingProcessed += LoggingEventOccurs;
            result.AddRange(
                manager.GetAndHandleScreenScrapeResult().Where(x=> rp.CurrenciesList.Select(y =>y.ID).ToList().Contains(x.CurrencySource.ID)).Select(MeasurementReading.FromCurrencyReading)
                );
        }

        private void LoggingEventOccurs(object sender, EventArgs e)
        {
            var loggingEventArgs = e as LoggingEventArgs;
            if (loggingEventArgs != null)
                Log.Debug(loggingEventArgs.Message);
        }
    }


}
