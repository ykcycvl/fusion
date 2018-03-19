using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fusion.Interfaces;

namespace Fusion.Models
{
    public class SKKModels
    {
        public class Aticle : IArticle
        {
            public int Cost { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public int Rating { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public string Comment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public List<IFile> Files { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public bool Deleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
            public List<IArticle> Articles { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public bool Deleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
            public IEmployee Director { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public IEmployee Chef { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public bool Deleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
            public IPosition Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public bool Deleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
            public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public bool Deleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
            public DateTime InspectionDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public IEmployee Inspector { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public IEmployee Director { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public IEmployee Chef { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public IEmployee SousChef { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public IEmployee HallManager { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public IRestaurant Restaurant { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public List<ICategory> Categories { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
    }
}