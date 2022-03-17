using System.Linq.Expressions;

namespace BooksyBookShopWeb.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);
        IEnumerable<T> GetAll(string? includeProperties = null);
        void Add(T category);
        void Remove(T category);
        void RemoveRange(IEnumerable<T> category);
    }
}
