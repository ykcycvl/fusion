using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}