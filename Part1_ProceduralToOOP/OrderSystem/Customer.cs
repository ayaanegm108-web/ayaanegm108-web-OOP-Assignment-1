using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;

namespace OrderSystem
{
    internal class Customer
    {
        private static int max_customers=50;
        private static  Customer [] Customers = new Customer[max_customers];
        public static int customerCount=0;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public bool IsVip;
       
        
        public Customer(int id , string name , string email,string city ,bool isVip)   
        {
            customerCount++;
            Customers[customerCount] = this;
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.City = city;
            this.IsVip = isVip;
        }
        

        //print customer details
        public override string ToString()
        {
            StringBuilder result = null!;
            result.Append($" id : {this.Id}\n");
            result.Append($"name : {this.Name}");
            result.Append($"email : {this.Email}");
            result.Append($"city : {this.City}");
            result.Append($"Is Vip : {(this.IsVip?"yes" : "No" )}");
            return result.ToString();
        }

    }
}
