using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.AppDataLayer.IServices
{
    public interface IOrderService : IWebAppService
    {
        int Create(Order order);
        bool UpdateOrder(Order order);
        void CompleteOrder(int orderId);
        Order GetWithOrderDetails(int id);
        Order GetWithCustomerAndOrderDetails(int id);
        List<Order> GetList(FilterParams fp, ref int totalCount);
        void UpdateOrderDetail(List<OrderDetail> orderDetails, int orderId);

    }
}
