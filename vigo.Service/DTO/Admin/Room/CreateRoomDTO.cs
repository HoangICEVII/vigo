using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vigo.Service.DTO.Admin.Room
{
    public class CreateRoomDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public int DefaultDiscount { get; set; }
        public List<RoomImage> Images { get; set; } = new List<RoomImage>();
        public int Days { get; set; }
        public decimal Price { get; set; }
        public int Avaiable { get; set; }
        public string Province { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int RoomTypeId { get; set; }
        public int BusinessPartnerId { get; set; }
        public List<int> Services { get; set; } = new List<int>();
    }

    public class RoomImage
    {
        public string Type { get; set; } = string.Empty;
        public List<string> Urls { get; set; } = new List<string>();
    }
}
