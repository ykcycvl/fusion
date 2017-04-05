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
            return View();
        }

        public ActionResult Sheet()
        {
            SalaryModels.Organization model = new SalaryModels.Organization() { FullName = "ФьюжнГрупп" };

            try
            {
                model.connection = ((V83.COMConnector)HttpContext.Application["connector"]).Connect(connectionString);
                model.Period = DateTime.Today;
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
            return View(model);
        }

        public ActionResult Timetable()
        {
            return View();
        }
    }
}