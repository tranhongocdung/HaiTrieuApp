using System;

namespace MVCWeb.Cores.Entities
{
    public partial class PaymentHistory
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Value { get; set; }
        public byte PaymentMethodId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Note { get; set; }
        public virtual Order Order { get; set; }
    }
}