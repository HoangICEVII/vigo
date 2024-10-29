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
using vigo.Service.Admin.IService;
using vigo.Service.DTO.Admin.Account;
using vigo.Service.DTO.Admin.Discount;

namespace vigo.Service.Admin.Service
{
    public class DiscountCouponService : IDiscountCouponService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkVigo _unitOfWorkVigo;

        public DiscountCouponService(IUnitOfWorkVigo unitOfWorkVigo, IMapper mapper)
        {
            _unitOfWorkVigo = unitOfWorkVigo;
            _mapper = mapper;
        }

        public async Task Create(CreateDiscountCouponDTO dto, ClaimsPrincipal user)
        {
            var DateNow = DateTime.Now;
            List<Expression<Func<DiscountCoupon, bool>>> conditions = new List<Expression<Func<DiscountCoupon, bool>>>()
            {
                e => e.DeletedDate == null
            };
            var count = (await _unitOfWorkVigo.DiscountCoupons.GetAll(conditions)).Count();
            DiscountCoupon data = new DiscountCoupon()
            {
                EndDate = dto.EndDate,
                StartDate = dto.StartDate,
                CreatedDate = DateNow,
                DeletedDate = null,
                Description = dto.Description,
                DiscountApply = dto.DiscountApply,
                DiscountCode = GenerateString.GenerateRandomString(6) + count,
                DiscountCount = 0,
                DiscountMax = dto.DiscountMax,
                DiscountType = dto.DiscountType,
                Image = dto.Image,
                Name = dto.Name,
                BusinessKey = "",
                UpdatedDate = DateNow,
                RoomApply = string.Join(",", dto.RoomApplyIds)
            };
            _unitOfWorkVigo.DiscountCoupons.Create(data);
            await _unitOfWorkVigo.Complete();
        }

        public async Task Delete(int id, ClaimsPrincipal user)
        {
            var DateNow = DateTime.Now;
            var data = await _unitOfWorkVigo.DiscountCoupons.GetById(id);
            data.DeletedDate = DateNow;
            await _unitOfWorkVigo.Complete();
        }

        public async Task<DiscountCouponDetailDTO> GetDetail(int id, ClaimsPrincipal user)
        {
            var data = await _unitOfWorkVigo.DiscountCoupons.GetById(id);
            return _mapper.Map<DiscountCouponDetailDTO>(data);
        }

        public async Task<PagedResultCustom<DiscountCouponDTO>> GetPaging(int page, int perPage, string? sortType, string? sortField, ClaimsPrincipal user)
        {
            List<Expression<Func<DiscountCoupon, bool>>> conditions = new List<Expression<Func<DiscountCoupon, bool>>>()
            {
                e => e.DeletedDate == null,
            };
            bool sortDown = false;
            if (sortType != null && sortType.Equals("DESC"))
            {
                sortDown = true;
            }
            Expression<Func<DiscountCoupon, DateTime>>? func = null;
            if (sortField != null && sortField.Equals("startDate"))
            {
                func = e => e.StartDate;
            }
            if (sortField != null && sortField.Equals("endDate"))
            {
                func = e => e.EndDate;
            }
            var data = await _unitOfWorkVigo.DiscountCoupons.GetPaging(conditions,
                                                                       null,
                                                                       null,
                                                                       func,
                                                                       page,
                                                                       perPage,
                                                                       sortDown);
            return new PagedResultCustom<DiscountCouponDTO>(_mapper.Map<List<DiscountCouponDTO>>(data.Items), data.TotalRecords, data.PageIndex, data.PageSize);
        }

        public async Task Update(DiscountCouponUpdateDTO dto, ClaimsPrincipal user)
        {
            var data = await _unitOfWorkVigo.DiscountCoupons.GetById(dto.Id);
            data.DiscountMax = dto.DiscountMax;
            data.DiscountApply = dto.DiscountApply;
            data.RoomApply = string.Join(",", dto.RoomApplyIds);
            data.StartDate = dto.StartDate;
            data.EndDate = dto.EndDate;
            data.UpdatedDate = DateTime.Now;
            data.Description = dto.Description;
            data.Image = dto.Image;
            data.DiscountType = dto.DiscountType;
            data.Name = dto.Name;
            await _unitOfWorkVigo.Complete();
        }
    }
}
