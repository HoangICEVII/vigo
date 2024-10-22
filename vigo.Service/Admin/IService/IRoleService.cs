using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Service.DTO.Admin.Role;

namespace vigo.Service.Admin.IService
{
    public interface IRoleService
    {
        Task<List<RoleDTO>> GetPaging(int indexPage);
        Task<List<string>> GetPermission();
        Task Create(RoleCreateDTO dto);
        Task Update(RoleUpdateDTO dto);
        Task Delete(int id);
    }
}
