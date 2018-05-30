using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Fusion.Models.Fabrika
{
    public class TSPModels
    {
        private FabrikaEntities db = new FabrikaEntities();
        public List<Request> RequestList = new List<Request>();
        public List<Nomenclature> Products = new List<Nomenclature>();
        public List<Restaurant> Restaurants = new List<Restaurant>();
        public List<Category> Categories = new List<Category>();
        public Request TSPRequest = new Request();
		public List<RestaurantAccess> Accesses = new List<RestaurantAccess>();
        public RestaurantAccess Access { get; set; }
        public Restaurant Restaurant { get; set; }
        public string RestaurantName { get; set; }
		
        public TSPModels()
        {
            
        }

        public TSPModels(int id, string UserName)
        {
            var request = db.Request.FirstOrDefault(p => p.Id == id);

            if (request != null)
            {
                var restaurants = db.RestaurantAccess.Where(p => p.UserName == UserName).ToList();

                foreach (var r in restaurants)
                    Restaurants.Add(r.Restaurant);

                Categories = db.Category.ToList();
                Products = db.Nomenclature.ToList();
                TSPRequest = request;
            }
        }

        public void AddRequest(string JSONData)
        {
            var request = db.Request.FirstOrDefault(p => p.RequestDate == DateTime.Today);

            if (request == null)
                request = new Request() { Deleted = false, RequestDate = DateTime.Today };

            db.SaveChanges();
        }

        public void GetList()
        {
            RequestList = db.Request.ToList();
        }

        public void Initialize(string UserName)
        {
            TSPRequest = db.Request.FirstOrDefault(p => p.RequestDate == DateTime.Today);

            if (TSPRequest == null)
            {
                TSPRequest = db.Request.Add(new Request() { Deleted = false, RequestDate = DateTime.Today });
                db.SaveChanges();
            }

            var restaurants = db.RestaurantAccess.Where(p => p.UserName == UserName).ToList();

            foreach (var r in restaurants)
                Restaurants.Add(r.Restaurant);

            Categories = db.Category.ToList();
            Products = db.Nomenclature.ToList();
        }

        public void SaveNomenclature(string JSONData)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                var heapdata = serializer.DeserializeObject(JSONData);

                foreach (var undata in (Array)heapdata)
                {
                    var r = (Dictionary<string, object>)undata;

                    object productId = null;
                    r.TryGetValue("ProductId", out productId);
                    object categoryID = null;
                    r.TryGetValue("CategoryID", out categoryID);
                    object name = null;
                    r.TryGetValue("Name", out name);
                    object measurement = null;
                    r.TryGetValue("Measurement", out measurement);
                    object active = null;
                    r.TryGetValue("Active", out active);

                    int catId = Convert.ToInt32(categoryID);

                    if (productId != null)
                    {
                        int prodId;
                        Int32.TryParse(productId.ToString(), out prodId);

                        if (prodId != 0)
                        {
                            var product = db.Nomenclature.FirstOrDefault(p => p.Id == prodId);
                            product.Active = Convert.ToBoolean(active);
                        }
                    }
                    else
                    {
                        db.Nomenclature.Add(new Nomenclature() { Active = true, CategoryID = catId, Created = DateTime.Now, Measurement = measurement.ToString().Trim(), Name = name.ToString() });
                    }
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(string JSON, string UserName)
        {
            try
            {
                List<int> rests = new List<int>();
                var serializer = new JavaScriptSerializer();
                var heapdata = serializer.DeserializeObject(JSON);

                Request request = null;

                foreach (var undata in (Array)heapdata)
                {
                    var r = (Dictionary<string, object>)undata;

                    object RequestID = null;
                    r.TryGetValue("RequestID", out RequestID);
                    object ProductId = null;
                    r.TryGetValue("ProductId", out ProductId);

                    int reqId = Convert.ToInt32(RequestID);

                    if (request == null)
                        request = db.Request.FirstOrDefault(p => p.Id == reqId);

                    foreach (var el in r)
                    {
                        if (el.Key.StartsWith("_r"))
                        {
                            int productId = Convert.ToInt32(ProductId);
                            int restId = Convert.ToInt32(el.Key.Substring(2));

                            if (!rests.Contains(restId))
                            {
                                rests.Add(restId);
                                List<RequestDetail> rds = db.RequestDetail.Where(p => p.RequestID == reqId && p.RestaurantID == restId).ToList();

                                if(rds != null)
                                    db.RequestDetail.RemoveRange(rds);
                            }

                            if (el.Value.ToString() != "")
                            {
                                db.RequestDetail.Add(new RequestDetail()
                                {
                                    Active = true,
                                    ProductID = productId,
                                    Quantity = Convert.ToDecimal(el.Value),
                                    RequestID = request.Id,
                                    RestaurantID = restId,
                                    UserName = UserName
                                });
                            }
                        }
                    }
                }

                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void GetNomenclature()
        {
            Categories = db.Category.ToList();
        }
        public void GetRestaurants()
        {
            Restaurants = db.Restaurant.ToList();
        }
        public void GetAccesses()
        {
            Accesses = db.RestaurantAccess.ToList();
        }
        public void EditAccess(int? id)
        {
            if(id == null)
            {
                Access = new RestaurantAccess();
            }
            else
            {
                Access = db.RestaurantAccess.FirstOrDefault(m => m.Id == id);
            }
        }
        public void EditRestaurant(int? id)
        {
            if (id == null)
            {
                Restaurant = new Restaurant();
            }
            else
            {
                Restaurant = db.Restaurant.FirstOrDefault(m => m.Id == id);
            }
        }
        public void SaveAccess(RestaurantAccess Access)
        {
            if(db.RestaurantAccess.Where(m => m.Id == Access.Id).Any())
            {
                db.RestaurantAccess.FirstOrDefault(m => m.Id == Access.Id).UserName = Access.UserName;
                db.RestaurantAccess.FirstOrDefault(m => m.Id == Access.Id).RestaurantID = Access.RestaurantID;
            }
            else
            {
                db.RestaurantAccess.Add(Access);
            }
            db.SaveChanges();
        }
        public void SaveRestaurant(Restaurant Restaurant)
        {
            if(db.Restaurant.Where(m => m.Id == Restaurant.Id).Any())
            {
                db.Restaurant.FirstOrDefault(m => m.Id == Restaurant.Id).Name = Restaurant.Name;
            }
            else
            {
                db.Restaurant.Add(Restaurant);
            }
            db.SaveChanges();
        }
        public IEnumerable<SelectListItem> RestaurantsSelectList
        {
            get
            {
                List<SelectListItem> restaurants = new List<SelectListItem>();
                foreach (var it in Restaurants)
                {
                    restaurants.Add(new SelectListItem() { Text = it.Name, Value = it.Id.ToString() });
                }
                SelectListItem sli = restaurants.FirstOrDefault(p => p.Text == RestaurantName);
                if (sli != null)
                    sli.Selected = true;
                return restaurants;
            }
        }
    }
}