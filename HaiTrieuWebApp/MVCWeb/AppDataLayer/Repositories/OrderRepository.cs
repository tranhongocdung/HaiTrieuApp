using System;
using System.Collections.Generic;
using System.Linq;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.AppDataLayer.Repositories
{
    public class OrderRepository
    {
        public static int Create(Order order)
        {
            order.CreatedOn = DateTime.Now;
            order.OrderStatusId = OrderStatus.Pending;
            var db = new DbAppContext();
            db.Orders.Add(order);
            db.SaveChanges();
            return order.Id;
        }
        public static bool Update(Order order)
        {
            var db = new DbAppContext();
            var currentOrder = db.Orders.FirstOrDefault(o => o.Id == order.Id);
            if (currentOrder == null) return false;
            currentOrder.CustomerId = order.CustomerId;
            currentOrder.Note = order.Note;
            currentOrder.DiscountValue = order.DiscountValue;
            currentOrder.DiscountType = order.DiscountType;
            db.SaveChanges();
            return true;
        }
        public static bool UpdateOrderDetail(List<OrderDetail> orderDetails, int orderId)
        {
            var db = new DbAppContext();
            //Delete
            var newOrderDetailIds = orderDetails.Where(o => o.Id != 0).Select(o => o.Id);
            var removedOrderDetails = db.OrderDetails.Where(o => !newOrderDetailIds.Contains(o.Id));
            db.OrderDetails.RemoveRange(removedOrderDetails);
            db.SaveChanges();
            //Update and add new
            orderDetails.ForEach(orderDetail =>
            {
                var currentOrderDetail = db.OrderDetails.FirstOrDefault(o => o.Id == orderDetail.Id);
                if (currentOrderDetail != null)
                {
                    currentOrderDetail.ProductId = orderDetail.ProductId;
                    currentOrderDetail.UnitPrice = orderDetail.UnitPrice;
                    currentOrderDetail.Quantity = orderDetail.Quantity;
                    currentOrderDetail.Note = orderDetail.Note;
                }
                else
                {
                    orderDetail.OrderId = orderId;
                    db.OrderDetails.Add(orderDetail);
                }
                db.SaveChanges();
            });
            return true;
        }

        public static Order GetWithCustomerAndOrderDetails(int id)
        {
            var db = new DbAppContext();
            return db.Orders.Include("Customer").Include("OrderDetails.Product").FirstOrDefault(o => o.Id == id);
        }
        public static Order Get(int id)
        {
            var db = new DbAppContext();
            return db.Orders.FirstOrDefault(o => o.Id == id);
        }
    }
}