﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fusion.Controllers.CallCenter
{
    public class CDRController : Controller
    {
        [MyAuthorize(Roles = "CallCenterReport,FusionAdmin")]
        public ActionResult Index()
        {
            Models.CallCenter.CDRViewModels.CDRInfo cdrinfo = new Models.CallCenter.CDRViewModels.CDRInfo();
            return View(cdrinfo);
        }
        [MyAuthorize(Roles = "CallCenterReport,FusionAdmin")]
        [HttpPost]
        public ActionResult Overall(Models.CallCenter.CDRViewModels.CDRInfo model)
        {
            if (model.start_dt == model.end_dt)
            {
                model.start_dt = model.start_dt.AddHours(9);
                model.end_dt = model.end_dt.AddHours(25);
                model.MissedCallsDetail();
                model.GetOverallStatistic(false);
                return View(model);
            }
            else
                if (model.start_dt > model.end_dt)
                    return View();
                else
                {
                    model.start_dt = model.start_dt.AddHours(9);
                    model.end_dt = model.end_dt.AddHours(25);
                    model.GetOverallStatistic(true);
                    return View("OverallForPeriod", model);
                }
        }

        [MyAuthorize(Roles = "CallCenterReport,FusionAdmin")]
        [HttpPost]
        public ActionResult ServiceLevelDetail(Models.CallCenter.CDRViewModels.CDRInfo model)
        {
            model.end_dt = model.end_dt.AddDays(1).AddSeconds(-1);
            model.ServiceLevelDetail();
            return View(model);
        }

        [MyAuthorize(Roles = "CallCenterReport,FusionAdmin")]
        [HttpPost]
        public ActionResult MissedCallsDetail(Models.CallCenter.CDRViewModels.CDRInfo model)
        {
            model.end_dt = model.end_dt.AddDays(1).AddSeconds(-1);
            model.MissedCallsDetail();
            return View(model);
        }

        [MyAuthorize(Roles = "CallCenterReport,FusionAdmin")]
        [HttpPost]
        public ActionResult QueueDetail(Models.CallCenter.CDRViewModels.CDRInfo model)
        {
            return View(model);
        }
        [MyAuthorize(Roles = "CallCenterReport,FusionAdmin")]
        [HttpGet]
        public ActionResult Records()
        {
            Models.CallCenter.CDRViewModels.CDRInfo cdrinfo = new Models.CallCenter.CDRViewModels.CDRInfo();
            return View(cdrinfo);
        }
        [MyAuthorize(Roles = "CallCenterReport,FusionAdmin")]
        [HttpPost]
        public ActionResult Records(Models.CallCenter.CDRViewModels.CDRInfo model)
        {
            model.end_dt = model.end_dt.AddDays(1).AddSeconds(-1);
            model.GetRecordsList();
            return View(model);
        }
        [MyAuthorize(Roles = "CallCenterReport,FusionAdmin")]
        public ActionResult CallReport(DateTime? date_start, DateTime? date_end)
        {
            if(date_start == null)
            {
                date_start = DateTime.Now.AddDays(-1);
            }
            if(date_end == null)
            {
                date_end = DateTime.Now;
            }
            else
            {
                date_end = date_end.Value.AddHours(23);
            }
            Models.CallCenter.CDR model = new Models.CallCenter.CDR();
            model.date_start = date_start;
            model.date_end = date_end;
            model.getCallList(date_start, date_end);
            return View(model);
        }
        [MyAuthorize(Roles = "CallCenterReport,FusionAdmin")]
        [HttpPost]
        public ActionResult CallReport(Models.CallCenter.CDR model)
        {
            return RedirectToAction("CallReport", new { model.date_start, model.date_end });
        }
    }
}