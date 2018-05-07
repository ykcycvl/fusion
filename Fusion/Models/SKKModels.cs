﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Interfaces;

namespace Fusion.Models
{
    public class SKKModels
    {
        public class Aticle : IArticle
        {
            public int Cost { get; set; }
            public int Rating { get; set; }
            public string Comment { get; set; }
            public List<IFile> Files { get; set; }
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Deleted { get; set; }

            public IArticle Add(IArticle Item)
            {
                throw new NotImplementedException();
            }

            public void AddFile(IFile File)
            {
                throw new NotImplementedException();
            }

            public void Delete(int Id)
            {
                throw new NotImplementedException();
            }

            public void DeleteFile(IFile File)
            {
                throw new NotImplementedException();
            }

            public IArticle Edit(IArticle Item)
            {
                throw new NotImplementedException();
            }
        }
        public class Category : ICategory
        {
            public List<IArticle> Articles { get; set; }
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Deleted { get; set; }

            public ICategory Add(ICategory Item)
            {
                throw new NotImplementedException();
            }

            public void Delete(int Id)
            {
                throw new NotImplementedException();
            }

            public ICategory Edit(ICategory Item)
            {
                throw new NotImplementedException();
            }
        }
        public class Restaurant : IRestaurant
        {
            public IEmployee Director { get; set; }
            public IEmployee Chef { get; set; }
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Deleted { get; set; }

            public IRestaurant Add(IRestaurant Item)
            {
                throw new NotImplementedException();
            }

            public void Delete(int Id)
            {
                throw new NotImplementedException();
            }

            public IRestaurant Edit(IRestaurant Item)
            {
                throw new NotImplementedException();
            }
        }
        public class Employee : IEmployee
        {
            public IPosition Position { get; set; }
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Deleted { get; set; }

            public IEmployee Add(IEmployee Item)
            {
                throw new NotImplementedException();
            }

            public void Delete(int Id)
            {
                throw new NotImplementedException();
            }

            public IEmployee Edit(IEmployee Item)
            {
                throw new NotImplementedException();
            }
        }
        public class Position : IPosition
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Deleted { get; set; }

            public IPosition Add(IPosition Item)
            {
                throw new NotImplementedException();
            }

            public void Delete(int Id)
            {
                throw new NotImplementedException();
            }

            public IPosition Edit(IPosition Item)
            {
                throw new NotImplementedException();
            }
        }
        public class InspectionReport : IInspectionReport
        {
            public DateTime InspectionDate { get; set; }
            public IEmployee Inspector { get; set; }
            public IEmployee Director { get; set; }
            public IEmployee Chef { get; set; }
            public IEmployee SousChef { get; set; }
            public IEmployee HallManager { get; set; }
            public IRestaurant Restaurant { get; set; }
            public List<ICategory> Categories { get; set; }

            public IInspectionReport Get(int RestaurantID, DateTime InspectionDate)
            {
                throw new NotImplementedException();
            }

            public IInspectionReport GetByID(int Id)
            {
                throw new NotImplementedException();
            }

