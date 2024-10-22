using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vigo.Domain.Entity
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int RoomId { get; set; }
        public int ImageTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}
