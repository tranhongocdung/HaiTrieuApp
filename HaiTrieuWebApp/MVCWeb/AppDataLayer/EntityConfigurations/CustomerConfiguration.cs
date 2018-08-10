using System.Data.Entity.ModelConfiguration;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.AppDataLayer.EntityConfigurations
{
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            ToTable("Customer");
            HasKey(o => o.Id);
            Property(x => x.CustomerName).IsRequired();
            Property(x => x.PhoneNo).IsRequired();
        }
    }
}