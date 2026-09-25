using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem
{
    internal class Order
    {
        private const int MaxLines = 20;
        private readonly List<OrderLine> _lines = new();

        public int Id { get; }
        public Customer Customer { get; }
        public string Date { get; }
        public bool IsPaid { get; private set; }
        public IReadOnlyList<OrderLine> Lines => _lines;

        public Order(int id, Customer customer, string date)
        {
            Id = id;
            Customer = customer;
            Date = date;
        }

        public void AddLine(Product product, int quantity)
        {
            if (IsPaid)
                throw new InvalidOperationException("Cannot change a paid order.");
            if (_lines.Count >= MaxLines)
                throw new InvalidOperationException("Order has too many lines.");

            product.Reserve(quantity); // throws for bad quantity / insufficient stock
            _lines.Add(new OrderLine(product, quantity));
        }

        
        public double CalculateTotal()
        {
            double total = _lines.Sum(l => l.LineTotal);
            if (Customer.IsVip)
                total *= 0.90d;
            return total;
        }

        public void MarkPaid()
        {
            if (_lines.Count == 0)
                throw new InvalidOperationException("Cannot pay an empty order.");
            IsPaid = true;
        }

        public void Print()
        {
            Console.WriteLine($"\n=== ORDER #{Id} ===");
            Console.WriteLine($"Date: {Date}");
            Console.WriteLine($"Customer: {Customer.Name} (#{Customer.Id})");
            Console.WriteLine($"Paid: {(IsPaid ? "yes" : "no")}");
            Console.WriteLine("Lines:");
            foreach (var line in _lines)
            {
                Console.WriteLine(
                    $"  - {line.Product.Name}  x{line.Quantity}  @{line.Product.Price:0.00}  = {line.LineTotal:0.00}");
            }
            Console.WriteLine($"TOTAL: {CalculateTotal():0.00}");
        }


    }
}
