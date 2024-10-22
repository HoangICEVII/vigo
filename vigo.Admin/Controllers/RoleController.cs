using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using vigo.Admin.Controllers.Base;
using vigo.Service.Admin.IService;
using vigo.Service.Admin.Service;
using vigo.Service.DTO;
using vigo.Service.DTO.Admin.Role;

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
                return Ok(data);
            }
            catch (Exception e)
            {
                return new ObjectResult(new { message = e.Message }) { StatusCode = 500 };
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(int indexPage)
        {
            try
            {
                var data = await _roleService.GetPaging(indexPage);
                return Ok(data);
            }
            catch (Exception e)
            {
                return new ObjectResult(new { message = e.Message }) { StatusCode = 500 };
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateDTO dto)
        {
            try
            {
                await _roleService.Create(dto);
                return Ok();
            }
            catch (Exception e)
            {
                return new ObjectResult(new { message = e.Message }) { StatusCode = 500 };
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(RoleUpdateDTO dto)
        {
            try
            {
                await _roleService.Update(dto);
                return Ok();
            }
            catch (Exception e)
            {
                return new ObjectResult(new { message = e.Message }) { StatusCode = 500 };
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _roleService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return new ObjectResult(new { message = e.Message }) { StatusCode = 500 };
            }
        }
    }
}
