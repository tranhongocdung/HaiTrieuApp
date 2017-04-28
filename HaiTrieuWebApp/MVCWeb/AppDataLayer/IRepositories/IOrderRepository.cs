using System.Collections.Generic;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.AppDataLayer.IRepositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        int Create(Order order);
        bool UpdateOrder(Order order);
        Order GetWithCustomerAndOrderDetails(int id);
        List<Order> GetList(FilterParams fp, ref int totalCount);
    }
}