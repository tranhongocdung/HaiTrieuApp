using System.Collections.Generic;

namespace MVCWeb.Cores.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentId { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}