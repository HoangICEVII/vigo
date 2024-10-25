using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Service.Admin.IService;
using vigo.Service.DTO.Admin.Booking;

namespace vigo.Service.Admin.Service
{
    public class BookingService : IBookingService
    {
        public Task<List<BookingDTO>> GetPaging()
        {
            throw new NotImplementedException();
        }

        public Task ReceiveBooking()
        {
            throw new NotImplementedException();
        }
    }
}
