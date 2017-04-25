using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCWeb.AppDataLayer.Entities
{
    [Table("OrderStatus")]
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }
        public string Label { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        //Enum
        public const int Pending = 1;
        public const int Completed = 2;
        public const int Cancelled = 3;
    }
}