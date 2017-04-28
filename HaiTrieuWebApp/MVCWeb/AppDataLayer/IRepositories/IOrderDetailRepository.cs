using System.Collections.Generic;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.AppDataLayer.IRepositories
{
    public interface IOrderDetailRepository : IGenericRepository<OrderDetail>
    {
        void UpdateOrderDetail(List<OrderDetail> orderDetails, int orderId);
    }
}