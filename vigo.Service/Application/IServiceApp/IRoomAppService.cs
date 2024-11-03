using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Helper;
using vigo.Service.DTO.Admin.Room;
using vigo.Service.DTO.Application.Room;

namespace vigo.Service.Application.IServiceApp
{
    public interface IRoomAppService
    {
        Task<PagedResultCustom<ProvinceV2DTO>> GetPaging(int page, int perPage, int? roomTypeId, string? provinceId, string? districtId, int? star);
        Task<RoomDetailDTO> GetDetail(int id);
    }
}