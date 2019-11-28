using GNB.IBM.HerokuApp.Proxy;
using GNB.IBM.HerokuApp.Proxy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GNB.IBM.Api
{
    public class DataManager
    {
        private Proxy _proxy { get; set; }
        public DataManager(Proxy proxy)
        {
            this._proxy = proxy;
        }
        private List<RateRecord> Rates { get; set; } = new List<RateRecord>();
        private List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public async Task<List<RateRecord>> GetRates(bool fromMemory = false)
        {
            List<RateRecord> records = new List<RateRecord>();
            if (_proxy.IsConnected && !fromMemory || _proxy.IsConnected && !Rates.Any())
            {
                //Try catch in case that proxy disconnect before next keepAlive check to return stored data
                try
                {
                    var rates = await _proxy.GetRates();
                    if (rates != null)
                        Rates.Clear();
                    foreach (var rate in rates)
                    {
                        records.Add(rate);
                        Rates.Add(rate);
                    }
                }
                catch (Exception ex)
                {
                    foreach (var rate in Rates)
                        records.Add(rate);
                }
            }
            else
            {
                foreach (var rate in Rates)
                    records.Add(rate);
            }
            return Rates;
        }
        public async Task<List<Transaction>> GetTransactions(bool fromMemory = false)
        {
            List<Transaction> transactions = new List<Transaction>();
            if (_proxy.IsConnected && !fromMemory || _proxy.IsConnected && !Transactions.Any())
            {
                //Try catch in case that proxy disconnect before next keepAlive check to return stored data
                try
                {
                    var transProxy = await _proxy.GetTransactions();
                    if (transProxy != null)
                        Transactions.Clear();
                    foreach (var tra in transProxy)
                    {
                        transactions.Add(tra);
                        Transactions.Add(tra);
                    }
                }
                catch (Exception ex)
                {
                    foreach (var tran in Transactions)
                        transactions.Add(tran);
                }
            }
            else
            {
                foreach (var tra in Transactions)
                    transactions.Add(tra);
            }
            return transactions;
        }

    }
}
