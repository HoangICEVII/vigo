using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vigo.Controllers.Base;
using vigo.Domain.Helper;
using vigo.Service.Application.IServiceApp;
using vigo.Service.Application.ServiceApp;
using vigo.Service.DTO;

namespace vigo.Controllers
{
    [Route("api/application/rooms")]
    [ApiController]
    public class RoomController : BaseController
    {
        private readonly IRoomAppService _roomAppService;
        private readonly IConfiguration _configuration;
        public RoomController(IRoomAppService roomAppService, IConfiguration configuration)
        {
            _roomAppService = roomAppService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaging(int page, int perPage, int? roomTypeId, string? sortType, string? sortField, string? searchName)
        {
            try
            {
                var data = await _roomAppService.GetPaging(page, perPage, roomTypeId, sortType, sortField, searchName);
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
