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
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string username, string email, string password,string password2, string name, string lastname, string dateOfBirth, string address, string userType, string blocked)
        {
            bool valid = true;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(password2) || 
                string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(dateOfBirth) || string.IsNullOrEmpty(address))
            {
                ViewBag.emptyError = "You must fill all fields!";
                return View();
            }

            if (Data.userList.ContainsKey(username))
            {
                ViewBag.usernameExistsError = "Username already in use!";
                valid = false;
            }
            foreach (User u in Data.userList.Values)
            {
                if (u.Email.Equals(email))
                {
                    ViewBag.emailError = "Email already in use!";
                    valid = false;
                }
            }
            if(password != password2)
            {
                ViewBag.passwordError = "Passwords must match!";
                valid = false;
            }

            if (valid)
            {
                if (userType == "USER")
                {
                    blocked = "False";
                }
                else
                {
                    blocked = "True";
                }
                User user = new User(username,email,password,name,lastname,dateOfBirth,address, (UserType)Enum.Parse(typeof(UserType), userType), bool.Parse(blocked));

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjekatSmartGridConnectionString"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UsersDetail", con))
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
                        cmd.Parameters.AddWithValue("@Blocked", blocked);
                        cmd.Parameters.AddWithValue("@status", "Insert");
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                Data.userList.Add(username, user);
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        //[HttpPost]
        //public ActionResult Login(string username, string password)
        //{
        //    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        //    {
        //        ViewBag.emptyError = "You must fill all the fields!";
        //        return View();
        //    }

        //    if (Data.users.ContainsKey(username) && Data.users[username].Password.Equals(password))
        //    {
        //        if (Data.users[username].Role == Role.COACH)
        //        {
        //            foreach (Coach c in Data.coaches)
        //            {
        //                if (Data.users[username].Username == c.Username)
        //                {
        //                    if (c.Blocked == false)
        //                    {
        //                        Session["USER"] = Data.users[username];
        //                        return RedirectToAction("Index", "Home");
        //                    }
        //                    else
        //                    {
        //                        ViewBag.error = "Account blocked!";
        //                        return View();
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Session["USER"] = Data.users[username];
        //            return RedirectToAction("Index", "Home");
        //        }
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        ViewBag.error = "Wrong login info!";
        //        return View();
        //    }
        //}
    }
}