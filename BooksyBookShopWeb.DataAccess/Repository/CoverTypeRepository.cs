using BooksyBookShopWeb.DataAccess.Repository.IRepository;
using BooksyBookShopWeb.Models;
using BooksyWeb.Data_Context;
using Microsoft.AspNetCore.Mvc;

namespace BooksyBookShopWeb.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly DataContext _data;
        public CoverTypeRepository(DataContext data) : base(data)
        {
            _data = data;
        }

        public void Update(CoverType coverType)
        {
            _data.CoverTypes.Update(coverType);
        }
    }
}
