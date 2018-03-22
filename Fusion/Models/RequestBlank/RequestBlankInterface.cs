using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Models.RequestBlank
{
    public interface IReference<T>
    {
        int Id { get; set; }
        string Name { get; set; }
        bool Deleted { get; set; }
        void Delete(int id);
        T Add(T Item);
        T Update(T Item);
    }
    public interface IRestaurant: IReference<IRestaurant>
    {

    }
    public interface IElement : IReference<IElement>
    {
        string Measure { get; set; }
    }
    public interface IRequestRow
    {
        IElement TSP { get; set; }
        float Amount { get; set; }
    }
    public interface IRequestColumn
    {
        IRestaurant Restaurant { get; set; }
        List<IRequestRow> Products { get; set; }

    }
    public interface IRequestBlank
    {

    }
}
