using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Service.DTO.Admin.Account;
using vigo.Service.DTO.Admin.Booking;

namespace vigo.Service.Admin.IService
{
    public interface IBookingService
    {
        Task<List<BookingDTO>> GetPaging();
        Task ReceiveBooking();
    }
}
