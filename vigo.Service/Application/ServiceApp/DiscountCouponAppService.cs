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
using vigo.Service.DTO.Application.Discount;

namespace vigo.Service.Application.ServiceApp
{
    public class DiscountCouponAppService : IDiscountCouponAppService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkVigo _unitOfWorkVigo;

        public DiscountCouponAppService(IUnitOfWorkVigo unitOfWorkVigo, IMapper mapper)
        {
            _unitOfWorkVigo = unitOfWorkVigo;
            _mapper = mapper;
        }
        public async Task<PagedResultCustom<DiscountCouponAppDTO>> GetPaging(int page, int perPage, string? searchName, ClaimsPrincipal user)
        {
            int? infoId = user.FindFirst("InfoId") != null ? int.Parse(user.FindFirst("InfoId")!.Value) : null;
            List<Expression<Func<DiscountCoupon, bool>>> conditions = new List<Expression<Func<DiscountCoupon, bool>>>()
            {
                e => e.DeletedDate == null
            };
            if (searchName != null)
            {
                conditions.Add(e => e.Name.ToLower().Contains(searchName.ToLower()));
            }
            var data = await _unitOfWorkVigo.DiscountCoupons.GetPaging(conditions,
                                                                       null,
                                                                       null,
                                                                       e => e.CreatedDate,
                                                                       page,
                                                                       perPage,
                                                                       true);
            var result = new List<DiscountCouponAppDTO>();
            foreach (var item in data.Items) {
                result.Add(new DiscountCouponAppDTO {
                    Description = item.Description,
                    DiscountCode = item.DiscountCode,
                    DiscountCount = item.DiscountCount,
                    DiscountMax = item.DiscountMax,
                    DiscountType = item.DiscountType,
                    DiscountValue = item.DiscountValue,
                    EndDate = item.EndDate,
                    Id = item.Id,
                    Image = item.Image,
                    Name = item.Name,
                    StartDate = item.StartDate,
                    IsReceived = item.UserUsed.Split(',').Contains(infoId.ToString())
                });
            }
            return new PagedResultCustom<DiscountCouponAppDTO>(result, data.TotalRecords, data.PageIndex, data.PageSize);
        }
    }
}
