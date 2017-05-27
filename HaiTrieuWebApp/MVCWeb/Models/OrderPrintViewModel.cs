using System.Linq;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.Models
{
    public class OrderPrintViewModel
    {
        //Customer
        public Customer Customer { get; set; }
        //Order
        public Order Order { get; set; }

        public int TotalCash
        {
            get { return Order.OrderDetails.Sum(o => o.Quantity*o.UnitPrice); }
        }

        public int Discount
        {
            get { return (Order.DiscountType == 0 ? (TotalCash*Order.DiscountValue/100) : Order.DiscountValue); }
        }

        public int FinalCash
        {
            get
            {
                return (TotalCash - Discount);
            }
        }
    }
}