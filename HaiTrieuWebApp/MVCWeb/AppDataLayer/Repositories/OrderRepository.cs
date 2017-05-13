using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using MVCWeb.AppDataLayer.Entities;
using MVCWeb.AppDataLayer.IRepositories;

namespace MVCWeb.AppDataLayer.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(DbAppContext context) : base(context) {}
        public int Create(Order order)
        {
            order.CreatedOn = DateTime.Now;
            order.OrderStatusId = OrderStatus.Pending;
            Insert(order);
            return order.Id;
        }
        public bool UpdateOrder(Order order)
        {
            var currentOrder = GetById(order.Id);
            if (currentOrder == null) return false;
            currentOrder.CustomerId = order.CustomerId;
            currentOrder.Note = order.Note;
            currentOrder.DiscountValue = order.DiscountValue;
            currentOrder.DiscountType = order.DiscountType;
            Update(order);
            return true;
        }

        public void CompleteOrder(int orderId)
        {
            var order = GetWithOrderDetails(orderId);
            if (order == null) return;
            order.OrderStatusId = OrderStatus.Completed;
            var totalCash = order.OrderDetails.Sum(o => o.UnitPrice*o.Quantity);
            var realCash = totalCash -
                           (order.DiscountType == 0 ? totalCash*order.DiscountValue/100 : order.DiscountValue);
            order.CompletedRealCash = realCash;
            Update(order);
        }

        public Order GetWithCustomerAndOrderDetails(int id)
        {
            return
                TableNoTracking.Include(o => o.Customer)
                    .Include(o => o.OrderDetails.Select(p => p.Product))
                    .FirstOrDefault(o => o.Id == id);
        }

        public Order GetWithOrderDetails(int id)
        {
            return
                Table.Include(o => o.OrderDetails).FirstOrDefault(o => o.Id == id);
        }

        public List<Order> GetList(FilterParams fp, ref int totalCount)
        {
            var list = TableNoTracking.Include(o => o.Customer)
                .Include(o => o.OrderDetails.Select(p => p.Product));
            if (fp.StatusId != 0)
            {
                list = list.Where(o => o.OrderStatusId == fp.StatusId);
            }
            if (!string.IsNullOrEmpty(fp.FromDate))
            {
                var fromDate = DateTime.ParseExact(fp.FromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                list = list.Where(o => o.CreatedOn >= fromDate);
            }
            if (!string.IsNullOrEmpty(fp.ToDate))
            {
                var toDate = DateTime.ParseExact(fp.ToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).AddDays(1);
                list = list.Where(o => o.CreatedOn < toDate);
            }
            if (fp.CustomerIds.Any())
            {
                list = list.Where(o => fp.CustomerIds.Contains(o.CustomerId));
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