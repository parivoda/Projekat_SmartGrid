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
    public class DelivererController : Controller
    {
        // GET: Deliverer
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult NewOrders()
        {
            User currentUser = (User)Session["USER"];

            if (currentUser == null || currentUser.UserType != UserType.DELIVERER)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult AcceptOrder(string id, string username, string product, string amount, string address, string comment, string price)
        {
            User currentUser = (User)Session["USER"];

            if (currentUser == null || currentUser.UserType != UserType.DELIVERER)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //string usr = username;
                //User uu = null;
                //foreach (User u in Data.userList.Values)
                //{
                //    if (u.Username == usr)
                //    {
                //        uu = (User)Data.userList[u.Email];

                //    }
                //}
                
                Order order = new Order(Int32.Parse(id), username, product, Int32.Parse(amount), address, comment, Int32.Parse(price), false);
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjekatSmartGridConnectionString"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("updateOrder", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@ProductName", product);
                        cmd.Parameters.AddWithValue("@Amount", Int32.Parse(amount));
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@Comment", comment);
                        cmd.Parameters.AddWithValue("@Price", Int32.Parse(price));
                        cmd.Parameters.AddWithValue("@Active", "False");
                        cmd.Parameters.AddWithValue("@status", "updateOrder");
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                Data.acceptedOrder.Add(currentUser.Username, order);
                return RedirectToAction("OrderAccepted");
            }
        }
        [HttpGet]
        public ActionResult OrderAccepted()
        {
            User currentUser = (User)Session["USER"];

            if (currentUser == null || currentUser.UserType != UserType.DELIVERER)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult MyOrders()
        {
            User currentUser = (User)Session["USER"];

            if (currentUser == null || currentUser.UserType != UserType.DELIVERER)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult CurrentOrder()
        {
            User currentUser = (User)Session["USER"];

            if (currentUser == null || currentUser.UserType != UserType.DELIVERER)
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