using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem
{
    internal class Product
    {
        public int Id { get; }
    public string Name { get; }
    public double Price { get; }
    public int Stock { get; private set; }

    public Product(int id, string name, double price, int stock)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required.", nameof(name));

        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
    }

    public void Reserve(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.", nameof(quantity));
        if (quantity > Stock)
            throw new InvalidOperationException($"Not enough stock for product #{Id}.");

        Stock -= quantity;
    }

    public override string ToString() =>
        $"#{Id}  {Name}  price={Price:0.00}  stock={Stock}";

    }
    }
