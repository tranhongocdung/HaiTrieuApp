using System;
using MVCWeb.AppDataLayer.Entities;
using MVCWeb.AppDataLayer.IRepositories;

namespace MVCWeb.AppDataLayer.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbAppContext context) : base(context)
        {
        }
        public int Create(Customer customer)
        {
            customer.CreatedOn = DateTime.Now;
            Insert(customer);
            return customer.Id;
        }
    }
}