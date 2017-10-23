using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;

namespace Fusion.Controllers
{
    public class ContentController : Controller
    {
        Entities db = new Entities();

        // GET: Content
        [MyAuthorize(Roles = "FusionAdmin, ContentEditor")]
        public ActionResult Index()
        {
            List<EmailTemplate> model = db.EmailTemplate.ToList();
            return View(model);
        }

        [MyAuthorize(Roles = "FusionAdmin, ContentEditor")]
        public ActionResult List()
        {
            List<EmailTemplate> model = db.EmailTemplate.ToList();
            return View(model);
        }

        [MyAuthorize(Roles = "FusionAdmin, ContentEditor")]
        public ActionResult Add()
        {
            return View();
        }

        [MyAuthorize(Roles = "FusionAdmin, ContentEditor")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(EmailTemplate model)
        {
            db.EmailTemplate.Add(model);
            db.SaveChanges();

            return RedirectToAction("List");
        }

        [MyAuthorize(Roles = "FusionAdmin, ContentEditor")]
        public ActionResult Edit(int id)
        {
            EmailTemplate model = db.EmailTemplate.FirstOrDefault(p => p.id == id);
            return View(model);
        }

        [MyAuthorize(Roles = "FusionAdmin, ContentEditor")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(EmailTemplate model)
        {
            var m = db.EmailTemplate.FirstOrDefault(p => p.id == model.id);
            m.Name = model.Name;
            m.Title = model.Title;
            m.Content = model.Content;
            db.SaveChanges();

            return RedirectToAction("List");
        }
    }
}