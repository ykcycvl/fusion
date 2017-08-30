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
    public class SB
    {
        public List<Models.sb_restaurants> restaurants { get; set; }
        public List<Models.sb_problems> problems { get; set; }
        public List<Models.sb_managers> managers { get; set; }
        public List<Models.sb_rights> rights { get; set; }
        public SBEntities_F list = new SBEntities_F();
        
        public string RestaurantName { get; set; }
        public string username { get; set; }
        public string ManagerName { get; set; }
        public class rests
        {
            public string name;
            public int id;
        }
        public class manags
        {
            public string name;
            public int id;
        }
        public List<rests> RestaurantsList { get; set; }
        public List<manags> ManagersList { get; set; }
        public void getProblems()
        {
            problems = list.sb_problems.ToList();
            restaurants = list.sb_restaurants.ToList();
            managers = list.sb_managers.ToList();
            rights = list.sb_rights.ToList();
            RestaurantsList = new List<rests>();
            ManagersList = new List<manags>();
            foreach (var it in restaurants)
            {
                var p = new rests();
                p.id = it.id;
                p.name = it.restaurant_name;
                RestaurantsList.Add(p);
            }
            foreach (var it in managers)
            {
                var p = new manags();
                p.id = it.id;
                p.name = it.name;
                ManagersList.Add(p);
            }
            if (RestaurantsList.Count() > 0 && RestaurantName != null && RestaurantName != "")
            {
                RestaurantName = restaurants[0].restaurant_name;
            }
        }
        public IEnumerable<SelectListItem> RestaurantsSelectList
        {
            get
            {
                List<SelectListItem> Rests = new List<SelectListItem>();
                for (int i = 0; i < RestaurantsList.Count(); i++)
                {
                    Rests.Add(new SelectListItem() { Text = RestaurantsList[i].name, Value = RestaurantsList[i].id.ToString() });
                }
                SelectListItem sli = Rests.FirstOrDefault(m => m.Text == RestaurantName);
                if (sli != null)
                {
                    sli.Selected = true;
                }
                return Rests;
            }
        }
        public IEnumerable<SelectListItem> ManagersSelectList
        {
            get
            {
                List<SelectListItem> Manags = new List<SelectListItem>();
                for (int i = 0; i < ManagersList.Count(); i++)
                {
                    Manags.Add(new SelectListItem() { Text = ManagersList[i].name, Value = ManagersList[i].id.ToString() });
                }
                SelectListItem sli = Manags.FirstOrDefault(m => m.Text == ManagerName);
                if (sli != null)
                {
                    sli.Selected = true;
                }
                return Manags;
            }
        }
        public void sentProblem()
        {
            foreach (var it in problems)
            {
                if (list.sb_problems.ToList().Where(c => c.id == it.id).Count() == 0)
                {
                        list.sb_problems.Add(new sb_problems { date = it.date, problem = it.problem, restaurant_id = it.restaurant_id, solution = it.solution, id = it.id, mgr_id = it.sb_managers.id });
                }
                else if(LoginViewModel.IsMemberOf(username, "SB_User"))
                {
                    list.sb_problems.FirstOrDefault(m => m.id == it.id).solution = it.solution;
                }
                else
                {
                    list.sb_problems.FirstOrDefault(m => m.id == it.id).solution = it.solution;
                    list.sb_problems.FirstOrDefault(m => m.id == it.id).problem = it.problem;
                    list.sb_problems.FirstOrDefault(m => m.id == it.id).date = it.date;
                    list.sb_problems.FirstOrDefault(m => m.id == it.id).restaurant_id = it.restaurant_id;
                    list.sb_problems.FirstOrDefault(m => m.id == it.id).mgr_id = it.sb_managers.id;
                }
                list.SaveChanges();
            }
        }
    }
}