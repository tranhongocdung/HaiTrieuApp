using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MVCWeb.AppDataLayer.Entities
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public int DiscountType { get; set; }
        public int DiscountValue { get; set; }
        public int OrderStatusId { get; set; }
        public int CompletedRealCash { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("OrderStatusId")]
        public OrderStatus OrderStatus { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [NotMapped]
        public string DiscountString
        {
            get { return DiscountValue != 0 ? DiscountValue.ToString("#,##0") + (DiscountType == 0 ? "%" : "") : ""; }
        }
        [NotMapped]
        public int TotalCash
        {
            get { return OrderDetails != null ? OrderDetails.Sum(o => o.Quantity*o.UnitPrice) : 0; }
        }
        [NotMapped]
        public int RealCash
        {
            get
            {
                return TotalCash - (DiscountValue != 0 ? (DiscountType == 0 ? TotalCash * DiscountValue / 100 : DiscountValue) : 0);
            }
        }
    }
}