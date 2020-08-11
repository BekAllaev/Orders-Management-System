using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        #region Access to Categories
        public Task AddCategories(IEnumerable<Category> categories)
        {
            throw new NotImplementedException();
        }

        public async Task AddCategory(Category category)
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8);

                await httpClient.PostAsync("Categories", httpContent);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task DeleteCategories(int[] IdRange)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteCategory(int id)
        {
            try
            {
                await httpClient.DeleteAsync($"Categories/{id}");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("Categories");

                var responseString = await response.Content.ReadAsStringAsync();

                var employee = JsonConvert.DeserializeObject<List<Category>>(responseString);

                return employee;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Category> GetCategoryById(int id)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"Categories/{id}");

                string responseString = await response.Content.ReadAsStringAsync();

                Category category = JsonConvert.DeserializeObject<Category>(responseString);

                return category;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateCategory(Category category)
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8);

                await httpClient.PutAsync($"Categories/{category.CategoryID}", httpContent);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Access to Customers
        public async Task AddCustomer(Customer customer)
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8);

                await httpClient.PostAsync("Customers", httpContent);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Task AddCustomers(IEnumerable<Customer> customers)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteCustomer(int id)
        {
            try
            {
                await httpClient.DeleteAsync($"Customers/{id}");
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Task DeleteCustomers(string[] IdRange)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"Customers/{id}");

                string responseString = await response.Content.ReadAsStringAsync();

                Customer customer = JsonConvert.DeserializeObject<Customer>(responseString);

                return customer;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("Customers");

                string responseString = await response.Content.ReadAsStringAsync();

                List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(responseString);

                return customers;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateCustomer(Customer customer)
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8);

                await httpClient.PutAsync($"Customers/{customer.CustomerID}", httpContent);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Access to Employees
        public async Task AddEmployee(Employee employee)
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8);

                await httpClient.PostAsync("Employes", httpContent);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Task AddEmployees(IEnumerable<Employee> employees)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteEmployee(int id)
        {
            try
            {
                await httpClient.DeleteAsync($"Employes/{id}");
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Task DeleteEmployees(int[] IdRange)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"Employes/{id}");

                string responseString = await response.Content.ReadAsStringAsync();

                Employee employee = JsonConvert.DeserializeObject<Employee>(responseString);

                return employee;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("Employes");

                string responseString = await response.Content.ReadAsStringAsync();

                List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(responseString);

                return employees;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateEmployee(Employee employee)
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8);

                await httpClient.PutAsync($"Employes/{employee.EmployeeID}", httpContent);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Access to Orders
        public async Task AddOrder(Order order,IEnumerable<Order_Detail> orderDetails)
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(order),Encoding.UTF8);

                await httpClient.PostAsync("Orders", httpContent);
            }
            catch (Exception e) 
            { 
                throw e; 
            }

        }

        public Task AddOrders(IEnumerable<Order> orders)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteOrder(int id)
        {
            try
            {
                await httpClient.DeleteAsync($"Orders/{id}");
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Task DeleteOrders(int[] IdRange)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetOrderById(int id)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"Orders/{id}");

                string responseString = await response.Content.ReadAsStringAsync();

                Order order = JsonConvert.DeserializeObject<Order>(responseString);

                return order;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("Orders");

                string responseString = await response.Content.ReadAsStringAsync();

                List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(responseString);

                return orders;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateOrder(Order order)
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8);

                await httpClient.PutAsync($"Orders/{order.OrderID}", httpContent);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Access to OrderDetails
        public async Task AddOrderDetail(Order_Detail orderDetail)
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(orderDetail), Encoding.UTF8);

                await httpClient.PostAsync("OrderDetails", httpContent);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task AddOrderDetails(IEnumerable<Order_Detail> orderDetails)
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(orderDetails, Formatting.Indented));

                await httpClient.PostAsync("OrderDetails/PostOrderDetails", httpContent);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteOrderDetail(int orderId, int productId)
        {
            try
            {
                await httpClient.DeleteAsync($"OrderDetails/{orderId}/{productId}");
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Task DeleteOrderDetails(int[] orderIds, int[] productIds = null)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteOrderDetailsByOrder(int orderId)
        {
            try
            {
                await httpClient.DeleteAsync($"OrderDetails/{orderId}");
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Order_Detail>> GetOrderDetails()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("OrderDetails");

                string responseString = await response.Content.ReadAsStringAsync();

                List<Order_Detail> orderDetails = JsonConvert.DeserializeObject<List<Order_Detail>>(responseString);

                return orderDetails;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Order_Detail>> GetOrderDetailsByOrder(int orderId)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"OrderDetails/{orderId}");

                string responseString = await response.Content.ReadAsStringAsync();

                List<Order_Detail> orderDetails = JsonConvert.DeserializeObject<List<Order_Detail>>(responseString);

                return orderDetails;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateOrderDetail(Order_Detail orderDetail)
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(orderDetail), Encoding.UTF8);

                await httpClient.PutAsync($"OrderDetails/{orderDetail.OrderID}/{orderDetail.ProductID}", httpContent);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Access to Products
        public async Task AddProduct(Product product)
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8);

                await httpClient.PostAsync("Products", httpContent);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Task AddProducts(IEnumerable<Product> products)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteProduct(int id)
        {
            try
            {
                await httpClient.DeleteAsync($"Products/{id}");
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Task DeleteProducts(int[] IdRange)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductById(int id)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"Products/{id}");

                string responseString = await response.Content.ReadAsStringAsync();

                Product product = JsonConvert.DeserializeObject<Product>(responseString);

                return product;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("Products");

                string responseString = await response.Content.ReadAsStringAsync();

                List<Product> products = JsonConvert.DeserializeObject<List<Product>>(responseString);

                return products;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateProduct(Product product)
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8);

                await httpClient.PutAsync($"Products/{product.ProductID}", httpContent);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Access to Shippers
        public async Task AddShipper(Shipper shipper)
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(shipper), Encoding.UTF8);

                await httpClient.PostAsync("Shippers", httpContent);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Task AddShippers(IEnumerable<Shipper> shippers)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteShipper(int id)
        {
            try
            {
                await httpClient.DeleteAsync($"Shippers/{id}");
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Task DeleteShippers(int[] IdRange)
        {
            throw new NotImplementedException();
        }

        public async Task<Shipper> GetShipperById(int id)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"Shippers/{id}");

                string responseString = await response.Content.ReadAsStringAsync();

                Shipper shipper = JsonConvert.DeserializeObject<Shipper>(responseString);

                return shipper;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Shipper>> GetShippers()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"Shippers");

                string responseString = await response.Content.ReadAsStringAsync();

                List<Shipper> shippers = JsonConvert.DeserializeObject<List<Shipper>>(responseString);

                return shippers;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateShipper(Shipper shipper)
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(shipper), Encoding.UTF8);

                await httpClient.PutAsync($"Shippers/{shipper.ShipperID}", httpContent);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Access to Suppliers
        public async Task AddSupplier(Supplier supplier)
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(supplier), Encoding.UTF8);

                await httpClient.PostAsync("Suppliers", httpContent);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Task AddSuppliers(IEnumerable<Supplier> suppliers)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteSupplier(int id)
        {
            try
            {
                await httpClient.DeleteAsync($"Suppliers/{id}");
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Task DeleteSuppliers(int[] IdRange)
        {
            throw new NotImplementedException();
        }

        public async Task<Supplier> GetSupplierById(int id)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"Suppliers/{id}");

                string responseString = await response.Content.ReadAsStringAsync();

                Supplier supplier = JsonConvert.DeserializeObject<Supplier>(responseString);

                return supplier;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"Suppliers");

                string responseString = await response.Content.ReadAsStringAsync();

                List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(responseString);

                return suppliers;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateSupplier(Supplier supplier)
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(supplier), Encoding.UTF8);

                await httpClient.PutAsync($"Suppliers/{supplier.SupplierID}", httpContent);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
