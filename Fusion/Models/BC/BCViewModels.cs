using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fusion.Models.BC
{
    public class Paginator
    {
        public int limit = 100;
        public int offset = 0;
        private int _currentPage;
        private int _totalcnt;
        public int currentPage 
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                offset = (_currentPage - 1) * limit;
            }
        }
        public int totalpages { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public int totalcnt
        {
            get { return _totalcnt; }
            set
            {
                _totalcnt = value;
                int c = _totalcnt % limit;
                if (c > 0)
                    c = 1;
                totalpages = (_totalcnt / limit) + c;
            }
        }
    }
    public class BCViewModels
    {
        public static string _birthday = "Birthday";
        public static string _weddingday = "WeddingDay";
        public static string _phone = "Phone";
        public static string _mobile = "Mobile";
        public static string _email = "E-Mail";
        public class Gender
        {
            public string DisplayName { get; set; }
            public string name { get; set; }
            public List<Gender> ToList()
            {
                List<Gender> gs = new List<Gender>();
                Gender g = new Gender();
                g.DisplayName = "хз";
                g.name = "undefined";
                gs.Add(g);
                g = new Gender();
                g.DisplayName = "Женский";
                g.name = "female";
                gs.Add(g);
                g = new Gender();
                g.DisplayName = "Мужской";
                g.name = "male";
                gs.Add(g);
                return gs;
            }
        }
        public class MaritalStatus
        {
            public string DisplayName { get; set; }
            public string name { get; set; }
            public List<MaritalStatus> ToList()
            {
                List<MaritalStatus> ms = new List<MaritalStatus>();
                MaritalStatus m = new MaritalStatus();
                m.DisplayName = "хз";
                m.name = "undefined";
                ms.Add(m);
                m = new MaritalStatus();
                m.DisplayName = "Холост/Не замужем";
                m.name = "single";
                ms.Add(m);
                m = new MaritalStatus();
                m.DisplayName = "Женат/Замужем";
                m.name = "married";
                ms.Add(m);
                m = new MaritalStatus();
                m.DisplayName = "Вдовец/Вдова";
                m.name = "widow";
                ms.Add(m);
                return ms;
            }
        }
        public class Holiday
        {
            public string DisplayName { get; set; }
            public string name { get; set; }
            public List<Holiday> ToList()
            {
                List<Holiday> holidays = new List<Holiday>();

                Holiday h = new Holiday();
                h.DisplayName = "День рождения";
                h.name = "Birthday";
                holidays.Add(h);
                h = new Holiday();
                h.DisplayName = "Свадьба";
                h.name = "WeddingDay";
                holidays.Add(h);
                return holidays;
            }
        }
        public class LinkChannel
        {
            public string DisplayName { get; set; }
            public string name { get; set; }
            public List<LinkChannel> ToList()
            {
                List<LinkChannel> lcs = new List<LinkChannel>();
                LinkChannel lc = new LinkChannel();
                lc.DisplayName = "Телефон";
                lc.name = "Phone";
                lcs.Add(lc);
                lc = new LinkChannel();
                lc.DisplayName = "Моб. телефон";
                lc.name = "Mobile";
                lcs.Add(lc);
                lc = new LinkChannel();
                lc.DisplayName = "Эл. почта";
                lc.name = "E-Mail";
                lcs.Add(lc);
                return lcs;
            }
        }
        public class CardStatus
        {
            public string DisplayName { get; set; }
            public string name { get; set; }
            public List<CardStatus> ToList()
            {
                List<CardStatus> cs = new List<CardStatus>();
                CardStatus c = new CardStatus();
                c.DisplayName = "Новая";
                c.name = "new";
                cs.Add(c);
                c = new CardStatus();
                c.DisplayName = "Инициализация";
                c.name = "initialized";
                cs.Add(c);
                c = new CardStatus();
                c.DisplayName = "Активна";
                c.name = "active";
                cs.Add(c);
                c = new CardStatus();
                c.DisplayName = "Отозвана";
                c.name = "revoked";
                cs.Add(c);
                return cs;
            }
        }
        public class PersonListViewModel: List<PersonViewModel>
        {
            [DisplayName("Ф.И.О.")]
            public string FIO { get; set; }
            [DisplayName("Номер карты")]
            public string card_no { get; set; }
            [DisplayName("Телефон")]
            public string phone { get; set; }
            [DisplayName("Баллов (от)")]
            public int bplimit { get; set; }
            [DisplayName("Операций (от)")]
            public int opdlimit { get; set; }
            public Paginator paginator { get; set; }
            public BCModels.Misc misc { get; set; }
            public string order { get; set; }
            public string OrderDirection { get; set; }
            public int page { get; set; }
            public void GetList(int offset, int limit, string order, string OrderDirection)
            {
                BCModels.PartyList pl = new BCModels.PartyList();
                pl.GetList(offset, limit, order, OrderDirection, bplimit, opdlimit);

                foreach (var p in pl)
                {
                    PersonViewModel pvm = new PersonViewModel();
                    pvm.Party = p;
                    pvm.FillVMData();
                    this.Add(pvm);
                }
            }
            public void FindByName(string name)
            {
                BCModels.PartyList pl = new BCModels.PartyList();
                pl.FindByName(name);

                foreach (var p in pl)
                {
                    PersonViewModel pvm = new PersonViewModel();
                    pvm.Party = p;
                    pvm.FillVMData();
                    this.Add(pvm);
                }
            }
            public void FindByCardNumber(string card_no)
            {
                BCModels.PartyList pl = new BCModels.PartyList();
                pl.FindByCardNumber(card_no);

                foreach (var p in pl)
                {
                    PersonViewModel pvm = new PersonViewModel();
                    pvm.Party = p;
                    pvm.FillVMData();
                    this.Add(pvm);
                }
            }
            public void FindByPhone(string phone)
            {
                BCModels.PartyList pl = new BCModels.PartyList();
                pl.FindByPhone(phone);

                foreach (var p in pl)
                {
                    PersonViewModel pvm = new PersonViewModel();
                    pvm.Party = p;
                    pvm.FillVMData();
                    this.Add(pvm);
                }
            }
            public int GetTotalCount()
            {
                BCModels.PartyList pl = new BCModels.PartyList();
                int cnt = pl.GetTotalCount(bplimit, opdlimit);
                return cnt;
            }
        }
        public class DiscountCardViewModel : BCModels.Retail.TDiscountCard
        {
            public IEnumerable<SelectListItem> CardStatusSelectList { get; set; }
            public ReceiptListViewModel Receipts { get; set; }
            public void FillDCVMData()
            {
                CardStatus cs = new CardStatus();
                IList<CardStatus> csl = cs.ToList();
                IEnumerable<SelectListItem> cardstatuses =
                    from c in csl
                    select new SelectListItem
                    {
                        Selected = this.status == c.name,
                        Text = c.DisplayName,
                        Value = c.name
                    };
                CardStatusSelectList = cardstatuses;
            }
            public void GetReceiptsRange(int? offset, int? limit)
            {
                Receipts = new ReceiptListViewModel();
                Receipts.GetReceiptsRangeByCardNo(offset, limit, this.card_no);
            }
        }
        public class DiscountCardListViewModel : BCModels.DiscountCards
        {
            public Paginator paginator { get; set; }
        }
        public class ReceiptListViewModel : BCModels.ReceiptList
        {
            public DateTime start_dt { get; set; }
            public DateTime end_dt { get; set; }
            public int party_id { get; set; }
            public Paginator paginator { get; set; }
            public BCModels.Misc misc { get; set; }
        }
        public class ReceiptViewModel : BCModels.Retail.TReceipt
        {
            public BCModels.ReceiptRowList receiptRows { get; set; }
            [DisplayName("Станция закрытия")]
            public BCModels.Retail.TPos pos { get; set; }
            public RK7.RK7HybridModels.PrintCheck check { get; set; }
        }
        public class PersonViewModel
        {
            //Особа
            public BCModels.Party.TParty Party { get; set; }
            //public BCModels.HolidayList Holidays { get; set; }
            [DisplayName("Пол")]
            public string PersonGender { get; set; }
            [DisplayName("Адреса")]
            public BCModels.Party.TPartyAddresses PersonAddresses { get; set; }
            [DisplayName("Счета")]
            public BCModels.SavingAccounts PersonSavingAccounts { get; set; }
            [DisplayName("Карты")]
            public BCModels.DiscountCards PersonDiscountCards { get; set; }
            [DisplayName("Телефоны")]
            public List<string> PersonPhones { get; set; }
            [DisplayName("Контакты")]
            public BCModels.LinkChannelsList LinkChannels { get; set; }
            [DisplayName("Эл. почта")]
            public List<string> PersonEmails { get; set; }
            public string Dicountcard { get; set; }
            public IEnumerable<SelectListItem> GenderSelectList { get; set; }
            public IEnumerable<SelectListItem> MaritalStatusSelectList { get; set; }
            public IEnumerable<SelectListItem> LinkChannelsSelectList { get; set; }
            public int TotalAccruedBP { get; set; }
            public int TotalSpentBP { get; set; }
            public void FillListData()
            {
                LinkChannel l = new LinkChannel();
                IList<LinkChannel> ls = l.ToList();
                IEnumerable<SelectListItem> linkChannels =
                    from c in ls
                    select new SelectListItem
                    {
                        Text = c.DisplayName,
                        Value = c.name
                    };
                LinkChannelsSelectList = linkChannels;

                Gender g = new Gender();
                IList<Gender> gs = g.ToList();
                IEnumerable<SelectListItem> genders =
                    from c in gs
                    select new SelectListItem
                    {
                        Text = c.DisplayName,
                        Value = c.name
                    };
                GenderSelectList = genders;

                MaritalStatus m = new MaritalStatus();
                IList<MaritalStatus> ms = m.ToList();
                IEnumerable<SelectListItem> maritalStatuses =
                    from c in ms
                    select new SelectListItem
                    {
                        Text = c.DisplayName,
                        Value = c.name
                    };
                MaritalStatusSelectList = maritalStatuses;
                //Дата рождения
                BCModels.HolidayList partyHolidays = new BCModels.HolidayList();
                //Каналы связи
                BCModels.LinkChannelsList partyLinkChannels = new BCModels.LinkChannelsList();
                //Адрес
                PersonAddresses = new BCModels.Party.TPartyAddresses();
                //Карты
                this.PersonDiscountCards = new BCModels.DiscountCards();
                //Счета
                this.PersonSavingAccounts = new BCModels.SavingAccounts();
            }
            public void FillVMData()
            {
                if (Party == null)
                    return;

                TotalAccruedBP = Party.GetTotalAccruedBP(Party.id);
                TotalSpentBP = Party.GetTotalSpentBP(Party.id);

                LinkChannel l = new LinkChannel();
                IList<LinkChannel> ls = l.ToList();
                IEnumerable<SelectListItem> linkChannels =
                    from c in ls
                    select new SelectListItem
                    {
                        Text = c.DisplayName,
                        Value = c.name
                    };
                LinkChannelsSelectList = linkChannels;

                Gender g = new Gender();
                IList<Gender> gs = g.ToList();
                IEnumerable<SelectListItem> genders =
                    from c in gs
                    select new SelectListItem
                    {
                        Selected = Party.gender == c.name,
                        Text = c.DisplayName,
                        Value = c.name
                    };
                GenderSelectList = genders;

                MaritalStatus m = new MaritalStatus();
                IList<MaritalStatus> ms = m.ToList();
                IEnumerable<SelectListItem> maritalStatuses =
                    from c in ms
                    select new SelectListItem
                    {
                        Selected = Party.marital_status == c.name,
                        Text = c.DisplayName,
                        Value = c.name
                    };
                MaritalStatusSelectList = maritalStatuses;

                //Пол
                foreach (var gn in gs)
                    if (gn.name == Party.gender)
                        PersonGender = gn.DisplayName;
                //Дата рождения
                BCModels.HolidayList partyHolidays = new BCModels.HolidayList();
                partyHolidays.GetList(Party.id);

                foreach (var h in partyHolidays)
                    if (h.name == _birthday)
                        this.Party.birthday = h.holiday;

                //Каналы связи
                this.LinkChannels = new BCModels.LinkChannelsList();
                this.LinkChannels.GetList(Party.id);
                PersonPhones = new List<string>();
                PersonEmails = new List<string>();

                foreach (var lc in this.LinkChannels)
                    if (lc.name == _mobile || lc.name == _phone)
                        PersonPhones.Add(lc.value);
                    else
                        if (lc.name == _email)
                            PersonEmails.Add(lc.value);
                //Адрес
                PersonAddresses = new BCModels.Party.TPartyAddresses();
                PersonAddresses.GetAddressByPartyID(Party.id);
                //Карты
                this.PersonDiscountCards = new BCModels.DiscountCards();
                this.PersonDiscountCards.GetListByPartyID(Party.id);

                if (this.PersonDiscountCards != null)
                    foreach (var dc in this.PersonDiscountCards)
                        dc.FillDCVMData();
                //Счета
                this.PersonSavingAccounts = new BCModels.SavingAccounts();
                this.PersonSavingAccounts.GetListByPartyID(Party.id);
            }
            public void GetPerson(int? id)
            {
                Party = new BCModels.Party.TParty();
                Party.GetPartyByID(id);
                FillVMData();
            }
        }
        public class TableReportViewModel : BCModels.Report
        {
            public BCModels.Misc misc { get; set; }
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
            public DateTime start_dt { get; set; }
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
            public DateTime end_dt { get; set; }
            public List<List<string>> viewData { get; set; }
        }
        public class OPDData : BCModels.BCSecurity.OPDData
        {
            public int opd { get; set; }
            public DateTime start_dt { get; set; }
            public DateTime end_dt { get; set; }
            public List<BCModels.BCSecurity.OPDData> OPDDataList = new List<BCModels.BCSecurity.OPDData>();
            public void GetOPD()
            {
                BCModels.BCSecurity.OPDData opddata = new BCModels.BCSecurity.OPDData();
                this.OPDDataList = opddata.GetOPD(this.opd, "", "", 0, 0, start_dt, end_dt);
            }
        }
    }
}