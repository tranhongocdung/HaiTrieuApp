﻿namespace MVCWeb.Cores.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public string Note { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}