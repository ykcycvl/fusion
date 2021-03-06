﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;
using System.Web.Configuration;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Web.Script.Serialization;
using System.Drawing;

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
        public ActionResult GetBioTimeData(string orgname, string period, string number, int? year)
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
                model.GetBioTimeData(number, (int)year, User.Identity.Name);
            else
                if (!String.IsNullOrEmpty(orgname) && !String.IsNullOrEmpty(period))
            {
                model.CreateDocument(User.Identity.Name);
                return RedirectToAction("TimeSheet", new { number = model.DocNumber, year = model.Date.Year });
            }

            return View(model);
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
            {
                model.GetDocument(number, (int)year, User.Identity.Name);

            }
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
        [MyAuthorize(Roles = "SuperAdmin")]
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
        [MyAuthorize(Roles = "SuperAdmin")]
        public ActionResult Rates(SalaryModels.Vega1CWS.EmployeesRateModel model)
        {
            model.Save(User.Identity.Name);
            return RedirectToAction("Rates", new { orgname = model.FullName, period = model.Period });
        }

        public ActionResult ExportToExcel(string data)
        {
            string DocNumber = "";
            var list = new System.Data.DataTable("teste");
            list.Columns.Add("Подразделение", typeof(string));
            list.Columns.Add("Должность", typeof(string));
            list.Columns.Add("Ф.И.О.", typeof(string));
            list.Columns.Add("Оклад", typeof(string));
            list.Columns.Add("Часы", typeof(string));
            list.Columns.Add("Начислено", typeof(string));
            list.Columns.Add("Удержано", typeof(string));
            list.Columns.Add("Сумма к выдаче", typeof(string));
            list.Columns.Add("Подпись", typeof(string));

            var serializer = new JavaScriptSerializer();
            var heapdata = serializer.DeserializeObject(data);

            foreach (var undata in (Array)heapdata)
            {
                var r = (Dictionary<string, object>)undata;

                if (r.Keys.Contains("id") && r["id"].ToString().Trim() != "" && !r.Keys.Contains("Code"))
                {
                    object Subdiv = null;
                    r.TryGetValue("subdiv", out Subdiv);
                    string position = "";
                    string FullName = "";
                    string Rate = "";
                    string Hours = "";
                    object totaldetsumObj = null;
                    r.TryGetValue("totaldetsum", out totaldetsumObj);
                    object aadsumObj = null;
                    r.TryGetValue("aadsum", out aadsumObj);
                    object totalaccsumObj = null;
                    r.TryGetValue("totalaccsum", out totalaccsumObj);

                    Decimal totaldetsum = 0;
                    Decimal aadsum = 0;
                    Decimal totalaccsum = 0;

                    Decimal.TryParse(totaldetsumObj.ToString(), out totaldetsum);
                    Decimal.TryParse(aadsumObj.ToString(), out aadsum);
                    Decimal.TryParse(totalaccsumObj.ToString(), out totalaccsum);

                    var numberFormatInfo = new System.Globalization.CultureInfo("en-Us", false).NumberFormat;
                    numberFormatInfo.NumberGroupSeparator = " ";
                    numberFormatInfo.NumberDecimalSeparator = ",";

                    list.Rows.Add(Subdiv, position, FullName, Rate, Hours, totalaccsum.ToString("N", numberFormatInfo), totaldetsum.ToString("N", numberFormatInfo), aadsum.ToString("N", numberFormatInfo), "");
                }

                if (r.Keys.Contains("Code") && r.Keys.Contains("DocNumber") && r.Keys.Contains("Date"))
                {
                    object Subdiv = null;
                    r.TryGetValue("subdiv", out Subdiv);
                    object position = null;
                    r.TryGetValue("Position", out position);
                    object FullName = null;
                    r.TryGetValue("FullName", out FullName);
                    object Rate = null;
                    r.TryGetValue("Rate", out Rate);
                    object Hours = null;
                    r.TryGetValue("Hours", out Hours);
                    object totaldetsumObj = null;
                    r.TryGetValue("totaldetsum", out totaldetsumObj);
                    object aadsumObj = null;
                    r.TryGetValue("aadsum", out aadsumObj);
                    object totalaccsumObj = null;
                    r.TryGetValue("totalaccsum", out totalaccsumObj);
                    object docNum = null;
                    r.TryGetValue("DocNumber", out docNum);

                    if(docNum != null)
                        DocNumber = docNum.ToString();

                    Decimal totaldetsum = 0;
                    Decimal aadsum = 0;
                    Decimal totalaccsum = 0;

                    Decimal.TryParse(totaldetsumObj.ToString(), out totaldetsum);
                    Decimal.TryParse(aadsumObj.ToString(), out aadsum);
                    Decimal.TryParse(totalaccsumObj.ToString(), out totalaccsum);

                    var numberFormatInfo = new System.Globalization.CultureInfo("en-Us", false).NumberFormat;
                    numberFormatInfo.NumberGroupSeparator = " ";
                    numberFormatInfo.NumberDecimalSeparator = ",";

                    list.Rows.Add(Subdiv, position, FullName, Rate, Hours, totalaccsum.ToString("N", numberFormatInfo), totaldetsum.ToString("N", numberFormatInfo), aadsum.ToString("N", numberFormatInfo), "");
                }
            }

            var grid = new GridView();
            grid.DataSource = list;
            grid.DataBind();

            bool chet = false;

            for (int i = 0; i < grid.Rows.Count; i++)
            {
                grid.Rows[i].Cells[8].Width = 150;

                if (i == 0)
                {
                    for (int j = 0; j < grid.Rows[i].Cells.Count; j++)
                    {
                        grid.Rows[i].Cells[j].BackColor = Color.FromArgb(189, 215, 238);
                        grid.Rows[i].Cells[j].Font.Bold = true;
                    }

                    continue;
                }

                if (grid.Rows[i].Cells[2].Text.Trim() == "&nbsp;")
                {
                    for (int j = 0; j < grid.Rows[i].Cells.Count; j++)
                    {
                        grid.Rows[i].Cells[j].BackColor = Color.FromArgb(189, 215, 238);
                        grid.Rows[i].Cells[j].Font.Bold = true;
                    }
                    
                    chet = false;
                    continue;
                }

                if (chet)
                    for (int j = 0; j < grid.Rows[i].Cells.Count; j++)
                        grid.Rows[i].Cells[j].BackColor = Color.FromArgb(230, 230, 230);

                chet = !chet;
            }

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Ведомость_" + DocNumber + ".xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("MyView");
        }
    }
}