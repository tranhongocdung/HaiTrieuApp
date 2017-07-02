using MVCWeb.AppDataLayer.Entities;
using MVCWeb.AppDataLayer.IRepositories;

namespace MVCWeb.AppDataLayer.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly IDbAppContext _context;
        public CustomerRepository(IDbAppContext context) : base(context)
        {
            _context = context as DbAppContext;
        }
    }
}