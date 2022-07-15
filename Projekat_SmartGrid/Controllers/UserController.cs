using Projekat_SmartGrid.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekat_SmartGrid.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult NewOrder()
        {
            User currentUser = (User)Session["USER"];

            if (currentUser == null || currentUser.UserType != UserType.USER)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult NewOrderAction(string id,string productName,string productPrice,string amount,string commentText)
        {
            User currentUser = (User)Session["USER"];

            if (currentUser == null || currentUser.UserType != UserType.USER)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                int Fee = 165;
                int totalPrice = Int32.Parse(amount) * Int32.Parse(productPrice) + Fee;
                Order order = new Order(Int32.Parse(id), currentUser.Username, productName, Int32.Parse(amount), currentUser.Address, commentText, totalPrice, true);
                

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjekatSmartGridConnectionString"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("CreateOrder", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", currentUser.Username);
                        cmd.Parameters.AddWithValue("@ProductName", productName);
                        cmd.Parameters.AddWithValue("@Amount", Int32.Parse(amount));
                        cmd.Parameters.AddWithValue("@Address", currentUser.Address);
                        cmd.Parameters.AddWithValue("@Comment", commentText);
                        cmd.Parameters.AddWithValue("@Price", totalPrice);
                        cmd.Parameters.AddWithValue("@Active", "True");
                        cmd.Parameters.AddWithValue("@status", "createOrder");
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                Data.orderList.Add(order);
                return RedirectToAction("Ordered");
            }
        }
        [HttpGet]
        public ActionResult Ordered()
        {
            User currentUser = (User)Session["USER"];

            if (currentUser == null || currentUser.UserType != UserType.USER)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult PastOrders()
        {
            User currentUser = (User)Session["USER"];

            if (currentUser == null || currentUser.UserType != UserType.USER)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

    }
}