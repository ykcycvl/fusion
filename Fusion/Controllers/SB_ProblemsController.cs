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
        public ActionResult Problems()
        {
            SB model = new SB();
            model.username = User.Identity.GetUserName();
            if (model.username == null || model.username == "")
            {
                return Redirect("~/Home");
            }
            else if (LoginViewModel.IsMemberOf(model.username, "SB_Admin") || LoginViewModel.IsMemberOf(model.username, "FusionAdmin") || LoginViewModel.IsMemberOf(model.username, "SB_User"))
            {
                model.getProblems();
                return View(model);
            }
            else return Redirect("~/Home");
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
    }
}