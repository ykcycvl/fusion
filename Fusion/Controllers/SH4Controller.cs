using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models.SH4;
using Sh4Ole;

namespace Fusion.Controllers
{
    public class SH4Controller : Controller
    {
        // GET: SH4
        public ActionResult Index(int? GroupID, int? GoodID)
        {
            /*SH4 model = new SH4();
            model.GetCost();*/
            //model.GetBalances(GroupID, GoodID);
            //return View(model);
            return View();
        }
        public ActionResult GoodsTree()
        {
            SH4 model = new SH4();

            if (model.Open() == 0)
            {
                model.GetGoodsTree(null);
                model.Close();
            }

            return View(model);
        }
        public ActionResult Goods(int? GroupID)
        {
            SH4 model = new SH4();

            if (model.Open() == 0)
            {
                model.GetBalances(GroupID, null);
                model.Close();
            }
            
            return View(model);
        }
        public ActionResult Cost(int? GroupID)
        {
            SH4 model = new SH4();

            if (model.Open() == 0)
            { 
                model.Close();
            }

            return View(model);
        }
    }
}