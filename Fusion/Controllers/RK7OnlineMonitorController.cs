using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;

namespace Fusion.Controllers
{
    public class RK7OnlineMonitorController : Controller
    {
        // GET: RK7OnlineMonitor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SystemBalanceReport()
        {
            RK7QueryResult model = new RK7QueryResult();
            model = model.Deserialize(model.GetSystemBalance("10.1.0.100", 22330));
            return View(model);
        }
    }
}