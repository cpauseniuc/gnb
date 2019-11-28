using GNB.IBM.HerokuApp.Proxy.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GNB.IBM.Api.ViewModels
{
    public class TransactionViewModel
    {
        public string SKU { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
    }
}
