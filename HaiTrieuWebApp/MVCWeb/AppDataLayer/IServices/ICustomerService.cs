using System.Collections.Generic;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.AppDataLayer.IServices
{
    public interface ICustomerService : IWebAppService
    {
        int Create(Customer customer);
        bool UpdateCustomer(Customer customer);
        List<Customer> GetList(FilterParams fp, ref int totalCount);
    }
}
