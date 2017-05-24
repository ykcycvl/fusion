using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;

namespace Fusion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            RSS model = new RSS();
            return View(model);
        }
    }
}