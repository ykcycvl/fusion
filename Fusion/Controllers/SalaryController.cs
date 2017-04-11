using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;
using System.Web.Configuration;

namespace Fusion.Controllers
{
    public class SalaryController : Controller
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["ZupConnectionString"].ConnectionString;
        // GET: Salary
        public ActionResult Index()
        {
            SalaryModels.DetAccList model = new SalaryModels.DetAccList();
            model.connection = ((V83.COMConnector)HttpContext.Application["connector"]).Connect(connectionString);
            model.GetDAList();
            return View(model);
        }

        public ActionResult Sheet(string OrgName, string period)
        {
            SalaryModels.Organization model = new SalaryModels.Organization();

            try
            {
                if (period == null || period == "")
                    model.Period = DateTime.Today;
                else
                    model.Period = DateTime.Parse(period);

                model.connection = ((V83.COMConnector)HttpContext.Application["connector"]).Connect(connectionString);
                model.GetDivisionList();

                if (OrgName != null)
                {
                    model.FullName = OrgName;
                }

                model.GetFullData();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            finally
            {
                model.connection = false;
                model.connection = null;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Sheet(SalaryModels.Organization model)
        {
            model.connection = ((V83.COMConnector)HttpContext.Application["connector"]).Connect(connectionString);
            model.Post();
            model.GetDivisionList();
            model.connection = false;
            model.connection = null;
            return View(model);
        }
    }
}