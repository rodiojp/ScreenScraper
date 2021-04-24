using System;
using System.Text;
using ScreenScraper.Domain.Extensions;
using System.Collections.Generic;
namespace ScreenScraper.Domain
{
    /// <summary>
    /// The parameters that are required for the Screen Scraper to return data
    /// </summary>
    /// <remarks>
    /// VERY IMPORTANT
    /// The command line parameters must match the constructor of the run parameters
    /// This ensures that an user friendly message for the erroneous run parameters
    /// </remarks>
    public class RunParameters
    {
        public string User;
        public string Password;
        public DateTime StartDate;
        public DateTime EndDate;
        public IEnumerable<Currency> CurrenciesList;
        //ToDo: delete
        public Periodicity Periodicity { get; set; } = Periodicity.Daily;

        public RunParameters(string user, string password, DateTime startDate, DateTime endDate, IEnumerable<Currency> currenciesList)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException($"'{nameof(user)}' cannot be null or empty.", nameof(user));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"'{nameof(password)}' cannot be null or empty.", nameof(password));
            }
            if (startDate == null)
            {
                throw new ArgumentException($"'{nameof(startDate)}' cannot be null.", nameof(startDate));
            }
            if (endDate == null)
            {
                throw new ArgumentNullException(nameof(endDate));
            }
            User = user;
            Password = password;
            StartDate = startDate;
            EndDate = endDate;
            CurrenciesList = currenciesList ?? throw new ArgumentNullException(nameof(currenciesList));
            //string per = null;
            //Periodicity periodicity = per == null ? Periodicity.Daily : EnumUtils.FromDescription<Periodicity>(per.ToUpper());
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendNewLine("Credentials:");
            sb.AppendNewLine($"User Name: '{User}', Password: '{Password}'");
            sb.AppendNewLine($"StartDate: '{StartDate}'");
            sb.AppendNewLine($"EndDate: '{EndDate}'");
            sb.AppendNewLine("List of Currencies:");
            foreach (Currency item in CurrenciesList)
            {
                sb.AppendNewLine($"  {item}");
            }
            return sb.ToString();
        }
    }
}