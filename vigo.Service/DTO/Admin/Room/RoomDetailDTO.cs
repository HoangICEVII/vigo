using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vigo.Service.DTO.Admin.Room
{
    public class RoomDetailDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public int ShowRoomId { get; set; }
        public int Days { get; set; }
        public decimal Price { get; set; }
        public int Avaiable { get; set; }
        public int RoomTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}
