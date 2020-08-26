using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
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

        #region Access to Categories
        public Task AddCategories(IEnumerable<Category> categories)
        {
            northwindContext.Categories.AddRange(categories);

            return Task.CompletedTask;
        }

        public Task AddCategory(Category category)
        {
            northwindContext.Categories.Add(category);

            return Task.CompletedTask;
        }

        public async Task DeleteCategories(int[] IdRange)
        {
            var categories = await northwindContext.Categories.Where(category => IdRange.Contains(category.CategoryID)).ToListAsync();

            northwindContext.Categories.RemoveRange(categories);
        }

        public async Task DeleteCategory(int id)
        {
            var category = await northwindContext.Categories.FindAsync(id);

            northwindContext.Categories.Remove(category);
        }

        public Task UpdateCategory(Category updatedCategory)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await northwindContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await northwindContext.Categories.FindAsync(id);

            return category;
        }
        #endregion

        #region Access to Customers
        public Task AddCustomer(Customer customer)
        {
            northwindContext.Customers.Add(customer);

            return Task.CompletedTask;
        }

        public Task AddCustomers(IEnumerable<Customer> customers)
        {
            northwindContext.Customers.AddRange(customers);

            return Task.CompletedTask;
        }

        public async Task DeleteCustomer(int id)
        {
            var customer = await northwindContext.Customers.FindAsync(id);

            northwindContext.Customers.Remove(customer);
        }

        public async Task DeleteCustomers(string[] IdRange)
        {
            var customers = await northwindContext.Customers.Where(customer => IdRange.Contains(customer.CustomerID)).ToListAsync();

            northwindContext.Customers.RemoveRange(customers);
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            var customer = await northwindContext.Customers.FindAsync(id);

            return customer;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await northwindContext.Customers.ToListAsync();
        }

        public Task UpdateCustomer(Customer updatedCustomer)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Access to Employees
        public Task AddEmployee(Employee employee)
        {
            northwindContext.Employees.Add(employee);

            return Task.CompletedTask;
        }

        public Task AddEmployees(IEnumerable<Employee> employees)
        {
            northwindContext.Employees.AddRange(employees);

            return Task.CompletedTask;
        }

        public async Task DeleteEmployee(int id)
        {
            var employee = await northwindContext.Employees.FindAsync(id);

            northwindContext.Employees.Remove(employee);
        }

        public async Task DeleteEmployees(int[] IdRange)
        {
            var employees = await northwindContext.Employees.Where(employee => IdRange.Contains(employee.EmployeeID)).ToListAsync();

            northwindContext.Employees.RemoveRange(employees);
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var employee = await northwindContext.Employees.FindAsync(id);

            return employee;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await northwindContext.Employees.ToListAsync();
        }

        public Task UpdateEmployee(Employee updatedEmployee)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Access to Orders
        public async Task AddOrder(Order order, IEnumerable<Order_Detail> orderDetails)
        {
            order.Order_Details = new HashSet<Order_Detail>(orderDetails);

            northwindContext.Orders.Add(order);
            await northwindContext.SaveChangesAsync();
        }

        public Task AddOrders(IEnumerable<Order> orders)
        {
            northwindContext.Orders.AddRange(orders);

            return Task.CompletedTask;
        }

        public async Task DeleteOrder(int id)
        {
            var order = await northwindContext.Orders.FindAsync(id);

            northwindContext.Orders.Remove(order);
        }

        public async Task DeleteOrders(int[] IdRange)
        {
            var orders = await northwindContext.Orders.Where(order => IdRange.Contains(order.OrderID)).ToListAsync();

            northwindContext.Orders.RemoveRange(orders);
        }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await northwindContext.Orders.FindAsync(id);

            return order;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await northwindContext.Orders.ToListAsync();
        }

        public Task UpdateOrder(Order updatedOrder)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Access to Order Details
        public Task AddOrderDetail(Order_Detail orderDetail)
        {
            northwindContext.Order_Details.Add(orderDetail);

            return Task.CompletedTask;
        }

        public Task AddOrderDetails(IEnumerable<Order_Detail> orderDetails)
        {
            northwindContext.Order_Details.AddRange(orderDetails);

            return Task.CompletedTask;
        }

        public async Task DeleteOrderDetail(int orderId, int productId)
        {
            var orderDetail = await northwindContext.Order_Details.FirstOrDefaultAsync(orderdetail => orderdetail.OrderID == orderId && orderdetail.ProductID == productId);

            northwindContext.Order_Details.Remove(orderDetail);
        }

        public async Task DeleteOrderDetails(int[] orderIds, int[] productIds = null)
        {
            List<Order_Detail> orderDetails = new List<Order_Detail>();

            if (productIds == null) orderDetails = await northwindContext.Order_Details.Where(orderDetail => orderIds.Contains(orderDetail.OrderID)).ToListAsync();
            else orderDetails = await northwindContext.Order_Details.Where(orderDetail => orderIds.Contains(orderDetail.OrderID) && productIds.Contains(orderDetail.ProductID)).ToListAsync();

            northwindContext.Order_Details.RemoveRange(orderDetails);
        }

        public async Task<IEnumerable<Order_Detail>> GetOrderDetails()
        {
            return await northwindContext.Order_Details.ToListAsync();
        }

        public async Task<IEnumerable<Order_Detail>> GetOrderDetailsByOrder(int orderId)
        {
            var orderDetails = await northwindContext.Order_Details.Where(orderDetail => orderDetail.OrderID == orderId).ToListAsync();

            return orderDetails;
        }

        public async Task DeleteOrderDetailsByOrder(int orderId)
        {
            var orderDetails = await northwindContext.Order_Details.Where(orderDetail => orderDetail.OrderID == orderId).ToListAsync();

            northwindContext.Order_Details.RemoveRange(orderDetails);
        }

        public Task UpdateOrderDetail(Order_Detail updatedOrderDetail)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Access to Products
        public Task AddProduct(Product product)
        {
            northwindContext.Products.Add(product);

            return Task.CompletedTask;
        }

        public Task AddProducts(IEnumerable<Product> products)
        {
            northwindContext.Products.AddRange(products);

            return Task.CompletedTask;
        }

        public async Task DeleteProduct(int id)
        {
            var product = await northwindContext.Products.FindAsync(id);

            northwindContext.Products.Remove(product);
        }

        public async Task DeleteProducts(int[] IdRange)
        {
            var products = await northwindContext.Products.Where(product => IdRange.Contains(product.ProductID)).ToListAsync();

            northwindContext.Products.RemoveRange(products);
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await northwindContext.Products.FindAsync(id);

            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await northwindContext.Products.ToListAsync();
        }

        public Task UpdateProduct(Product updatedProduct)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Access to Shippers
        public Task AddShipper(Shipper shipper)
        {
            northwindContext.Shippers.Add(shipper);

            return Task.CompletedTask;
        }

        public Task AddShippers(IEnumerable<Shipper> shippers)
        {
            northwindContext.Shippers.AddRange(shippers);

            return Task.CompletedTask;
        }

        public async Task DeleteShipper(int id)
        {
            var shipper = await northwindContext.Shippers.FindAsync(id);

            northwindContext.Shippers.Remove(shipper);
        }

        public async Task DeleteShippers(int[] IdRange)
        {
            var shippers = await northwindContext.Shippers.Where(shipper => IdRange.Contains(shipper.ShipperID)).ToListAsync();

            northwindContext.Shippers.RemoveRange(shippers);
        }

        public async Task<Shipper> GetShipperById(int id)
        {
            var shipper = await northwindContext.Shippers.FindAsync(id);

            return shipper;
        }

        public async Task<IEnumerable<Shipper>> GetShippers()
        {
            return await northwindContext.Shippers.ToListAsync();
        }

        public Task UpdateShipper(Shipper updatedShipper)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Access to Supplier
        public Task AddSupplier(Supplier supplier)
        {
            northwindContext.Suppliers.Add(supplier);

            return Task.CompletedTask;
        }

        public Task AddSuppliers(IEnumerable<Supplier> suppliers)
        {
            northwindContext.Suppliers.AddRange(suppliers);

            return Task.CompletedTask;
        }

        public async Task DeleteSupplier(int id)
        {
            var supplier = await northwindContext.Suppliers.FindAsync(id);

            northwindContext.Suppliers.Remove(supplier);
        }

        public async Task DeleteSuppliers(int[] IdRange)
        {
            var suppliers = await northwindContext.Suppliers.Where(supplier => IdRange.Contains(supplier.SupplierID)).ToListAsync();

            northwindContext.Suppliers.RemoveRange(suppliers);
        }

        public async Task<Supplier> GetSupplierById(int id)
        {
            var supplier = await northwindContext.Suppliers.FindAsync(id);

            return supplier;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            return await northwindContext.Suppliers.ToListAsync();
        }

        public Task UpdateSupplier(Supplier updatedSupplier)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
