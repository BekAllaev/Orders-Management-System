using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using OMS.Data;
using OMS.Data.Models;

namespace OMS.DataAccessWeb
{
    public class NorthwindWebRepository : INorthwindRepository
    {
        readonly HttpClient httpClient;

        public NorthwindWebRepository(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        #region Access
        #endregion
        public Task AddCategories(IEnumerable<Category> categories)
        {
            throw new NotImplementedException();
        }

        public Task AddCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Task AddCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task AddCustomers(IEnumerable<Customer> customers)
        {
            throw new NotImplementedException();
        }

        public Task AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task AddEmployees(IEnumerable<Employee> employees)
        {
            throw new NotImplementedException();
        }

        public Task AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task AddOrderDetail(Order_Detail orderDetail)
        {
            throw new NotImplementedException();
        }

        public Task AddOrderDetails(IEnumerable<Order_Detail> orderDetails)
        {
            throw new NotImplementedException();
        }

        public Task AddOrders(IEnumerable<Order> orders)
        {
            throw new NotImplementedException();
        }

        public Task AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task AddProducts(IEnumerable<Product> products)
        {
            throw new NotImplementedException();
        }

        public Task AddShipper(Shipper shipper)
        {
            throw new NotImplementedException();
        }

        public Task AddShippers(IEnumerable<Shipper> shippers)
        {
            throw new NotImplementedException();
        }

        public Task AddSupplier(Supplier supplier)
        {
            throw new NotImplementedException();
        }

        public Task AddSuppliers(IEnumerable<Supplier> suppliers)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategories(int[] IdRange)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCustomers(string[] IdRange)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEmployees(int[] IdRange)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderDetail(int orderId, int productId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderDetails(int[] orderIds, int[] productIds = null)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderDetailsByOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrders(int[] IdRange)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProducts(int[] IdRange)
        {
            throw new NotImplementedException();
        }

        public Task DeleteShipper(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteShippers(int[] IdRange)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSupplier(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSuppliers(int[] IdRange)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetCustomerById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployeeById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetEmployees()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order_Detail>> GetOrderDetails()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order_Detail>> GetOrderDetailsByOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetOrders()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Task<Shipper> GetShipperById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Shipper>> GetShippers()
        {
            throw new NotImplementedException();
        }

        public Task<Supplier> GetSupplierById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Supplier>> GetSuppliers()
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderDetail(Order_Detail orderDetail)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task UpdateShipper(Shipper shipper)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSupplier(Supplier supplier)
        {
            throw new NotImplementedException();
        }
    }
}
