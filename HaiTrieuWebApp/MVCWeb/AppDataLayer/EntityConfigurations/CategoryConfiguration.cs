using System.Data.Entity.ModelConfiguration;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.AppDataLayer.EntityConfigurations
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            ToTable("Category");
            HasKey(o => o.Id);
            HasMany(o => o.Products).WithMany(o => o.Categories).Map(o =>
            {
                o.MapLeftKey("CategoryId");
                o.MapRightKey("ProductId");
                o.ToTable("Product_Category");
            });
        }
    }
}