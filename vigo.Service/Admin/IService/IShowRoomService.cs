using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Helper;
using vigo.Service.DTO.Admin.ShowRoom;

namespace vigo.Service.Admin.IService
{
    public interface IShowRoomService
    {
        Task<PagedResultCustom<ShowRoomDTO>> GetPaging(int page, int perPage, int? provinceId, int? districtId, string sortType, string sortField, string? searchName, ClaimsPrincipal user);
        Task<ShowRoomDetailDTO> GetDetail(int id);
        Task Create(CreateShowRoomDTO dto);
        Task Update(UpdateShowRoomDTO dto);
        Task Delete(int id);
    }
}
