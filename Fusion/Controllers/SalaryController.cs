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
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TimeSheets(string orgname)
        {
            SalaryModels.Vega1CWS.TimeSheetViewModel model = new SalaryModels.Vega1CWS.TimeSheetViewModel();
            model.FullName = orgname;
            model.GetTimeSheetList(orgname);
            return View(model);
        }
        public ActionResult TimeSheet(string orgname, string period, string number, int? year)
        {
            SalaryModels.Vega1CWS.TimeSheetViewModel model = new SalaryModels.Vega1CWS.TimeSheetViewModel();

            if (!String.IsNullOrEmpty(orgname))
                model.FullName = orgname;

            if (!String.IsNullOrEmpty(period))
                model.Period = DateTime.Parse(period);
            else
                model.Period = new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, 1);

            model.GetOrganizationList();

            if (!String.IsNullOrEmpty(number) && year != null)
                model.GetDocument(number, (int)year);
            else
                if (!String.IsNullOrEmpty(orgname) && !String.IsNullOrEmpty(period))
                {
                    model.CreateDocument();
                    return RedirectToAction("TimeSheet", new { number = model.DocNumber, year = model.Date.Year });
                }

            return View(model);
        }
        public ActionResult AccrualsAndDetentions(string orgname)
        {
            SalaryModels.Vega1CWS.AccrualAndDetentionViewModel model = new SalaryModels.Vega1CWS.AccrualAndDetentionViewModel();
            model.FullName = orgname;
            model.GetAccrualsAndDetentionsDocuments(orgname);
            return View(model);
        }
        public ActionResult AAD(string orgname, string period, string number, int? year)
        {
            SalaryModels.Vega1CWS.AccrualAndDetentionViewModel model = new SalaryModels.Vega1CWS.AccrualAndDetentionViewModel();

            if (!String.IsNullOrEmpty(orgname))
                model.FullName = orgname;

            if (!String.IsNullOrEmpty(period))
                model.Period = DateTime.Parse(period);
            else
                model.Period = new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, 1);

            model.GetOrganizationList();

            if (!String.IsNullOrEmpty(number) && year != null)
                model.GetDocument(number, (int)year);
            else
                if (!String.IsNullOrEmpty(orgname) && !String.IsNullOrEmpty(period))
                {
                    model.CreateDocument();
                    return RedirectToAction("AAD", new { number = model.DocNumber, year = model.Date.Year });
                }

            return View(model);
        }
        [HttpPost]
        public ContentResult SaveTimeSheet(string data)
        {
            ContentResult result = new ContentResult();
            result.ContentType = "json";
            SalaryModels.Vega1CWS.TimeSheetViewModel model = new SalaryModels.Vega1CWS.TimeSheetViewModel();

            try
            {
                if (model.SaveDocument(data))
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
        public ContentResult SaveAAD(string data)
        {
            ContentResult result = new ContentResult();
            result.ContentType = "json";
            SalaryModels.Vega1CWS.AccrualAndDetentionViewModel model = new SalaryModels.Vega1CWS.AccrualAndDetentionViewModel();

            try
            {
                if (model.SaveDocument(data))
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
        public ActionResult SalarySheet()
        {
            return View();
        }
        public ActionResult Test()
        {
            SalaryModels.Vega1CWS.AccrualAndDetentionViewModel model = new SalaryModels.Vega1CWS.AccrualAndDetentionViewModel();
            model.GetEmployees("ФьюжнГрупп");
            model.GetEmployeesAccrualsType();
            model.GetEmployeesDetentionsType();
            return View();
        }
    }
}