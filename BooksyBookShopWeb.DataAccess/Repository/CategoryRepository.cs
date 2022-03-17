using BooksyBookShopWeb.DataAccess.Repository.IRepository;
using BooksyBookShopWeb.Models;
using BooksyWeb.Data_Context;
using Microsoft.AspNetCore.Mvc;

namespace BooksyBookShopWeb.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly DataContext _data;
        public CategoryRepository(DataContext data) : base(data)
        {
            _data = data;
        }

        public void Update(Category category)
        {
            _data.Categories.Update(category);
        }
    }
}
