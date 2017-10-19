using System;
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
            model.getProblems();
            model.sendSingleProblem();
            return Redirect("/SB_problems/Problems");
        }



        public ActionResult Problem_top()
        {
            SB model = new SB();
            model.getProblems();
            model.getProblemsTop();
            return View(model);
        }
        [HttpPost]
        public ActionResult Problem_top(SB model)
        {
            model.getProblems();
            model.sendProblemTop();
            return View(model);
        }



        public ActionResult SendProblemTop()
        {
            SB model = new SB();
            model.createProblem();
            model.getProblems();
            model.getProblemsTop();
            return View(model);
        }
        [HttpPost]
        public ActionResult SendProblemTop(SB model)
        {
            model.getProblems();
            model.sendSingleProblemTop();
            return Redirect("/SB_problems/Problem_top");
        }
    }
}