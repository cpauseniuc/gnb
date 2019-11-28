using Flurl.Http;
using GNB.IBM.HerokuApp.Proxy.Entities;
using GNB.IBM.HerokuApp.Proxy.Infraestructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GNB.IBM.HerokuApp.Proxy
{
    public partial class Proxy
    {
        public async Task<List<Transaction>> GetTransactions()
        {
            try
            {
                return await (BaseUrl + "/transactions.json").GetJsonAsync<List<Transaction>>();
            }
            catch (FlurlHttpException ex)
            {
                var res = await ex.GetResponseJsonAsync<HerokuAppError>();
                if (res != null)
                {
                    throw new ProxyException(res.Error, ex);
                }
                else
                {
                    throw new ProxyException(ex.Message, ex);

                }
            }
            catch (Exception ex)
            {
                throw new ProxyException(ex.Message, ex);

            }
        }

    }
}
