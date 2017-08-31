using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Reflection;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace Fusion.Models
{
    public class ZakupModel
    {
        public class vendors1
        {
           public string name { get; set; }
           public int id { get; set; }
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
            public string measurement { get; set; }
            public string category { get; set; }
            public int id { get; set; }
            public decimal? price { get; set; }
        }
        public List<items1> listItem { get; set; }
        public List<Models.bd_nomenclature> items { get; set; }
        public List<Models.bd_measurement> maesurements { get; set; }
        public List<Models.bd_category> categ { get; set; }
        public string categoryName { get; set; }
        public string CategoryName1 { get; set; }
        public int categoryId { get; set; }
        public string vendorName { get; set; }
        public string stateName { get; set; }
        public string measurementName { get; set; }
        public List<ZakupModel.vendors1> vendors { get; set; }
        public List<Models.bd_vendor> vendorList { get; set; }
        public Entities list = new Entities();
        public IEnumerable<SelectListItem> catListFull { get; set; }
        public IEnumerable<SelectListItem> statesSelect { get; set; }
        public List<Models.bd_order> orders { get; set; }
        public List<Models.bd_states> states { get; set; }
        [DataType(DataType.Date)]
        public DateTime Period { get; set; }
        public List<categs> Categories { get; set; }
        public string username { get; set; }
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
            items = list.bd_nomenclature.ToList();
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
        public void sendOrder( string username)
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
    }
}