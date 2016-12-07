using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models.Bringo;

namespace Fusion.Controllers
{
    public class BringoController : Controller
    {
        // GET: Bringo
        public ActionResult Index()
        {
            Bringo model = new Bringo();
            if (model.Authorize())
                model.GetDeliveryOrders();
            return View(model);
        }
        public ActionResult SendOrder(long id)
        {
            Bringo model = new Bringo();

            if (model.Authorize())
                model.SendOrder(id);

            return View(model);
        }
    }
}