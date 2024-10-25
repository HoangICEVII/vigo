using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.AccountFolder;
using vigo.Domain.Helper;
using vigo.Service.DTO;
using vigo.Service.DTO.Admin.Account;

namespace vigo.Service.Admin.IService
{
    public interface IAccountService
    {
        Task<UserAuthen> AdminLogin(LoginViaFormDTO dto);
        Task<PagedResult<BusinessPartnerDTO>> GetBusinessPartnerPaging(int page, int perPage, bool? nameSort, string? searchName);
        Task<BusinessPartnerDetailDTO> GetBusinessPartnerDetail(int id);
        Task<PagedResult<EmployeeDTO>> GetEmployeePaging(int page, int perPage, bool? nameSort, bool? salarySort, bool? dobSort, string? searchName);
        Task<EmployeeDetailDTO> GetEmployeeDetail(int id);
        Task CreateBusinessPartner(CreateBusinessAccountDTO dto, ClaimsPrincipal user);
        Task CreateEmployee(CreateEmployeeAccount dto, ClaimsPrincipal user);
        //Task Update(int id, UpdateBookDTO dto, ClaimsPrincipal user);
        Task DeleteEmployee(Guid id, ClaimsPrincipal user);
        Task DeleteBusinessPartner(Guid id, ClaimsPrincipal user);
    }
}
