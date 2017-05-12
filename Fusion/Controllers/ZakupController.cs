﻿using System;
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
    public class ZakupController : Controller
    {
        //
        // GET: /Zakup/
        public ActionResult Index(ZakupModel model)
        {
            string name = User.Identity.GetUserName();
            model.username = name;
            if (name == null || name == "")
            {
                return Redirect("~/Home");
            }
            if ((LoginViewModel.IsMemberOf(name, "ZakupAdmin")))
            {
                return View();
            }
            else if ((LoginViewModel.IsMemberOf(name, "ZakupUser")))
            {
                return Redirect("~/Zakup/Orders");
            }
            else return Redirect("~/Home");
        }
        public ActionResult Nomenclatures()
        {
            ZakupModel model = new ZakupModel();
            model.getVendors();
            model.getNomenclatures();
            //model.getList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nomenclatures(ZakupModel model)
        {
            model.items.Count();
            model.PostNom();
            model.getVendors();
            model.getNomenclatures();
            //model.getList();
            return View(model);
        }
        public ActionResult Vendors()
        {
            Models.ZakupModel model = new Models.ZakupModel();
            model.getVendors();
            return View(model);
        }
        [HttpPost]
        public ActionResult vendors(ZakupModel model)
        {
            model.PostVen();
            model.getVendors();
            return View(model);
        }
        public ActionResult orders(string period)
        {
            ZakupModel model = new ZakupModel();
            string name = User.Identity.GetUserName();
            model.username = name;
                if (period == null || period == "")
                    model.Period = DateTime.Today;
                else
                    model.Period = DateTime.Parse(period);
                model.getOrders();
                return View(model);
        }
        [HttpPost]
        public ActionResult orders(ZakupModel model)
        {
            string name = User.Identity.GetUserName();
            model.username = name;
            model.sendOrderList();
            model.getOrders();
            return View(model);
        }
        public ActionResult CreateOrder(int catId = 0)
        {
            ZakupModel model = new ZakupModel();
            model.getVendors();
            model.getNomenclatures();
            model.getOrders();
            model.categoryId = catId;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateOrder(ZakupModel model)
        {
            string username;
            username = User.Identity.GetUserName();
            model.getOrders();
            model.sendOrder(username);
            model.getVendors();
            model.getNomenclatures();
            return View(model);
        }
    }
}