            public IInspectionReport Save(IInspectionReport Report)
            {
                throw new NotImplementedException();
            }
        }
        public class ActDataMock
        {
            public int ArticleId { get; set; }
            public bool Accord { get; set; }
            public Models.ActData ActData { get; set; }
        }
        public class PostedFiles
        {
            public int ArticleId { get; set; }
            public HttpPostedFileBase file { get; set; }
        }
        public SKKEntities1 SKK = new SKKEntities1();
        public Models.Act Act { get; set; }
        public Models.ActData ActDataItem { get; set; }
        public Models.Employee employee { get; set; }
        public Models.Restaurant restaurant { get; set; }
        public Models.ArticleBlock ArticleBlock { get; set; }
        public Models.Article Article { get; set; }
        public List<Models.Act> Acts { get; set; }
        public List<Models.ActFile> ActFiles { get; set; }
        public List<Models.ActData> ActData { get; set; }
        public List<Models.Position> Positions { get; set; }
        public List<Models.Employee> Employees { get; set; }
        public List<Models.Restaurant> Restaurants { get; set; }
        public List<Models.Article> Articles { get; set; }
        public List<Models.ArticleBlock> ArticleBlocks { get; set; }
        public List<ActDataMock> actDataMock { get; set; }
        public List<Percents> PercentsList { get; set; }
        public List<PercentsBlock> percentsBlockList { get; set; }
        public string DirectorName { get; set; }
        public string ManagerName { get; set; }
        public string ChiefName { get; set; }
        public string SuChiefName { get; set; }
        public string AuditorName { get; set; }
        public string PositionName { get; set; }
        public string BlockName { get; set; }
        public string RestaurantName { get; set; }
        public int ActId { get; set; }
        public class Percents
        {
            public int restaurantID { get; set; }
            public double? percent { get; set; }
            public double? percent_prev { get; set; }
            public double? percent_today { get; set; }
            public string restaurantName { get; set; }
            public List<ActData> actDataList { get; set; }
        }
        public class PercentsBlock
        {
            public string blockName { get; set; }
            public double? rating { get; set; }
            public string restaurantName { get; set; }
        }

