using System;
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
            Region = string.Empty;
            Area = string.Empty;
            Note = string.Empty;
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Region { get; set; }
        public string Area { get; set; }
        public string Note { get; set; }
        public DateTime CreatedOn { get; set; }

        public string SuggestName
        {
            get
            {
                return CustomerName +
                        (!string.IsNullOrEmpty(PhoneNo) ? " - " + PhoneNo : "") +
                        (!string.IsNullOrEmpty(Email) ? " - " + Email : "");
            }
        }
        public string SuggestNameFull
        {
            get
            {
                return CustomerName +
                       (!string.IsNullOrEmpty(PhoneNo) ? " - " + PhoneNo : "") +
                       (!string.IsNullOrEmpty(Email) ? " - " + Email : "") +
                       (!string.IsNullOrEmpty(Region) ? " - " + Region : "") +
                       (!string.IsNullOrEmpty(Area) ? " - " + Area : "");
            }
        }

        public string FullAddress
        {
            get
            {
                return Address +
                       (!string.IsNullOrEmpty(Region) ? " - " + Region : "") +
                       (!string.IsNullOrEmpty(Area) ? " - " + Area : "");
            }
        }
    }
}