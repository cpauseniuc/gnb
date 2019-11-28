using Fuxion.Configuration;
using GNB.IBM.HerokuApp.Proxy;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GNB.IBM.Api.ServiceCollectionExtensions
{
    public static class ConfigureProxyCollectionExtension
    {
        public static void AddHerokuApp(this IServiceCollection services)
        {
            var configurationManager = services.BuildServiceProvider().GetRequiredService<IConfigurationManager>();
            var apiConfig = configurationManager.Get<ApiConfiguration>();
            configurationManager.Save();
            if (apiConfig != null)
            {
                var proxy = new Proxy();
                proxy.Host = apiConfig.ApiHostName;
                proxy.Port = apiConfig.ApiPort;
                proxy.UseHttps = apiConfig.UseHttps;
                proxy.ConnectionMode = Fuxion.Net.ConnectionMode.Automatic;
                services.AddSingleton(proxy);
            }
            else
                throw new Exception("Invalid configuration");
        }
    }
}
