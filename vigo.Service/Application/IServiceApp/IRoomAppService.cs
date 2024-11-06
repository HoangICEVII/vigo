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
        Task<ProvinceV2DTO> GetPaging(int page, int perPage, int? roomTypeId, string provinceId, string? districtId, DateTime checkIn, DateTime checkOut, List<int>? stars, List<int>? services, decimal? price);
        Task<List<RoomAppDTO>> GetRelatedRoom(int businessPartnerId);
        Task<RoomDetailDTO> GetDetail(int id);
        Task<PriceRangeDTO> GetPriceRange();
    }
}