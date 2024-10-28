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
using vigo.Service.DTO.Admin.Room;

namespace vigo.Service.Admin.IService
{
    public interface IAccountService
    {
        Task<UserAuthen> AdminLogin(LoginViaFormDTO dto);
        Task<PagedResultCustom<BusinessPartnerDTO>> GetBusinessPartnerPaging(int page, int perPage, string? sortType, string? sortField, string? searchName);
        Task<List<BusinessPartnerShortDTO>> GetAllBusinessPartner();
        Task<BusinessPartnerDetailDTO> GetBusinessPartnerDetail(int id);
        Task<PagedResultCustom<EmployeeDTO>> GetEmployeePaging(int page, int perPage, string? sortType, string? sortField, string? searchName);
        Task<EmployeeDetailDTO> GetEmployeeDetail(int id);
        Task CreateBusinessPartner(CreateBusinessAccountDTO dto, ClaimsPrincipal user);
        Task CreateEmployee(CreateEmployeeAccount dto, ClaimsPrincipal user);
        Task UpdateEmployee(UpdateEmployeeDTO dto, ClaimsPrincipal user);
        Task UpdateBusiness(UpdateBusinessPartnerDTO dto, ClaimsPrincipal user);
        Task DeleteEmployee(Guid id, ClaimsPrincipal user);
        Task DeleteBusinessPartner(Guid id, ClaimsPrincipal user);
    }
}
