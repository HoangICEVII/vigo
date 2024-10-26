using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using vigo.Admin.Controllers.Base;
using vigo.Domain.AccountFolder;
using vigo.Domain.Helper;
using vigo.Service.Admin.IService;
using vigo.Service.Admin.Service;
using vigo.Service.DTO;
using vigo.Service.DTO.Admin.Account;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace vigo.Admin.Controllers
{
    [Route("api/admin/accounts")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [HttpPost("login-via-form")]
        public async Task<IActionResult> Login(LoginViaFormDTO dto)
        {
            try
            {
                var userAuthen = await _accountService.AdminLogin(dto);
                string token = CreateToken(userAuthen);
                return CreateResponse(new TokenRes() { AccessToken = token}, "login success", 200, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return CreateResponse(null, "login fail", 500, null);
            }
        }

        [HttpGet("business-partners")]
        public async Task<IActionResult> GetBusinessPartnerPaging(int page, int perPage, string sortType, string sortField, string? searchName)
        {
            try
            {
                var data = await _accountService.GetBusinessPartnerPaging(page, perPage, sortType, sortField, searchName);
                Option options = new Option()
                {
                    Name = "",
                    PageSize = data.PageSize,
                    Page = data.PageIndex,
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

        [HttpGet("business-partners/{id}")]
        public async Task<IActionResult> GetBusinessPartnerDetail(int id)
        {
            try
            {
                var data = await _accountService.GetBusinessPartnerDetail(id);
                return CreateResponse(data, "get success", 200, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return CreateResponse(null, "get fail", 500, null);
            }
        }

        [HttpGet("system-employees")]
        public async Task<IActionResult> GetEmployeePaging(int page, int perPage, string sortType, string sortField, string? searchName)
        {
            try
            {
                var data = await _accountService.GetEmployeePaging(page, perPage, sortType, sortField, searchName);
                Option options = new Option()
                {
                    Name = "",
                    PageSize = data.PageSize,
                    Page = data.PageIndex,
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

        [HttpGet("system-employees/{id}")]
        public async Task<IActionResult> GetEmployeeDetail(int id)
        {
            try
            {
                var data = await _accountService.GetEmployeeDetail(id);
                return CreateResponse(data, "get success", 200, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return CreateResponse(null, "get fail", 500, null);
            }
        }

        [HttpPost("business-partner")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateBusinessPartner(CreateBusinessAccountDTO dto)
        {
            try
            {
                await _accountService.CreateBusinessPartner(dto, User);
                return CreateResponse(null, "create success", 200, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return CreateResponse(null, "create fail", 500, null);
            }
        }

        [HttpPost("employees")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeAccount dto)
        {
            try
            {
                await _accountService.CreateEmployee(dto, User);
                return CreateResponse(null, "create success", 200, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return CreateResponse(null, "create fail", 500, null);
            }
        }

        [HttpDelete("employees/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                await _accountService.DeleteEmployee(id, User);
                return CreateResponse(null, "delete success", 200, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return CreateResponse(null, "delete fail", 500, null);
            }
        }

        [HttpDelete("business-partner/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteBusinessPartner(Guid id)
        {
            try
            {
                await _accountService.DeleteBusinessPartner(id, User);
                return CreateResponse(null, "delete success", 200, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return CreateResponse(null, "delete fail", 500, null);
            }
        }

        private string CreateToken(UserAuthen userAuthen)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:Key").Value!);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("RoleId", userAuthen.RoleId.ToString()),
                    new Claim("BusinessKey", userAuthen.BusinessKey)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
