using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WPF_Modern_Chart_Client.ModelClasses;

namespace WPF_Modern_Chart_Client.ServiceAdapter
{
   public class ProxyAdapter
    {

        public ObservableCollection<SalesTerritory> _SalesData;

        public async Task<ObservableCollection<SalesTerritory>> GetSalesInformation()
        {
            using (var webClient = new HttpClient())
            {
                webClient.BaseAddress = new Uri("http://localhost:1166/");
                webClient.DefaultRequestHeaders.Accept.Clear();
                webClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("Application/json"));
                HttpResponseMessage resp = await webClient.GetAsync("api/SalesTerritories");

                if(resp.IsSuccessStatusCode)
                {
                    _SalesData = await resp.Content.ReadAsAsync<ObservableCollection<SalesTerritory>>();
                }

                return _SalesData;
            }
        }
    }
}
