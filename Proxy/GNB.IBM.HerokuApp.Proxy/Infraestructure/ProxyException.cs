using System;
using System.Collections.Generic;
using System.Text;

namespace GNB.IBM.HerokuApp.Proxy.Infraestructure
{
    public class ProxyException : Exception
    {
        public ProxyException() { }
        public ProxyException(string msg) : base(msg) { }
        public ProxyException(string msg, Exception ex) : base(msg, ex) { }
    }
}
