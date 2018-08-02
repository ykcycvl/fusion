using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;
using Jitbit.Utils;
using Fusion.Models.SKK;

namespace Fusion.Controllers
{
    public class SKKController : Controller
    {
        // GET: SKK
        [MyAuthorize(Roles = "FusionAdmin, SKK")]
        public ActionResult Index()
        {
            return View();
        }
        [MyAuthorize(Roles = "FusionAdmin, SKK")]
        public ActionResult Restaurants()
        {
            SKKModels model = new SKKModels();
            model.getInfoList();
            return View(model);
        }
        [MyAuthorize(Roles = "FusionAdmin, SKK")]
        public ActionResult Employees()
        {
            SKKModels model = new SKKModels();
            model.getInfoList();
            return View(model);
        }
        [MyAuthorize(Roles = "FusionAdmin, SKK")]
        public ActionResult addEmployee(int? id)
        {
            SKKModels model = new SKKModels();
            model.getInfoList();
            if (id != null)
            {
                model.getEmployeeById(id);
            }
            else
            {
                model.createEmployee();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult addEmployee(SKKModels model)
        {
            model.saveEmployee();
            model.getInfoList();
            return RedirectToAction("Employees");
        }
        [MyAuthorize(Roles = "FusionAdmin, SKK")]
        public ActionResult addRestaurant(int? id)
        {
            SKKModels model = new SKKModels();
            model.getInfoList();
            model.getBlocks();
            if (id == null)
            {
                model.createRestaurant();
            }
            else
            {
                model.getRestaurantById(id);
            }
            model.getAccesses();
            model.createAccess();
            if (model.ArticleBlockAccesses.Where(m => m.restaurant_id == id).Any())
            {
                foreach (var it in model.BlockAccessList)
                {
                    if (model.ArticleBlockAccesses.Where(m => m.block_id == it.BlockId && m.restaurant_id == id).Any())
                    {
                        it.isActive = true;
                    }
                    else
                    {
                        it.isActive = false;
                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult addRestaurant(SKKModels model)
        {
            model.saveRestaurant();
            return Redirect("~/SKK/Index");
        }
        [MyAuthorize(Roles = "FusionAdmin, SKK")]
        public ActionResult ArticleBlocks()
        {
            SKKModels model = new SKKModels();
            model.getBlocks();
            return View(model);
        }
        [MyAuthorize(Roles = "FusionAdmin, SKK")]
        public ActionResult addBlock(int? id)
        {
            SKKModels model = new SKKModels();
            if (id != null)
            {
                model.getBlockById(id);
            }
            else
            {
                model.createBlock();
            }
            model.getArticles();
            /*model.getAccesses();
            model.createAccess();
            if(model.ArticleBlockAccesses.Where(m => m.block_id == id).Any())
            {
                foreach(var it in model.BlockAccessList)
                {
                    if(model.ArticleBlockAccesses.Where(m => m.block_id == id && m.restaurant_id == it.RestaurantId).Any())
                    {
                        it.isActive = true;
                    }
                    else
                    {
                        it.isActive = false;
                    }
                }
            }*/
            return View(model);
        }
        [HttpPost]
        public ActionResult addblock(SKKModels model)
        {
            model.saveBlock();
            return Redirect("~/SKK/Index");
        }
        [MyAuthorize(Roles = "FusionAdmin, SKK")]
        public ActionResult Articles()
        {
            SKKModels model = new SKKModels();
            model.getBlocks();
            model.getArticles();
            return View(model);
        }
        [MyAuthorize(Roles = "FusionAdmin, SKK")]
        public ActionResult addArticle(int? id)
        {
            SKKModels model = new SKKModels();
            model.getBlocks();
            if (id != null)
            {
                model.getArticleById(id);
            }
            else
            {
                model.createArticle();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult addArticle(SKKModels model)
        {
            model.saveArticle();
            return Redirect("~/SKK/Articles");
        }
        [MyAuthorize(Roles = "FusionAdmin, SKK")]
        public ActionResult Positions()
        {
            SKKModels model = new SKKModels();
            model.getPositions();
            model.getEmployees();
            return View(model);
        }
        [MyAuthorize(Roles = "FusionAdmin, SKK")]
        public ActionResult Acts()
        {
            SKKModels model = new SKKModels();
            model.getInfoList();
            model.getActs();
            return View(model);
        }
        [MyAuthorize(Roles = "FusionAdmin, SKK")]
        public ActionResult addAct(int? id)
        {
            SKKModels model = new SKKModels();
            model.getInfoList();
            if (id != null)
            {
                model.getActById(id);
            }
            else
            {
                model.createAct();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult addAct(SKKModels model)
        {
            model.getInfoList();
            model.ActId = model.saveAct();
            return RedirectToAction("addActData", new { actId = model.ActId });
        }
        public ActionResult Act(int id)
        {
            SKKModels model = new SKKModels();
            model.getInfoList();
            model.getBlocks();
            model.getArticles();
            model.getActs();
            model.getActDataList();
            model.getActFilesById(id);
            model.getAccesses();
            model.ActId = id;
            return View(model);
        }
        [MyAuthorize(Roles = "FusionAdmin, SKK")]
        public ActionResult addActData(int? actID)
        {
            SKKModels model = new SKKModels();
            model.getInfoList();
            model.getBlocks();
            model.getArticles();
            model.getActs();
            model.getActDataList();
            model.getActFilesById(actID);
            model.getAccesses();
            int? restaurant_id = model.Acts.FirstOrDefault(m => m.id == actID).restaurant_id;
            model.actDataMock = new List<SKKModels.ActDataMock>();
            if (actID != null)
            {
                model.createActDataMock(actID, restaurant_id);
            }
            else
            {
                return Redirect("~/SKK/Acts");
            }
            model.ActId = (int)actID;
            return View(model);
        }
        [HttpPost]
        public ActionResult addActData(SKKModels model)
        {
            /* List<HttpPostedFileBase> files = new List<HttpPostedFileBase>();
             for (int i = 0; i < Request.Files.Count; i++)
             {
                 var file = Request.Files[i];
                 files.Add(file);
             }*/
            model.getInfoList();
            model.getBlocks();
            model.getArticles();
            model.getActs();
            model.getActDataList();
            List<SKKModels.PostedFiles> files = new List<SKKModels.PostedFiles>();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                if (Request.Files[i].ContentLength != 0)
                {
                    var file = Request.Files[i];
                    string articleID = Request.Files.AllKeys[i].ToString();
                    int articleId = Int32.Parse(articleID);
                    files.Add(new SKKModels.PostedFiles { ArticleId = articleId, file = file });
                    file.SaveAs(Server.MapPath("~/Files/" + file.FileName));
                }
            }
            model.saveActData(model.ActId, files);
            return RedirectToAction("addActData", new { actID = model.ActId });
        }
        public ActionResult ExportToExcel(int actID)
        {
            SKKModels model = new SKKModels();
            model.getActs();
            model.getActDataList();
            model.getBlocks();
            model.getArticles();
            model.getInfoList();
            var table = new System.Data.DataTable("SKK");

            return null;
        }
        public ActionResult Analytics(DateTime? date_start, DateTime? date_end)
        {
            SKKModels model = new SKKModels();
            model.getActs();
            model.getActDataList();
            model.getBlocks();
            model.getArticles();
            model.getInfoList();
            if(date_start == null)
            {
                date_start = DateTime.Now.AddMonths(-1);
            }
            if(date_end == null)
            {
                date_end = DateTime.Now;
            }
            model.date_start = date_start;
            model.date_end = date_end;
            model.getAnalytics(date_start, date_end);
            return View(model);
        }
        [HttpPost]
        public ActionResult Analytics(SKKModels model)
        {
            return RedirectToAction("Analytics", new { date_start = model.date_start, date_end = model.date_end });
        }
        public int getLastArticle()
        {
            SKKModels model = new SKKModels();
            model.getArticles();
            return model.GetLastArticle();
        }
        [HttpPost]
        public ContentResult SaveArticlesAjax(string data, int block_id)
        {
            ContentResult result = new ContentResult();
            result.ContentType = "json";
            SKKModels model = new SKKModels();
            model.getArticles();
            try
            {
                if (model.SaveArticlesAjax(data, block_id))
                {
                    result.Content = @"{ ""result"": ""success"",""message"": ""Успешно сохранено"" }";
                }
                else
                {
                    result.Content = @"{ ""result"": ""error"",""message"": ""Ошибка"" }";
                }
            }
            catch(Exception ex)
            {
                result.Content = @"{ ""result"": ""error"",""message"": ""Ошибка"" }";
            }
            return result;
        }
    }
}