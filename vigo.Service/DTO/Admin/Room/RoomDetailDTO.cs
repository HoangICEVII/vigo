using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Service.DTO.Admin.Service;

namespace vigo.Service.DTO.Admin.Room
{
    public class RoomDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public int Days { get; set; }
        public decimal Price { get; set; }
        public int Avaiable { get; set; }
        public int RoomTypeId { get; set; }
        public int BusinessPartnerId { get; set; }
        public int DefaultDiscount { get; set; }
        public int Province { get; set; }
        public int District { get; set; }
        public int Street { get; set; }
        public string Address { get; set; } = string.Empty;
        public List<ServiceDTO> Services { get; set; } = new List<ServiceDTO>();
        public List<RoomImageDTO> Images { get; set; } = new List<RoomImageDTO>();
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}
