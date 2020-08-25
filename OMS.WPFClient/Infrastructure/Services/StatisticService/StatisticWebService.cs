using Newtonsoft.Json;
using OMS.WPFClient.Modules.Dashboard.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OMS.WPFClient.Infrastructure.Services.StatisticService
{
    public class StatisticWebService : IStatisticService
    {
        HttpClient httpClient;

        public StatisticWebService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<CustomersByCountry>> GetCustomersByCountries()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("Statistics/GetCustomersByCountries");

                string responseString = await response.Content.ReadAsStringAsync();

                IEnumerable<CustomersByCountry> customersByCountries = JsonConvert.DeserializeObject<IEnumerable<CustomersByCountry>>(responseString);

                return customersByCountries;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<OrdersByCountry>> GetOrdersByCountries()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("Statistics/GetOrdersByCountries");

                string responseString = await response.Content.ReadAsStringAsync();

                IEnumerable<OrdersByCountry> ordersByCountries = JsonConvert.DeserializeObject<IEnumerable<OrdersByCountry>>(responseString);

                return ordersByCountries;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<ProductsByCateogries>> GetProductsByCategories()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("Statistics/GetProductsByCategories");

                string responseString = await response.Content.ReadAsStringAsync();

                IEnumerable<ProductsByCateogries> productsByCateogries = JsonConvert.DeserializeObject<IEnumerable<ProductsByCateogries>>(responseString);

                return productsByCateogries;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<PurchasesByCustomers>> GetPurchasesByCustomers()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("Statistics/GetPurchasesByCustomers");

                string responseString = await response.Content.ReadAsStringAsync();

                IEnumerable<PurchasesByCustomers> purchasesByCustomers = JsonConvert.DeserializeObject<IEnumerable<PurchasesByCustomers>>(responseString);

                return purchasesByCustomers;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<SalesByCategory>> GetSalesByCategories()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("Statistics/GetSalesByCategories");

                string responseString = await response.Content.ReadAsStringAsync();

                IEnumerable<SalesByCategory> salesByCategories = JsonConvert.DeserializeObject<IEnumerable<SalesByCategory>>(responseString);

                return salesByCategories;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<SalesByCountry>> GetSalesByCountries()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("Statistics/GetSalesByCountries");

                string responseString = await response.Content.ReadAsStringAsync();

                IEnumerable<SalesByCountry> salesByCountries = JsonConvert.DeserializeObject<IEnumerable<SalesByCountry>>(responseString);

                return salesByCountries;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<EmployeeSales>> GetSalesByEmployees()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("Statistics/GetSalesByEmployees");

                string responseString = await response.Content.ReadAsStringAsync();

                IEnumerable<EmployeeSales> employeeSales = JsonConvert.DeserializeObject<IEnumerable<EmployeeSales>>(responseString);

                return employeeSales;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<string> GetSummary(string summary)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"Statistics/GetSummary?summaryType={summary}");

                string responseString = await response.Content.ReadAsStringAsync();

                return responseString;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
