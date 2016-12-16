using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;

namespace Fusion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            RSS model = new RSS();
            model.getRss("http://www.horeca.ru/kernel/rss/news.restaurant.xml");
            model.getRss("http://www.carbis.ru/forum/external.php?type=RSS2&forumids=112");
            model.channels[model.channels.Count - 1].rssTitle = "Форум Carbis: R-Keeper 7";
            model.getRss("http://www.carbis.ru/forum/external.php?type=RSS2&forumids=11");
            model.channels[model.channels.Count - 1].rssTitle = "Форум Carbis: StoreHouse";
            return View(model);
        }
    }
}