using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fusion.Controllers.CallCenter
{
    public class CallCenterController : Controller
    {
        // GET: CallCenter
        [MyAuthorize(Roles = "CallCenterReport,FusionAdmin")]
        public ActionResult Index()
        {
            return View();
        }
        [MyAuthorize(Roles = "CallCenterReport,FusionAdmin")]
        public ActionResult OperatorsReport(Fusion.Models.CallCenter.CallCenterModels.OperatorReport model)
        {
            if (model.start_dt == Convert.ToDateTime("01.01.0001"))
                model.start_dt = DateTime.Today.AddDays(-10);
            if (model.end_dt == Convert.ToDateTime("01.01.0001"))
                model.end_dt = DateTime.Today;

            model.GetOperatorOrdersSum();
            return View("OperatorsReport", model);
        }
        [MyAuthorize(Roles = "CallCenterReport,FusionAdmin")]
        public ActionResult CallingList(Fusion.Models.CallCenter.CallCenterModels.Order model)
        {
            if (model.start_dt == Convert.ToDateTime("01.01.0001"))
                model.start_dt = DateTime.Today.AddDays(-1);
            if (model.end_dt == Convert.ToDateTime("01.01.0001"))
                model.end_dt = DateTime.Today.AddDays(-1);

            model.GetOrders();
            return View(model);
        }
        public ActionResult aok()
        {
            Fusion.Models.CallCenter.CallCenterModels.RestReport m = new Models.CallCenter.CallCenterModels.RestReport();
            m.GetReport();
            return View(m);
        }
    }
}