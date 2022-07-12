using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekat_SmartGrid.Models
{
    public class Data
    {
        public static Dictionary<string,User> userList = new Dictionary<string,User>();
        public static List<Product> productList = new List<Product>();
        public static List<Order> orderList = new List<Order>();
    }
}