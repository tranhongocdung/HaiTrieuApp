using System;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.AppDataLayer.Repositories
{
    public class CustomerRepository
    {
        public static int Create(Customer customer)
        {
            customer.CreatedOn = DateTime.Now;
            var db = new DbAppContext();
            db.Customers.Add(customer);
            db.SaveChanges();
            return customer.Id;
        }
    }
}