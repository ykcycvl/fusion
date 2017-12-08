using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;

namespace Fusion.Controllers
{
    public class PIUController : Controller
    {
        // GET: PIU
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTest()
        {
            PIU model = new PIU();
            model.GetTest();
            return View(model);
        }

        [HttpPost]
        public ContentResult Save(string data)
        {
            ContentResult result = new ContentResult();
            result.ContentType = "json";

            PIU model = new PIU();

            try
            {
                if (model.Save(data, User.Identity.Name))
                    result.Content = @"{ ""result"": ""success"",""message"": ""Успешно сохранено"" }";
                else
                    result.Content = @"{ ""result"": ""error"",""message"": ""Ошибка"" }";
            }
            catch (Exception ex)
            {
                result.Content = @"{ ""result"": ""error"",""message"": ""Ошибка"" }";
            }

            return result;
        }
    }
}