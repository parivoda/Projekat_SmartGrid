using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekat_SmartGrid.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public int Price { get; set; }

        public Order()
        {

        }
        public Order(int id,string name,int amount,string address,string comment,int price)
        {
            Id = id;
            Name = name;
            Amount = amount;
            Address = address;
            Comment = comment;
            Price = price;
        }
    }
}