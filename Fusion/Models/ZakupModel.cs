using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Reflection;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using System.ComponentModel.DataAnnotations;
using Jitbit.Utils;
using System.Web.Script.Serialization;
using Sh4Ole;
using System.Text;
using System.Data.SqlClient;

//using DevExtreme.AspNet;
//using DevExtreme.AspNet.Mvc;
namespace Fusion.Models
{
    public class ZakupModel
    {
        //классы для сторхауса
        public class GoodsTreeItem
        {
            [Display(Name = "ID")]
            public int ID { get; set; }
            [Display(Name = "Родитель")]
            public int? ParentID { get; set; }
            [Display(Name = "Наименование")]
            public string Name { get; set; }
            [Display(Name = "Код")]
            public string Code { get; set; }
            [Display(Name = "Внешний код")]
            public string ExternalCode { get; set; }
        }
        public class Good
        {
            public dynamic id { get; set; }
            public dynamic GroupID { get; set; }
            public dynamic GroupName { get; set; }
            public dynamic Name { get; set; }
            public dynamic CodePrefix { get; set; }
            public dynamic CodeNumber { get; set; }
            public dynamic Unit { get; set; }
            public dynamic ComplectID { get; set; }
            public dynamic ComplectName { get; set; }
        }
        public class GoodsBalance
        {
            public Good good { get; set; }
            public dynamic balance { get; set; }
            public double cost { get; set; }
            public double cost_bal { get; set; }
            public double NDS { get; set; }
            public double NSP { get; set; }
        }
        public class remnants
        {
            public int RID { get; set; }
            public string name { get; set; }
            public string measurements { get; set; }
            public dynamic quantity { get; set; }
            public double vat { get; set; }
            public string storehouse { get; set; }
        }
        public class storehouses
        {
            public double id { get; set; }
            public string name { get; set; }
        }
        //классы для селектов, экспорта в эксель и пр.
        public class vendors1
        {
            public string name { get; set; }
            public int id { get; set; }
        }
        public class remnants_for_noms
        {
            public int nomenclature_id { get; set; }
            public string nomenclature_name { get; set; }
            public int? count { get; set; }
        }
        public class restaurants_and_remnants
        {
            public int restaurant_id { get; set; }
            public string restaurant_name { get; set; }
            public List<remnants_for_noms> remnants {get; set;}
        }
        public int restaurant_id { get; set; }
        public List<restaurants_and_remnants> remnants_overall { get; set; }
        public class chartOrders
        {
            public string MonthName { get; set; }
            public string RestName { get; set; }
            public int count { get; set; }
        }
        public class categs
        {
            public string name;
            public int id;
        }
        public class items1
        {
            public string name { get; set; }
            public string vendor_name { get; set; }
            public int vendor_id { get; set; }
            public string measurement_name { get; set; }
            public int measurement_id { get; set; }
            public string category_name { get; set; }
            public int category_id { get; set; }
            public int id { get; set; }
            public decimal? price { get; set; }
            public string state_name { get; set; }
            public int state_id { get; set; }
        }
        public class ExportData
        {
            public int VendorName { get; set; }
            public DateTime date { get; set; }
            public int RestaurantName { get; set; }
            public string VendorName_Name { get; set; }
        }
        public class vendor_for_send
        {
            public DateTime date_end { get; set; }
            public DateTime date_from { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int state { get; set; }
        }
        public class order
        {
            public int id { get; set; }
            public DateTime date_start { get; set; }
            public DateTime? date_end { get; set; }
            public string comment { get; set; }
            public string state { get; set; }
            public int state_id { get; set; }
            public decimal count { get; set; }
            public int measurement_id { get; set; }
        }
        public class reclamation
        {
            public int id { get; set; }
            public int problem { get; set; }
            public string solution { get; set; }
            public DateTime date { get; set; }
            public int restaurant_id { get; set; }
            public int vendor_id { get; set; }
            public int nomenclature_id { get; set; }
        }
        /*Здесь будут переменные*/
        public List<GoodsTreeItem> GoodsTree = new List<GoodsTreeItem>();
        public List<Good> Goods = new List<Good>();
        public List<GoodsBalance> GoodsBalances = new List<GoodsBalance>();
        public List<items1> listItem { get; set; }
        public List<bd_rests> remnantsSQL { get; set; }
        public List<Models.bd_nomenclature> items { get; set; }
        public List<Models.bd_measurement> maesurements { get; set; }
        public List<Models.bd_category> categ { get; set; }
        public List<Models.bd_employee> usersList { get; set; }
        public List<Models.bd_organization> organizationList { get; set; }
        public List<Models.bd_subdivision> restaurantsList { get; set; }
        public List<ZakupModel.vendors1> vendors { get; set; }
        public List<bd_reclamation> reclamations { get; set; }
        public string ReclamationDataWebix { get; set; }
        public List<bd_nomenclature_state> nomeclature_states { get; set; }
        public List<bd_reclamation_problems> reclamation_problems { get; set; }
        public List<bd_reclamation_files> reclamation_files { get; set; }
        public Entities list = new Entities();
        public IEnumerable<SelectListItem> catListFull { get; set; }
        public IEnumerable<SelectListItem> statesSelect { get; set; }
        public List<Models.bd_order> orders { get; set; }
        public List<Models.bd_states> states { get; set; }
        public List<categs> Categories { get; set; }
        public List<storehouses> storehousesList { get; set; }
        public List<remnants> remnantsList { get; set; }
        public List<Models.bd_vendor> vendorList { get; set; }
        public bd_vendor vendor { get; set; }
        public bd_reclamation reclamation_item { get; set; }
        public string vendor_name { get; set; }
        public string reclamation_problemName { get; set; }
        public string restaurantName { get; set; }
        public string nomenclatureName { get; set; }
        public DateTime? date_end { get; set; }
        public DateTime? date_from { get; set; }
        public ExportData export { get; set; }
        public DateTime analyticsDate { get; set; }
        public vendor_for_send vendor_for_send_item { get; set; }
        public string categoryName { get; set; }
        public string CategoryName1 { get; set; }
        public string organizationName { get; set; }
        public int categoryId { get; set; }
        public string vendorName { get; set; }
        public string stateName { get; set; }
        public string nomenclature_state_name { get; set; }
        public DateTime dateExportForEmployee { get; set; }
        public string measurementName { get; set; }
        [DataType(DataType.Date)]
        public DateTime Period { get; set; }
        public string username { get; set; }
        public int sh_id { get; set; }

