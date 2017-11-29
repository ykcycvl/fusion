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
        Entities db = new Entities();

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
            ViewBag.ErrorMessage = "";
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

            try
            {
                var autoSendToDelivery = db.VegaPersonalSetting.FirstOrDefault(p => p.VegaSetting.SettingName == "SendOrderToDelivery" && p.UserName.ToLower() == User.Identity.Name.ToString().ToLower());

                if (autoSendToDelivery != null && !string.IsNullOrEmpty(autoSendToDelivery.SettingValue) && autoSendToDelivery.SettingValue == "true")
                {
                    var order = db.DLVOrder.FirstOrDefault(p => p.SiteOrderID == id);
                    var internetOrderSetStatusF = db.VegaPersonalSetting.FirstOrDefault(p => p.VegaSetting.SettingName == "InternetOrderSetStatusF" && p.UserName.ToLower() == User.Identity.Name.ToString().ToLower());
                    var extSourceID = db.VegaPersonalSetting.FirstOrDefault(p => p.VegaSetting.SettingName == "InternetOrderID" && p.UserName.ToLower() == User.Identity.Name.ToString().ToLower());

                    if (order == null)
                    {
                        order = db.DLVOrder.Add(new DLVOrder() { SiteOrderID = id, SendDateTime = DateTime.Now, Success = false });
                        db.SaveChanges();
                    }

                    string esid = "31";

                    if (extSourceID != null && !String.IsNullOrEmpty(extSourceID.SettingValue))
                        esid = extSourceID.SettingValue;

                    InternetOrders.SODresponse response = model.SendOrderToDelivery(User.Identity.Name.ToString(), esid);

                    if (response.Success)
                    {
                        order.Success = true;
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage += " Ошибка при оформлении интернет-заказа: " + ex.Message;
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
        public ActionResult SendToDelivery(int id)
        {
            var order = db.DLVOrder.FirstOrDefault(p => p.SiteOrderID == id);
            var internetOrderSetStatusF = db.VegaPersonalSetting.FirstOrDefault(p => p.VegaSetting.SettingName == "InternetOrderSetStatusF" && p.UserName == User.Identity.Name.ToString());
            var extSourceID = db.VegaPersonalSetting.FirstOrDefault(p => p.VegaSetting.SettingName == "InternetOrderID" && p.UserName == User.Identity.Name.ToString());

            if (order == null || order != null)
            {
                if (order == null)
                {
                    order = db.DLVOrder.Add(new DLVOrder() { SiteOrderID = id, SendDateTime = DateTime.Now, Success = false });
                    db.SaveChanges();
                }

                InternetOrders.OrderInfo model = new InternetOrders.OrderInfo();
                model.GetOrder(id, User.Identity.Name.ToString());

                if (internetOrderSetStatusF != null && Boolean.Parse(internetOrderSetStatusF.SettingValue.ToLower()))
                {
                    model.SetStatus('F');
                }

                string esid = "31";

                if (extSourceID != null && !String.IsNullOrEmpty(extSourceID.SettingValue))
                    esid = extSourceID.SettingValue;


                InternetOrders.SODresponse response = model.SendOrderToDelivery(User.Identity.Name.ToString(), esid);

                if (response.Success)
                {
                    order.Success = true;
                }

                db.SaveChanges();
                return View(response);
            }
            else
            {
                InternetOrders.SODresponse response = new InternetOrders.SODresponse() { OrderID = id, Success = false, Message = "Этот заказ уже был отправлен в деливери. Повторная отправка невозможна." };
                return View(response);
            }
        }
    }
}