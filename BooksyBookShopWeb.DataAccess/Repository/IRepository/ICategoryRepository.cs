using BooksyBookShopWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BooksyBookShopWeb.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}
