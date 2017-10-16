using Fusion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fusion.Controllers
{
    public class ProfileController : Controller
    {
        Entities db = new Entities();
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Settings()
        {
            string userName = User.Identity.Name.ToString();
            var settings = db.VegaSetting.ToList();
            return View(settings);
        }
    }
}