using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;
using System.IO;

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

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            //Папка, куда будут сохраняться изображения для базы знаний
            const string uploadFolder = "UserFiles/Issues/Images";

            if (upload.ContentLength <= 0)
            {
                string output = "{\"uploaded\": 0,\"error\": { \"message\":\"Нулевая длина\" }}";
                return Content(output);
            }

            //Получаем имя файла
            var fileName = DateTime.Now.ToString("ddMMyyyyHHmmssff_") + Path.GetFileName(upload.FileName);
            var path = Path.Combine(Server.MapPath(string.Format("~/{0}", uploadFolder)), fileName);

            //Сохраняем файл

            try
            {
                upload.SaveAs(path);

                var url = string.Format("{0}{1}/{2}/{3}", Request.Url.GetLeftPart(UriPartial.Authority),
                    Request.ApplicationPath == "/" ? string.Empty : Request.ApplicationPath,
                    uploadFolder, fileName);

                string output = "{ \"uploaded\": 1, \"fileName\": \"" + fileName + "\", \"url\": \"/" + uploadFolder + "/" + fileName + "\" }";
                return Content(output);
            }
            catch (Exception ex)
            {
                string output = "{\"uploaded\": 0,\"error\": { \"message\":\"" + ex.Message + "\"}";
                return Content(output);
            }
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

        public ActionResult Edit(int id)
        {
            IssuesModels.IssueModel model = new IssuesModels.IssueModel();
            model.Get(id);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(IssuesModels.IssueModel model, HttpPostedFileBase upload)
        {
            model.LastModified = DateTime.Now;
            model.LastEditor = LoginViewModel.GetUserProperty(User.Identity.Name, "DisplayName");
            model.Save();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult New(IssuesModels.IssueModel model, HttpPostedFileBase upload)
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