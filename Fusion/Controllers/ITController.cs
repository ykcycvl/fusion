using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;

namespace Fusion.Controllers
{
    public class ITController : Controller
    {
        // GET: IT
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Articles(string id)
        {
            return View();
        }

        public ActionResult Calendar()
        {
            return View();
        }

        public ActionResult Tasks(int? id)
        {
            return View();
        }

        public ContentResult CalendarData(string username)
        {
            ITDeptModels model = new ITDeptModels();
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/xml";
            cr.Content = "<data>";
            cr.Content += model.GetTasks(username);
            cr.Content += "</data>";
            return cr;
        }
        [HttpPost]
        public ActionResult Test(string data)
        {
            return View();
        }
    }
}