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
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Verify()
        {
            User currentUser = (User)Session["USER"];

            if (currentUser == null || currentUser.UserType != UserType.ADMIN)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult VerifyDeliverer(string username, string name, string lastname, string password, string email, string address, string dateOfBirth, string userType)
        {
            User currentUser = (User)Session["USER"];

            if (currentUser == null || currentUser.UserType != UserType.ADMIN)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                
                Data.userList.Remove(email);


                User user = new User(username, email, password, name, lastname, dateOfBirth, address, (UserType)Enum.Parse(typeof(UserType), userType), false);

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjekatSmartGridConnectionString"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("VerifyDeliverer", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Lastname", lastname);
                        cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@UserType", userType);
                        cmd.Parameters.AddWithValue("@Blocked", "False");
                        cmd.Parameters.AddWithValue("@status", "verify");
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                Data.userList.Add(user.Email, user);

                return RedirectToAction("Verify");
            }
        }
        public ActionResult AddProducts()
        {
            User currentUser = (User)Session["USER"];

            if (currentUser == null || currentUser.UserType != UserType.ADMIN)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult AddProductAction(string id,string name,string price,string ingredients)
        {
            User currentUser = (User)Session["USER"];

            if (currentUser == null || currentUser.UserType != UserType.ADMIN)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                bool valid = true;

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(price) || string.IsNullOrEmpty(ingredients))
                {
                    ViewBag.emptyError = "You must fill all fields!";
                    return View();
                }

                if (valid)
                {
                    
                    Product product = new Product(Int32.Parse(id),name, Int32.Parse(price), ingredients);

                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjekatSmartGridConnectionString"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("ProductDetail", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@Price", Int32.Parse(price));
                            cmd.Parameters.AddWithValue("@Ingredients", ingredients);
                            cmd.Parameters.AddWithValue("@status", "insertProduct");
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    Data.productList.Add(product);
                    return RedirectToAction("ProductAdded");
                }
                else
                {
                    return View();
                }
            }
        }
        public ActionResult ProductAdded()
        {
            User currentUser = (User)Session["USER"];

            if (currentUser == null || currentUser.UserType != UserType.ADMIN)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult AllOrders()
        {
            User currentUser = (User)Session["USER"];

            if (currentUser == null || currentUser.UserType != UserType.ADMIN)
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