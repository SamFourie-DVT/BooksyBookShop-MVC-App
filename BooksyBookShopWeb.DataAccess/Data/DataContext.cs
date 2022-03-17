using BooksyBookShopWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BooksyWeb.Data_Context
{
    public class DataContext : IdentityDbContext
    {
        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("server=(localdb)\\Local_New;database=BooksyDB;trusted_connection=true");
            }
        }
    }
}
