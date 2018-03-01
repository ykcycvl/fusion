using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;
using System.Web.Script.Serialization;

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
        public ContentResult CallToGuest(int id)
        {
            ContentResult cr = new ContentResult();
            InternetOrders.OrderInfo model = new InternetOrders.OrderInfo();
            model.GetOrder(id, User.Identity.Name.ToString());

            try
            {
                var propsPhone = model.Properties.FirstOrDefault(p => p.OrderPropsId == 2);

                if (propsPhone != null)
                {
                    string phone = propsPhone.Value.Trim().Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Replace("+7", "");
                    Fusion.Models.InternetOrders.Message message = new InternetOrders.Message() { To = User.Identity.Name.ToString().Trim().ToLower(), Phone = phone };
                    var json = new JavaScriptSerializer().Serialize(message);
                    QuasiPhone q = new QuasiPhone();
                    q.Send(User.Identity.Name.ToString().ToLower(), json);
                    cr.Content = "Запрос отправлен!";
                }
            }
            catch (Exception ex)
            {
                cr.Content = "Не удалось отправить запрос на звонок: " + ex.Message;
            }

            cr.Content += @"<script>
    function Close() 
    {
        window.close();
    }
    setTimeout(Close, 3000);
</script>";

            return cr;
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
                var tmp = db.VegaPersonalSetting.Where(p => p.UserName.ToLower() == User.Identity.Name.ToString().ToLower());
                List<VegaPersonalSetting> settings = new List<VegaPersonalSetting>();

                if (tmp != null)
                    settings = tmp.ToList();

                var internetOrderSetStatusF = settings.FirstOrDefault(p => p.VegaSetting.SettingName == "InternetOrderSetStatusF" && p.UserName.ToLower() == User.Identity.Name.ToString().ToLower());
                var extSourceID = settings.FirstOrDefault(p => p.VegaSetting.SettingName == "InternetOrderID" && p.UserName.ToLower() == User.Identity.Name.ToString().ToLower());

                string esid = "31";

                if (extSourceID != null && !String.IsNullOrEmpty(extSourceID.SettingValue))
                    esid = extSourceID.SettingValue;

                var order = db.DLVOrder.FirstOrDefault(p => p.SiteOrderID == id);

                if (order == null || order != null)
                {
                    if (order == null)
                    {
                        order = db.DLVOrder.Add(new DLVOrder() { SiteOrderID = id, SendDateTime = DateTime.Now, Success = false });
                        db.SaveChanges();
                    }

                    if (!order.Success)
                    {
                        InternetOrders.SODresponse response = model.SendOrderToDelivery(User.Identity.Name.ToString(), esid);

                        if (response.Success)
                        {
                            order.Success = true;
                        }

                        db.SaveChanges();
                    }
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

        public void PayCheck()
        {
            InternetOrders.Sberbank model = new InternetOrders.Sberbank();
            model.PayCheck();
        }

        public ActionResult Sberbank(string orderNumber)
        {
            InternetOrders.Sberbank model = new InternetOrders.Sberbank();

            if (!String.IsNullOrEmpty(orderNumber))
            {
                model.SearchOrderPayment(orderNumber);
            }

            return View(model);
        }
    }
}