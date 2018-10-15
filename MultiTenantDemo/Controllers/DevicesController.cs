using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiTenantDemo.ActionFilters;
using MultiTenantDemo.Data;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MultiTenantDemo.Controllers
{
    [Route("api/v{version:apiVersion}/devices")]
    [Authorize]
    public class DevicesController : Controller
    {
        private readonly IRepository _mRepo;
        
        public DevicesController(IRepository repo)
        {
           _mRepo = repo;
        }
        
        [HttpGet]
        [SwaggerOperation("GetDevices")]
        [ValidateActionParameters]
        public IActionResult Get([FromQuery, Required]int page, [FromQuery, Required]int pageSize)
        {
            return new ObjectResult(_mRepo.GetAll(page, pageSize));
        }

       
    }
}
