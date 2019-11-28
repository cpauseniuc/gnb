using Flurl.Http;
using Fuxion.Net;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GNB.IBM.HerokuApp.Proxy
{
    public partial class Proxy : ConnectableNotifier<Proxy>
    {

        public Proxy()
        {
            PropertyChanged += (s, e) => e.Case<object>(a =>
            {
                try
                {
                    FlurlHttp.ConfigureClient(BaseUrl, cli =>
                    {
                        cli.Settings.Timeout = TimeSpan.FromMinutes(30);
                        if (cli.HttpMessageHandler is HttpClientHandler hch)
                            hch.ServerCertificateCustomValidationCallback += (_, __, ___, ____) => true;
                    });
                }
                catch (Exception ex) { }
            }, nameof(Host), nameof(Port), nameof(UseHttps));
        }
        public string Host
        {
            get => GetValue<string>();
            set { SetValue(value); ConnectionPropertyChanged(); }
        }
        public int Port
        {
            get => GetValue<int>();
            set { SetValue(value); ConnectionPropertyChanged(); }
        }
        public bool UseHttps
        {
            get => GetValue<bool>();
            set { SetValue(value); ConnectionPropertyChanged(); }
        }
        public string BaseUrl => ((UseHttps) ? "https://" : "http://") + Host + ":" + Port;
        protected override async Task OnConnect()
        {
            await GetRates();
        }
        protected override async Task OnKeepAlive()
        {
            Console.WriteLine("============> Start KeepAlive <============");
            await GetRates();

        }
        protected override Task OnDisconnect() => Task.CompletedTask; 
    }
}
