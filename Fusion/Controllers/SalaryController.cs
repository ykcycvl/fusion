using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;
using System.Web.Configuration;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Fusion.Controllers
{
    public class SalaryController : Controller
    {
        [MyAuthorize(Roles = "VegaCMAdmin, ZUP")]
        public ActionResult Index()
        {
            return View();
        }
        [MyAuthorize(Roles = "VegaCMAdmin, ZUP")]
        public ActionResult TimeSheets(string orgname)
        {
            SalaryModels.Vega1CWS.TimeSheetViewModel model = new SalaryModels.Vega1CWS.TimeSheetViewModel();
            model.FullName = orgname;
            model.GetTimeSheetList(orgname, User.Identity.Name);
            return View(model);
        }
        [MyAuthorize(Roles = "VegaCMAdmin, ZUP")]
        public ActionResult TimeSheet(string orgname, string period, string number, int? year)
        {
            SalaryModels.Vega1CWS.TimeSheetViewModel model = new SalaryModels.Vega1CWS.TimeSheetViewModel();

            if (!String.IsNullOrEmpty(orgname))
                model.FullName = orgname;

            if (!String.IsNullOrEmpty(period))
                model.Period = DateTime.Parse(period);
            else
                model.Period = new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, 1);

            model.GetOrganizationList(User.Identity.Name);

            if (!String.IsNullOrEmpty(number) && year != null)
                model.GetDocument(number, (int)year, User.Identity.Name);
            else
                if (!String.IsNullOrEmpty(orgname) && !String.IsNullOrEmpty(period))
                {
                    model.CreateDocument(User.Identity.Name);
                    return RedirectToAction("TimeSheet", new { number = model.DocNumber, year = model.Date.Year });
                }

            return View(model);
        }
        [MyAuthorize(Roles = "VegaCMAdmin, ZUP")]
        public ActionResult AccrualsAndDetentions(string orgname)
        {
            SalaryModels.Vega1CWS.AccrualAndDetentionViewModel model = new SalaryModels.Vega1CWS.AccrualAndDetentionViewModel();
            model.FullName = orgname;
            model.GetAccrualsAndDetentionsDocuments(orgname, User.Identity.Name);
            return View(model);
        }
        [MyAuthorize(Roles = "VegaCMAdmin, ZUP")]
        public ActionResult AAD(string orgname, string period, string number, int? year)
        {
            SalaryModels.Vega1CWS.AccrualAndDetentionViewModel model = new SalaryModels.Vega1CWS.AccrualAndDetentionViewModel();

            if (!String.IsNullOrEmpty(orgname))
                model.FullName = orgname;

            if (!String.IsNullOrEmpty(period))
                model.Period = DateTime.Parse(period);
            else
                model.Period = new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, 1);

            model.GetOrganizationList(User.Identity.Name);

            if (!String.IsNullOrEmpty(number) && year != null)
                model.GetDocument(number, (int)year, User.Identity.Name);
            else
                if (!String.IsNullOrEmpty(orgname) && !String.IsNullOrEmpty(period))
                {
                    model.CreateDocument(User.Identity.Name);
                    return RedirectToAction("AAD", new { number = model.DocNumber, year = model.Date.Year });
                }

            return View(model);
        }
        [HttpPost]
        [MyAuthorize(Roles = "VegaCMAdmin, ZUP")]
        public ContentResult SaveTimeSheet(string data)
        {
            ContentResult result = new ContentResult();
            result.ContentType = "json";
            SalaryModels.Vega1CWS.TimeSheetViewModel model = new SalaryModels.Vega1CWS.TimeSheetViewModel();

            try
            {
                if (model.SaveDocument(data, User.Identity.Name))
                    result.Content = @"{ ""result"": ""success"",""message"": ""Успешно сохранено"", ""sheetnumber"" : """ + model.DocNumber + @""" }";
                else
                    result.Content = @"{ ""result"": ""error"",""message"": ""Ошибка"", ""sheetnumber"" : """ + model.DocNumber + @""" }";
            }
            catch (Exception ex)
            {
                result.Content = @"{ ""result"": ""error"",""message"": """ + ex.Message + @""", ""sheetnumber"" : """ + model.DocNumber + @""" }";
            }

            return result;
        }
        [HttpPost]
        [MyAuthorize(Roles = "VegaCMAdmin, ZUP")]
        public ContentResult SaveAAD(string data)
        {
            ContentResult result = new ContentResult();
            result.ContentType = "json";
            SalaryModels.Vega1CWS.AccrualAndDetentionViewModel model = new SalaryModels.Vega1CWS.AccrualAndDetentionViewModel();

            try
            {
                if (model.SaveDocument(data, User.Identity.Name))
                    result.Content = @"{ ""result"": ""success"",""message"": ""Успешно сохранено"", ""sheetnumber"" : """ + model.DocNumber + @""" }";
                else
                    result.Content = @"{ ""result"": ""error"",""message"": ""Ошибка"", ""sheetnumber"" : """ + model.DocNumber + @""" }";
            }
            catch (Exception ex)
            {
                result.Content = @"{ ""result"": ""error"",""message"": """ + "Ошибка" + @""", ""sheetnumber"" : """ + model.DocNumber + @""" }";
            }

            return result;
        }
        [MyAuthorize(Roles = "VegaCMAdmin, ZUP")]
        public ActionResult SalarySheet()
        {
            return View();
        }
        /*[MyAuthorize(Roles = "VegaCMAdmin, ZUP")]
        public ActionResult Test()
        {
            SalaryModels.Vega1CWS.AccrualAndDetentionViewModel model = new SalaryModels.Vega1CWS.AccrualAndDetentionViewModel();
            model.GetEmployees("ФьюжнГрупп", User.Identity.Name);
            model.GetEmployeesAccrualsType(User.Identity.Name);
            model.GetEmployeesDetentionsType(User.Identity.Name);
            return View();
        }*/
        [MyAuthorize(Roles = "VegaCMAdmin, ZUP")]
        public ActionResult Rates(string orgname, string period)
        {
            SalaryModels.Vega1CWS.EmployeesRateModel model = new SalaryModels.Vega1CWS.EmployeesRateModel();
            model.Subdivisions = new List<SalaryModels.Subdivision>();
            model.FullName = orgname;
            DateTime dt = new DateTime();
            DateTime.TryParse(period, out dt);
            model.Period = dt;
            model.GetOrganizationList(User.Identity.Name);

            if (!String.IsNullOrEmpty(orgname))
                model.GetEmployees(orgname, User.Identity.Name);

            return View(model);
        }
        [HttpPost]
        public ActionResult Rates(SalaryModels.Vega1CWS.EmployeesRateModel model)
        {
            model.Save(User.Identity.Name);
            return RedirectToAction("Rates", new { orgname = model.FullName, period = model.Period });
        }
    }
}