        //методы для сторхауса
        Sh4Ole.SH4App sh4 = new SH4App();
        public int Open()
        {
            sh4.SetServerName("10.1.0.14:pTa10002t60000");
            return sh4.DBLoginEx("Admin", "2707");
        }
        public void Close()
        {
            sh4.DBLogout();
        }
        public void getStorehouses()
        {
            storehousesList = new List<storehouses>();
            int IndQuery = sh4.pr_CreateProc("CorrTree");
            sh4.pr_SetValByName(IndQuery, 0, "101.1.0", 0); //16
            sh4.pr_Post(IndQuery, 0);
            int h = sh4.pr_ExecuteProc(IndQuery);
            int flds = sh4.pr_FieldCount(IndQuery, 1);
            while (sh4.pr_EOF(IndQuery, 1) != 1)
            {
                storehouses storehouse = new storehouses();
                storehouse.id = sh4.pr_ValByName(IndQuery, 1, "101.1.0");
                storehouse.name = sh4.pr_ValByName(IndQuery, 1, "101.3.0");
                storehousesList.Add(storehouse);
                sh4.pr_Next(IndQuery, 1);
            }
            sh4.pr_CloseProc(IndQuery);
        }
        public void GetGoodsTree(int? id)
        {
            int res = sh4.GoodsTree();

            if (res >= 0)
            {
                while (sh4.EOF(res) != 1)
                {
                    GoodsTreeItem gti = new GoodsTreeItem();
                    gti.ID = sh4.ValByName(res, "1.209.1.0");

                    if (sh4.ValByName(res, "1.209.2.0").GetType() != typeof(DBNull))
                        gti.ParentID = sh4.ValByName(res, "1.209.2.0");

                    Encoding srcEncodingFormat = Encoding.GetEncoding(1251);
                    Encoding dstEncodingFormat = Encoding.UTF8;
                    byte[] originalByteString = srcEncodingFormat.GetBytes(sh4.ValByName(res, "1.209.3.0").ToString());
                    byte[] convertedByteString = Encoding.Convert(srcEncodingFormat, dstEncodingFormat, originalByteString);
                    gti.Name = dstEncodingFormat.GetString(convertedByteString);
                    gti.Code = sh4.ValByName(res, "1.209.4.0").ToString();
                    gti.ExternalCode = sh4.ValByName(res, "1.209.5.0").ToString();

                    GoodsTree.Add(gti);
                    sh4.Next(res);
                }

                sh4.CloseQuery(res);
            }
        }
        public void getRemnants(int? GroupID, int storehouse)
        {
            //получение остатков на основа id склада и товарной группы, если товарная группа пустая - выгружаем все номенклатуры
            remnantsList = new List<remnants>();
            int res = sh4.pr_CreateProc("GsRemns");
            sh4.pr_SetValByName(res, 0, "0.1.0", DateTime.Today.ToOADate());
            if (GroupID != null)
            {
                sh4.pr_SetValByName(res, 0, "209.1.5", GroupID);      // RID Товарной группы (null -по всем)
            }
            //sh4.pr_SetValByName(res, 0, "102.1.6", null);       // RID Скла да (null -по всем)
            sh4.pr_SetValByName(res, 0, "101.1.7", storehouse);       // RID Группы складов (null -по всем)
            //sh4.pr_SetValByName(res, 0, "208.1.8", null);       // RID Основные Категории товаров (null -по всем)
            //sh4.pr_SetValByName(res, 0, "219.1.9", null);       // RID Бухгалтерские Категории товаров (null -по всем)

            sh4.pr_SetValByName(res, 0, "0.3.0", 0);      // Опции построения отчета
            //sh4.pr_SetValByName(res, 0, "1.1.0", 0);            // Поле кода типа группировки tUint16
            //sh4.pr_SetValByName(res, 0, "100.1.4", 0);          // RID валюты, в которой строить отчет
            sh4.pr_SetValByName(res, 0, "0.6.0", 0);            // tUint16 Тип сравнения кол-ва (<, >, и т.п.): бит 1 <, бит 2 ==, бит 3 >
            sh4.pr_SetValByName(res, 0, "0.7.0", 0);             // tUint16 С чем сравнивать кол-во: 0 - с нулем, 1 - с мин. запасом, 2 - с макс. запасом
            sh4.pr_Post(res, 0);
            int h = sh4.pr_ExecuteProc(res);
            int flds = sh4.pr_FieldCount(res, 2);
            int count = sh4.pr_EOF(res, 2);
            int count2 = sh4.pr_EOF(res, 1);
            while (sh4.pr_EOF(res, 2) != 1)
            {
                remnants remnants = new remnants();
                dynamic RID = sh4.pr_ValByName(res, 2, "210.1.9");
                remnants.RID = (int)RID;
                dynamic Name = sh4.pr_ValByName(res, 2, "210.2.9").ToString();
                remnants.name = (string)Name;
                remnants.measurements = sh4.pr_ValByName(res, 2, "206.2.9").ToString();
                remnants.quantity = sh4.pr_ValByName(res, 2, "0.2.0");
                dynamic Vat = sh4.pr_ValByName(res, 2, "0.4.0");
                remnants.vat = (double)Vat;
                remnantsList.Add(remnants);
                sh4.pr_Next(res, 2);
            }
            sh4.pr_CloseProc(res);
        }
        public void GetGoods(int? id)
        {
            GetGoodsTree(id);

            if (id == null)
            {
                for (int i = 0; i < GoodsTree.Count; i++)
                {
                    int res = sh4.Goods(GoodsTree[i].ID);

                    if (res >= 0)
                    {
                        while (sh4.EOF(res) != 1)
                        {
                            Good g = new Good();
                            g.id = sh4.ValByName(res, "1.210.1.0");
                            g.GroupID = sh4.ValByName(res, "1.209.1.0");
                            g.Name = sh4.ValByName(res, "1.210.2.0");
                            g.CodePrefix = sh4.ValByName(res, "1.210.3.0");
                            g.CodeNumber = sh4.ValByName(res, "1.210.4.0");
                            g.Unit = sh4.ValByName(res, "1.206.2.0");
                            g.ComplectID = sh4.ValByName(res, "1.200.1.1");
                            g.ComplectName = sh4.ValByName(res, "1.200.2.1");

                            Goods.Add(g);
                            sh4.Next(res);
                        }

                        sh4.CloseQuery(res);
                    }
                }
            }
            else
            {
                int res = sh4.Goods((int)id);

                if (res >= 0)
                {
                    while (sh4.EOF(res) != 1)
                    {
                        Good g = new Good();
                        g.id = sh4.ValByName(res, "1.210.1.0");
                        g.GroupID = sh4.ValByName(res, "1.209.1.0");
                        g.Name = sh4.ValByName(res, "1.210.2.0");
                        g.CodePrefix = sh4.ValByName(res, "1.210.3.0");
                        g.CodeNumber = sh4.ValByName(res, "1.210.4.0");
                        g.Unit = sh4.ValByName(res, "1.206.2.0");
                        g.ComplectID = sh4.ValByName(res, "1.200.1.1");
                        g.ComplectName = sh4.ValByName(res, "1.200.2.1");

                        Goods.Add(g);
                        sh4.Next(res);
                    }

                    sh4.CloseQuery(res);
                }
            }

            for (int i = 0; i < Goods.Count; i++)
            {
                Goods[i].GroupName = GoodsTree.FirstOrDefault(p => p.ID == Goods[i].GroupID).Name;
            }
        }
        public void GetBalances(int? GroupID, int? GoodID)
        {
            if (GroupID == null && GoodID == null)
            {
                GetGoods(null);

                //Остатки
                for (int i = 0; i < Goods.Count; i++)
                {
                    DateTime dtStart = DateTime.Parse("01." + DateTime.Today.AddMonths(-1).Month + "." + DateTime.Today.AddMonths(-1).Year);
                    DateTime dtEnd = DateTime.Parse(DateTime.DaysInMonth(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month) + "." + DateTime.Today.AddMonths(-1).Month + "." + DateTime.Today.AddMonths(-1).Year);
                    int res = sh4.GsFifo(Goods[i].id, 0, dtStart.ToOADate(), dtEnd.ToOADate());

                    if (res >= 0)
                    {
                        while (sh4.EOF(res) != 1)
                        {
                            if (sh4.ValByName(res, "1.105.1.1").GetType() == typeof(DBNull))
                            {
                                GoodsBalance gb = new GoodsBalance();
                                gb.good = Goods[i];
                                gb.balance = sh4.ValByName(res, "1.105.3.0");
                                gb.cost_bal = sh4.ValByName(res, "1.105.4.0");
                                GoodsBalances.Add(gb);
                                break;
                            }

                            sh4.Next(res);
                        }

                        sh4.CloseQuery(res);
                    }
                }
            }
            else
                if (GroupID != null)
            {
                GetGoods(GroupID);

                for (int i = 0; i < Goods.Count; i++)
                {
                    DateTime dtStart = DateTime.Parse("01." + DateTime.Today.AddMonths(-1).Month + "." + DateTime.Today.AddMonths(-1).Year);
                    DateTime dtEnd = DateTime.Parse(DateTime.Today.Day + "." + DateTime.Today.Month + "." + DateTime.Today.Year);
                    int res = sh4.GsFifo(Goods[i].id, 0, dtStart.ToOADate(), dtEnd.ToOADate());

                    if (res >= 0)
                    {
                        while (sh4.EOF(res) != 1)
                        {
                            if (sh4.ValByName(res, "1.105.1.1").GetType() == typeof(DBNull))
                            {
                                GoodsBalance gb = new GoodsBalance();
                                gb.good = Goods[i];
                                gb.balance = sh4.ValByName(res, "1.105.3.0");

                                dynamic sum = sh4.ValByName(res, "2.105.4.0");

                                dynamic NDS = sh4.ValByName(res, "2.105.5.0");
                                dynamic NSP = sh4.ValByName(res, "2.105.6.0");

                                gb.cost_bal = 0;

                                if (NDS != DBNull.Value)
                                    gb.NDS = (double)NDS;

                                if (NSP != DBNull.Value)
                                    gb.NSP = (double)NSP;

                                GoodsBalances.Add(gb);
                                break;
                            }

                            sh4.Next(res);
                        }

                        sh4.CloseQuery(res);
                    }
                }
            }
            else
            {
                GetGoodsTree(null);
                int res = sh4.GoodByRID((int)GoodID);

                if (res >= 0)
                {
                    Good g = new Good();

                    while (sh4.EOF(res) != 1)
                    {
                        g.id = sh4.ValByName(res, "1.210.1.0");
                        g.GroupID = sh4.ValByName(res, "1.209.1.0");
                        g.Name = sh4.ValByName(res, "1.210.2.0");
                        g.CodePrefix = sh4.ValByName(res, "1.210.3.0");
                        g.CodeNumber = sh4.ValByName(res, "1.210.4.0");
                        g.Unit = sh4.ValByName(res, "1.206.2.0");
                        g.ComplectID = sh4.ValByName(res, "1.200.1.1");
                        g.ComplectName = sh4.ValByName(res, "1.200.2.1");
                        g.GroupName = GoodsTree.FirstOrDefault(p => p.ID == g.GroupID).Name;

                        Goods.Add(g);
                        sh4.Next(res);
                    }

                    sh4.CloseQuery(res);

                    res = sh4.GsFifo((int)GoodID, 0, DateTime.Today.ToOADate(), DateTime.Today.ToOADate());

                    if (res >= 0)
                    {
                        while (sh4.EOF(res) != 1)
                        {
                            if (sh4.ValByName(res, "1.105.1.1").GetType() == typeof(DBNull))
                            {
                                GoodsBalance gb = new GoodsBalance();
                                gb.good = g;
                                gb.balance = sh4.ValByName(res, "1.105.3.0");
                                dynamic sum = sh4.ValByName(res, "1.105.4.0");

                                if (gb.balance > 0)
                                    gb.cost_bal = (double)sum / (double)gb.balance;
                                else
                                    gb.cost_bal = 0;

                                GoodsBalances.Add(gb);
                                break;
                            }

                            sh4.Next(res);
                        }

                        sh4.CloseQuery(res);
                    }
                }
            }

            if (GoodsBalances.Count > 0)
            {
                DateTime startdate = DateTime.Parse("01." + DateTime.Today.AddMonths(-1).Month + "." + DateTime.Today.AddMonths(-1).Year);
                DateTime enddate = DateTime.Parse(DateTime.Today.Day + "." + DateTime.Today.Month + "." + DateTime.Today.Year);

                //Себестоимость
                for (int i = 0; i < GoodsBalances.Count; i++)
                {
                    GoodsBalances[i].cost = GetCost(GoodsBalances[i].good.id, startdate, enddate);
                }
            }
        }
        public double GetCost(int GoodsID, DateTime startdate, DateTime enddate)
        {
            dynamic sum = 0;
            dynamic cnt = 0;
            int res = sh4.GsFifo(GoodsID, 0, startdate.ToOADate(), enddate.ToOADate());

            if (res >= 0)
            {
                while (sh4.EOF(res) != 1)
                {
                    if (sh4.ValByName(res, "2.103.10.1").GetType() != typeof(DBNull))
                        if (sh4.ValByName(res, "2.103.10.1") == 12 || sh4.ValByName(res, "2.103.10.1") == 12)
                        {
                            sum += sh4.ValByName(res, "2.105.4.0");
                            cnt += sh4.ValByName(res, "2.105.3.0");
                        }

                    sh4.Next(res);
                }

                sh4.CloseQuery(res);
            }

            if (cnt != 0)
                return (double)sum / (double)cnt;
            else
                return 0;
        }
        public void createVendor()
        {
            vendor = new bd_vendor();
        }
        public void getList()
        {
            vendorList = list.bd_vendor.ToList();
            IEnumerable<SelectListItem> catList =
                from c in vendorList
                select new SelectListItem
                {
                    Text = c.name,
                    Value = c.id.ToString()
                };
            catListFull = catList;
            foreach (var it in catListFull)
            {
                if (it.Value == this.items.FirstOrDefault(m => m.bd_vendor.name == it.Text).id.ToString())
                {
                    it.Selected = true;
                }
            }
        }
        public void sendMail(string to, string body, string subject)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                MailMessage mail = new MailMessage();
                string FROM = "noreply@tokyo-bar.ru";
                mail.Body = body;
                mail.From = new MailAddress(FROM);
                mail.To.Add(new MailAddress(to));
                mail.Subject = subject;
                smtp.Host = "srv-ex00.fg.local";
                smtp.Port = 2525;
                smtp.EnableSsl = false;
                smtp.Credentials = new NetworkCredential("noreply", "123456zZ");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(mail);
                mail.Dispose();
            }
        }
        public void getNomenclatures()
        {
            items = list.bd_nomenclature.Where(p => p.state == 1).OrderBy(m => m.vendor_id).ToList();
            nomeclature_states = list.bd_nomenclature_state.ToList();
            categ = list.bd_category.ToList();
            maesurements = list.bd_measurement.ToList();
            remnantsSQL = list.bd_rests.ToList();
            Categories = new List<categs>();
            foreach (var it in categ)
            {
                var p = new categs();
                p.id = it.id;
                p.name = it.name;
                Categories.Add(p);
            }
            if (Categories.Count() > 0 && categoryName != null && categoryName != "")
            {
                categoryName = Categories[0].name;
                categoryId = Categories[0].id;
            }
            vendors = new List<ZakupModel.vendors1>();
            foreach (var it in vendorList)
            {
                var p = new vendors1();
                p.id = it.id;
                p.name = it.name;
                vendors.Add(p);
            }
        }
        public IEnumerable<SelectListItem> CategoriesSelectList
        {
            get
            {
                List<SelectListItem> Orgs = new List<SelectListItem>();

                for (int i = 0; i < Categories.Count; i++)
                {
                    Orgs.Add(new SelectListItem() { Text = Categories[i].name, Value = Categories[i].id.ToString() });
                }

                SelectListItem sli = Orgs.FirstOrDefault(p => p.Text == categoryName);

                if (sli != null)
                    sli.Selected = true;

                return Orgs;
            }
        }
        public IEnumerable<SelectListItem> MeasurementSelectList
        {
            get
            {
                List<SelectListItem> meas = new List<SelectListItem>();
                for (int i = 0; i < maesurements.Count; i++)
                {
                    meas.Add(new SelectListItem() { Text = maesurements[i].name, Value = maesurements[i].id.ToString() });
                }
                SelectListItem sli = meas.FirstOrDefault(m => m.Text == measurementName);
                if (sli != null)
                {
                    sli.Selected = true;
                }
                return meas;
            }
        }
        public IEnumerable<SelectListItem> vendorsSelectList
        {
            get
            {
                List<SelectListItem> vends = new List<SelectListItem>();

                for (int i = 0; i < vendors.Count; i++)
                {
                    vends.Add(new SelectListItem() { Text = vendors[i].name, Value = vendors[i].id.ToString() });
                }
                SelectListItem sli = vends.FirstOrDefault(p => p.Text == vendorName);

                if (sli != null)
                    sli.Selected = true;

                return vends;
            }
        }
        public void getOrders()
        {
            orders = list.bd_order.ToList();
            states = list.bd_states.ToList();
        }
        public void getMeasurements()
        {
            maesurements = list.bd_measurement.ToList();
        }
        public IEnumerable<SelectListItem> stateSelectList
        {
            get
            {
                List<SelectListItem> states1 = new List<SelectListItem>();

                for (int i = 0; i < states.Count; i++)
                {
                    states1.Add(new SelectListItem() { Text = states[i].name, Value = states[i].id.ToString() });
                }
                SelectListItem sli = states1.FirstOrDefault(p => p.Text == stateName);

                if (sli != null)
                    sli.Selected = true;

                return states1;
            }
        }
        public IEnumerable<SelectListItem> nomenclature_states_SelectList
        {
            get
            {
                List<SelectListItem> states = new List<SelectListItem>();
                for(int i = 0; i < nomeclature_states.Count; i++)
                {
                    states.Add(new SelectListItem() { Text = nomeclature_states[i].name, Value = nomeclature_states[i].id.ToString() });
                }
                SelectListItem sli = states.FirstOrDefault(p => p.Text == nomenclature_state_name);
                if (sli != null)
                    sli.Selected = true;
                return states;
            }
        }
        public void getVendors()
        {
            vendorList = list.bd_vendor.ToList();
        }
        public void getUsers()
        {
            usersList = list.bd_employee.ToList();
            restaurantsList = list.bd_subdivision.ToList();
            organizationList = list.bd_organization.ToList();
        }
        public IEnumerable<SelectListItem> organizationSelectList
        {
            get
            {
                List<SelectListItem> orgs = new List<SelectListItem>();
                foreach (var it in organizationList)
                {
                    orgs.Add(new SelectListItem() { Text = it.name, Value = it.id.ToString() });
                }
                SelectListItem sli = orgs.FirstOrDefault(m => m.Text == organizationName);
                if (sli != null)
                    sli.Selected = true;
                return orgs;
            }
        }
        //старый метод для сохранения номенклатур, сейчас не используется
        public void PostNom()
        {
            foreach (var it in items)
            {
                if (list.bd_nomenclature.ToList().Where(c => c.id == it.id).Count() == 0)
                {
                    list.bd_nomenclature.Add(new bd_nomenclature { id = it.id, category_id = it.category_id, name = it.name, measurement_id = it.measurement_id, Price = it.Price, vendor_id = it.vendor_id });
                }
                else
                {
                    list.bd_nomenclature.FirstOrDefault(m => m.id == it.id).Price = it.Price;
                    list.bd_nomenclature.FirstOrDefault(m => m.id == it.id).vendor_id = it.vendor_id;
                    list.bd_nomenclature.FirstOrDefault(m => m.id == it.id).category_id = it.category_id;
                    list.bd_nomenclature.FirstOrDefault(m => m.id == it.id).measurement_id = it.measurement_id;
                }
            }
            list.SaveChanges();
        }
        //старый метод для сохранения поставщиков, сейчас не используется
        public void PostVen()
        {
            foreach (var it in vendorList)
            {
                if (list.bd_vendor.ToList().Where(c => c.id == it.id).Count() == 0)
                {
                    list.bd_vendor.Add(new bd_vendor { id = it.id, code = it.code, INN = it.INN, KPP = it.KPP, name = it.name, phones = it.phones });
                }
                else
                {
                    list.bd_vendor.FirstOrDefault(m => m.id == it.id).name = it.name;
                    list.bd_vendor.FirstOrDefault(m => m.id == it.id).INN = it.INN;
                    list.bd_vendor.FirstOrDefault(m => m.id == it.id).KPP = it.KPP;
                    list.bd_vendor.FirstOrDefault(m => m.id == it.id).phones = it.phones;
                }
            }
            list.SaveChanges();
        }
        //метод для создания заказов от товароведа
        public void sendOrder(string username)
        {
            foreach (var it in items)
            {
                if (it.Count != null)
                {
                    list.bd_order.Add(new bd_order { count = it.Count, category_id = it.category_id, date = DateTime.Today, id = orders.Count + 1, measurement_id = it.measurement_id, nomenclature_id = it.id, organization_id = 1, employee = username, state = 1 });
                }
            }
            list.SaveChanges();
        }
        //мето для сохранения списка ресторанов с организациями
        public void postOrganizations()
        {
            foreach (var it in restaurantsList)
            {
                list.bd_subdivision.FirstOrDefault(m => m.id == it.id).name = it.name;
                list.bd_subdivision.FirstOrDefault(m => m.id == it.id).organization_id = it.organization_id;
                list.bd_subdivision.FirstOrDefault(m => m.id == it.id).address = it.address;
            }
            list.SaveChanges();
        }
        //старый метод для сохранения списка заказов, сейчас не используется
        public void sendOrderList()
        {
            foreach (var it in orders)
            {
                if (it.state != null)
                {
                    list.bd_order.FirstOrDefault(m => m.id == it.id).state = it.state;
                }
                if (it.comment != null)
                {
                    list.bd_order.FirstOrDefault(m => m.id == it.id).comment = it.comment;
                }
                list.bd_order.FirstOrDefault(m => m.id == it.id).date_end = it.date_end;
                list.bd_order.FirstOrDefault(m => m.id == it.id).comment = it.comment;
            }
            list.SaveChanges();
        }
        //метод для сохранения списка заказов ajax
        public bool SaveOrders(string JSONString)
        {
            var serializer = new JavaScriptSerializer();
            var heapdata = serializer.DeserializeObject(JSONString);
            List<order> listOrders = new List<order>();
            foreach (var undata in (Array)heapdata)
            {
                var r = (Dictionary<string, object>)undata;

                object id = null;
                r.TryGetValue("id", out id);
                object date_start = null;
                r.TryGetValue("date_from", out date_start);
                object date_end = null;
                r.TryGetValue("date_end", out date_end);
                object state = null;
                r.TryGetValue("state", out state);
                object comment = null;
                r.TryGetValue("comment", out comment);
                object count = null;
                r.TryGetValue("count", out count);
                object measurement = null;
                r.TryGetValue("measurement_name", out measurement);

                order order = new order();
                order.id = Int32.Parse(id.ToString());
                order.date_start = DateTime.Parse(date_start.ToString());
                if (date_end.ToString() == "")
                {
                    order.date_end = null;
                }
                else
                {
                    order.date_end = DateTime.Parse(date_end.ToString());
                }
                order.comment = comment.ToString();
                order.count = Convert.ToDecimal(count.ToString());
                order.state_id = states.FirstOrDefault(m => m.name == state.ToString()).id;
                order.measurement_id = maesurements.FirstOrDefault(m => m.name == measurement.ToString()).id;
                listOrders.Add(order);
            }
            foreach (var p in listOrders)
            {
                if (orders.FirstOrDefault(m => m.id == p.id).state != p.state_id || orders.FirstOrDefault(m => m.id == p.id).comment != p.comment || orders.FirstOrDefault(m => m.id == p.id).date_end != p.date_end || orders.FirstOrDefault(m => m.id == p.id).count != p.count || orders.FirstOrDefault(m => m.id == p.id).measurement_id != p.measurement_id)
                {
                    list.bd_order.FirstOrDefault(m => m.id == p.id).state = p.state_id;
                    list.bd_order.FirstOrDefault(m => m.id == p.id).comment = p.comment;
                    list.bd_order.FirstOrDefault(m => m.id == p.id).date_end = p.date_end;
                    list.bd_order.FirstOrDefault(m => m.id == p.id).date = p.date_start;
                    list.bd_order.FirstOrDefault(m => m.id == p.id).count = p.count;
                    list.bd_order.FirstOrDefault(m => m.id == p.id).measurement_id = p.measurement_id;
                }
            }
            list.SaveChanges();
            return true;
        }
        //метод для сохранения списка номенклатур ajax
        public bool SaveDocument(string JSONString)
        {
            var serializer = new JavaScriptSerializer();
            var heapdata = serializer.DeserializeObject(JSONString);
            List<items1> listItems = new List<items1>();
            foreach (var undata in (Array)heapdata)
            {
                var r = (Dictionary<string, object>)undata;

                object id = null;
                r.TryGetValue("id", out id);
                object name = null;
                r.TryGetValue("name", out name);
                object vendor_name = null;
                r.TryGetValue("vendor_name", out vendor_name);
                object category_name = null;
                r.TryGetValue("category_name", out category_name);
                object measurement_name = null;
                r.TryGetValue("measurement_name", out measurement_name);
                object price = null;
                r.TryGetValue("price", out price);
                object state_name = null;
                r.TryGetValue("state", out state_name);

                items1 item = new items1();
                item.id = Int32.Parse(id.ToString());
                item.name = name.ToString();
                item.price = Decimal.Parse(price.ToString());
                item.vendor_name = vendor_name.ToString();
                item.vendor_id = vendorList.FirstOrDefault(m => m.name == item.vendor_name).id;
                item.measurement_name = measurement_name.ToString();
                item.measurement_id = maesurements.FirstOrDefault(m => m.name == item.measurement_name).id;
                item.category_name = category_name.ToString();
                item.category_id = categ.FirstOrDefault(m => m.name == item.category_name).id;
                item.state_id = nomeclature_states.FirstOrDefault(m => m.name == state_name.ToString()).id;
                listItems.Add(item);
            }
            foreach (var p in listItems)
            {
                if (list.bd_nomenclature.Where(m => m.id == p.id).Count() != 0)
                {
                    if(items.Where(m => m.id == p.id).Count() != 0 )
                    {
                        if (items.FirstOrDefault(m => m.id == p.id).name != p.name || items.FirstOrDefault(m => m.id == p.id).bd_vendor.name != p.vendor_name || items.FirstOrDefault(m => m.id == p.id).Price != p.price || items.FirstOrDefault(m => m.id == p.id).category_id != p.category_id || items.FirstOrDefault(m => m.id == p.id).measurement_id != p.measurement_id || items.FirstOrDefault(m => m.id == p.id).state != p.state_id)
                        {
                            list.bd_nomenclature.FirstOrDefault(m => m.id == p.id).name = p.name;
                            list.bd_nomenclature.FirstOrDefault(m => m.id == p.id).Price = p.price;
                            list.bd_nomenclature.FirstOrDefault(m => m.id == p.id).vendor_id = p.vendor_id;
                            list.bd_nomenclature.FirstOrDefault(m => m.id == p.id).measurement_id = p.measurement_id;
                            list.bd_nomenclature.FirstOrDefault(m => m.id == p.id).category_id = p.category_id;
                            list.bd_nomenclature.FirstOrDefault(m => m.id == p.id).state = p.state_id;
                        }
                    }
                }
                else
                {
                    list.bd_nomenclature.Add(new bd_nomenclature { name = p.name, vendor_id = p.vendor_id, Price = p.price, id = p.id, category_id = p.category_id, measurement_id = p.measurement_id, state = p.state_id });
                }
            }
            list.SaveChanges();
            return true;
        }
        public void getReclamations()
        {
            reclamations = list.bd_reclamation.ToList();
            reclamation_problems = list.bd_reclamation_problems.ToList();
            reclamation_files = list.bd_reclamation_files.ToList();
            states = list.bd_states.ToList();
            /*foreach(var it in reclamations)
            {
                ReclamationDataWebix += "{id: \"" + it.id + "\", Details: \"" + it.id + "\", Date: \"" + it.date.ToShortDateString() + "\", Restaurant: \"" + it.bd_subdivision.name + "\", Vendor: \"" + it.bd_vendor.name.Trim().Replace('"', '\'') + "\", Nomenclature: \"" + it.bd_nomenclature.name.Trim().Replace('"', '\'') + "\", Problem: \"" + it.bd_reclamation_problems.problem.Trim().Replace('"', '\'') + "\", State: \"" + it.bd_states.name + "\", Word: \"" + it.id + "\"";
                ReclamationDataWebix += "}, \r\n";
            }*/
        }
        public IEnumerable<SelectListItem> reclamation_problemsSelectList
        {
            get
            {
                List<SelectListItem> problems = new List<SelectListItem>();
                foreach (var it in reclamation_problems)
                {
                    problems.Add(new SelectListItem() { Text = it.problem, Value = it.id.ToString() });
                }
                SelectListItem sli = problems.FirstOrDefault(m => m.Text == reclamation_problemName);
                if (sli != null)
                    sli.Selected = true;
                return problems;
            }
        }
        public IEnumerable<SelectListItem> restaurantsSelectList
        {
            get
            {
                List<SelectListItem> rests = new List<SelectListItem>();
                foreach (var it in restaurantsList)
                {
                    rests.Add(new SelectListItem() { Text = it.name, Value = it.id.ToString() });
                }
                SelectListItem sli = rests.FirstOrDefault(m => m.Text == restaurantName);
                if (sli != null)
                    sli.Selected = true;
                return rests;
            }
        }
        public IEnumerable<SelectListItem> nomenclaturesSelectList
        {
            get
            {
                List<SelectListItem> noms = new List<SelectListItem>();
                foreach (var it in items)
                {
                    noms.Add(new SelectListItem() { Text = it.name, Value = it.id.ToString() });
                }
                SelectListItem sli = noms.FirstOrDefault(m => m.Text == nomenclatureName);
                if (sli != null)
                    sli.Selected = true;
                return noms;
            }
        }
        public void createReclamation()
        {
            reclamation_item = new bd_reclamation();
        }
        public void saveReclamations()
        {
            foreach (var it in reclamations)
            {
                if (list.bd_reclamation.FirstOrDefault(m => m.id == it.id).solution != it.solution || list.bd_reclamation.FirstOrDefault(m => m.id == it.id).comment != it.comment || list.bd_reclamation.FirstOrDefault(m => m.id == it.id).state_id != it.state_id)
                {
                    list.bd_reclamation.FirstOrDefault(m => m.id == it.id).solution = it.solution;
                    list.bd_reclamation.FirstOrDefault(m => m.id == it.id).comment = it.comment;
                    list.bd_reclamation.FirstOrDefault(m => m.id == it.id).state_id = it.state_id;
                }
                list.SaveChanges();
            }
        }
        public void sendReclamation(List<HttpPostedFileBase> files)
        {
            string fileName = null;
            int reclamation_id = 0;
            if(reclamation_item.id != 0)
            {
                reclamation_id = reclamation_item.id;
                list.bd_reclamation.FirstOrDefault(m => m.id == reclamation_id).solution = reclamation_item.solution;
                list.bd_reclamation.FirstOrDefault(m => m.id == reclamation_id).comment = reclamation_item.comment;
                if(list.bd_reclamation.FirstOrDefault(m => m.id == reclamation_item.id).state_id != reclamation_item.state_id)
                {
                    list.bd_reclamation.FirstOrDefault(m => m.id == reclamation_id).state_id = reclamation_item.state_id;
                    string body = string.Format("У рекламации за {0} изменился статус на {1}, поставщик - {2}",reclamation_item.date.ToShortDateString() ,states.FirstOrDefault(m => m.id == reclamation_item.state_id).name, vendorList.FirstOrDefault(m => m.id == reclamation_item.vendor_id).name);
                    string subject = "Изменение статуса рекламации";
                    foreach (var it in usersList.Where(m => m.subdiv_id == reclamation_item.restaurant_id))
                    {
                        string to = string.Format("{0}@tokyo-bar.ru", it.domain_login);
                        sendMail(to, body, subject);
                    }
                }
            }
            else
            {
                    list.bd_reclamation.Add(new bd_reclamation { date = reclamation_item.date, problem_id = reclamation_item.problem_id, restaurant_id = usersList.FirstOrDefault(m => m.domain_login == username).bd_subdivision.id, nomenclature_id = reclamation_item.nomenclature_id, vendor_id = reclamation_item.vendor_id, comment = reclamation_item.comment, state_id = 1 });
                    string to = "reclamations@tokyo-bar.ru";
                    string body = String.Format("Пришла новая рекламация от {0} \r\nПоставщик - {1} \r\nРесторан - {2} \r\nПричина - {3}.", reclamation_item.date.Date, vendorList.FirstOrDefault(m => m.id == reclamation_item.vendor_id).name, usersList.FirstOrDefault(m => m.domain_login == username).bd_subdivision.name, reclamation_problems.FirstOrDefault(m => m.id == reclamation_item.problem_id).problem);
                    string subject = "Новая рекламация";
                sendMail(to, body, subject);
            }
            list.SaveChanges();
            if (files.Any())
            {
                foreach(var it in files)
                {
                    if(!string.IsNullOrEmpty(it.FileName))
                    {
                        int id = list.bd_reclamation.ToList().Last().id;
                        fileName = System.IO.Path.GetFileName(it.FileName);
                        it.SaveAs(HttpContext.Current.Server.MapPath("~/Files/" + fileName));
                        list.bd_reclamation_files.Add(new bd_reclamation_files { reclamation_id = id, file = fileName });
                    }
                }
            }
            list.SaveChanges();
        }
        public void getRemnants(int restaurant_id)
        {
            remnants_overall = new List<restaurants_and_remnants>();
            foreach (var it in restaurantsList.Where(m => m.id == restaurant_id))
            {
                restaurants_and_remnants rest = new restaurants_and_remnants();
                rest.remnants = new List<remnants_for_noms>();
                rest.restaurant_id = it.id;
                rest.restaurant_name = it.name;
                foreach (var g in items)
                {
                    remnants_for_noms remnants_by_nom = new remnants_for_noms();
                    remnants_by_nom.nomenclature_id = g.id;
                    remnants_by_nom.nomenclature_name = g.name;
                    if (remnantsSQL.Where(j => j.nomenclature_id == g.id && j.restaurant_id == it.id).Count() == 0)
                    {
                        remnants_by_nom.count = 0;
                    }
                    else
                    {
                        if (remnantsSQL.FirstOrDefault(h => h.nomenclature_id == g.id && h.restaurant_id == it.id).count == null)
                        {
                            remnants_by_nom.count = 0;
                        }
                        else
                        {
                            remnants_by_nom.count = remnantsSQL.FirstOrDefault(h => h.nomenclature_id == g.id && h.restaurant_id == it.id).count;
                        }
                    }
                    rest.remnants.Add(remnants_by_nom);
                }
                remnants_overall.Add(rest);
            }
            remnants_overall.Count();
        }
        public void saveRemnants(int restaurant_id)
        {
            foreach(var it in remnants_overall[0].remnants)
            {
                if(list.bd_rests.Where(m => m.restaurant_id == restaurant_id && m.nomenclature_id == it.nomenclature_id).Count() == 0)
                {
                    list.bd_rests.Add(new bd_rests { nomenclature_id = it.nomenclature_id, restaurant_id = restaurant_id, count = it.count });
                }
                else
                {
                    list.bd_rests.FirstOrDefault(m => m.nomenclature_id == it.nomenclature_id && m.restaurant_id == restaurant_id).count = it.count;
                }
            }
            list.SaveChanges();
        }
        public int getLastNomenclature()
        {
            int id = list.bd_nomenclature.ToList().Last().id;
            return id;
        }
    }
}