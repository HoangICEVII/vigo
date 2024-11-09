using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Entity;
using vigo.Domain.Helper;
using vigo.Domain.Interface.IUnitOfWork;
using vigo.Domain.User;
using vigo.Service.Application.IServiceApp;
using vigo.Service.DTO.Admin.Account;
using vigo.Service.DTO.Application;
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

        public async Task<List<BookingBankDataDTO>> Book(CreateBookDTO dto, ClaimsPrincipal user)
        {
            int infoId = int.Parse(user.FindFirst("InfoId")!.Value);
            DiscountCoupon coupon = await _unitOfWorkVigo.DiscountCoupons.GetById(dto.CouponId);
            var room = await _unitOfWorkVigo.Rooms.GetById(dto.RoomId);
            decimal price = room.Price * (dto.CheckOutDate - dto.CheckInDate).Days;
            decimal discountPrice = 0;
            if (coupon.DiscountType.ToString().Equals("fixed_amount"))
            {
                discountPrice = coupon.DiscountValue;
            }
            if (coupon.DiscountType.ToString().Equals("percentage"))
            {
                discountPrice = coupon.DiscountValue * price / 100;
            }
            Booking data = new Booking()
            {
                Approved = false,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                CreatedDate = DateTime.Now,
                DiscountCode = coupon.DiscountCode,
                DiscountPrice = discountPrice,
                RoomId = room.Id,
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                Price = price,
                TotalPrice = price - discountPrice,
                TouristId = infoId
            };
            _unitOfWorkVigo.Bookings.Create(data);
            await _unitOfWorkVigo.Complete();

            List<BookingBankDataDTO> result = new List<BookingBankDataDTO>();
            List<Expression<Func<BusinessPartnerBank, bool>>> conditions = new List<Expression<Func<BusinessPartnerBank, bool>>>()
            {
                e => e.BusinessPartnerId == room.BusinessPartnerId
            };
            var businessBank = await _unitOfWorkVigo.BusinessPartnerBanks.GetAll(conditions);

            foreach (var item in businessBank) {
                var bank = await _unitOfWorkVigo.Banks.GetById(item.BankId);
                result.Add(new BookingBankDataDTO()
                {
                    BankName = bank.Name,
                    BankNumber = item.BankNumber,
                    Logo = bank.Logo,
                    OwnerName = item.OwnerName,
                    QRURL = $"https://img.vietqr.io/image/{bank.Code}-{item.BankNumber}-compact.png"
                });
            }

            return result;
        }

        public async Task<PagedResultCustom<BookingAppDTO>> GetPaging(int page, int perPage, ClaimsPrincipal user)
        {
            int infoId = int.Parse(user.FindFirst("InfoId")!.Value);
            List<Expression<Func<Booking, bool>>> conditions = new List<Expression<Func<Booking, bool>>>()
            {
                e => e.TouristId == infoId
            };
            var data = await _unitOfWorkVigo.Bookings.GetPaging(conditions,
                                                                null,
                                                                null,
                                                                e => e.CreatedDate,
                                                                page,
                                                                perPage,
                                                                true);
            return new PagedResultCustom<BookingAppDTO>(_mapper.Map<List<BookingAppDTO>>(data.Items), data.TotalRecords, data.PageIndex, data.PageSize);
        }

        public async Task<decimal> GetPrice(int roomId, int couponId, DateTime checkInDate, DateTime checkOutDate, ClaimsPrincipal user)
        {
            int infoId = int.Parse(user.FindFirst("InfoId")!.Value);
            var room = await _unitOfWorkVigo.Rooms.GetById(roomId);
            var coupon = await _unitOfWorkVigo.DiscountCoupons.GetById(couponId);
            decimal price = room.Price * (checkOutDate-checkInDate).Days;
            if (coupon.UserUsed.Split(",").Contains(infoId.ToString()))
            {
                throw new CustomException("phiếu giảm giá đã được sử dụng");
            }
            if (coupon.DiscountType.ToString().Equals("fixed_amount"))
            {
                price -= coupon.DiscountValue;
            }
            if (coupon.DiscountType.ToString().Equals("percentage"))
            {
                price -= coupon.DiscountValue * price / 100;
            }
            return price;
        }
    }
}
