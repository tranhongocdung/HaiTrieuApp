using System.Collections.Generic;
using System.Linq;
using MVCWeb.AppDataLayer.Entities;
using MVCWeb.AppDataLayer.IRepositories;

namespace MVCWeb.AppDataLayer.Repositories
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(DbAppContext context) : base(context) {}
        public void UpdateOrderDetail(List<OrderDetail> orderDetails, int orderId)
        {
            //Delete
            var newOrderDetailIds = orderDetails.Where(o => o.Id != 0).Select(o => o.Id);
            var removedOrderDetails = Table.Where(o => o.OrderId == orderId && !newOrderDetailIds.Contains(o.Id));
            Delete(removedOrderDetails);
            //Update and add new
            orderDetails.ForEach(orderDetail =>
            {
                var currentOrderDetail = GetById(orderDetail.Id);
                if (currentOrderDetail != null)
                {
                    currentOrderDetail.ProductId = orderDetail.ProductId;
                    currentOrderDetail.UnitPrice = orderDetail.UnitPrice;
                    currentOrderDetail.Quantity = orderDetail.Quantity;
                    currentOrderDetail.Note = orderDetail.Note;
                    Update(currentOrderDetail);
                }
                else
                {
                    orderDetail.OrderId = orderId;
                    Insert(orderDetail);
                }
            });
        }
    }
}