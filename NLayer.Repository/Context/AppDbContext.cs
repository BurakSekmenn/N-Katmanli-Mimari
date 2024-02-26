using Microsoft.EntityFrameworkCore;
using NLayer.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ProductFeature>().HasData(
                               new ProductFeature { Id = 1, Color = "Kırmızı",Height=100,Width=200,ProductId=1 },
                               new ProductFeature { Id = 2, Color = "Mavi",Height = 900, Width = 210, ProductId = 2 },
                               new ProductFeature {Id = 3, Color = "Siyah", Height = 150, Width = 200, ProductId = 3 }
                                                                            );





            base.OnModelCreating(modelBuilder);

        }


    }
}
