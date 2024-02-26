using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
             new Product { Id = 1, Name = "Pilot Kalem", Price = 120, Stock = 100, CategoryId = 1,CreatedDate=DateTime.Now },
             new Product { Id = 2, Name = "100 Yaprak Kareli Defter", Price = 100, Stock = 200, CategoryId = 2,CreatedDate=DateTime.Now },
             new Product { Id = 3, Name = "Unutulamayanlar", Price = 150, Stock = 300, CategoryId = 3,CreatedDate=DateTime.Now }

                                                                                                                                            );
        }
    }
}
