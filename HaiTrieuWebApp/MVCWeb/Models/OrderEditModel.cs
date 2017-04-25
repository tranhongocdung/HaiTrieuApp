using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.Models
{
    public class OrderEditModel
    {
        //Customer
        public Customer Customer { get; set; }
        //Order
        public Order Order { get; set; }
        public string OrderDetailJson { get; set; }
    }
}