using Caching.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching.Data.EF
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product() { ProductId = 1, Name = "shoes" , Price = 10},
                new Product() { ProductId = 2, Name = "clothes", Price = 10 },
                new Product() { ProductId = 3, Name = "computer", Price = 10 },
                new Product() { ProductId = 4, Name = "phone", Price = 10 }
                );
            base.OnModelCreating(modelBuilder);
        }



    }
}
