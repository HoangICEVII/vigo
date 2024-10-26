using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Helper;
using vigo.Service.DTO.Admin.Room;
using vigo.Service.DTO.Admin.Service;
using vigo.Service.DTO.Admin.ShowRoom;

namespace vigo.Service.Admin.IService
{
    public interface IRoomTypeService
    {
        Task<PagedResultCustom<RoomTypeDTO>> GetPaging(int page, int perPage, string? sortType, string? sortField, string? searchName, ClaimsPrincipal user);
        Task<List<RoomTypeDTO>> GetAll(ClaimsPrincipal user);
        Task<RoomTypeDTO> GetDetail(int id);
        Task Create(RoomTypeCreateDTO dto);
        Task Update(RoomTypeUpdateDTO dto);
        Task Delete(int id);
    }
}
