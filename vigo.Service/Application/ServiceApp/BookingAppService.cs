using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Entity;
using vigo.Domain.Helper;
using vigo.Domain.Interface.IUnitOfWork;
using vigo.Domain.User;
using vigo.Service.Application.IServiceApp;
using vigo.Service.DTO.Admin.Account;
using vigo.Service.DTO.Application.Booking;

namespace vigo.Service.Application.ServiceApp
{
    public class BookingAppService : IBookingAppService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkVigo _unitOfWorkVigo;

        public BookingAppService(IUnitOfWorkVigo unitOfWorkVigo, IMapper mapper)
        {
            _unitOfWorkVigo = unitOfWorkVigo;
            _mapper = mapper;
        }

        public async Task<PagedResultCustom<BookingAppDTO>> Booking(int page, int perPage)
        {
            var data = await _unitOfWorkVigo.Bookings.GetPaging(null,
                                                                null,
                                                                null,
                                                                e => e.CreatedDate,
                                                                page,
                                                                perPage,
                                                                true);
            var result = new PagedResultCustom<BookingAppDTO>(_mapper.Map<List<BookingAppDTO>>(data.Items), data.TotalRecords, data.PageIndex, data.PageSize);
            return result;
        }
    }
}
