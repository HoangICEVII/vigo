using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Helper;
using vigo.Service.DTO.Application.Booking;

namespace vigo.Service.Application.IServiceApp
{
    public interface IBookingAppService
    {
        Task<PagedResultCustom<BookingAppDTO>> GetPaging(int page, int perPage, ClaimsPrincipal user);
    }
}
