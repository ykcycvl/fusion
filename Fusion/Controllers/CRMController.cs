using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;

namespace Fusion.Controllers
{
    public class CRMController : Controller
    {
        // GET: CRM
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult Index()
        {
            CRMViewModels.OverallViewModel model = new CRMViewModels.OverallViewModel();
            model.IHA = new CRMViewModels.OverallViewModel.InvalidHoldersAccounts();
            model.IHA.GetIHA();
            return View(model);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult CreateAccounts()
        {
            CRMViewModels.OverallViewModel.InvalidHoldersAccounts model = new CRMViewModels.OverallViewModel.InvalidHoldersAccounts();
            model.AddAccounts();
            return RedirectToAction("Index");
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult Transactions(long? people_id)
        {
            CRMViewModels.TransactionsViewModel model = new CRMViewModels.TransactionsViewModel();
            model.people_id = people_id;
            model.StartDateTime = DateTime.Today.AddDays(-7);
            model.EndDateTime = DateTime.Today.AddDays(1);
            model.Search();
            return View(model);
        }
        [HttpPost]
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult Transactions(CRMViewModels.TransactionsViewModel model)
        {
            model.Search();
            return View(model);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult ViewCheck(Guid TransactionGuid)
        {
            CRMViewModels.CheckInfo model = new CRMViewModels.CheckInfo();
            if (model.GetCheck(TransactionGuid))
                return View(model);
            else
                return View();
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult Persons(CRMViewModels.PersonsViewModel model)
        {
            model.Search();
            return View(model);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult OPD()
        {
            ReportViewModels.OPDViewModel model = new ReportViewModels.OPDViewModel();
            model.count = 2;
            model.StartDateTime = DateTime.Today.AddDays(-10);
            model.EndDateTime = DateTime.Today.AddDays(1);
            model.Search();
            return View(model);
        }
        [HttpPost]
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult OPD(ReportViewModels.OPDViewModel model)
        {
            model.EndDateTime = model.EndDateTime.AddDays(1);
            model.Search();
            return View(model);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult FPD()
        {
            ReportViewModels.FPDViewModel model = new ReportViewModels.FPDViewModel();
            model.StartDateTime = DateTime.Today.AddDays(-10);
            model.EndDateTime = DateTime.Today.AddDays(1);
            model.Search();
            return View(model);
        }
        [HttpPost]
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult FPD(ReportViewModels.FPDViewModel model)
        {
            model.EndDateTime = model.EndDateTime.AddDays(1);
            model.Search();
            return View(model);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult FPM()
        {
            ReportViewModels.FPMViewModel model = new ReportViewModels.FPMViewModel();
            model.StartDateTime = DateTime.Today.AddDays(-10);
            model.EndDateTime = DateTime.Today.AddDays(1);
            model.Search();
            return View(model);
        }
        [HttpPost]
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult FPM(ReportViewModels.FPMViewModel model)
        {
            model.EndDateTime = model.EndDateTime.AddDays(1);
            model.Search();
            return View(model);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult SPM()
        {
            ReportViewModels.MiscReports model = new ReportViewModels.MiscReports();
            model.StartDateTime = DateTime.Today.AddMonths(-6);
            model.EndDateTime = DateTime.Today.AddDays(1);
            model.Summ = 2000;
            model.SPM();
            return View(model);
        }
        [HttpPost]
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult SPM(ReportViewModels.MiscReports model)
        {
            model.EndDateTime = DateTime.Today.AddDays(1);
            model.SPM();
            return View(model);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult ASIOD()
        {
            ReportViewModels.MiscReports model = new ReportViewModels.MiscReports();
            model.StartDateTime = DateTime.Today.AddMonths(-2);
            model.EndDateTime = DateTime.Today.AddDays(1);
            model.ASIOD();
            return View(model);
        }
        [HttpPost]
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult ASIOD(ReportViewModels.MiscReports model)
        {
            model.EndDateTime = DateTime.Today.AddDays(1);
            model.ASIOD();
            return View(model);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult CAO()
        {
            ReportViewModels.MiscReports model = new ReportViewModels.MiscReports();
            model.StartDateTime = DateTime.Today.AddMonths(-1);
            model.EndDateTime = DateTime.Today.AddDays(1);
            model.Summ = 3000;
            model.CAO();
            return View(model);
        }
        [HttpPost]
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult CAO(ReportViewModels.MiscReports model)
        {
            model.EndDateTime = DateTime.Today.AddDays(1);
            model.CAO();
            return View(model);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult Reports()
        {
            return View();
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult CreatePerson()
        {
            RKCRM.AddHoldersModel model = new RKCRM.AddHoldersModel();
            model.Holder = new RKCRM.Holder();
            model.Holder.Group_ID = "45";
            model.Holder.Birth = "01.12.1900";
            model.Holder.Contacts = new List<RKCRM.Holder.ContactInfo>();
            model.Holder.Contacts.Add(new RKCRM.Holder.ContactInfo() { Type_ID = "254", Dispatch = "True", Value = "" });
            return View(model);
        }
        [HttpPost]
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult CreatePerson(RKCRM.AddHoldersModel model)
        {
            if (ModelState.IsValid)
                if (model.AddHolder())
                    return RedirectToAction("EditPerson", new { people_id = model.Holder.Holder_ID });

            return View(model);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult EditPerson(string people_id)
        {
            RKCRM.EditHoldersModel model = new RKCRM.EditHoldersModel();
            model.Holder = model.GetHolderInfo(people_id);
            return View(model);
        }
        [HttpPost]
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult EditPerson(RKCRM.EditHoldersModel model)
        {
            model.EditHolder(User.Identity.Name.ToString());

            if (model.LastResult == "")
                return RedirectToAction("EditPerson", new { people_id = model.Holder.Holder_ID });
            else
            {
                if (model.Holder.Cards == null)
                    model.Holder.Cards = new List<RKCRM.Holder.CardInfo>();

                if (model.Holder.Accounts == null)
                    model.Holder.Accounts = new List<RKCRM.Holder.AccountInfo>();

                if (model.Holder.Contacts == null)
                    model.Holder.Contacts = new List<RKCRM.Holder.ContactInfo>();

                return View(model);
            }
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult AddTransaction(string CardCode, Decimal bp)
        {
            RKCRM.Transaction model = new RKCRM.Transaction();
            model.transaction = new RKCRM.Transaction.TransactionInfo();
            model.transaction.External_ID = "159357";
            model.transaction.External_Index = "951357";
            model.transaction.Amount = bp.ToString();
            model.transaction.Transaction_Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " +10:00";
            model.transaction.External_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            model.transaction.Card_Code = CardCode;
            model.transaction.Account_Type_ID = "16";

            if (bp >= 0)
            {
                model.transaction.Remarks = "Ручное начисление средств через Вегу пользователем " + User.Identity.Name.ToString();
            }
            else
            {
                model.transaction.Remarks = "Ручное списание средств через Вегу пользователем " + User.Identity.Name.ToString();
            }

            if (model.AddTransaction())
            {
                if (bp >= 0)
                    ViewBag.ErrorMessage += "Начислено " + bp.ToString() + " б.";
                else
                    ViewBag.ErrorMessage += "Списано " + bp.ToString() + " б.";
            }
            else
                ViewBag.ErrorMessage += model.LastResult;

            return View("Notification/DefaultNotification");
        }

        public ActionResult Import(string filename)
        {
            string[] sdata = System.IO.File.ReadAllLines("/WorkPrograms/Fusion/Fusion/tmp/" + filename);

            for (int i = 0; i < sdata.Length; i++)
            {
                string[] s = sdata[i].Split(';');

                RKCRM.ImportHoldersModel model = new RKCRM.ImportHoldersModel();
                model.Holder = new RKCRM.Holder();
                model.Holder.L_Name = s[2];

                model.Holder.Birth = s[14];

                if(s[3] != "")
                    model.Holder.F_Name = s[3];
                else
                    model.Holder.F_Name = "-";

                model.Holder.M_Name = s[4];
                model.Holder.Full_Name = s[5];
                model.Holder.Contacts = new List<RKCRM.Holder.ContactInfo>();

                if(s[12] != "")
                    model.Holder.Contacts.Add(new RKCRM.Holder.ContactInfo() { Type_ID = "254", Value = s[12] });

                /*if(s[11] != "")
                    model.Holder.Contacts.Add(new RKCRM.Holder.ContactInfo() { Type_ID = "252", Value = s[11] });*/

                model.Holder.Cards = new List<RKCRM.Holder.CardInfo>();
                model.Holder.Cards.Add(new RKCRM.Holder.CardInfo() { Card_Code = s[0], Offered = "01.12.2016", Expired = "01.01.2100" });
                model.AddHolder();
            }

            return View();
        }
    }
}