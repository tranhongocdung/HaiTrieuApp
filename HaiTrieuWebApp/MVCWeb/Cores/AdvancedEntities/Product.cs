using System.Linq;

namespace MVCWeb.Cores.Entities
{
    public partial class Product
    {
        public string CategoryListForTableView
        {
            get
            {
                if (Categories.Any())
                {
                    return string.Join(", ", Categories.OrderBy(o => o.ParentId).Select(o => o.CategoryName));
                }
                return "";
            }
        }
    }
}