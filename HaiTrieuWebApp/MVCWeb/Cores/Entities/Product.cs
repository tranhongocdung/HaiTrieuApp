using System.Collections.Generic;

namespace MVCWeb.Cores.Entities
{
    public partial class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int UnitPrice { get; set; }
        public int OriginalPrice { get; set; }
        public string ShortDescription { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}