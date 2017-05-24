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
        string connectionString = WebConfigurationManager.ConnectionStrings["ZupConnectionString"].ConnectionString;
        // GET: Salary
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sheet(string OrgName, string period, string number)
        {
            SalaryModels.Organization model = new SalaryModels.Organization();

            try
            {
                model.connection = ((V83.COMConnector)HttpContext.Application["connector"]).Connect(connectionString);
                model.GetOrganizationList();

                if (OrgName != null)
                    model.FullName = OrgName;

                if (String.IsNullOrEmpty(period))
                    model.Period = DateTime.Today;
                else
                    model.Period = DateTime.Parse(period);

                if (!String.IsNullOrEmpty(number))
                {
                    model.SheetNumber = number;
                    model.GetSheetData();
                }
                else
                    model.GetEmployees();

                if (!String.IsNullOrEmpty(OrgName) && !String.IsNullOrEmpty(period) && String.IsNullOrEmpty(number))
                    model.CreateTimeSheet();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            finally
            {
                if (model.connection != null)
                {
                    Marshal.Release(Marshal.GetIDispatchForObject(model.connection));
                    model.connection = null;
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            if (String.IsNullOrEmpty(OrgName) && String.IsNullOrEmpty(period) && String.IsNullOrEmpty(number))
                return View(model);
            else
                if (String.IsNullOrEmpty(number))
                    return RedirectToAction("Sheet", new { @number = model.SheetNumber });
                else
                    return View(model);
        }

        public ActionResult TimeSheets(string OrgName)
        {
            SalaryModels.TimeSheetsViewModel model = new SalaryModels.TimeSheetsViewModel();

            try
            {
                model.connection = ((V83.COMConnector)HttpContext.Application["connector"]).Connect(connectionString);
                model.GetOrganizationList();
                model.OrganizationName = OrgName;
                model.Get();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            finally
            {
                Marshal.Release(Marshal.GetIDispatchForObject(model.connection));
                model.connection = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            return View(model);
        }

        public ActionResult AccrualsAndDetentions()
        {
            return View();
        }

        public ActionResult SAC(string number)
        {
            SalaryModels.SalariesAndContributions model = new SalaryModels.SalariesAndContributions();
            model.Number = number;
            model.Employees = new List<SalaryModels.Employee>();
            model.OrganizationName = "";
            model.connection = ((V83.COMConnector)HttpContext.Application["connector"]).Connect(connectionString);

            if (!String.IsNullOrEmpty(number))
                model.Get(number);

            Marshal.Release(Marshal.GetIDispatchForObject(model.connection));
            model.connection = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            return View(model);
        }

        public ActionResult EmployeeSalaryDetail(string number, string code)
        {
            SalaryModels.EmployeeSalaryDetail model = new SalaryModels.EmployeeSalaryDetail();
            model.connection = ((V83.COMConnector)HttpContext.Application["connector"]).Connect(connectionString);
            model.Get(number, code);
            Marshal.Release(Marshal.GetIDispatchForObject(model.connection));
            model.connection = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return View(model);
        }
        public ActionResult ChargeList()
        {
            SalaryModels.DetAccList model = new SalaryModels.DetAccList();
            model.connection = ((V83.COMConnector)HttpContext.Application["connector"]).Connect(connectionString);
            model.GetDAList();
            Marshal.Release(Marshal.GetIDispatchForObject(model.connection));
            model.connection = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return View(model);
        }

        public ActionResult SalaryCharge(string OrgName, string period)
        {
            SalaryModels.Organization model = new SalaryModels.Organization();

            try
            {
                model.connection = ((V83.COMConnector)HttpContext.Application["connector"]).Connect(connectionString);
                model.GetOrganizationList();

                if (OrgName != null)
                    model.FullName = OrgName;

                if (String.IsNullOrEmpty(period))
                    model.Period = DateTime.Today;
                else
                    model.Period = DateTime.Parse(period);

                model.Accruals = model.GetAccruals();
                model.Detentions = model.GetDetentions();
                model.GetEmployees();

                for (int i = 0; i < model.Subdivisions.Count; i++)
                {
                    for (int j = 0; j < model.Subdivisions[i].Employees.Count; j++)
                    {
                        model.Subdivisions[i].Employees[j].Accruals = model.Accruals;
                        model.Subdivisions[i].Employees[j].Detentions = model.Detentions;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            finally
            {
                Marshal.Release(Marshal.GetIDispatchForObject(model.connection));
                model.connection = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            return View(model);
            return View();
        }
        [HttpPost]
        public ActionResult SalaryCharge(SalaryModels.Organization model)
        {
            model.connection = ((V83.COMConnector)HttpContext.Application["connector"]).Connect(connectionString);
            model.PostDetentionsAndAccruals();
            model.GetOrganizationList();
            model.Accruals = model.GetAccruals();
            model.Detentions = model.GetDetentions();

            for (int i = 0; i < model.Subdivisions.Count; i++)
            {
                for (int j = 0; j < model.Subdivisions[i].Employees.Count; j++)
                {
                    model.Subdivisions[i].Employees[j].Accruals = model.Accruals;
                    model.Subdivisions[i].Employees[j].Detentions = model.Detentions;
                }
            }

            Marshal.Release(Marshal.GetIDispatchForObject(model.connection));
            model.connection = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return View(model);
        }
        public ActionResult SalarySheet()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public ContentResult SaveTimeSheet(string data)
        {
            ContentResult result = new ContentResult();
            result.ContentType = "json";

            SalaryModels.TimeSheet model = new SalaryModels.TimeSheet();

            try
            {
                model.connection = ((V83.COMConnector)HttpContext.Application["connector"]).Connect(connectionString);
                model.ETS = model.Deserialize(data.ToString()).Where(p => p.Code != null).ToList();

                if (model.Save())
                    result.Content = @"{ ""result"": ""success"",""message"": ""Успшено сохранено"", ""sheetnumber"" : """ + model.SheetNumber + @""" }";
                else
                    result.Content = @"{ ""result"": ""error"",""message"": ""Ошибка"", ""sheetnumber"" : """ + model.SheetNumber + @""" }";
            }
            catch (Exception ex)
            {
                result.Content = @"{ ""result"": ""error"",""message"": "" Ошибка: " + ex.Message + @""", ""sheetnumber"" : """ + model.SheetNumber + @""" }";
            }
            finally
            {
                if (model.connection != null)
                    Marshal.Release(Marshal.GetIDispatchForObject(model.connection));
                
                model.connection = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            return result;
        }
    }
}