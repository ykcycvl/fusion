using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;
using Microsoft.AspNet.Identity;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using Jitbit.Utils;

namespace Fusion.Controllers
{
    public class ZakupController : Controller
    {
        //
        // GET: /Zakup/
        [MyAuthorize(Roles = "FusionAdmin, ZakupAdmin, ZakupUser")]
        public ActionResult Index(ZakupModel model)
        {
            string name = User.Identity.GetUserName();
            model.username = name;
            return View();
        }
        [MyAuthorize(Roles = "ZakupAdmin")]
        public ActionResult Nomenclatures()
        {
            ZakupModel model = new ZakupModel();
            model.getVendors();
            model.getNomenclatures();
            return View(model);
        }
        /*
        [HttpPost]
        public ActionResult Nomenclatures(ZakupModel model)
        {
            model.items.Count();
            model.PostNom();
            model.getVendors();
            model.getNomenclatures();
            //model.getList();
            return View(model);
        }
         * */
        [MyAuthorize(Roles = "ZakupAdmin")]
        public ActionResult Vendors()
        {
            Models.ZakupModel model = new Models.ZakupModel();
            model.getVendors();
            return View(model);
        }
        [HttpPost]
        public ActionResult vendors(ZakupModel model)
        {
            model.PostVen();
            model.getVendors();
            return View(model);
        }
        [MyAuthorize(Roles = "FusionAdmin, ZakupAdmin, ZakupUser")]
        public ActionResult orders(string period)
        {
            ZakupModel model = new ZakupModel();
            string name = User.Identity.GetUserName();
            model.username = name;
                if (period == null || period == "")
                    model.Period = DateTime.Today;
                else
                    model.Period = DateTime.Parse(period);
                model.getOrders();
                return View(model);
        }
        [HttpPost]
        public ActionResult orders(ZakupModel model)
        {
            string name = User.Identity.GetUserName();
            model.username = name;
            model.sendOrderList();
            model.getOrders();
            return View(model);
        }
        [MyAuthorize(Roles = "FusionAdmin, ZakupAdmin, ZakupUser")]
        public ActionResult CreateOrder(int catId)
        {
            ZakupModel model = new ZakupModel();
            model.getVendors();
            model.getNomenclatures();
            model.getOrders();
            if (catId == null)
            {
                catId = 1;
            }
            else
            {
                model.categoryId = catId;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateOrder(ZakupModel model)
        {
            string username;
            username = User.Identity.GetUserName();
            model.getOrders();
            model.sendOrder(username);
            model.getVendors();
            model.getNomenclatures();
            model.categoryId = 1;
            return View(model);
        }
        public ActionResult Export()
        {
            ZakupModel model = new ZakupModel();
            model.getOrders();
            model.getVendors();
            model.getNomenclatures();
            return View(model);
        }
        [HttpPost]
        public ActionResult Export(ZakupModel model)
        {
            model.getOrders();
            model.getVendors();
            model.getNomenclatures();
            CsvExport Export = new CsvExport();
            model.export.VendorName_Name = model.vendorList.FirstOrDefault(m => m.id == model.export.VendorName).name;
            if (model.export.date != null && model.export.date.Year.ToString() != "1")
            {
                foreach (var it in model.orders.Where(c => c.bd_nomenclature.vendor_id == model.export.VendorName && c.date.Value.Year + c.date.Value.Month == model.export.date.Year + model.export.date.Month))
                {
                    Export.AddRow();
                    Export["Дата"] = it.date;
                    Export["Наименование"] = it.bd_nomenclature.name;
                    Export["Ед. измерения"] = it.bd_nomenclature.bd_measurement.name;
                    Export["Количество"] = it.count;
                    Export["Сумма"] = it.count * it.bd_nomenclature.Price;
                    Export["Ресторан"] = it.bd_employee.bd_subdivision.name;
                    Export["Поставщик"] = it.bd_nomenclature.bd_vendor.name;  
                }
                return File(Export.ExportToBytesWin(), "text/csv", "Заявки за " + model.export.date + " для "+ model.export.VendorName_Name +".csv");
            }
            else
            {
                foreach (var it in model.orders.Where(c => c.bd_nomenclature.vendor_id == model.export.VendorName))
                {
                    Export.AddRow();
                    Export["Дата"] = it.date;
                    Export["Наименование"] = it.bd_nomenclature.name;
                    Export["Ед. измерения"] = it.bd_nomenclature.bd_measurement.name;
                    Export["Количество"] = it.count;
                    Export["Сумма"] = it.count * it.bd_nomenclature.Price;
                    Export["Ресторан"] = it.bd_employee.bd_subdivision.name;
                    Export["Поставщик"] = it.bd_nomenclature.bd_vendor.name;   
                }
                return File(Export.ExportToBytesWin(), "text/csv", "Заявки за все время для " + model.export.VendorName_Name + ".csv");
            }

        }
        [MyAuthorize(Roles = "FusionAdmin, ZakupAdmin, ZakupUser")]
        public ActionResult Orders_by_vendors(int? vendor_id)
        {
            ZakupModel model = new ZakupModel();
            if (model.date_from == null)
            {
                model.date_from = DateTime.Today;
            }
            model.getVendors();
            model.getOrders();
            return View(model);
        }
        [MyAuthorize(Roles = "FusionAdmin, ZakupAdmin, ZakupUser")]
        public ActionResult OrdersList(string vendor_name, DateTime? date_from)
        {
            ZakupModel model = new ZakupModel();
            model.vendor_name = vendor_name;
            model.date_from = date_from;
            model.getVendors();
            model.getNomenclatures();
            model.getOrders();
            return View(model);
        }
        [MyAuthorize(Roles = "FusionAdmin, ZakupAdmin, ZakupUser")]
        [HttpPost]
        public ActionResult OrdersList(ZakupModel model) 
        {
            model.getOrders();
            model.getVendors();
            model.getNomenclatures();
            model.vendor_for_send_item.name = model.vendor_name;
            foreach (var it in model.list.bd_order.Where(m => m.bd_nomenclature.bd_vendor.name == model.vendor_for_send_item.name && m.date == model.vendor_for_send_item.date_from))
            {
                it.date_end = model.vendor_for_send_item.date_end;
            }
            model.list.SaveChanges();
            return Redirect("~/Zakup/Orders_by_vendors");
        }
        
        [HttpPost]
        public ContentResult SaveNomenclatureAjax(string data)
        {
            ContentResult result = new ContentResult();
            result.ContentType = "json";
            ZakupModel model = new ZakupModel();
            model.getVendors();
            model.getNomenclatures();
            try
            {
                if (model.SaveDocument(data))
                {
                    result.Content = @"{ ""result"": ""success"",""message"": ""Успешно сохранено"" }";
                }
                else
                {
                    result.Content = @"{ ""result"": ""error"",""message"": ""Ошибка"" }";
                }
            }
           catch (Exception ex) 
           {
               result.Content = @"{ ""result"": ""error"",""message"": ""Ошибка"" }";
           }
            return result;
        }
        public ActionResult TestOrders(string period)
        {
            ZakupModel model = new ZakupModel();
            string name = User.Identity.GetUserName();
            model.username = name;
            if (period == null || period == "")
                model.Period = DateTime.Today;
            else
                model.Period = DateTime.Parse(period);
            model.getOrders();
            return View(model);
        }
        [HttpPost]
        public ActionResult SaveOrdersAjax(string data)
        {
            ContentResult result = new ContentResult();
            result.ContentType = "json";
            ZakupModel model = new ZakupModel();
            model.getVendors();
            model.getNomenclatures();
            model.getOrders();
            try
            {
                if (model.SaveOrders(data))
                {
                    result.Content = @"{ ""result"": ""success"",""message"": ""Успешно сохранено"" }";
                }
                else
                {
                    result.Content = @"{ ""result"": ""error"",""message"": ""Ошибка"" }";
                }
            }
            catch (Exception ex)
            {
                result.Content = @"{ ""result"": ""error"",""message"": ""Ошибка"" }";
            }
            return result;
        }
    }
}