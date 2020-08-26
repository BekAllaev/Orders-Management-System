using Newtonsoft.Json;
using OMS.WPFClient.Modules.Orders.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OMS.WPFClient.Infrastructure.Services.InvoiceInfoService
{
    public class InvoiceInfoWeb : IInvoiceInfoService
    {
        HttpClient httpClient;

        public InvoiceInfoWeb(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<OrderDetailObject>> GetOrderDetailsInfo(int orderId)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"Orders/GetOrderDetailsInfo/?id={orderId}");

                string responseString = await response.Content.ReadAsStringAsync();

                IEnumerable<OrderDetailObject> orderDetailObjects = JsonConvert.DeserializeObject<IEnumerable<OrderDetailObject>>(responseString);

                return orderDetailObjects;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<OrderObject>> GetOrdersInfo(int orderId)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"Orders/GetOrdersInfo/?id={orderId}");

                string responseString = await response.Content.ReadAsStringAsync();

                IEnumerable<OrderObject> ordersObjects = JsonConvert.DeserializeObject<IEnumerable<OrderObject>>(responseString);

                return ordersObjects;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