        //get
        public void getActs()
        {
            Acts = SKK.Act.ToList();
        }
        public void getActDataList()
        {
            ActData = SKK.ActData.ToList();
        }
        public void getActDataById(int? ActId)
        {
            ActData = SKK.ActData.Where(m => m.act_id == ActId).ToList();
        }
        public void getActFilesById(int? ActDataId)
        {
            ActFiles = SKK.ActFile.Where(m => m.act_data_id == ActDataId).ToList();
        }
        public void getBlocks()
        {
            ArticleBlocks = SKK.ArticleBlock.ToList();
        }
        public void getArticles()
        {
            Articles = SKK.Article.ToList();
        }
        public void getArticleById(int? id)
        {
            Article = SKK.Article.FirstOrDefault(m => m.id == id);
        }
        public void getActById(int? id)
        {
            Act = SKK.Act.FirstOrDefault(m => m.id == id);
        }
        public void getPositions()
        {
            Positions = SKK.Position.ToList();
        }
        public void getEmployees()
        {
            Employees = SKK.Employee.ToList();
        }
        public void getRestaurantById(int? id)
        {
            restaurant = SKK.Restaurant.FirstOrDefault(m => m.id == id);
        }
        public void getEmployeeById(int? id)
        {
            employee = SKK.Employee.FirstOrDefault(m => m.id == id);
        }
        public void getInfoList()
        {
            Positions = SKK.Position.ToList();
            Employees = SKK.Employee.ToList();
            Restaurants = SKK.Restaurant.ToList();
        }
        public void getBlockById(int? id)
        {
            ArticleBlock = SKK.ArticleBlock.FirstOrDefault(m => m.id == id);
        }
        public void getAnalytics()
        {
            PercentsList = new List<Percents>();
            percentsBlockList = new List<PercentsBlock>();
            foreach (var it in Restaurants)
            {
                int? rating = 0;
                int? weight = 0;
                int? weight_prev = 0;
                int? weight_today = 0;
                int? rating_prev = 0;
                int? rating_today = 0;

                foreach (var iy in Acts.Where(n => n.restaurant_id == it.id))
                {
                    foreach (var iu in ActData.Where(j => j.act_id == iy.id))
                    {
                        rating += iu.rating;
                        weight += Articles.FirstOrDefault(k => k.id == iu.article_id).weight;
                    }
                }
                foreach(var h in Acts.Where(j => j.restaurant_id == it.id && j.date.Value.Month == DateTime.Today.Month && j.date.Value.Year == DateTime.Now.Year))
                {
                    foreach(var t in ActData.Where(m => m.act_id == h.id))
                    {
                        rating_today += t.rating;
                        weight_today += Articles.FirstOrDefault(k => k.id == t.article_id).weight;
                    }
                }
                foreach (var q in Acts.Where(f => f.restaurant_id == it.id && f.date.Value.Month == DateTime.Today.Month-1 && f.date.Value.Year == DateTime.Now.Year))
                {
                    foreach (var r in ActData.Where(m => m.act_id == q.id))
                    {
                        rating_prev += r.rating;
                        weight_prev += Articles.FirstOrDefault(k => k.id == r.article_id).weight;
                    }
                }
                if (weight == 0)
                {
                    weight = 1;
                }
                if (weight_prev == 0)
                {
                    weight_prev = 1;
                }
                if (weight_today == 0)
                {
                    weight_today = 1;
                }
                double? percent = (rating * 100) / weight;
                double? percent_prev = (rating_prev * 100) / weight_prev;
                double? percent_today = (rating_today * 100) / weight_today;
                PercentsList.Add(new Percents { restaurantID = it.id, percent = percent, restaurantName = it.name, percent_prev = percent_prev, percent_today = percent_today, actDataList = new List<Models.ActData>() });
                foreach (var h in Acts.Where(j => j.restaurant_id == it.id && j.date.Value.Month == DateTime.Today.Month && j.date.Value.Year == DateTime.Now.Year))
                {
                    foreach (var t in ActData.Where(m => m.act_id == h.id))
                    {
                        PercentsList.FirstOrDefault(m => m.restaurantID == it.id).actDataList.Add(t);
                    }
                }
            }
            foreach (var it in PercentsList)
            {
                foreach (var ih in it.actDataList.GroupBy(m => m.Article.ArticleBlock.name).Select(n => new { n.Key, rating = n.Sum(j => j.rating), weight = n.Sum(k => k.Article.weight) }))
                {
                    double? rating = (ih.rating * 100) / ih.weight;
                    percentsBlockList.Add(new PercentsBlock { blockName = ih.Key, rating = rating, restaurantName = it.restaurantName });
                }
            }
        }
        //create
        public void createActDataMock(int? actID)
        {
            actDataMock = new List<SKKModels.ActDataMock>();
            if (ActData.Where(m => m.act_id == actID).Any())
            {
                //getActDataById(actID);
                //createActDataMock(actID);
                foreach (var it in Articles)
                {
                    if (!ActData.Where(n => n.article_id == it.id && n.act_id == actID).Any())
                    {
                        ActData actdata = new ActData() { article_id = it.id, accord = true, comment = "", rating = 0, act_id = actID };
                        actDataMock.Add(new SKKModels.ActDataMock { Accord = true, ArticleId = it.id, ActData = actdata });
                    }
                    else
                    {
                        actDataMock.Add(new SKKModels.ActDataMock { Accord = (bool)ActData.FirstOrDefault(n => n.article_id == it.id && n.act_id == actID).accord, ActData = ActData.FirstOrDefault(n => n.article_id == it.id && n.act_id == actID), ArticleId = it.id });
                    }
                }
            }
            else
            {
                createActData();
                foreach (var it in Articles)
                {
                    ActData actData = new ActData() { accord = true, rating = 0, comment = "", article_id = it.id, act_id = actID };
                    actDataMock.Add(new SKKModels.ActDataMock { ArticleId = it.id, ActData = actData, Accord = true });
                }
            }
        }
        public void createBlock()
        {
            ArticleBlock = new ArticleBlock();
        }
        public void createArticle()
        {
            Article = new Article();
        }
        //save
        public void saveArticle()
        {
            if (Article.id == 0)
            {
                SKK.Article.Add(Article);
            }
            else
            {
                SKK.Article.FirstOrDefault(m => m.id == Article.id).name = Article.name;
                SKK.Article.FirstOrDefault(m => m.id == Article.id).weight = Article.weight;
                SKK.Article.FirstOrDefault(m => m.id == Article.id).block_id = Article.block_id;
            }
            SKK.SaveChanges();
        }
        public void createAct()
        {
            Act = new Act();
        }
        public void createActData()
        {
            ActDataItem = new Models.ActData();
        }
        public void createRestaurant()
        {
            restaurant = new Models.Restaurant();
        }
        public void createEmployee()
        {
            employee = new Models.Employee();
        }
        public int saveAct()
        {
            if (Act.id == 0)
            {
                Act.chief_id = Restaurants.FirstOrDefault(m => m.id == Act.restaurant_id).chief_id;
                Act.manager_id = Restaurants.FirstOrDefault(m => m.id == Act.restaurant_id).manager_id;
                Act.director_id = Restaurants.FirstOrDefault(m => m.id == Act.restaurant_id).director_id;
                Act.suchief_id = Restaurants.FirstOrDefault(m => m.id == Act.restaurant_id).suchief_id;
                SKK.Act.Add(Act);
            }
            else
            {
                SKK.Act.FirstOrDefault(m => m.id == Act.id).date = Act.date;
                SKK.Act.FirstOrDefault(m => m.id == Act.id).description = Act.description;
                SKK.Act.FirstOrDefault(m => m.id == Act.id).auditor_id = Act.auditor_id;
                SKK.Act.FirstOrDefault(m => m.id == Act.id).restaurant_id = Act.restaurant_id;
            }
            SKK.SaveChanges();
            return Act.id;
        }
        public void saveBlock()
        {
            if (ArticleBlock.id == 0)
            {
                SKK.ArticleBlock.Add(ArticleBlock);
            }
            else
            {
                SKK.ArticleBlock.FirstOrDefault(m => m.id == ArticleBlock.id).name = ArticleBlock.name;
            }
            SKK.SaveChanges();
        }
        public void saveActData(int actId, List<PostedFiles> files)
        {
            List<int> tempArticleId = new List<int>();
            foreach (var it in actDataMock)
            {
                if (it.Accord == true)
                {
                    it.ActData.accord = true;
                    it.ActData.rating = Articles.FirstOrDefault(m => m.id == it.ArticleId).weight;
                    it.ActData.comment = "";
                }
                if (ActData.Where(m => m.act_id == actId && m.article_id == it.ArticleId).Any())
                {
                    ActData.FirstOrDefault(m => m.act_id == actId && m.article_id == it.ArticleId).comment = it.ActData.comment;
                    ActData.FirstOrDefault(m => m.act_id == actId && m.article_id == it.ArticleId).rating = it.ActData.rating;
                    ActData.FirstOrDefault(m => m.act_id == actId && m.article_id == it.ArticleId).accord = it.Accord;
                }
                else
                {
                    SKK.ActData.Add(new Models.ActData { act_id = actId, article_id = it.ArticleId, accord = it.Accord, comment = it.ActData.comment, rating = it.ActData.rating });
                }
            }
            SKK.SaveChanges();
            foreach (var it in ActData.Where(m => m.act_id == ActId))
            {
                if (files.Where(n => n.ArticleId == it.article_id).Any())
                {
                    SKK.ActFile.Add(new ActFile { act_data_id = it.id, filename = files.FirstOrDefault(n => n.ArticleId == it.article_id).file.FileName });
                }
            }
            SKK.SaveChanges();
        }
        public void saveEmployee()
        {
            if (employee.id != 0)
            {
                SKK.Employee.FirstOrDefault(m => m.id == employee.id).domain_name = employee.domain_name;
                SKK.Employee.FirstOrDefault(m => m.id == employee.id).full_name = employee.full_name;
                SKK.Employee.FirstOrDefault(m => m.id == employee.id).position_id = employee.position_id;
            }
            else
            {
                SKK.Employee.Add(employee);
            }
            SKK.SaveChanges();
        }
        public void saveRestaurant()
        {
            if (restaurant.id == 0)
            {
                SKK.Restaurant.Add(restaurant);
            }
            else
            {
                SKK.Restaurant.FirstOrDefault(m => m.id == restaurant.id).manager_id = restaurant.manager_id;
                SKK.Restaurant.FirstOrDefault(m => m.id == restaurant.id).suchief_id = restaurant.suchief_id;
                SKK.Restaurant.FirstOrDefault(m => m.id == restaurant.id).director_id = restaurant.director_id;
                SKK.Restaurant.FirstOrDefault(m => m.id == restaurant.id).chief_id = restaurant.chief_id;
                SKK.Restaurant.FirstOrDefault(m => m.id == restaurant.id).name = restaurant.name;
            }
            SKK.SaveChanges();
        }
        //селекты

