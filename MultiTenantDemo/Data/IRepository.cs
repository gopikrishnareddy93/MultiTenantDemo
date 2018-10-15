using System.Collections.Generic;
using MultiTenantDemo.Data.Model;
using MultiTenantDemo.Model;

namespace MultiTenantDemo.Data
{
    public interface IRepository
    {
        List<Device> GetAll(int page, int pageCount);
        APIUser GetUserByUsernameAndPassword(UserViewModel userViewModel);
    }
}
