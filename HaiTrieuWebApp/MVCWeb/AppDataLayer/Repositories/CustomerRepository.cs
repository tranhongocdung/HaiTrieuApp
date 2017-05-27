using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
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
        public bool UpdateCustomer(Customer customer)
        {
            var currentCustomer = GetById(customer.Id);
            if (currentCustomer == null) return false;
            currentCustomer.CustomerName = customer.CustomerName;
            currentCustomer.PhoneNo = customer.PhoneNo;
            currentCustomer.Email = customer.Email;
            currentCustomer.Address= customer.Address;
            currentCustomer.Region = customer.Region;
            currentCustomer.Area = customer.Area;
            currentCustomer.Note = customer.Note;
            Update(customer);
            return true;
        }
        public List<Customer> GetList(FilterParams fp, ref int totalCount)
        {
            var list = TableNoTracking;
            if (!string.IsNullOrEmpty(fp.Keyword))
            {
                list = list.Where(o => o.CustomerName.Contains(fp.Keyword) || o.Email.Contains(fp.Keyword) || o.PhoneNo.Contains(fp.Keyword));
            }
            totalCount = list.Count();
            list = list.OrderBy(fp.SortField + (fp.SortASC ? " ASC" : " DESC"));
            if (fp.PageNumber == 0) return list.ToList();
            var skip = (fp.PageNumber - 1) * fp.PageSize;
            var take = fp.PageSize;
            list = list.Skip(skip).Take(take);
            return list.ToList();
        }
    }
}