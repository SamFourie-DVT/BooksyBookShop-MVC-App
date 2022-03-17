using BooksyBookShopWeb.DataAccess.Repository.IRepository;
using BooksyWeb.Data_Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksyBookShopWeb.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _data;
        public UnitOfWork(DataContext data)
        {
            _data = data;
            Category = new CategoryRepository(_data);
            CoverType = new CoverTypeRepository(_data);
            Product = new ProductRepository(_data);
        }

        public ICategoryRepository Category{get; private set;}
        public ICoverTypeRepository CoverType{get; private set;}
        public IProductRepository Product { get; private set;}


        public void Save()
        {
            _data.SaveChangesAsync();
        }
    }
}
