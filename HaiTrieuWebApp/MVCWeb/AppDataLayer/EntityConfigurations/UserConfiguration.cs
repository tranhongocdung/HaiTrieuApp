using System.Data.Entity.ModelConfiguration;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.AppDataLayer.EntityConfigurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("User");
            HasKey(o => o.Id);
        }
    }
}