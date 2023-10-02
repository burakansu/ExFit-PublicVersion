using Data.Custom;
using System.Linq.Expressions;

namespace Data.Abstract
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        T Get(Expression<Func<T, bool>> filter);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter);

        Response Insert(T p, int CurrentUserId);
        Response Insert(T p);

        Response Update(T p, int CurrentUserId);
        Response Update(T p);


        Response Delete(T p, int CurrentUserId);
        Response Delete(T p);


        Response AddRange(List<T> items, int CurrentUserId);
        Response AddRange(List<T> items);

        Response RemoveRange(List<T> items, int CurrentUserId);
        Response RemoveRange(List<T> items);
    }
}
