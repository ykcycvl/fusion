using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;

namespace Fusion.Controllers
{
    public class CalendarController : Controller
    {
        [MyAuthorize(Roles = "VegaCMAdmin, Управляющие")]
        public ActionResult Index()
        {
            CalendarModel model = new CalendarModel();

            using (var context = new PrincipalContext(ContextType.Domain, "fg.local"))
            {
                GroupPrincipal group = GroupPrincipal.FindByIdentity(context, "Управляющие");

                if (group != null)
                    foreach (Principal p in group.GetMembers())
                    {
                        UserPrincipal theUser = p as UserPrincipal;

                        if (!theUser.IsAccountLockedOut())
                            model.employees.Add(new CalendarModel.Employee() { Name = theUser.Name, Login = theUser.SamAccountName });
                    }
            }

            if (!LoginViewModel.IsMemberOf(User.Identity.Name, "VegaCMAdmin"))
                return RedirectToAction("ViewCalendar", new { @period = DateTime.Today.ToString("01.MM.yyyy") });
            else
                return View(model);
        }
        [MyAuthorize(Roles = "VegaCMAdmin, Управляющие")]
        public ActionResult Add()
        {
            CalendarTaskViewModel model = new CalendarTaskViewModel();
            model.DateStart = DateTime.Today;
            model.DateEnd = DateTime.Today;
            return View(model);
        }
        [HttpPost]
        [MyAuthorize(Roles = "VegaCMAdmin, Управляющие")]
        public ActionResult Add(CalendarTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Save();
                return RedirectToAction("ViewTask", new { @id = model.id });
            }
            else
                return View(model);
        }
        [MyAuthorize(Roles = "VegaCMAdmin, Управляющие")]
        public ActionResult Edit(int id)
        {
            CalendarTaskViewModel model = new CalendarTaskViewModel(id);
            return View(model);
        }
        [HttpPost]
        [MyAuthorize(Roles = "VegaCMAdmin, Управляющие")]
        public ActionResult Edit(CalendarTaskViewModel model)
        {
            model.Save();
            return RedirectToAction("ViewTask", new { @id=model.id });
        }
        [MyAuthorize(Roles = "VegaCMAdmin, Управляющие")]
        public ActionResult ViewCalendar(string period, string userName)
        {
            Planner model = new Planner();

            DateTime dt;

            if (!DateTime.TryParse(period, out dt))
                dt = DateTime.Today;

            model.Period = dt;

            if (!LoginViewModel.IsMemberOf(User.Identity.Name, "VegaCMAdmin"))
                userName = User.Identity.Name;

            model.userName = userName;

            return View(model);
        }
        [MyAuthorize(Roles = "VegaCMAdmin, Управляющие")]
        public ActionResult ViewTask(int id)
        {
            CalendarTaskViewModel model = new CalendarTaskViewModel(id);
            return View(model);
        }
        [MyAuthorize(Roles = "VegaCMAdmin, Управляющие")]
        public ActionResult Delete(int id)
        {
            CalendarTaskViewModel model = new CalendarTaskViewModel(id);
            model.Delete();
            return RedirectToAction("Index");
        }
        [MyAuthorize(Roles = "VegaCMAdmin, Управляющие")]
        public ActionResult Clone(int id)
        {
            CalendarTaskViewModel model = new CalendarTaskViewModel(id);
            model.Clone();
            return RedirectToAction("Edit", new { @id = model.id });
        }

        [MyAuthorize(Roles = "VegaCMAdmin, Управляющие")]
        public ActionResult TestCalendar(string period, string userName)
        {
            Planner model = new Planner();

            DateTime dt;

            if (!DateTime.TryParse(period, out dt))
                dt = DateTime.Today;

            model.Period = dt;

            if (!LoginViewModel.IsMemberOf(User.Identity.Name, "VegaCMAdmin"))
                userName = User.Identity.Name;

            model.userName = userName;
            model.GetTasks();

            return View(model);
        }
        public ContentResult CalendarData(string username)
        {
            ITDeptModels model = new ITDeptModels();
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/xml";
            cr.Content = "<data>";
            cr.Content += model.GetTasks(username);
            cr.Content += "</data>";
            return cr;
        }
    }
}