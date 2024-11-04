using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vigo.Service.DTO.Application.Room
{
    public class GetRoomDTO
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int? RoomTypeId { get; set; }
        public string ProvinceId { get; set; } = string.Empty;
        public string? DistrictId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public List<int> Stars { get; set; } = new List<int>();
    }
}
