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
        public class Problem
        {
            public string problem { get; set; }
            public int id { get; set; }
            public string solution { get; set; }
            public DateTime date { get; set; }
            public int manager_id { get; set; }
            public int restaurant_id { get; set; }
        }
        public Problem problem { get; set; }
        public List<Models.sb_restaurants> restaurants { get; set; }
        public List<Models.sb_problems> problems { get; set; }
        public List<Models.sb_managers> managers { get; set; }
        public List<Models.sb_rights> rights { get; set; }
        public List<sb_top> problems_top { get; set; }
        public Entities list = new Entities();

        public string RestaurantName { get; set; }
        public string username { get; set; }
        public string ManagerName { get; set; }
        public DateTime Period { get; set; }
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

                if (LoginViewModel.IsMemberOf(username, "SB_User"))
                {
                    list.sb_problems.FirstOrDefault(m => m.id == it.id).solution = it.solution;
                }
                else
                {
                    if (Period.Year + Period.Month == it.date.Value.Month + it.date.Value.Year)
                    {
                        list.sb_problems.FirstOrDefault(m => m.id == it.id).solution = it.solution;
                        list.sb_problems.FirstOrDefault(m => m.id == it.id).problem = it.problem;
                        list.sb_problems.FirstOrDefault(m => m.id == it.id).date = it.date;
                        list.sb_problems.FirstOrDefault(m => m.id == it.id).restaurant_id = it.restaurant_id;
                        list.sb_problems.FirstOrDefault(m => m.id == it.id).mgr_id = it.sb_managers.id;
                    }
                }
                list.SaveChanges();
            }
        }
        public void sendSingleProblem()
        {
            list.sb_problems.Add(new sb_problems { date = problem.date, problem = problem.problem, restaurant_id = problem.restaurant_id, solution = problem.solution, id = list.sb_problems.Count() + 1, mgr_id = problem.manager_id });
            list.SaveChanges();
        }
        public void getProblemsTop()
        {
            restaurants = list.sb_restaurants.ToList();
            problems_top = list.sb_top.ToList();
        }
        public void sendProblemTop()
        {
            foreach (var it in problems_top)
            {
                list.sb_top.FirstOrDefault(m => m.id == it.id).date = it.date;
                list.sb_top.FirstOrDefault(m => m.id == it.id).problem = it.problem;
                list.sb_top.FirstOrDefault(m => m.id == it.id).solution = it.solution;
                list.sb_top.FirstOrDefault(m => m.id == it.id).restaurant_id = it.restaurant_id;
            }
            list.SaveChanges();
        }
        public void sendSingleProblemTop()
        {
            list.sb_top.Add(new sb_top { date = problem.date, problem = problem.problem, restaurant_id = problem.restaurant_id, solution = problem.solution });
            list.SaveChanges();
        }
        public void createProblem()
        {
            problem = new Problem();
        }
    }
}