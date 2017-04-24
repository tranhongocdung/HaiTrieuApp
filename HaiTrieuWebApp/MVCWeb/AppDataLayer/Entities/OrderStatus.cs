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

        //Enum
        public const int Pending = 0;
        public const int Completed = 1;
        public const int Cancelled = 2;
    }
}