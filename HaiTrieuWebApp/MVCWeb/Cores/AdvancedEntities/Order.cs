﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MVCWeb.Cores.Entities
{
    public partial class Order
    {
        [NotMapped]
        public string DiscountString
            => DiscountValue != 0 ? DiscountValue.ToString("#,##0") + (DiscountType == 0 ? "%" : "") : "";

        [NotMapped]
        public decimal TotalCash => OrderDetails != null ? OrderDetails.Sum(o => o.Quantity * o.UnitPrice) : 0;
        [NotMapped]
        public decimal RealCash
            => TotalCash - (DiscountValue != 0 ? (DiscountType == 0 ? TotalCash * DiscountValue / 100 : DiscountValue) : 0);
        [NotMapped]
        public decimal RemainingCash => RealCash - PaidCash;
        [NotMapped]
        public decimal PaidCash => PaymentHistories?.Sum(o => o.Value) ?? 0;
    }
}