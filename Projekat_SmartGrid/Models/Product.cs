using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekat_SmartGrid.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Ingredients { get; set; }

        public Product() { }

        public Product(int id,string name,int price,string ingredients)
        {
            Id = id;
            Name = name;
            Price = price;
            Ingredients = ingredients;
        }
    }
}