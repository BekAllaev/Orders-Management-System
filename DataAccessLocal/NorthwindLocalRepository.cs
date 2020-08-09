using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.Data;
using OMS.Data.Models;

namespace OMS.DataAccessLocal
{
    public class NorthwindLocalRepository : INorthwindRepository
    {
        private NorthwindContext northwindContext;

        public NorthwindLocalRepository(NorthwindContext northwindContext)
        {
            this.northwindContext = northwindContext;
        }

        public Task AddCategories(IEnumerable<Category> newCategories)
        {
            throw new NotImplementedException();
        }

        public Task AddCategory(Category newCategory)
        {
            throw new NotImplementedException();
        }

        public Task AddCustomer(Customer newCustomer)
        {
            throw new NotImplementedException();
        }

        public Task AddCustomers(IEnumerable<Customer> newCustomers)
        {
            throw new NotImplementedException();
        }

        public Task AddEmployee(Employee newEmployee)
        {
            throw new NotImplementedException();
        }

        public Task AddEmployees(IEnumerable<Employee> newEmployees)
        {
            throw new NotImplementedException();
        }

        public Task AddOrder(Order newOrder)
        {
            throw new NotImplementedException();
        }

        public Task AddOrderDetail(Order_Detail newOrderDetail)
        {
            throw new NotImplementedException();
        }

        public Task AddOrderDetails(IEnumerable<Order_Detail> newOrderDetails)
        {
            throw new NotImplementedException();
        }

        public Task AddOrders(IEnumerable<Order> newOrders)
        {
            throw new NotImplementedException();
        }

        public Task AddProduct(Product newProduct)
        {
            throw new NotImplementedException();
        }

        public Task AddProducts(IEnumerable<Product> newProducts)
        {
            throw new NotImplementedException();
        }

        public Task AddShipper(Shipper newShipper)
        {
            throw new NotImplementedException();
        }

        public Task AddShippers(IEnumerable<Shipper> newShippers)
        {
            throw new NotImplementedException();
        }

        public Task AddSupplier(Supplier newSupplier)
        {
            throw new NotImplementedException();
        }

        public Task AddSuppliers(IEnumerable<Supplier> newSuppliers)
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

        public Task DeleteCustomers(int[] IdRange)
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

        public Task DeleteOrderDetail(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderDetails(int[] IdRange)
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

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await northwindContext.Customers.ToListAsync();
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

        public Task UpdateCategory(Category updatedCategory)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCustomer(Customer updatedCustomer)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEmployee(Employee updatedEmployee)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrder(Order updatedOrder)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderDetail(Order_Detail updatedOrderDetail)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProduct(Product updatedProduct)
        {
            throw new NotImplementedException();
        }

        public Task UpdateShipper(Shipper updatedShipper)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSupplier(Supplier updatedSupplier)
        {
            throw new NotImplementedException();
        }
    }
}
