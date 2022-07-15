using Projekat_SmartGrid.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Projekat_SmartGrid
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            LoadUsers();
            LoadOrders();
            LoadProducts();
        }

        public void LoadUsers()
        {
            string cs = ConfigurationManager.ConnectionStrings["ProjekatSmartGridConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users", con);
                cmd.CommandType = System.Data.CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var users = new User();

                    users.Id = Convert.ToInt32(rdr["Id"]);
                    users.Username = rdr["Username"].ToString();
                    users.Email = rdr["Email"].ToString();
                    users.Password = rdr["Password"].ToString();
                    users.Name = rdr["Name"].ToString();
                    users.Lastname = rdr["Lastname"].ToString();
                    users.DateOfBirth = rdr["DateOfBirth"].ToString();
                    users.Address = rdr["Address"].ToString();
                    users.UserType = (UserType)Enum.Parse(typeof(UserType), rdr["UserType"].ToString());
                    users.Image = rdr["Image"].ToString();
                    users.Blocked = Convert.ToBoolean(rdr["Blocked"]);
                    Data.userList.Add(users.Email,users);
                }
            }
        }
        public void LoadOrders()
        {
            string cs = ConfigurationManager.ConnectionStrings["ProjekatSmartGridConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Orders", con);
                cmd.CommandType = System.Data.CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var orders = new Order();

                    //string username = rdr["Username"].ToString();
                    //User uu = null;
                    //foreach (User u in Data.userList.Values)
                    //{
                    //    if(u.Username == username)
                    //    {
                    //        uu = (User)Data.userList[u.Email];
                            
                    //    }
                    //}

                    orders.Id = Convert.ToInt32(rdr["Id"]);
                    orders.User = rdr["Username"].ToString();
                    orders.Product = rdr["ProductName"].ToString();
                    orders.Amount = Convert.ToInt32(rdr["Amount"]);
                    orders.Address = rdr["Address"].ToString();
                    orders.Comment = rdr["Comment"].ToString();
                    orders.Price = Convert.ToInt32(rdr["Price"]);
                    orders.Active = Convert.ToBoolean(rdr["Active"]);
                    Data.orderList.Add(orders);
                    
                }
            }
        }

        public void LoadProducts()
        {
            string cs = ConfigurationManager.ConnectionStrings["ProjekatSmartGridConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Products", con);
                cmd.CommandType = System.Data.CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var products = new Product();

                    products.Id = Convert.ToInt32(rdr["Id"]);
                    products.Name = rdr["Name"].ToString();
                    products.Price = Convert.ToInt32(rdr["Price"]);
                    products.Ingredients = rdr["Ingredients"].ToString();
                    Data.productList.Add(products);
                }
            }
        }
    }
}
