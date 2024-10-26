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
        public int ShowRoomId { get; set; }
        public int Days { get; set; }
        public decimal Price { get; set; }
        public int Avaiable { get; set; }
        public int RoomTypeId { get; set; }
        public List<int> Services { get; set; } = new List<int>();
    }
}
