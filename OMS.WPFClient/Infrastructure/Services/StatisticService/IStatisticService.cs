using OMS.WPFClient.Modules.Dashboard.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OMS.WPFClient.Infrastructure.Services.StatisticService
{
    public interface IStatisticService
    {
        Task<IEnumerable<OrdersByCountry>> GetOrdersByCountries();

        Task<IEnumerable<SalesByCategory>> GetSalesByCategories();

        Task<IEnumerable<SalesByCountry>> GetSalesByCountries();

        Task<IEnumerable<CustomersByCountry>> GetCustomersByCountries();

        Task<IEnumerable<PurchasesByCustomers>> GetPurchasesByCustomers();

        Task<IEnumerable<EmployeeSales>> GetSalesByEmployees();

        Task<IEnumerable<ProductsByCateogries>> GetProductsByCategories();

        Task<string> GetSummary(string summary);

    }
}

