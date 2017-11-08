using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCWeb.AppDataLayer.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }    

    }
}