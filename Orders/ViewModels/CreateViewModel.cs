using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DynamicData;
using DynamicData.Binding;
using System.Reactive.Linq;
using System.Reactive;
using DataAccessLocal;
using ReactiveUI.Fody.Helpers;

namespace Orders.ViewModels
{
    public class CreateViewModel : ReactiveObject, INavigationAware, IRegionMemberLifetime
    {
        #region Declarations
        NorthwindContext northwindContext;

        ReadOnlyObservableCollection<ProductInOrder> _productsInOrder;
        ReadOnlyObservableCollection<ProductOnStore> _productsInStore;
        ReadOnlyObservableCollection<Employee> _employees;
        ReadOnlyObservableCollection<Customer> _customers;

        SourceCache<ProductOnStore, int> products;
        SourceCache<ProductInOrder, int> productsInOrder;
        SourceList<Employee> employees;
        SourceList<Customer> customers;
        #endregion

        public CreateViewModel(NorthwindContext northwindContext)
        {
            this.northwindContext = northwindContext;

            products = new SourceCache<ProductOnStore, int>(p => p.ProductID);
            productsInOrder = new SourceCache<ProductInOrder, int>(p => p.ProductID);
            employees = new SourceList<Employee>();
            customers = new SourceList<Customer>();

            var canRemoveAllExecute = productsInOrder.CountChanged.
                Select(currentCountOfItems =>
                {
                    if (currentCountOfItems == 0) return false;
                    else return true;
                });

            productsInOrder.CountChanged.Subscribe(currentCount => { CountOfProductsInOrder = currentCount; });

            var canCreateOrderExecute = this.WhenAnyValue(vm => vm.SelectedCustomer, vm => vm.SelectedEmployee, vm => vm.CountOfProductsInOrder).
                Select(x =>
                {
                    if (x.Item1 == null || x.Item2 == null || x.Item3 == 0) return false;
                    return true;
                });

            this.WhenAnyValue(vm => vm.SelectedCustomer, vm => vm.SelectedEmployee, vm => vm.CountOfProductsInOrder).
                Subscribe(x =>
                {
                    if (x.Item1 != null || x.Item2 != null || x.Item3 != 0) OrderDate = DateTime.Now.ToLongDateString();
                    else OrderDate = "";
                });

            products.Connect().
                OnItemAdded(a => MakeSubscribtion(a)).
                Filter(x => x.UnitsInStock != 0).
                Sort(SortExpressionComparer<ProductOnStore>.Ascending(item => item.ProductID)).
                ObserveOnDispatcher().
                Bind(out _productsInStore).
                Subscribe();

            productsInOrder.Connect().
                Sort(SortExpressionComparer<ProductInOrder>.Ascending(x => x.ProductID)).
                ObserveOnDispatcher().
                Bind(out _productsInOrder).
                OnItemAdded(x => SubscribeToChanges(x)).
                OnItemRemoved(y => SetInitialValues(y)).
                Subscribe();

            employees.Connect().
                ObserveOnDispatcher().
                Bind(out _employees).
                Subscribe();

            customers.Connect().
                ObserveOnDispatcher().
                Bind(out _customers).
                Subscribe();

            RemoveAllCommand = ReactiveCommand.Create(RemoveAllCommandExecute, canRemoveAllExecute);
            CreateOrderCommand = ReactiveCommand.Create(CreateOrderExecute, canCreateOrderExecute);
        }

        #region Commands

        #region Remove all 
        /// <summary>
        /// Removing all products from order
        /// </summary>
        public ReactiveCommand<Unit, Unit> RemoveAllCommand { get; }

        private void RemoveAllCommandExecute()
        {
            var listOfProducts = products.Items.ToList();

            listOfProducts.ForEach(product => { if (product.Added) product.Added = false; });
        }
        #endregion

        #region Create
        public ReactiveCommand<Unit, Unit> CreateOrderCommand { get; }

