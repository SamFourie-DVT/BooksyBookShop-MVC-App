using BooksyBookShopWeb.DataAccess.Repository.IRepository;
using BooksyBookShopWeb.Models;
using BooksyWeb.Data_Context;
using Microsoft.AspNetCore.Mvc;

namespace BooksyBookShopWeb.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly DataContext _data;
        public ProductRepository(DataContext data) : base(data)
        {
            _data = data;
        }

        public void Update(Product product)
        {
            //_data.Products.Update(product);
            var ProductFromDB = _data.Products.FirstOrDefault(u => u.Id == product.Id);
            if (ProductFromDB != null)
            {
                ProductFromDB.Title = product.Title;
                ProductFromDB.Description = product.Description; 
                ProductFromDB.ISBN = product.ISBN;
                ProductFromDB.CategoryId = product.CategoryId;
                ProductFromDB.CoverTypeId = product.CoverTypeId;
                ProductFromDB.ListPrice = product.ListPrice;
                ProductFromDB.Price = product.Price;
                ProductFromDB.Price50 = product.Price50;
                ProductFromDB.Price100 = product.Price100;
                ProductFromDB.Author = product.Author;
                if (product.ImageUrl != null)
                {
                    ProductFromDB.ImageUrl = product.ImageUrl;
                }
            }

        }
    }
}
