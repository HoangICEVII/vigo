using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vigo.Service.DTO.Application.Booking
{
    public class BookingAppDTO
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int RoomId { get; set; }
        public decimal Price { get; set; }
        public string DiscountCode { get; set; } = string.Empty;
        public decimal DiscountPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsReceived { get; set; }
    }
}
