using OMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Data
{
    /// <summary>
    /// Provide CRUD operations over entities of Northwind database 
    /// </summary>
    public interface INorthwindRepository
    {
        #region Access to Employees
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task AddEmployee(Employee employee);
        Task AddEmployees(IEnumerable<Employee> employees);
        Task DeleteEmployee(int id);
        Task DeleteEmployees(int[] IdRange);
        Task UpdateEmployee(Employee employee);
        #endregion

        #region Access to Products
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task AddProduct(Product product);
        Task AddProducts(IEnumerable<Product> products);
        Task DeleteProduct(int id);
        Task DeleteProducts(int[] IdRange);
        Task UpdateProduct(Product product);
        #endregion

        #region Access to Customers
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomerById(int id);
        Task AddCustomer(Customer customer);
        Task AddCustomers(IEnumerable<Customer> customers);
        Task DeleteCustomer(int id);
        Task DeleteCustomers(int[] IdRange);
        Task UpdateCustomer(Customer customer);
        #endregion

        #region Access to Categories
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
        Task AddCategory(Category category);
        Task AddCategories(IEnumerable<Category> categories);
        Task DeleteCategory(int id);
        Task DeleteCategories(int[] IdRange);
        Task UpdateCategory(Category category);
        #endregion

        #region Access to Orders
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrderById(int id);
        Task AddOrder(Order order);
        Task AddOrders(IEnumerable<Order> orders);
        Task DeleteOrder(int id);
        Task DeleteOrders(int[] IdRange);
        Task UpdateOrder(Order order);
        #endregion

        #region Access to Order Details
        Task<IEnumerable<Order_Detail>> GetOrderDetails();
        //Task<Order_Detail> GetOrderDetailById(int id);
        Task AddOrderDetail(Order_Detail orderDetail);
        Task AddOrderDetails(IEnumerable<Order_Detail> orderDetails);
        Task DeleteOrderDetail(int id);
        Task DeleteOrderDetails(int[] IdRange);
        Task UpdateOrderDetail(Order_Detail orderDetail);
        #endregion

        #region Access to Shippers
        Task<IEnumerable<Shipper>> GetShippers();
        Task<Shipper> GetShipperById(int id);
        Task AddShipper(Shipper shipper);
        Task AddShippers(IEnumerable<Shipper> shippers);
        Task DeleteShipper(int id);
        Task DeleteShippers(int[] IdRange);
        Task UpdateShipper(Shipper shipper);
        #endregion

        #region Access to Suppliers
        Task<IEnumerable<Supplier>> GetSuppliers();
        Task<Supplier> GetSupplierById(int id);
        Task AddSupplier(Supplier supplier);
        Task AddSuppliers(IEnumerable<Supplier> suppliers);
        Task DeleteSupplier(int id);
        Task DeleteSuppliers(int[] IdRange);
        Task UpdateSupplier(Supplier supplier);
        #endregion
    }
}
