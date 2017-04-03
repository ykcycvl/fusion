using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;

namespace Fusion.Controllers
{
    public class SalaryController : Controller
    {
        // GET: Salary
        public ActionResult Index()
        {
            SalaryModels.Organization model = new SalaryModels.Organization() { FullName = "ФьюжнГрупп" };
            model.GetFullData();
            return View(model);
        }

        public ActionResult Sheet()
        {
            SalaryModels.Organization model = new SalaryModels.Organization() { FullName = "ФьюжнГрупп" };
            model.Period = DateTime.Today;
            model.GetFullData();
            return View(model);
        }

        [HttpPost]
        public ActionResult Sheet(SalaryModels.Organization model)
        {
            model.Post();
            return View(model);
        }

        public ActionResult Timetable()
        {
            return View();
        }
    }
}