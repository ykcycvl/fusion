using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Interfaces
{
    public interface IReference<T>
    {
        int Id { get; set; }
        string Name { get; set; }
        bool Deleted { get; set; }
        T Add(T Item);
        T Edit(T Item);
        void Delete(int Id);
    }

    public interface IInspectionReport
    {
        DateTime InspectionDate { get; set; }
        IEmployee Inspector { get; set; }
        IEmployee Director { get; set; }
        IEmployee Chef { get; set; }
        IEmployee SousChef { get; set; }
        IEmployee HallManager { get; set; }
        IRestaurant Restaurant { get; set; }
        List<ICategory> Categories { get; set; }
        IInspectionReport GetByID(int Id);
        IInspectionReport Get(int RestaurantID, DateTime InspectionDate);
        IInspectionReport Save(IInspectionReport Report);
    }

    public interface IRestaurant : IReference<IRestaurant>
    {
        IEmployee Director { get; set; }
        IEmployee Chef { get; set; }
    }

    public interface ICategory: IReference<ICategory>
    {
        List<IArticle> Articles { get; set; }
    }

    public interface IArticle: IReference<IArticle>
    {
        int Cost { get; set; }
        int Rating { get; set; }
        string Comment { get; set; }
        List<IFile> Files { get; set; }
        void AddFile(IFile File);
        void DeleteFile(IFile File);
    }
    public interface IFile
    {
        string FullFilePath { get; set; }
        string Description { get; set; }
    }

    public interface IEmployee: IReference<IEmployee>
    {
        IPosition Position { get; set; }
    }

    public interface IPosition: IReference<IPosition>
    {
    }
}