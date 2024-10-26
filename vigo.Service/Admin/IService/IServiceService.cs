using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Helper;
using vigo.Service.DTO.Admin.Service;
using vigo.Service.DTO.Admin.ShowRoom;

namespace vigo.Service.Admin.IService
{
    public interface IServiceService
    {
        Task<PagedResultCustom<ServiceDTO>> GetPaging(int page, int perPage, string? sortType, string? sortField, string? searchName, ClaimsPrincipal user);
        Task<List<ServiceDTO>> GetAll(ClaimsPrincipal user);
        Task<ServiceDTO> GetDetail(int id);
        Task Create(ServiceCreateDTO dto);
        Task Update(ServiceUpdateDTO dto);
        Task Delete(int id);
    }
}