        private void CreateOrderExecute()
        {
            Order newOrder = new Order();
            Order lastOrder = northwindContext.Orders.ToList().Last();
            int idOfLastOrder = lastOrder.OrderID;
            string shipCountry = "USA";

            newOrder.OrderID = ++idOfLastOrder;
            newOrder.CustomerID = SelectedCustomer.CustomerID;
            newOrder.EmployeeID = SelectedEmployee.EmployeeID;
            newOrder.OrderDate = DateTime.Parse(OrderDate);
            newOrder.ShipCountry = shipCountry;

            using (var contextTransaction = northwindContext.Database.BeginTransaction())
            {
                try
                {
                    northwindContext.Orders.Add(newOrder);
                    northwindContext.SaveChanges();

                    northwindContext.Order_Details.AddRange(new List<Order_Detail>(
                            ProductsInOrder.Select(p => new Order_Detail
                            {
                                OrderID = newOrder.OrderID,
                                ProductID = p.ProductID,
                                UnitPrice = (decimal)p.UnitPrice,
                                Quantity = (short)p.SelectedQuantity,
                                Discount = p.SelectedQuantity
                            })));

                    northwindContext.SaveChanges();

                    contextTransaction.Commit();
                }
                catch (Exception)
                {
                    contextTransaction.Rollback();
                }
            }

            productsInOrder.Clear();
            OrderDate = String.Empty;
            SelectedCustomer = null;
            SelectedEmployee = null;
        }
        #endregion

        #endregion

        #region Utilities
        /// <summary>
        /// Subscribes to the changes of added property 
        /// </summary>
        /// <param name="productOnStore">
        /// New product that was added into products on store list
        /// </param>
        private void MakeSubscribtion(ProductOnStore productOnStore)
        {
            productOnStore.WhenAnyValue(a => a.Added).
            Subscribe(isInOrder =>
            {
                if (isInOrder)
                {
                    ProductInOrder newProductInOrder = new ProductInOrder(productOnStore);
                    productsInOrder.AddOrUpdate(newProductInOrder);
                }
                else { productsInOrder.Remove(productOnStore.ProductID); }
            });
        }

        private void SetInitialValues(ProductInOrder productToRemove)
        {
            productToRemove.SelectedQuantity = 0;
            productToRemove.SourceProductOnStore.UnitsInStock += productToRemove.SourceProductOnStore.UnitsOnOrder;
            productToRemove.SourceProductOnStore.UnitsOnOrder = 0;

            TotalSum -= productToRemove.Sum;
        }

        private void SubscribeToChanges(ProductInOrder newProductInOrder)
        {
            int previousSelectedQuantity = 0;

            newProductInOrder.WhenAnyValue(x => x.SelectedDiscount, x => x.SelectedQuantity)
            .Subscribe(a =>
            {
                int newSelectedDiscount = a.Item1;
                int newSelectedQuantity = a.Item2;

                //Value(price) that will be added(or removed) to(from) TotalPrice
                decimal newValue = (decimal)newProductInOrder.UnitPrice * (newSelectedQuantity - previousSelectedQuantity);

                //-1% or +1% discount
                decimal percentageOff = (decimal)(newSelectedDiscount - newProductInOrder.PreviousSelectedDiscount) / 100;

                //Executing when the SelectedDiscount has changed
                if (percentageOff != 0)
                {
                    newProductInOrder.Sum -= newProductInOrder.SelectedQuantity * (decimal)newProductInOrder.UnitPrice * percentageOff;
                    TotalSum -= newProductInOrder.SelectedQuantity * (decimal)newProductInOrder.UnitPrice * percentageOff;
                }
                //Executing when the SelectedQuantity has changed and the SelectedDiscount is greater than zero.
                else if (newSelectedDiscount != 0)
                {
                    decimal sumOff = ((decimal)newProductInOrder.PreviousSelectedDiscount / 100) * newProductInOrder.SelectedQuantity * (decimal)newProductInOrder.UnitPrice;
                    decimal sumToAdd = sumOff - newProductInOrder.Sum;

                    newProductInOrder.Sum = sumOff;
                    TotalSum += sumToAdd;

                    TotalSumString = TotalSum.ToString("C2");

                    return;
                }

                newProductInOrder.PreviousSelectedDiscount = newSelectedDiscount;

                newProductInOrder.Sum += newValue;
                TotalSum += newValue;

                TotalSumString = TotalSum.ToString("C2");
            });

            newProductInOrder.WhenAnyValue(x => x.SelectedQuantity)
                .Select(newSelectedQuantity =>
                {
                    int deltaQuantity = newSelectedQuantity - previousSelectedQuantity; //Quantity of products that will be added or removed(in case of negative value) from the stock of the current product

                    previousSelectedQuantity = newSelectedQuantity;

                    return deltaQuantity;
                })
                .Subscribe(newValue =>
                {
                    newProductInOrder.SourceProductOnStore.UnitsInStock -= (short)newValue;
                    newProductInOrder.SourceProductOnStore.UnitsOnOrder += (short)newValue;

                    SaveChangesToProducts(newProductInOrder, (int)newValue);
                });
        }

