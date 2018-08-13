using System.Collections.Generic;

namespace MVCWeb.Cores.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}