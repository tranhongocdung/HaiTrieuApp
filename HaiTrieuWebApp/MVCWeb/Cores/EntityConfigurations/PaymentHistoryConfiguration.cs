using System.Data.Entity.ModelConfiguration;
using MVCWeb.Cores.Entities;

namespace MVCWeb.Cores.EntityConfigurations
{
    public class PaymentHistoryConfiguration : EntityTypeConfiguration<PaymentHistory>
    {
        public PaymentHistoryConfiguration()
        {
            ToTable("PaymentHistory");
            HasKey(o => o.Id);
            HasRequired(o => o.Order).WithMany(o => o.PaymentHistories).HasForeignKey(o => o.OrderId);
        }
    }
}