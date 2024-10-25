using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using vigo.Admin.Controllers.Base;
using vigo.Service.Admin.IService;
using vigo.Service.Admin.Service;
using vigo.Service.DTO;
using vigo.Service.DTO.Admin.Role;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace vigo.Admin.Controllers
{
    [Route("api/admin/roles")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("permission")]
        public async Task<IActionResult> GetPermission()
        {
            try
            {
                var data = await _roleService.GetPermission();
                return CreateResponse(data, "get success", 200, null);
            }
            catch (Exception e)
            {
                return CreateResponse(null, "get fail", 500, null);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPaging(int page, int perPage)
        {
            try
            {
                var data = await _roleService.GetPaging(page, perPage);
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
                return CreateResponse(null, "get fail", 500, null);
            }
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _roleService.GetAll();
                return CreateResponse(data, "get success", 200, null);
            }
            catch (Exception e)
            {
                return CreateResponse(null, "get fail", 500, null);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateDTO dto)
        {
            try
            {
                await _roleService.Create(dto);
                return CreateResponse(null, "create success", 200, null);
            }
            catch (Exception e)
            {
                return CreateResponse(null, "create fail", 500, null);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(RoleUpdateDTO dto)
        {
            try
            {
                await _roleService.Update(dto);
                return CreateResponse(null, "update success", 200, null);
            }
            catch (Exception e)
            {
                return CreateResponse(null, "update fail", 500, null);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _roleService.Delete(id);
                return CreateResponse(null, "delete success", 200, null);
            }
            catch (Exception e)
            {
                return CreateResponse(null, "delete fail", 500, null);
            }
        }
    }
}
