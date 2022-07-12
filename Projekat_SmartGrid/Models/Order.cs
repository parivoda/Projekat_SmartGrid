using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekat_SmartGrid.Models
{
    public class Order
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Product { get; set; }
        public int Amount { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public int Price { get; set; }
        public bool Active { get; set; }

        public Order()
        {

        }
        public Order(int id,User user,string product,int amount,string address,string comment,int price,bool active)
        {
            Id = id;
            User = user;
            Product = product;
            Amount = amount;
            Address = address;
            Comment = comment;
            Price = price;
            Active = active;
        }
        public Order(int id, string product, int amount, string address, string comment, int price, bool active)
        {
            Id = id;
            Product = product;
            Amount = amount;
            Address = address;
            Comment = comment;
            Price = price;
            Active = active;
        }
    }
}