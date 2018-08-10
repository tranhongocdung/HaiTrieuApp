using System.Data.Entity.ModelConfiguration;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.AppDataLayer.EntityConfigurations
{
    public class OrderStatusConfiguration : EntityTypeConfiguration<OrderStatus>
    {
        public OrderStatusConfiguration()
        {
            ToTable("OrderStatus");
            HasKey(o => o.Id);
        }
    }
}