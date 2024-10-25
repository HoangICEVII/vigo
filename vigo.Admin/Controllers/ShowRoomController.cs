using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vigo.Admin.Controllers.Base;
using vigo.Domain.Helper;
using vigo.Service.Admin.IService;
using vigo.Service.DTO.Admin.Room;
using vigo.Service.DTO;
using System.Security.Claims;
using vigo.Service.DTO.Admin.ShowRoom;

namespace vigo.Admin.Controllers
{
    [Route("api/admin/showrooms")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ShowRoomController : BaseController
    {
        private readonly IShowRoomService _showRoomService;
        public ShowRoomController(IShowRoomService showRoomService)
        {
            _showRoomService = showRoomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaging(int page, int perPage, int? provinceId, int? districtId, string sortType, string sortField, string? searchName)
        {
            try
            {
                var data = await _showRoomService.GetPaging(page, perPage, provinceId, districtId, sortType, sortField, searchName, User);
                Option options = new Option()
                {
                    Name = "",
                    Page = data.PageIndex,
                    PageSize = data.PageSize,
                    TotalRecords = data.TotalRecords
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
                var data = await _showRoomService.GetDetail(id);
                return CreateResponse(data, "get success", 200, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return CreateResponse(null, "get fail", 500, null);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateShowRoomDTO dto)
        {
            try
            {
                await _showRoomService.Create(dto);
                return CreateResponse(null, "create success", 200, null);
            }
            catch (CustomException e)
            {
                return CreateResponse(null, e.Message, 500, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return CreateResponse(null, "create fail", 500, null);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateShowRoomDTO dto)
        {
            try
            {
                await _showRoomService.Update(dto);
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
                await _showRoomService.Delete(id);
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
