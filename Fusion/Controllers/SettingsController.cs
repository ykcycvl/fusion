using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;

namespace Fusion.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {
            return View();
        }

        [MyAuthorize(Roles = "FusionAdmin")]
        public ActionResult UserDataBind()
        {
            Settings model = new Settings();
            model.GetUserBidings();
            return View(model);
        }

        [MyAuthorize(Roles = "FusionAdmin")]
        [HttpPost]
        public ActionResult UserDataBind(Settings model)
        {
            model.SaveUserBindings();
            return View();
        }

        [MyAuthorize(Roles = "FusionAdmin")]
        public ActionResult UserSettings()
        {
            Settings model = new Settings();
            model.GetUserSettings(User.Identity.Name.ToLower());
            return View(model);
        }

        [MyAuthorize(Roles = "FusionAdmin")]
        [HttpPost]
        public ActionResult UserSettings(Settings model)
        {
            model.SaveUserSettings(User.Identity.Name.ToLower());
            return RedirectToAction("UserSettings");
        }
    }
}