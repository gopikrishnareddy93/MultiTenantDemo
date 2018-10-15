using System.ComponentModel.DataAnnotations;

namespace MultiTenantDemo.Model
{
    public class DeviceViewModel
    {
        [Required]
        public string Title { get; set; }
        
        public string DeviceCode { get; set; }
    }
}