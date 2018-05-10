using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public bool Save(string JSON, string UserName)
        {
            bool result = false;

            try
            {
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
                            if (el.Value.ToString() != "")
                                db.RequestDetail.Add(new RequestDetail()
                                {
                                    Active = true,
                                    ProductID = Convert.ToInt32(ProductId),
                                    Quantity = Convert.ToDecimal(el.Value),
                                    RequestID = request.Id,
                                    RestaurantID = Convert.ToInt32(el.Key.Substring(2)),
                                    UserName = UserName
                                });
                        }
                    }
                }

                db.SaveChanges();
                result = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}