﻿using System.Data.Entity.ModelConfiguration;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.AppDataLayer.EntityConfigurations
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            ToTable("Product");
            HasKey(o => o.Id);
            HasMany(o => o.Categories).WithMany(o => o.Products).Map(o =>
            {
                o.MapLeftKey("ProductId");
                o.MapRightKey("CategoryId");
                o.ToTable("Product_Category");
            });
        }
    }
}