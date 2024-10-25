using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vigo.Admin.Controllers.Base;
using vigo.Service.Admin.IService;
using vigo.Service.Admin.Service;
using vigo.Service.DTO;
using vigo.Service.DTO.Admin.Discount;
using vigo.Service.DTO.Admin.Role;

namespace vigo.Admin.Controllers
{
    [Route("api/admin/discount-coupons")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DiscountController : BaseController
    {
        private readonly IDiscountCouponService _discountCouponService;
        private readonly IConfiguration _configuration;
        public DiscountController(IDiscountCouponService discountCouponService, IConfiguration configuration)
        {
            _discountCouponService = discountCouponService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaging(int page, int perPage, int showRoomId, bool? startDateSort, bool? endDateSort)
        {
            try
            {
                var data = await _discountCouponService.GetPaging(page, perPage, showRoomId, startDateSort, endDateSort, User);
                Option options = new Option()
                {
                    Name = "",
                    Page = data.PageIndex,
                    TotalPage = data.TotalPages
                };
                return CreateResponse(data.Items, "get success", 200, options);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return CreateResponse(null, "get fail", 500, null);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            try
            {
                var data = await _discountCouponService.GetDetail(id, User);
                return CreateResponse(data, "get success", 200, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return CreateResponse(null, "get fail", 500, null);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDiscountCouponDTO dto)
        {
            try
            {
                await _discountCouponService.Create(dto, User);
                return CreateResponse(null, "create success", 200, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return CreateResponse(null, "create fail", 500, null);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(DiscountCouponUpdateDTO dto)
        {
            try
            {
                await _discountCouponService.Update(dto, User);
                return CreateResponse(null, "update success", 200, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return CreateResponse(null, "update fail", 500, null);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _discountCouponService.Delete(id, User);
                return CreateResponse(null, "delete success", 200, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return CreateResponse(null, "delete fail", 500, null);
            }
        }
    }
}
