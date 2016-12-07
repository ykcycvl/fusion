using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fusion.Controllers.BC
{
    public class BCController : Controller
    {
        public ActionResult Test()
        {
            return View();
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult Index()
        {
            Fusion.Models.BC.BCViewModels.TableReportViewModel trvm = new Models.BC.BCViewModels.TableReportViewModel();
            trvm.misc = new Models.BC.BCModels.Misc();
            trvm.misc.GetTotalBP();
            trvm.misc.GetTotalBPAccrued();
            trvm.misc.GetTotalBPSpent();
            trvm.misc.GetTotalPersonCount();
            trvm.misc.GetFreeCardsCount();
            return View(trvm);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult EditPerson(int? id)
        {
            if (id == null)
                return RedirectToAction("Persons");

            Fusion.Models.BC.BCViewModels.PersonViewModel pvm = new Models.BC.BCViewModels.PersonViewModel();
            pvm.GetPerson(id);
            return View(pvm);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult LinkCard(int party_id, string card_no)
        {
            Models.BC.BCViewModels.DiscountCardViewModel dcvm = new Models.BC.BCViewModels.DiscountCardViewModel();
            dcvm.card_no = card_no;

            if (dcvm.FindCard(dcvm.card_no))
            {
                if (dcvm.owner_id != 0)
                {
                    ViewBag.ErrorMessage = "Карта уже привязана к другому участнику клуба";
                }
                else
                {
                    dcvm.LinkCard(dcvm.id, party_id);
                    ViewBag.SuccessMessage = "Карта успешно привязана";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Карта с таким номером не найдена";
            }

            return View("Notification/DefaultNotification");
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult AddBP(int card_id, int bp)
        {
            Models.BC.BCModels.Retail.TDiscountCard dc = new Models.BC.BCModels.Retail.TDiscountCard();
            dc.id = card_id;

            int res = dc.AddBP(bp, User.Identity.Name.ToString());

            ViewBag.ErrorMessage += "Начислено " + res.ToString() + " баллов";

            return View("Notification/DefaultNotification");
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult SubBP(int card_id, int bp)
        {
            Models.BC.BCModels.Retail.TDiscountCard dc = new Models.BC.BCModels.Retail.TDiscountCard();
            dc.id = card_id;

            int res = dc.SubBP(bp, User.Identity.Name.ToString());
            ViewBag.ErrorMessage += "Изъято " + res.ToString() + " баллов";

            return View("Notification/DefaultNotification");
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        [HttpPost]
        public ActionResult EditPerson(Fusion.Models.BC.BCViewModels.PersonViewModel model, string save, string addCard, string add_card_no)
        {
            model.Party.name = model.Party.name.Trim();
            //Сохранение пати
            model.Party.Save();
            //Сохранение счетов
            if (model.PersonSavingAccounts != null)
                foreach (var psa in model.PersonSavingAccounts)
                    psa.Save();
            //Сохранение карт
            if (model.PersonDiscountCards != null)
                foreach (var pdc in model.PersonDiscountCards)
                    pdc.Save();
            //Сохранение дня рождения
            model.GetPerson(model.Party.id);
            return View(model);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult Persons(string order, string OrderDirection, string FIO, string card_no, string phone, int? bplimit, int? opdlimit, int? page)
        {
            Fusion.Models.BC.BCViewModels.PersonListViewModel plvm = new Models.BC.BCViewModels.PersonListViewModel();

            if (bplimit == null)
                bplimit = 0;

            if (opdlimit == null)
                opdlimit = 0;

            plvm.bplimit = (int)bplimit;
            plvm.opdlimit = (int)opdlimit;

            if (page == null)
                plvm.page = 1;
            else
                plvm.page = (int)page;

            if (OrderDirection == null || OrderDirection == "")
                plvm.OrderDirection = "ASC";
            else
                plvm.OrderDirection = OrderDirection;

            if (order == null || order == "")
                plvm.order = "name";
            else
                plvm.order = order;

            if (FIO != null && FIO != string.Empty)
            {
                plvm.FindByName(FIO);
            }
            else
                if (card_no != null && card_no != string.Empty)
                {
                    plvm.FindByCardNumber(card_no);
                }
                else
                {
                    if (phone != null && phone != string.Empty)
                    {
                        plvm.FindByPhone(phone);
                    }
                    else
                    {
                        plvm.paginator = new Models.BC.Paginator();
                        plvm.paginator.currentPage = plvm.page;
                        plvm.paginator.totalcnt = plvm.GetTotalCount();
                        plvm.paginator.ControllerName = "BCController";
                        plvm.paginator.ActionName = "Persons";
                        plvm.GetList(plvm.paginator.offset, plvm.paginator.limit, plvm.order, plvm.OrderDirection);
                    }
                }
            //Общая сумма баллов в обороте
            plvm.misc = new Models.BC.BCModels.Misc();
            plvm.misc.GetTotalBP();
            plvm.misc.GetWithoutSavingAccountTotalCount();
            return View(plvm);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult Transactions(int? party_id)
        {
            if (party_id != null)
            {
                Fusion.Models.BC.BCModels.Party.TParty p = new Models.BC.BCModels.Party.TParty();
                p.GetPartyByID(party_id);
                ViewBag.PersonName = p.name;
                Fusion.Models.BC.BCViewModels.ReceiptListViewModel rlvm = new Models.BC.BCViewModels.ReceiptListViewModel();
                rlvm.party_id = Convert.ToInt32(party_id);
                rlvm.start_dt = DateTime.Today.AddDays(-90);
                rlvm.end_dt = DateTime.Today;
                rlvm.GetPartyReceiptsRangeForPeriod(rlvm.start_dt, rlvm.end_dt, (int)party_id);
                return View(rlvm);
            }
            else 
            {
                Fusion.Models.BC.BCViewModels.ReceiptListViewModel rlvm = new Models.BC.BCViewModels.ReceiptListViewModel();
                rlvm.start_dt = DateTime.Today;
                rlvm.end_dt = DateTime.Today;
                rlvm.GetReceiptsRangeForPeriod(rlvm.start_dt, rlvm.end_dt);
                return View(rlvm);
            }
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        [HttpPost]
        public ActionResult Transactions(int party_id, DateTime start_dt, DateTime end_dt)
        {
            if (party_id != 0)
            {
                Fusion.Models.BC.BCModels.Party.TParty p = new Models.BC.BCModels.Party.TParty();
                p.GetPartyByID(party_id);
                ViewBag.PersonName = p.name;
                Fusion.Models.BC.BCViewModels.ReceiptListViewModel rlvm = new Models.BC.BCViewModels.ReceiptListViewModel();
                rlvm.GetPartyReceiptsRangeForPeriod(start_dt, end_dt, party_id);
                return View(rlvm);
            }
            else
            {
                Fusion.Models.BC.BCViewModels.ReceiptListViewModel rlvm = new Models.BC.BCViewModels.ReceiptListViewModel();
                rlvm.GetReceiptsRangeForPeriod(start_dt, end_dt);
                return View(rlvm);
            }
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult DailyBPReport()
        {
            Fusion.Models.BC.BCViewModels.TableReportViewModel trvm = new Models.BC.BCViewModels.TableReportViewModel();
            trvm.start_dt = DateTime.Today.AddDays(-30);
            trvm.end_dt = DateTime.Today;
            trvm.viewData = trvm.GetDailySpentAccruedBP(trvm.start_dt, trvm.end_dt);
            return View(trvm);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        [HttpPost]
        public ActionResult DailyBPReport(DateTime start_dt, DateTime end_dt)
        {
            if (start_dt == null)
                start_dt = DateTime.Today.AddDays(-30);

            if (end_dt == null)
                end_dt = DateTime.Today;
                
            Fusion.Models.BC.BCViewModels.TableReportViewModel trvm = new Models.BC.BCViewModels.TableReportViewModel();
            trvm.start_dt = (DateTime)start_dt;
            trvm.end_dt = (DateTime)end_dt;
            trvm.viewData = trvm.GetDailySpentAccruedBP(trvm.start_dt, trvm.end_dt);
            return View(trvm);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult MonthlyBPReport()
        {
            Fusion.Models.BC.BCViewModels.TableReportViewModel trvm = new Models.BC.BCViewModels.TableReportViewModel();
            trvm.start_dt = DateTime.Today.AddMonths(-12);
            trvm.end_dt = DateTime.Today;
            trvm.viewData = trvm.GetMonthlySpentAccruedBP(trvm.start_dt, trvm.end_dt);
            return View(trvm);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        [HttpPost]
        public ActionResult MonthlyBPReport(DateTime start_dt, DateTime end_dt)
        {
            if (start_dt == null)
                start_dt = DateTime.Today.AddDays(-30);

            if (end_dt == null)
                end_dt = DateTime.Today;

            Fusion.Models.BC.BCViewModels.TableReportViewModel trvm = new Models.BC.BCViewModels.TableReportViewModel();
            trvm.start_dt = (DateTime)start_dt;
            trvm.end_dt = (DateTime)end_dt;
            trvm.viewData = trvm.GetMonthlySpentAccruedBP(trvm.start_dt, trvm.end_dt);
            return View(trvm);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult CreatePerson()
        {
            Fusion.Models.BC.BCViewModels.PersonViewModel pvm = new Models.BC.BCViewModels.PersonViewModel();
            pvm.FillListData();
            return View(pvm);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        [HttpPost]
        public ActionResult CreatePerson(Fusion.Models.BC.BCViewModels.PersonViewModel pvm)
        {
            pvm.Party.name = pvm.Party.name.Trim();
            pvm.Party.Save();
            return RedirectToAction("EditPerson", new { id = pvm.Party.id });
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult CreateSavingAccountsForEmpty()
        {
            Fusion.Models.BC.BCModels.Misc m = new Models.BC.BCModels.Misc();
            m.CreateSavingAccountsForEmpty();
            return RedirectToAction("Persons");
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult ViewCheck(int? id)
        {
            if (id == null)
                RedirectToAction("Index");

            Models.BC.BCViewModels.ReceiptViewModel rvm = new Models.BC.BCViewModels.ReceiptViewModel();
            rvm.GetReceiptByID((int)id);
            rvm.receiptRows = new Models.BC.BCModels.ReceiptRowList();
            rvm.receiptRows.GetReceiptRows((int)id);
            rvm.pos = new Models.BC.BCModels.Retail.TPos();
            rvm.pos.GetPOSByID(rvm.pos_id);
            rvm.check = new Models.RK7.RK7HybridModels.PrintCheck();
            rvm.check.GetPrintCheck("{" + rvm.transaction_no + "}");
            return View(rvm);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult Cards(int? page)
        {
            if(page == null)
                page = 1;

            Models.BC.BCViewModels.DiscountCardListViewModel dclvm = new Models.BC.BCViewModels.DiscountCardListViewModel();
            dclvm.paginator = new Models.BC.Paginator();
            dclvm.paginator.currentPage = (int)page;
            dclvm.paginator.totalcnt = dclvm.GetTotalCount();
            dclvm.paginator.ControllerName = "BCController";
            dclvm.paginator.ActionName = "Cards";
            dclvm.GetList(dclvm.paginator.offset, dclvm.paginator.limit);
            return View(dclvm);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult Reports()
        {
            return View("ReportIndex");
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        [HttpGet]
        public ActionResult Checks(int? page, DateTime? start_dt, DateTime? end_dt)
        {
            if (page == null)
                page = 1;

            Fusion.Models.BC.BCViewModels.ReceiptListViewModel rlvm = new Models.BC.BCViewModels.ReceiptListViewModel();
            rlvm.start_dt = DateTime.Today.AddMonths(-1);
            rlvm.end_dt = DateTime.Today;
            rlvm.paginator = new Models.BC.Paginator();
            rlvm.paginator.currentPage = (int)page;
            rlvm.paginator.totalcnt = rlvm.GetCount(rlvm.start_dt, rlvm.end_dt);
            rlvm.GetReceiptList(rlvm.paginator.offset, rlvm.paginator.limit, rlvm.start_dt, rlvm.end_dt);
            rlvm.paginator.ControllerName = "BCController";
            rlvm.paginator.ActionName = "Checks";
            return View(rlvm);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult AddLinkChannel(int party_id, string lc_name, string lc_value)
        {
            Models.BC.BCModels.Party.TLinkChannel lc = new Models.BC.BCModels.Party.TLinkChannel();
            lc.party_id = party_id;
            lc.name = lc_name;
            lc.value = lc_value;
            if (lc.Add())
                ViewBag.SuccessMessage = "Добавлено";
            else
                ViewBag.ErrorMessage = "Произошла ошибка во время добавления";

            return View("Notification/DefaultNotification");
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        public ActionResult LinkSavingAccount(int id)
        {
            Fusion.Models.BC.BCModels.Retail.TSavingAccount sa = new Models.BC.BCModels.Retail.TSavingAccount();
            sa.Create(id);
            return RedirectToAction("Persons");
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        [HttpGet]
        public ActionResult UnlinkSavingAccount(int id)
        {
            Fusion.Models.BC.BCModels.Retail.TSavingAccount sa = new Models.BC.BCModels.Retail.TSavingAccount();
            sa.Unlink(id);
            return View();
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        [HttpGet]
        public ActionResult OPD(int? opd)
        {
            Models.BC.BCViewModels.OPDData opddata = new Models.BC.BCViewModels.OPDData();

            if (opd == null)
                opd = 2;

            opddata.start_dt = DateTime.Today.AddDays(-7);
            opddata.end_dt = DateTime.Today;
            opddata.opd = (int)opd;
            opddata.GetOPD();
            return View(opddata);
        }
        [MyAuthorize(Roles = "BonusClubManager, BonusClubAdmin, FusionAdmin")]
        [HttpPost]
        public ActionResult OPD(int? opd, DateTime? start_dt, DateTime? end_dt)
        {
            Models.BC.BCViewModels.OPDData opddata = new Models.BC.BCViewModels.OPDData();

            if (opd == null)
                opd = 2;

            if (start_dt == null)
                opddata.start_dt = DateTime.Today.AddDays(-7);
            else
                opddata.start_dt = (DateTime)start_dt;

            if (end_dt == null)
                opddata.end_dt = DateTime.Today;
            else
                opddata.end_dt = (DateTime)end_dt;

            opddata.opd = (int)opd;
            opddata.GetOPD();
            return View(opddata);
        }
    }
}