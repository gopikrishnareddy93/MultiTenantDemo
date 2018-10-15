using System;

namespace MultiTenantDemo.Data.Model
{  
    public class APIUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public string Token { get; set; }
        public string LoginState { get; set; }
    }
}