        /*
            1 - Упр
            2 - Шеф
            3 - Су-шеф
            4 - Менеджер
            5 - аудитор
        */
        public IEnumerable<SelectListItem> DirectorsSelectList
        {
            get
            {
                List<SelectListItem> employees = new List<SelectListItem>();
                foreach (var it in Employees.Where(m => m.position_id == 1))
                {
                    employees.Add(new SelectListItem() { Text = it.full_name, Value = it.id.ToString() });
                }
                SelectListItem sli = employees.FirstOrDefault(p => p.Text == DirectorName);
                if (sli != null)
                    sli.Selected = true;
                return employees;
            }
        }
        public IEnumerable<SelectListItem> ChiefsSelectList
        {
            get
            {
                List<SelectListItem> employees = new List<SelectListItem>();
                foreach (var it in Employees.Where(m => m.position_id == 2))
                {
                    employees.Add(new SelectListItem() { Text = it.full_name, Value = it.id.ToString() });
                }
                SelectListItem sli = employees.FirstOrDefault(p => p.Text == ChiefName);
                if (sli != null)
                    sli.Selected = true;
                return employees;
            }
        }
        public IEnumerable<SelectListItem> SuChiefsSelectList
        {
            get
            {
                List<SelectListItem> employees = new List<SelectListItem>();
                foreach (var it in Employees.Where(m => m.position_id == 3))
                {
                    employees.Add(new SelectListItem() { Text = it.full_name, Value = it.id.ToString() });
                }
                SelectListItem sli = employees.FirstOrDefault(p => p.Text == SuChiefName);
                if (sli != null)
                    sli.Selected = true;
                return employees;
            }
        }
        public IEnumerable<SelectListItem> ManagersSelectList
        {
            get
            {
                List<SelectListItem> employees = new List<SelectListItem>();
                foreach (var it in Employees.Where(m => m.position_id == 4))
                {
                    employees.Add(new SelectListItem() { Text = it.full_name, Value = it.id.ToString() });
                }
                SelectListItem sli = employees.FirstOrDefault(p => p.Text == ManagerName);
                if (sli != null)
                    sli.Selected = true;
                return employees;
            }
        }
        public IEnumerable<SelectListItem> AuditorsSelectList
        {
            get
            {
                List<SelectListItem> employees = new List<SelectListItem>();
                foreach (var it in Employees.Where(m => m.position_id == 5))
                {
                    employees.Add(new SelectListItem() { Text = it.full_name, Value = it.id.ToString() });
                }
                SelectListItem sli = employees.FirstOrDefault(p => p.Text == AuditorName);
                if (sli != null)
                    sli.Selected = true;
                return employees;
            }
        }
        public IEnumerable<SelectListItem> PositionsSelectList
        {
            get
            {
                List<SelectListItem> positions = new List<SelectListItem>();
                foreach (var it in Positions)
                {
                    positions.Add(new SelectListItem() { Text = it.name, Value = it.id.ToString() });
                }
                SelectListItem sli = positions.FirstOrDefault(p => p.Text == PositionName);
                if (sli != null)
                    sli.Selected = true;
                return positions;
            }
        }
        public IEnumerable<SelectListItem> BlocksSelectList
        {
            get
            {
                List<SelectListItem> blocks = new List<SelectListItem>();
                foreach (var it in ArticleBlocks)
                {
                    blocks.Add(new SelectListItem() { Text = it.name, Value = it.id.ToString() });
                }
                SelectListItem sli = blocks.FirstOrDefault(p => p.Text == BlockName);
                if (sli != null)
                    sli.Selected = true;
                return blocks;
            }
        }
        public IEnumerable<SelectListItem> RestaurantsSelectList
        {
            get
            {
                List<SelectListItem> restaurants = new List<SelectListItem>();
                foreach (var it in Restaurants)
                {
                    restaurants.Add(new SelectListItem() { Text = it.name, Value = it.id.ToString() });
                }
                SelectListItem sli = restaurants.FirstOrDefault(p => p.Text == RestaurantName);
                if (sli != null)
                    sli.Selected = true;
                return restaurants;
            }
        }
    }
}