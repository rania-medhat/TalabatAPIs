using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PresistenceLayer.Data.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p=>p.ProductBrand)
                .WithMany()
                .HasForeignKey(p=>p.BrandId);

            builder.HasOne(p => p.ProductType)
               .WithMany()
               .HasForeignKey(p => p.TypeId);

            builder.Property(p => p.Name).HasColumnType("nvarchar(200)");

            builder.Property(p => p.Description).HasColumnType("nvarchar(500)");

            builder.Property(p => p.Price).HasColumnType("nvarchar(18)");
        }
    }
}
