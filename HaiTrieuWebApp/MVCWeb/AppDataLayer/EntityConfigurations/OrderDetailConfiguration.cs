using System.Data.Entity.ModelConfiguration;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.AppDataLayer.EntityConfigurations
{
    public class OrderDetailConfiguration : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailConfiguration()
        {
            ToTable("OrderDetail");
            HasKey(o => o.Id);
            HasRequired(o => o.Order).WithMany(o => o.OrderDetails).HasForeignKey(o => o.OrderId);
            HasRequired(o => o.Product).WithMany(o => o.OrderDetails).HasForeignKey(o => o.ProductId);
        }
    }
}