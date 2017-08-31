﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;
using Microsoft.AspNet.Identity;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;

namespace Fusion.Controllers
{
    public class SB_ProblemsController : Controller
    {
        // GET: SB_Problems
        public ActionResult Index()
        {
            return View();
        }
         [MyAuthorize(Roles = "SB_Admin, SB_User")]
        public ActionResult Problems(string period)
        {
            SB model = new SB();
            model.username = User.Identity.GetUserName();
             if(period != "" && period != null) {
                 model.Period = DateTime.Parse(period);
             }
             else
             {
                 model.Period = DateTime.Today;
             }
             model.getProblems();
            return View(model);
        }
        [HttpPost]
        public ActionResult Problems(SB model)
        {
            model.username = User.Identity.GetUserName();
            model.sentProblem();
            model.getProblems();
            return View(model);
        }
        public ActionResult Restaurants()
        {
            SB model = new SB();
            model.getProblems();
            return View(model);
        }
        public ActionResult SendProblem()
        {
            SB model = new SB();
            model.getProblems();
            return View(model);
        }
        [HttpPost]
        public ActionResult SendProblem(SB model)
        {
            model.sendSingleProblem();
            return Redirect("/SB_problems/Problems");
        }
    }
}