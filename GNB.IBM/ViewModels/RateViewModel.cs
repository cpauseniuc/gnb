using GNB.IBM.HerokuApp.Proxy.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GNB.IBM.Api.ViewModels
{
    public class RateViewModel
    {
        public Currency From { get; set; }
        public Currency To { get; set; }
        public decimal Rate { get; set; }
    }
}
