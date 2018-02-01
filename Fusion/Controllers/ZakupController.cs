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
using Newtonsoft.Json;
using Microsoft.Office;
//using DevExtreme.AspNet.Data;
//using DevExtreme.AspNet.Mvc;

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
        [MyAuthorize(Roles = "FusionAdmin, ZakupAdmin")]
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
        [MyAuthorize(Roles = "ZakupAdmin, ZakupUser")]
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
            model.getUsers();
            model.restaurant_id = model.usersList.FirstOrDefault(m => m.domain_login == User.Identity.GetUserName()).bd_subdivision.id;
            if (catId == null)
            {
                catId = 1;
            }
            else
            {
                model.categoryId = catId;
            }
            /*model.getRemnants(model.restaurant_id);
            model.Open();
            model.getStorehouses();
            double double_storehouse_id = model.storehousesList.FirstOrDefault(m => m.name == model.restaurantsList.FirstOrDefault(g => g.id == model.restaurant_id).name).id;
            int storehouse_id = Int32.Parse(double_storehouse_id.ToString());
            model.getRemnants(null, storehouse_id);
            model.Close();*/
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
            return Redirect("~/Zakup/orders");
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
                    Export["Юр. лицо"] = model.list.bd_subdivision.FirstOrDefault(m => m.id == it.bd_employee.subdiv_id).bd_organization.name;
                    Export["Поставщик"] = it.bd_nomenclature.bd_vendor.name;
                }
                return File(Export.ExportToBytesWin(), "text/csv", "Заявки за " + model.export.date + " для " + model.export.VendorName_Name + ".csv");
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
                    Export["Юр. лицо"] = it.bd_organization.name;
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
                it.state = model.vendor_for_send_item.state;
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
        //public ActionResult TestOrders(string period)
        //{
        //    ZakupModel model = new ZakupModel();
        //    string name = User.Identity.GetUserName();
        //    model.username = name;
        //    if (period == null || period == "")
        //        model.Period = DateTime.Today;
        //    else
        //        model.Period = DateTime.Parse(period);
        //    model.getOrders();
        //    return View(model);
        //}
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
        [HttpPost]
        public ActionResult ExportOrders(ZakupModel model)
        {
            model.getOrders();
            model.getVendors();
            model.getNomenclatures();
            CsvExport export = new CsvExport();
            if (model.dateExportForEmployee != null)
            {
                foreach (var it in model.orders.Where(m => m.date == model.dateExportForEmployee && m.employee == model.username).OrderBy(m => m.bd_nomenclature.vendor_id))
                {
                    export.AddRow();
                    export["Поставщик"] = it.bd_nomenclature.bd_vendor.name;
                    export["Наиименование"] = it.bd_nomenclature.name;
                    export["Количество"] = it.count;
                    export["Ед. Измерения"] = it.bd_measurement.name;
                    export["Цена"] = it.bd_nomenclature.Price;
                    export["Дата прихода"] = it.date_end;
                }
                return File(export.ExportToBytesWin(), "text/csv", "Заявки за " + model.dateExportForEmployee + ".csv");
            }
            else return View();

        }
        [MyAuthorize(Roles = "FusionAdmin, ZakupAdmin")]
        public ActionResult Restaurants()
        {
            ZakupModel model = new ZakupModel();
            model.getUsers();
            return View(model);
        }
        [HttpPost]
        public ActionResult Restaurants(ZakupModel model)
        {
            model.postOrganizations();
            model.getUsers();
            return View(model);
        }
        [MyAuthorize(Roles = "FusionAdmin, ZakupAdmin")]
        public ActionResult Users()
        {
            ZakupModel model = new ZakupModel();
            model.getUsers();
            return View(model);
        }
        [MyAuthorize(Roles = "FusionAdmin, ZakupAdmin")]
        public ActionResult addVendor()
        {
            ZakupModel model = new ZakupModel();
            model.createVendor();
            return View(model);
        }
        [HttpPost]
        public ActionResult addVendor(ZakupModel model)
        {
            model.list.bd_vendor.Add(new bd_vendor { name = model.vendor.name, code = model.vendor.code, phones = model.vendor.phones, INN = model.vendor.INN });
            model.list.SaveChanges();
            return Redirect("~/Zakup/Vendors");
        }
        [MyAuthorize(Roles = "FusionAdmin, ZakupAdmin")]
        public ActionResult Analytics(string period)
        {
            ZakupModel model = new ZakupModel();
            if (period == null)
            {
                model.analyticsDate = DateTime.Today;
            }
            else
            {
                model.analyticsDate = DateTime.Parse(period);
            }
            model.getOrders();
            model.getVendors();
            model.getNomenclatures();
            model.getUsers();
            return View(model);
        }
        public ActionResult Remnants(int? GroupID, int storehouse_name)
        {
            ZakupModel model = new ZakupModel();
            model.getVendors();
            model.getNomenclatures();
            if (model.Open() == 0)
            {
                if (GroupID != null)
                {
                    model.getRemnants(GroupID, storehouse_name);
                    model.GetBalances(GroupID, null);
                }
                else model.getRemnants(null, storehouse_name);
                model.Close();
            }
            return View(model);
        }
        [MyAuthorize(Roles = "FusionAdmin, ZakupAdmin")]
        public ActionResult Storehouses()
        {
            ZakupModel model = new ZakupModel();
            if (model.Open() == 0)
            {
                model.getStorehouses();
                model.Close();
            }
            return View(model);
        }
        public ActionResult GoodsTree_remnants(int storehouse_name)
        {
            ZakupModel model = new ZakupModel();
            model.sh_id = storehouse_name;
            if (model.Open() == 0)
            {
                model.GetGoodsTree(null);
                model.Close();
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult getOrdersListMonth()
        {
            ZakupModel model = new ZakupModel();
            model.getVendors();
            model.getNomenclatures();
            model.getOrders();
            var ordersList = new List<object>();
            foreach (var it in model.orders.GroupBy(m => m.date.Value.Month).Select(g => new { name = g.Key, count = g.Count() }).OrderBy(h => h.name))
            {
                ZakupModel.chartOrders chart = new ZakupModel.chartOrders();
                chart.MonthName = it.name.ToString();
                chart.count = it.count;
                ordersList.Add(chart);
            }
            return Content(JsonConvert.SerializeObject(ordersList), "application/json");
        }
        [HttpGet]
        public ActionResult getOrdersListNoms()
        {
            ZakupModel model = new ZakupModel();
            model.getVendors();
            model.getNomenclatures();
            model.getOrders();
            var ordersList = new List<object>();
            foreach (var it in model.orders.GroupBy(m => m.bd_employee.bd_subdivision.name).Select(g => new { restName = g.Key, count = g.Count() }))
            {
                ZakupModel.chartOrders order = new ZakupModel.chartOrders();
                order.RestName = it.restName;
                order.count = it.count;
                ordersList.Add(order);
            }
            return Content(JsonConvert.SerializeObject(ordersList), "application/json");
        }
        public ActionResult TestAnalytics()
        {
            return View();
        }
        [MyAuthorize(Roles = "FusionAdmin, ZakupAdmin, ZakupUser")]
        public ActionResult Reclamation()
        {
            ZakupModel model = new ZakupModel();
            model.getUsers();
            model.getVendors();
            model.getNomenclatures();
            model.getReclamations();
            return View(model);
        }
        [HttpPost]
        public ActionResult Reclamation(ZakupModel model)
        {
            model.saveReclamations();
            model.getUsers();
            model.getVendors();
            model.getNomenclatures();
            model.getReclamations();
            return View(model);
        }
        public ActionResult addReclamation(int? id)
        {
            ZakupModel model = new ZakupModel();
            model.username = User.Identity.GetUserName();
            model.getUsers();
            model.getVendors();
            model.getNomenclatures();
            model.getReclamations();
            model.createReclamation();
            if(id != null)
            {
                model.reclamation_item = model.reclamations.FirstOrDefault(m => m.id == id);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult addReclamation(ZakupModel model)
        {
            List<HttpPostedFileBase> files = new List<HttpPostedFileBase>();
            for(int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                files.Add(file);
            }
            model.getUsers();
            model.getVendors();
            model.getReclamations();
            model.sendReclamation(files);
            return Redirect("~/Zakup/Reclamation");
        }
        [MyAuthorize(Roles = "FusionAdmin, ZakupAdmin, ZakupUser")]
        public ActionResult Orders_by_employees()
        {
            ZakupModel model = new ZakupModel();
            model.getVendors();
            model.getNomenclatures();
            model.getUsers();
            model.getOrders();
            
                List<IGrouping<string, bd_order>> ordersDyn = new List<IGrouping<string, bd_order>>();
                foreach (var it in model.orders.GroupBy(m => m.bd_employee.domain_login))
                {
                    ordersDyn.Add(it);
                }
                return View(ordersDyn);
        }
        [MyAuthorize(Roles = "FusionAdmin, ZakupAdmin")]
        public ActionResult Remnants_rest(int restaurant_id = -1)
        {
            ZakupModel model = new ZakupModel();
            model.restaurant_id = restaurant_id;
            model.getVendors();
            model.getNomenclatures();
            model.getUsers();
            model.getRemnants(restaurant_id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Remnants_rest(ZakupModel model)
        {
            model.getVendors();
            model.getNomenclatures();
            model.getUsers();
            model.saveRemnants(model.restaurant_id);
            return Redirect("Remnants_rest");
        }
        public ActionResult ReclamationExport(int reclamation_id)
        {
            GeneratedCode.GeneratedClass.Reclamation reclamation = new GeneratedCode.GeneratedClass.Reclamation();
            ZakupModel model = new ZakupModel();
            model.getUsers();
            model.getVendors();
            model.getNomenclatures();
            model.getReclamations();
            reclamation.address = model.reclamations.FirstOrDefault(m => m.id == reclamation_id).bd_subdivision.address;
            reclamation.date = model.reclamations.FirstOrDefault(m => m.id == reclamation_id).date;
            reclamation.date_end = model.reclamations.FirstOrDefault(m => m.id == reclamation_id).date;
            reclamation.FIO = model.reclamations.FirstOrDefault(m => m.id == reclamation_id).bd_subdivision.bd_employee.FirstOrDefault(j => j.subdiv_id == model.reclamations.FirstOrDefault(h => h.id == reclamation_id).restaurant_id).full_name;
            reclamation.count = 0;
            reclamation.nomenclature = model.reclamations.FirstOrDefault(m => m.id == reclamation_id).bd_nomenclature.name;
            reclamation.organisation = model.reclamations.FirstOrDefault(m => m.id == reclamation_id).bd_subdivision.bd_organization.name;
            reclamation.vendor = model.reclamations.FirstOrDefault(m => m.id == reclamation_id).bd_vendor.name;
            return File(new GeneratedCode.GeneratedClass().CreatePackageAsBytes(reclamation), "application/msword", "123.docx");
        }
        public ActionResult Test()
        {
            return View();
        }
    }
}