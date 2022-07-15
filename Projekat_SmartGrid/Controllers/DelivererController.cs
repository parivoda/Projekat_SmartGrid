using Projekat_SmartGrid.Models;
using System;
using System.Collections.Generic;
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
        public ActionResult AcceptOrder(string id, string username, string product, string amount, string address, string comment, string price)
        {
            User currentUser = (User)Session["USER"];

            if (currentUser == null || currentUser.UserType != UserType.DELIVERER)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string usr = username;
                User uu = null;
                foreach (User u in Data.userList.Values)
                {
                    if (u.Username == usr)
                    {
                        uu = (User)Data.userList[u.Email];

                    }
                }

                Order order = new Order(Int32.Parse(id), uu, product, Int32.Parse(amount), address, comment, Int32.Parse(price), false);

                currentUser.AcceptedOrders.Add(order);
                return RedirectToAction("OrderAccepted");
            }
        }
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
    }
}