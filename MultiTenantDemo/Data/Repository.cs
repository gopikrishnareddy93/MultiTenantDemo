using System.Collections.Generic;
using System.Linq;
using MultiTenantDemo.Data.Model;
using MultiTenantDemo.Model;

namespace MultiTenantDemo.Data
{
    public class Repository : IRepository
    {
        private readonly DeviceContext m_Context;

        public Repository(DeviceContext context)
        {
            m_Context = context;
        }

        public List<Device> GetAll(int page, int pageCount)
        {
            return m_Context.Devices.Skip((page - 1) * pageCount).Take(pageCount).ToList();
        }

        public APIUser GetUserByUsernameAndPassword(UserViewModel userViewModel)
        {
            if (userViewModel == null || string.IsNullOrWhiteSpace(userViewModel.Username) || string.IsNullOrWhiteSpace(userViewModel.Password))
            {
                return null;
            }

            return  m_Context.APIUsers.FirstOrDefault(x => x.Username == userViewModel.Username && x.Password == userViewModel.Password);

        }
    }
}
