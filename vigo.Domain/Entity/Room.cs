using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vigo.Domain.Entity
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public int Days { get; set; }
        public decimal Price { get; set; }
        public int Avaiable {  get; set; }
        public int RoomTypeId { get; set; }
        public int BusinessPartnerId { get; set; }
        public int DefaultDiscount { get; set; }
        public string Province {  get; set; } = string.Empty;
        public string District {  get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
