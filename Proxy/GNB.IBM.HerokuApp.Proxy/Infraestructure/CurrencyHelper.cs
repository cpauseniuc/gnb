using GNB.IBM.HerokuApp.Proxy.Entities;
using GNB.IBM.HerokuApp.Proxy.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GNB.IBM.HerokuApp.Proxy.Infraestructure
{
    public static class CurrencyHelper
    {
        private static List<RateRecord> rates = new List<RateRecord>();
        public static decimal GetRate(Currency from, Currency to, List<RateRecord> ratesRecord)
        {
            rates = ratesRecord;
            _graph = null;
            ConstructGraph();
            return Rate(from, to);
        }
        private static Dictionary<Currency, List<Currency>> _graph;
        private static void ConstructGraph()
        {
            if (_graph == null)
            {
                _graph = new Dictionary<Currency, List<Currency>>();
                foreach (var rate in rates)
                {
                    if (!_graph.ContainsKey(rate.From))
                        _graph[rate.From] = new List<Currency>();
                    if (!_graph.ContainsKey(rate.To))
                        _graph[rate.To] = new List<Currency>();
                    _graph[rate.From].Add(rate.To);
                    _graph[rate.To].Add(rate.From);
                }
            }
        }
        public static decimal Rate(Currency from, Currency to)
        {
            if (_graph[from].Contains(to))
            {
                return GetKnownRate(from, to);
            }
            else
            {
                foreach (var code in _graph[from])
                {
                    decimal rate = Rate(code, to);
                    if (rate != 0) 
                        return rate * GetKnownRate(from, code);
                }
            }
            return 0; 
        }
        public static decimal GetKnownRate(Currency from, Currency to)
        {
            var rate = rates.SingleOrDefault(fr => fr.From == from && fr.To == to);
            var rate_i = rates.SingleOrDefault(fr => fr.From == to && fr.To == from);
            if (rate == null)
                return 1 / rate_i.Rate;
            return rate.Rate;
        }
    }
}
