using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenScraper.Domain
{
    public enum Periodicity
    {
        [Description("0")]
        Daily,
        [Description("1")]
        Monthly
    }
}
