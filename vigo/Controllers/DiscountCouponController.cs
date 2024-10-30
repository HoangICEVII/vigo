using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vigo.Controllers.Base;
using vigo.Domain.Helper;
using vigo.Service.Application.IServiceApp;
using vigo.Service.Application.ServiceApp;
using static System.Runtime.InteropServices.JavaScript.JSType;
using vigo.Service.DTO;

namespace vigo.Controllers
{
    [Route("api/application/discount-coupons")]
    [ApiController]
    public class DiscountCouponController : BaseController
    {
        private readonly IDiscountCouponAppService _discountCouponAppService;
        private readonly IConfiguration _configuration;
        public DiscountCouponController(IDiscountCouponAppService discountCouponAppService, IConfiguration configuration)
        {
            _discountCouponAppService = discountCouponAppService;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetPaging(int page, int perPage, string? searchName)
        {
            try
            {
                var data = await _discountCouponAppService.GetPaging(page, perPage, searchName, User);
                Option options = new Option()
                {
                    Name = "",
                    PageSize = data.PageSize,
                    Page = data.PageIndex,
                    TotalRecords = data.TotalRecords
                };
                return CreateResponse(data.Items, "get success", 200, options);
            }
            catch (CustomException e)
            {
                return CreateResponse(null, e.Message, 500, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return CreateResponse(null, "get fail", 500, null);
            }
        }
    }
}
