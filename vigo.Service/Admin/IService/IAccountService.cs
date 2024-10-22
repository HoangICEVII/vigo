using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.AccountFolder;
using vigo.Service.DTO;
using vigo.Service.DTO.Admin;

namespace vigo.Service.Admin.IService
{
    public interface IAccountService
    {
        Task<UserAuthen> AdminLogin(LoginViaFormDTO dto);
        Task Create(CreateBusinessAccountDTO dto, ClaimsPrincipal user);
        //Task Update(int id, UpdateBookDTO dto, ClaimsPrincipal user);
        Task Delete(int id, ClaimsPrincipal user);
    }
}
