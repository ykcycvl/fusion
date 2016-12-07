using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fusion.Controllers.MiscReport
{
    public class MiscReportController : Controller
    {
        // GET: MiscReport
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PTReport(DateTime? start_dt, DateTime? end_dt)
        {
            if (start_dt == null || end_dt == null)
                return View();

            Models.ReportModels.TableReportModel trm = new Models.ReportModels.TableReportModel();
            trm.start_dt = (DateTime)start_dt;
            trm.end_dt = (DateTime)end_dt;
            trm.PTReport();
            return View(trm);
        }
    }
}