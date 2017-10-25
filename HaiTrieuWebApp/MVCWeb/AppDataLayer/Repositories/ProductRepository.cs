using MVCWeb.AppDataLayer.Entities;
using MVCWeb.AppDataLayer.IRepositories;

namespace MVCWeb.AppDataLayer.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IDbAppContext _context;

        public ProductRepository(IDbAppContext context) : base(context)
        {
            _context = context as DbAppContext;
        }
        
    }
}