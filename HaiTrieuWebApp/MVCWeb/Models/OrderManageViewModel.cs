using System.Collections.Generic;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.Models
{
    public class OrderManageViewModel : BasePagingViewModel
    {
        public string CustomerIds { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<Order> Orders { get; set; }
    }
}