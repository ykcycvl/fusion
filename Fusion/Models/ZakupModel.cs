using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Reflection;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Jitbit.Utils;
using System.Web.Script.Serialization;
using Sh4Ole;
using System.Text;
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
        }
        public class order
        {
            public int id { get; set; }
            public DateTime date_start { get; set; }
            public DateTime? date_end { get; set; }
            public string comment { get; set; }
            public string state { get; set; }
            public int state_id { get; set; }
            public int count { get; set; }
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
        public List<Models.bd_nomenclature> items { get; set; }
        public List<Models.bd_measurement> maesurements { get; set; }
        public List<Models.bd_category> categ { get; set; }
        public List<Models.bd_employee> usersList { get; set; }
        public List<Models.bd_organization> organizationList { get; set; }
        public List<Models.bd_subdivision> restaurantsList { get; set; }
        public List<ZakupModel.vendors1> vendors { get; set; }
        public List<bd_reclamation> reclamations { get; set; }
        public List<bd_reclamation_problems> reclamation_problems { get; set; }
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

        public void getNomenclatures()
        {
            items = list.bd_nomenclature.OrderBy(m => m.vendor_id).ToList();
            categ = list.bd_category.ToList();
            maesurements = list.bd_measurement.ToList();
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
                order.count = Int32.Parse(count.ToString());
                order.state_id = states.FirstOrDefault(m => m.name == state.ToString()).id;
                listOrders.Add(order);
            }
            foreach (var p in listOrders)
            {
                if (orders.FirstOrDefault(m => m.id == p.id).state != p.state_id || orders.FirstOrDefault(m => m.id == p.id).comment != p.comment || orders.FirstOrDefault(m => m.id == p.id).date_end != p.date_end || orders.FirstOrDefault(m => m.id == p.id).count != p.count)
                {
                    list.bd_order.FirstOrDefault(m => m.id == p.id).state = p.state_id;
                    list.bd_order.FirstOrDefault(m => m.id == p.id).comment = p.comment;
                    list.bd_order.FirstOrDefault(m => m.id == p.id).date_end = p.date_end;
                    list.bd_order.FirstOrDefault(m => m.id == p.id).date = p.date_start;
                    list.bd_order.FirstOrDefault(m => m.id == p.id).count = p.count;
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

                items1 item = new items1();
                item.id = Int32.Parse(id.ToString());
                item.name = name.ToString();
                item.price = Int32.Parse(price.ToString());
                item.vendor_name = vendor_name.ToString();
                item.vendor_id = vendorList.FirstOrDefault(m => m.name == item.vendor_name).id;
                item.measurement_name = measurement_name.ToString();
                item.measurement_id = maesurements.FirstOrDefault(m => m.name == item.measurement_name).id;
                item.category_name = category_name.ToString();
                item.category_id = categ.FirstOrDefault(m => m.name == item.category_name).id;
                listItems.Add(item);
            }
            foreach (var p in listItems)
            {
                if (list.bd_nomenclature.Where(m => m.id == p.id).Count() != 0)
                {
                    if (items.FirstOrDefault(m => m.id == p.id).name != p.name || items.FirstOrDefault(m => m.id == p.id).bd_vendor.name != p.vendor_name || items.FirstOrDefault(m => m.id == p.id).Price != p.price || items.FirstOrDefault(m => m.id == p.id).category_id != p.category_id || items.FirstOrDefault(m => m.id == p.id).measurement_id != p.measurement_id)
                    {
                        list.bd_nomenclature.FirstOrDefault(m => m.id == p.id).name = p.name;
                        list.bd_nomenclature.FirstOrDefault(m => m.id == p.id).Price = p.price;
                        list.bd_nomenclature.FirstOrDefault(m => m.id == p.id).vendor_id = p.vendor_id;
                        list.bd_nomenclature.FirstOrDefault(m => m.id == p.id).measurement_id = p.measurement_id;
                        list.bd_nomenclature.FirstOrDefault(m => m.id == p.id).category_id = p.category_id;
                    }
                }
                else
                {
                    list.bd_nomenclature.Add(new bd_nomenclature { name = p.name, vendor_id = p.vendor_id, Price = p.price, id = p.id, category_id = p.category_id, measurement_id = p.measurement_id });
                }
            }
            list.SaveChanges();
            return true;
        }
        public void getReclamations()
        {
            reclamations = list.bd_reclamation.ToList();
            reclamation_problems = list.bd_reclamation_problems.ToList();
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
                if (list.bd_reclamation.FirstOrDefault(m => m.id == it.id).solution != it.solution)
                {
                    list.bd_reclamation.FirstOrDefault(m => m.id == it.id).solution = it.solution;
                }
                list.SaveChanges();
            }
        }
        public void sendReclamation()
        {
            list.bd_reclamation.Add(new bd_reclamation { date = reclamation_item.date, problem_id = reclamation_item.problem_id, restaurant_id = usersList.FirstOrDefault(m => m.domain_login == username).bd_subdivision.id, nomenclature_id = reclamation_item.nomenclature_id, vendor_id = reclamation_item.vendor_id });
            list.SaveChanges();
        }
    }
}