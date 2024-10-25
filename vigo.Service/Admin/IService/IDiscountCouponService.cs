using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using vigo.Service.DTO.Admin.Account;
using vigo.Service.DTO.Admin.Discount;

namespace vigo.Service.Admin.IService
{
    public interface IDiscountCouponService
    {
        Task<List<DiscountCouponDTO>> GetPaging();
        Task Create(CreateDiscountCouponDTO dto, ClaimsPrincipal user);
        Task GetDetail(int id);
    }
}
