using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vigo.Controllers.Base;
using vigo.Domain.Helper;
using vigo.Service.Application.IServiceApp;
using static System.Runtime.InteropServices.JavaScript.JSType;
using vigo.Service.DTO;

namespace vigo.Controllers
{
    [Route("api/application/bookings")]
    [ApiController]
    public class BookingController : BaseController
    {
        private readonly IBookingAppService _bookingAppService;
        private readonly IConfiguration _configuration;
        public BookingController(IBookingAppService bookingAppService, IConfiguration configuration)
        {
            _bookingAppService = bookingAppService;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetPaging(int page, int perPage)
        {
            try
            {
                var data = await _bookingAppService.GetPaging(page, perPage, User);
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
