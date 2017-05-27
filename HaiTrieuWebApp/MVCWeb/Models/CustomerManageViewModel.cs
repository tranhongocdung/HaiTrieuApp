using System.Collections.Generic;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.Models
{
    public class CustomerManageViewModel : BasePagingViewModel
    {
        public string Keyword { get; set; }
        public List<Customer> Customers { get; set; }
    }
}