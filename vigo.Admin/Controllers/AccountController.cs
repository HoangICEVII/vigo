using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using vigo.Admin.Controllers.Base;
using vigo.Domain.AccountFolder;
using vigo.Service.Admin.IService;
using vigo.Service.DTO;

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
                return Ok(token);
            }
            catch (Exception e)
            {
                return new ObjectResult(new { message = e.Message }) { StatusCode = 500 };
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
