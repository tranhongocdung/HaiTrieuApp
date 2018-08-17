using System.Collections.Generic;
using MVCWeb.Cores.Entities;

namespace MVCWeb.Models
{
    public class CategoryManageViewModel : CategoryEditViewModel
    {
        public string Keyword { get; set; }
        public List<Category> Categories { get; set; }
        public List<Category> ParentCategories { get; set; }
    }
}