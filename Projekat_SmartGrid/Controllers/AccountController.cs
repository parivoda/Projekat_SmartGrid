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
                Data.userList.Add(email, user);
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.emptyError = "You must fill all the fields!";
                return View();
            }
            if (Data.userList[email].UserType.Equals(UserType.DELIVERER))
            {
                if (Data.userList[email].Blocked.Equals(false))
                {
                    if (Data.userList.ContainsKey(email) && Data.userList[email].Password.Equals(password))
                    {
                        Session["USER"] = Data.userList[email];
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        ViewBag.error = "Wrong login info!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.error = "You must wait for Administrator to approve your register request";
                    return View();
                }
                
            }
            else
            {
                if (Data.userList.ContainsKey(email) && Data.userList[email].Password.Equals(password))
                {
                    Session["USER"] = Data.userList[email];
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ViewBag.error = "Wrong login info!";
                    return View();
                }
            }
            
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        public ActionResult ProfileView()
        {
            if (Session["USER"] == null)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }
        public ActionResult EditProfile(string username, string name, string lastname,string password, string email, string address, string dateOfBirth,string userType)
        {
            User thisUser = (User)Session["User"];

            Data.userList.Remove(thisUser.Email);

            bool valid = true;
            foreach (var item in Data.userList.Values)
            {
                if (item.Email.ToLower().Equals(email.ToLower()))
                {
                    ViewBag.emailError = "Email already in use!";
                    valid = false;
                }
            }
            foreach (var item in Data.userList.Values)
            {
                if (item.Username.ToLower().Equals(username.ToLower()))
                {
                    ViewBag.usernameError = "Username already in use!";
                    valid = false;
                }
            }
            if (valid)
            {

                User user = new User(username,email,password,name,lastname,dateOfBirth,address, (UserType)Enum.Parse(typeof(UserType), userType), false);

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjekatSmartGridConnectionString"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateUser", con))
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
                        cmd.Parameters.AddWithValue("@status", "update");
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                Data.userList.Add(user.Email, user);
                
                Session["USER"] = Data.userList[thisUser.Email];
            }

            return RedirectToAction("Index", "Home");
        }
    }
}