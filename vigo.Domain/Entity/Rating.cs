using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vigo.Domain.Entity
{
    public class Rating
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public int RoomId { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public string UpdateComment { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public bool Status { get; set; }
    }
}
