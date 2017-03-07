using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;

namespace Fusion.Controllers
{
    public class InternetOrdersController : Controller
    {
        [MyAuthorize(Roles = "CallCenterReport,FusionAdmin")]
        // GET: InternetOrders
        public ActionResult Index(string[] filter_status)
        {
            InternetOrders model = new InternetOrders();
            model.GetOrderList(User.Identity.Name.ToString(), filter_status);
            return View(model);
        }

        [MyAuthorize(Roles = "CallCenterReport,FusionAdmin")]
        public ActionResult Edit(int id)
        {
            InternetOrders.OrderInfo model = new InternetOrders.OrderInfo();
            model.GetOrder(id, User.Identity.Name.ToString());
            return View(model);
        }

        [MyAuthorize(Roles = "CallCenterReport,FusionAdmin")]
        public ActionResult Print(int id)
        {
            InternetOrders.OrderInfo model = new InternetOrders.OrderInfo();
            model.GetOrder(id, User.Identity.Name.ToString());

            try
            {
                model.SetStatus('F');
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Произошлая какая-то ебаная ебала при обновлении статуса. Ошибка ниже: " + ex.Message;
            }

            return View(model);
        }

        [MyAuthorize(Roles = "CallCenterReport,FusionAdmin")]
        public ActionResult Cancel(int id)
        {
            InternetOrders.OrderInfo model = new InternetOrders.OrderInfo();
            model.GetOrder(id, User.Identity.Name.ToString());
            model.SetStatus('C');
            return RedirectToAction("Edit", new { @id = id });
        }

        [MyAuthorize(Roles = "CallCenterReport,FusionAdmin")]
        public ActionResult SetStatus(int id, string status)
        {
            InternetOrders.OrderInfo model = new InternetOrders.OrderInfo();
            model.GetOrder(id, User.Identity.Name.ToString());
            model.SetStatus(status[0]);
            return RedirectToAction("Edit", new { @id = id });
        }

        [MyAuthorize(Roles = "CallCenterReport,FusionAdmin")]
        public ActionResult SendToRkeeper(int id)
        { 
            InternetOrders.OrderInfo model = new InternetOrders.OrderInfo();
            model.GetOrder(id, User.Identity.Name.ToString());
            return View(model);
        }
    }
}