        private void SaveChangesToProducts(ProductInOrder newProductInOrder, int newValue)
        {
            Product productToReplace = northwindContext.Products.First(x => x.ProductID == newProductInOrder.ProductID);

            productToReplace.UnitsInStock -= (short)newValue;

            northwindContext.SaveChanges();
        }
        #endregion

        #region Implementation of INavigationAware
        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (products.Count == 0) await Task.Run(() =>
            {
                var listOfProducts = northwindContext.Products.ToList();
                var listOfProductsOnStore = listOfProducts.Select(b => new ProductOnStore(b));
                products.AddOrUpdate(listOfProductsOnStore);
            });
            if (employees.Count == 0) await Task.Run(() => employees.AddRange(northwindContext.Employees));
            if (customers.Count == 0) await Task.Run(() => customers.AddRange(northwindContext.Customers));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) { return true; }

        public void OnNavigatedFrom(NavigationContext navigationContext) { }
        #endregion

        #region Properties
        public ReadOnlyObservableCollection<ProductInOrder> ProductsInOrder => _productsInOrder;
        public ReadOnlyObservableCollection<ProductOnStore> ProductsInStore => _productsInStore;
        public ReadOnlyObservableCollection<Employee> Employees => _employees;
        public ReadOnlyObservableCollection<Customer> Customers => _customers;

        [Reactive] public Employee SelectedEmployee { set; get; }
        [Reactive] public Customer SelectedCustomer { set; get; }
        [Reactive] public string OrderDate { set; get; }
        [Reactive] public ProductInOrder SelectedProductInOrder { private get; set; }
        [Reactive] int CountOfProductsInOrder { set; get; }

        string _totalSumString;
        public string TotalSumString
        {
            set { this.RaiseAndSetIfChanged(ref _totalSumString, value); }
            get { return _totalSumString; }
        }

        private decimal TotalSum { set; get; }
        #endregion

        #region Implementation of IRegionMemeberLifetime
        public bool KeepAlive => true;
        #endregion

        #region Screen objects
        public class ProductOnStore : AbstractNotifyPropertyChanged
        {
            public ProductOnStore(Product sourceProduct)
            {
                this.ProductID = sourceProduct.ProductID;
                this.ProductName = sourceProduct.ProductName;
                this.UnitPrice = sourceProduct.UnitPrice;
                this.UnitsInStock = sourceProduct.UnitsInStock;
                this.UnitsOnOrder = sourceProduct.UnitsOnOrder;
            }

            #region Properties
            public int ProductID { set; get; }

            public string ProductName { set; get; }

            public decimal? UnitPrice { set; get; }

            short? _unitsInStock;
            public short? UnitsInStock
            {
                set { SetAndRaise(ref _unitsInStock, value); }
                get { return _unitsInStock; }
            }

            short? _unitsOnOrder;
            public short? UnitsOnOrder
            {
                set { SetAndRaise(ref _unitsOnOrder, value); }
                get { return _unitsOnOrder; }
            }

            bool _added;
            public bool Added
            {
                set { SetAndRaise(ref _added, value); }
                get { return _added; }
            }
            #endregion
        }

        public class ProductInOrder : ReactiveObject
        {
            public ProductInOrder(ProductOnStore product)
            {
                ProductID = product.ProductID;
                ProductName = product.ProductName;
                UnitPrice = product.UnitPrice;
                SelectedDiscount = 0;
                SelectedQuantity = 1;

                QunatityInStoke = product.UnitsInStock;

                SourceProductOnStore = product;
            }

            #region Properties
            public int ProductID { set; get; }

            public string ProductName { set; get; }

            public decimal? UnitPrice { set; get; }

            public short? QunatityInStoke { set; get; }

            [Reactive]
            public short SelectedQuantity { set; get; }

            [Reactive]
            public int SelectedDiscount { set; get; }

            public decimal Sum { set; get; }

            public ProductOnStore SourceProductOnStore { private set; get; }

            public int PreviousSelectedDiscount { set; get; }
            #endregion
        }
        #endregion
    }
}
