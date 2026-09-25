using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem
{
    internal class System
    {
        private readonly List<Customer> _customers = new();
        private readonly List<Product> _products = new();
        private readonly List<Order> _orders = new();
        public void AddCustomer(int id, string name, string email, string city, bool isVip)
        {
            if (_customers.Any(c => c.Id == id))
            {
                Console.WriteLine($"ERROR: customer id {id} already exists.");
                return;
            }
            _customers.Add(new Customer(id, name, email, city, isVip));
        }

        public void AddProduct(int id, string name, double price, int stock)
        {
            if (_products.Any(p => p.Id == id))
            {
                Console.WriteLine($"ERROR: product id {id} already exists.");
                return;
            }
            _products.Add(new Product(id, name, price, stock));
        }

        public Order? CreateOrder(int orderId, int customerId, string date)
        {
            if (_orders.Any(o => o.Id == orderId))
            {
                Console.WriteLine($"ERROR: order id {orderId} already exists.");
                return null;
            }

            var customer = _customers.FirstOrDefault(c => c.Id == customerId);
            if (customer is null)
            {
                Console.WriteLine($"ERROR: customer id {customerId} not found.");
                return null;
            }

            var order = new Order(orderId, customer, date);
            _orders.Add(order);
            return order;
        }

        public void AddLineToOrder(int orderId, int productId, int quantity)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order is null)
            {
                Console.WriteLine($"ERROR: order id {orderId} not found.");
                return;
            }

            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product is null)
            {
                Console.WriteLine($"ERROR: product id {productId} not found.");
                return;
            }

            try
            {
                order.AddLine(product, quantity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public void MarkOrderPaid(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order is null)
            {
                Console.WriteLine($"ERROR: order id {orderId} not found.");
                return;
            }

            try
            {
                order.MarkPaid();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public void PrintCustomers()
        {
            Console.WriteLine($"\n=== CUSTOMERS ({_customers.Count}) ===");
            foreach (var c in _customers)
                Console.WriteLine(c);
        }

        public void PrintProducts()
        {
            Console.WriteLine($"\n=== PRODUCTS ({_products.Count}) ===");
            foreach (var p in _products)
                Console.WriteLine(p);
        }

        public void PrintOrder(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order is null)
            {
                Console.WriteLine($"ERROR: order id {orderId} not found.");
                return;
            }
            order.Print();
        }

        public void PrintAllOrders()
        {
            Console.WriteLine($"\n=== ALL ORDERS ({_orders.Count}) ===");
            foreach (var o in _orders)
                o.Print();
        }

        public double TotalSalesPaidOnly() =>
            _orders.Where(o => o.IsPaid).Sum(o => o.CalculateTotal());

        public void SeedSampleData()
        {
            AddCustomer(1, "Mona Ali", "mona@example.com", "Cairo", true);
            AddCustomer(2, "Omar Hassan", "omar@example.com", "Alexandria", false);
            AddCustomer(3, "Sara Nabil", "sara@example.com", "Giza", false);

            AddProduct(101, "USB Cable", 50.0d, 100);
            AddProduct(102, "Wireless Mouse", 250.0d, 40);
            AddProduct(103, "Mechanical Keyboard", 1200.0d, 15);
            AddProduct(104, "Laptop Stand", 400.0d, 25);
        }

        public void RunDemoScenario()
        {
            CreateOrder(1001, 1, "2026-09-15");
            AddLineToOrder(1001, 101, 2);
            AddLineToOrder(1001, 102, 1);
            MarkOrderPaid(1001);

            CreateOrder(1002, 2, "2026-09-15");
            AddLineToOrder(1002, 103, 1);
            AddLineToOrder(1002, 104, 1);

            CreateOrder(1003, 3, "2026-09-16");
            AddLineToOrder(1003, 101, 5);
            MarkOrderPaid(1003);
        }
    }
}
