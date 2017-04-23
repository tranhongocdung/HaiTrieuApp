using System.Data.Entity;

namespace MVCWeb.AppDataLayer.Entities
{
    public class DbAppContext : DbContext
    {
        public DbAppContext()
            : base("name=HaiTrieuDBConnectionString")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}