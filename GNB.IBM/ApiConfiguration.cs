using Fuxion.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GNB.IBM.Api
{
    public class ApiConfiguration : ConfigurationItem<ApiConfiguration>
    {
        public override Guid ConfigurationItemId => Guid.Parse("{C9EB3049-2404-46D5-B090-DE6EB1924019}");
        public string ApiHostName { get; set; } = "quiet-stone-2094.herokuapp.com";
        public int ApiPort { get; set; } = 80;
        public bool UseHttps { get; set; } = false;
    }
}
