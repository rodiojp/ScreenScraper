using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ScreenScraper.WebService.Contracts;

namespace ScreenScraper.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IMeasurementFetch
    {

        [OperationContract]
        string GetData(int value);

        // TODO: Add your service operations here
        [OperationContract]
        IEnumerable<MeasurementReading> GetMesurementsFromParameters(string user, string password, DateTime startDate, DateTime endDate,
           IEnumerable<int> currencies);

    }
}
