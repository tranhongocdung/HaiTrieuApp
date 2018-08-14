using System.Collections.Generic;
using MVCWeb.Cores.Entities;

namespace MVCWeb.Models
{
    public class CategoryManageViewModel : BasePagingViewModel
    {
        public string Keyword { get; set; }
        public List<Category> Categories { get; set; }
    }
}