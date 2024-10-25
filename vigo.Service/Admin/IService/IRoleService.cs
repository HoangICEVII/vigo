using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Helper;
using vigo.Service.DTO.Admin.Role;

namespace vigo.Service.Admin.IService
{
    public interface IRoleService
    {
        Task<PagedResultCustom<RoleDTO>> GetPaging(int page, int perPage, bool? createDateSort, string? searchName);
        Task<List<RoleDTO>> GetAll();
        Task<RoleDetailDTO> GetDetail(int id);
        Task<List<RolePermissionDTO>> GetPermission();
        Task Create(RoleCreateDTO dto);
        Task Update(RoleUpdateDTO dto);
        Task Delete(int id);
    }
}
