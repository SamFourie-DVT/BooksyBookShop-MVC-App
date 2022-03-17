using BooksyBookShopWeb.DataAccess.Repository.IRepository;
using BooksyWeb.Data_Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BooksyBookShopWeb.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _data;
        internal DbSet<T> _dbSet;

        public Repository(DataContext data)
        {
            _data = data;
            //_data.Products.Include(u => u.Category);
            this._dbSet = _data.Set<T>();
        }

        public void Add(T category)
        {
            _dbSet.Add(category);
        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] {','},
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                };
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            query = query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                };
            }

            return query.FirstOrDefault();
        }

        public void Remove(T category)
        {
            _dbSet.Remove(category);
        }

        public void RemoveRange(IEnumerable<T> category)
        {
            _dbSet.RemoveRange(category);
        }
    }
}
