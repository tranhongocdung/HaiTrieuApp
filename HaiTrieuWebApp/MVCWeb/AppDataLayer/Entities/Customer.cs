using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCWeb.AppDataLayer.Entities
{
    [Table("Customer")]
    public class Customer
    {
        public Customer()
        {
            CustomerName = string.Empty;
            PhoneNo = string.Empty;
            Email = string.Empty;
            Address = string.Empty;
            District = string.Empty;
            City = string.Empty;
            Note = string.Empty;
        }
        [Key]
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Note { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}