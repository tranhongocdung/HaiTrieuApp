using System.ComponentModel.DataAnnotations.Schema;

namespace MVCWeb.Cores.Entities
{
    public partial class PaymentHistory
    {
        [NotMapped]
        public string PaymentMethod => PaymentMethodId == Entities.PaymentMethod.Cash ? "Tiền mặt" : "Chuyển khoản";
    }
}