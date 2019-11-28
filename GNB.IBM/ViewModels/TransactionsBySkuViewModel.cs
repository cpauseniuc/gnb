using GNB.IBM.HerokuApp.Proxy.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GNB.IBM.Api.ViewModels
{
    public class TransactionsBySkuViewModel
    {
        public decimal TotalAmount { get; set; }
        public Currency Currency { get; set; }
        public List<TransactionViewModel> Transactions { get; set; } = new List<TransactionViewModel>();
    }
}
