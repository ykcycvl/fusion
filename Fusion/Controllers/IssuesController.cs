using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;

namespace Fusion.Controllers
{
    public class IssuesController : Controller
    {
        // GET: Issues
        public ActionResult Index()
        {
            IssuesModels.IssueModel model = new IssuesModels.IssueModel();
            List<Issue> issueList = model.List();
            return View(issueList);
        }

        public ActionResult View(int id)
        {
            IssuesModels.IssueModel model = new IssuesModels.IssueModel();
            model.Get(id);
            return View(model);
        }

        public ActionResult New()
        {
            IssuesModels.IssueModel model = new IssuesModels.IssueModel();
            model.Author = LoginViewModel.GetUserProperty(User.Identity.Name, "DisplayName");
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult New(IssuesModels.IssueModel model)
        {
            model.Created = DateTime.Now;
            model.Author = LoginViewModel.GetUserProperty(User.Identity.Name, "DisplayName");
            model.LastModified = DateTime.Now;
            model.LastEditor = LoginViewModel.GetUserProperty(User.Identity.Name, "DisplayName");
            model.Save();
            return View(model);
        }
    }
}