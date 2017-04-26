using System.Linq;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.Models
{
    public class OrderPrintModel
    {
        //Customer
        public Customer Customer { get; set; }
        //Order
        public Order Order { get; set; }

        public int TotalCash => Order.OrderDetails.Sum(o => o.Quantity*o.UnitPrice);
        public int Discount => (Order.DiscountType == 0 ? (TotalCash * Order.DiscountValue /100) : Order.DiscountValue);
        public int FinalCash => (TotalCash - Discount);
    }
}