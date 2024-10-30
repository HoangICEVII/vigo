using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Helper;
using vigo.Service.DTO.Admin.Room;

namespace vigo.Service.Application.IServiceApp
{
    public interface IRoomAppService
    {
        Task<PagedResultCustom<RoomDTO>> GetPaging(int page, int perPage, int? roomTypeId, string? sortType, string? sortField, string? searchName);
        Task<RoomDetailDTO> GetDetail(int id);
    }
}