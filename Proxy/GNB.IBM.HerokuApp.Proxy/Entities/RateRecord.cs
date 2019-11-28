using GNB.IBM.HerokuApp.Proxy.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GNB.IBM.HerokuApp.Proxy.Entities
{
    public class RateRecord
    {
        public Currency From { get; set; }
        public Currency To { get; set; }
        public decimal Rate { get; set; }
        public decimal GetInverse()
        {
            return 1 / Rate;
        }
    }
}
