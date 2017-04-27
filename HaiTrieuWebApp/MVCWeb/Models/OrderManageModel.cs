using System.Collections.Generic;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.Models
{
    public class OrderManageModel : BasePagingViewModel
    {
        public string Customer { get; set; }
        public string CreateOn { get; set; }
        public List<Order> Orders { get; set; }
    }
}