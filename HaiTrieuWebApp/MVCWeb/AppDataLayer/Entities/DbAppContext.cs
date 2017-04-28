using System.Data.Entity;

namespace MVCWeb.AppDataLayer.Entities
{
    public class DbAppContext : DbContext
    {
        public DbAppContext()
            : base("name=HaiTrieuDBConnectionString")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DbAppContext>());
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
    }
}