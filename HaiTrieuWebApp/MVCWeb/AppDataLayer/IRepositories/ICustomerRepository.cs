using System.Collections.Generic;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.AppDataLayer.IRepositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        int Create(Customer customer);
        bool UpdateCustomer(Customer customer);
        List<Customer> GetList(FilterParams fp, ref int totalCount);
    }
}