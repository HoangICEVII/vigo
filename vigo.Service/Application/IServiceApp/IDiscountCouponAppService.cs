using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Helper;
using vigo.Service.DTO.Application.Discount;

namespace vigo.Service.Application.IServiceApp
{
    public interface IDiscountCouponAppService
    {
        Task<PagedResultCustom<DiscountCouponAppDTO>> GetPaging(int page, int perPage, string? searchName);
    }
}
