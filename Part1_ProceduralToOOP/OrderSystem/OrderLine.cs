using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem
{
    internal class OrderLine 
    {
        public Product Product { get; }
        public int Quantity { get; }

        public OrderLine(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public double LineTotal => Product.Price * Quantity;
    }
}
