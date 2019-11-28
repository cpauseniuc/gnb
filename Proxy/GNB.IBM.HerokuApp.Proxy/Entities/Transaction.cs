using GNB.IBM.HerokuApp.Proxy.Enums;
using GNB.IBM.HerokuApp.Proxy.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GNB.IBM.HerokuApp.Proxy.Entities
{
    public class Transaction
    {
        public string SKU { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public  decimal GetAmountByCurrency(Currency desiredCurrency, List<RateRecord> rates)
        {
            decimal desiredAmount = 0;
            if (rates != null && rates.Any())
            {
                var rate = CurrencyHelper.GetRate(Currency, desiredCurrency, rates);
                return Amount * rate;
            }
            return desiredAmount;       
        }

        
    }
}