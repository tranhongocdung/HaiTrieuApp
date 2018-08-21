using System.Collections.Generic;
using MVCWeb.Cores.Entities;

namespace MVCWeb.Models
{
    public class CategoryEditViewModel
    {
        public Category Category { get; set; }
        public List<Category> ParentCategories { get; set; }
        public bool IsParentCategoryDisabled { get; set; }
        public bool IsDeletionDisabled { get; set; }
    }
}