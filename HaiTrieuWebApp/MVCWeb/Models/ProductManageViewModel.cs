using System.Collections.Generic;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.Models
{
    public class ProductManageViewModel : BasePagingViewModel
    {
        public string Keyword { get; set; }
        public List<Product> Products { get; set; }
    }
}