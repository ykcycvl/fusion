using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;

namespace Fusion.Controllers
{
    public class RK7Controller : Controller
    {
        // GET: RK7
        public ActionResult Index()
        {
            RK7APIModels.RespGetRefsDataMenuItems model = new RK7APIModels.RespGetRefsDataMenuItems();
            model = model.Deserialize(model.GetMenuItemsFromRK());
            return View(model);
        }
        public ActionResult Menu()
        {
            RK7APIModels.RespGetRefsDataMenuItems model = new RK7APIModels.RespGetRefsDataMenuItems();
            model = model.Deserialize(model.GetMenuItemsFromRK());
            return View(model);
        }
        public ActionResult MenuPortions()
        {
            RK7APIModels model = new RK7APIModels();
            model.getMenuItemsGeneratedProps();
            return View(model.Props);
        }
    }
}