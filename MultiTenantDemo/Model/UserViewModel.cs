using System.ComponentModel.DataAnnotations;

namespace MultiTenantDemo.Model
{
    public class UserViewModel
    {  
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
