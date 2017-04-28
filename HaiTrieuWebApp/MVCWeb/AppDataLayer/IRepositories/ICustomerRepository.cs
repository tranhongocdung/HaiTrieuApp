using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.AppDataLayer.IRepositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        int Create(Customer customer);
    }
}