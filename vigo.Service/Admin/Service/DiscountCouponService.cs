using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using vigo.Service.Admin.IService;
using vigo.Service.DTO.Admin.Discount;

namespace vigo.Service.Admin.Service
{
    public class DiscountCouponService : IDiscountCouponService
    {
        public Task Create(CreateDiscountCouponDTO dto, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public Task GetDetail(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DiscountCouponDTO>> GetPaging()
        {
            throw new NotImplementedException();
        }
    }
}
