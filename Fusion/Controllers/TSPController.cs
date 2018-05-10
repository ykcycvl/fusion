using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models.Fabrika;

namespace Fusion.Controllers
{
    public class TSPController : Controller
    {
        // GET: TSP
        [MyAuthorize(Roles = "FusionAdmin, TSP")]
        public ActionResult Index()
        {
            TSPModels model = new TSPModels();
            model.GetList();
            return View(model);
        }

        [MyAuthorize(Roles = "FusionAdmin, TSP")]
        public ActionResult Add()
        {
            TSPModels model = new TSPModels();
            model.Initialize(User.Identity.Name.ToLower());
            return View(model);
        }

        [MyAuthorize(Roles = "FusionAdmin, TSP")]
        public ActionResult Edit(int id)
        {
            TSPModels model = new TSPModels(id, User.Identity.Name.ToLower());
            return View(model);
        }

        [HttpPost]
        [MyAuthorize(Roles = "FusionAdmin, SKK")]
        public ContentResult Save(string data)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "json";

            try
            {
                TSPModels model = new TSPModels();
                model.Save(data, User.Identity.Name);

                cr.Content = @"{ ""result"": ""success"",""message"": ""Успешно сохранено"" }";
            }
            catch (Exception ex)
            {
                cr.Content = @"{ ""result"": ""error"",""message"": """ + ex.Message + @""" }";
            }
            
            return cr;
        }
    }
}