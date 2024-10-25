using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.AccountFolder;
using vigo.Domain.Helper;
using vigo.Domain.Interface.IRepository;
using vigo.Domain.Interface.IUnitOfWork;
using vigo.Domain.User;
using vigo.Infrastructure.UnitOfWork;
using vigo.Service.Admin.IService;
using vigo.Service.DTO;
using vigo.Service.DTO.Admin.Account;

namespace vigo.Service.Admin.Service
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkVigo _unitOfWorkVigo;

        public AccountService(IUnitOfWorkVigo unitOfWorkVigo, IMapper mapper)
        {
            _unitOfWorkVigo = unitOfWorkVigo;
            _mapper = mapper;
        }

        public async Task<UserAuthen> AdminLogin(LoginViaFormDTO dto)
        {
            return await _unitOfWorkVigo.Accounts.LoginViaForm(dto.Email, dto.Password, true);
        }

        public async Task CreateBusinessPartner(CreateBusinessAccountDTO dto, ClaimsPrincipal user)
        {
            var salt = PasswordHasher.CreateSalt();
            var hashedPassword = PasswordHasher.HashPassword(dto.Password, salt);
            Guid accountId = Guid.NewGuid();
            var DateNow = DateTime.Now;
            string[] temp = dto.FullName.Split(' ');
            Account account = new Account()
            {
                Id = accountId,
                CreatedDate = DateNow,
                DeletedDate = null,
                UpdatedDate = DateNow,
                Email = dto.Email,
                Password = hashedPassword,
                EmailActive = true,
                RoleId = dto.RoleId,
                UserType = "BusinessPartner",
                Salt = salt
            };
            _unitOfWorkVigo.Accounts.Create(account);
            BusinessPartner info = new BusinessPartner()
            {
                AccountId = accountId,
                CreatedDate = DateNow,
                DeletedDate = null,
                UpdatedDate= DateNow,
                Name = temp.Last(),
                CompanyName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                BusinessKey = PasswordHasher.HashPassword(dto.FullName, DateNow.ToString())
            };
            _unitOfWorkVigo.BusinessPartners.Create(info);
            await _unitOfWorkVigo.Complete();
        }

        public async Task CreateEmployee(CreateEmployeeAccount dto, ClaimsPrincipal user)
        {
            var salt = PasswordHasher.CreateSalt();
            var hashedPassword = PasswordHasher.HashPassword(dto.Password, salt);
            Guid accountId = Guid.NewGuid();
            var DateNow = DateTime.Now;
            string[] temp = dto.FullName.Split(' ');
            Account account = new Account()
            {
                Id = accountId,
                CreatedDate = DateNow,
                DeletedDate = null,
                UpdatedDate = DateNow,
                Email = dto.Email,
                Password = hashedPassword,
                EmailActive = true,
                RoleId = dto.RoleId,
                UserType = "SystemEmployee",
                Salt = salt
            };
            _unitOfWorkVigo.Accounts.Create(account);
            SystemEmployee info = new SystemEmployee()
            {
                AccountId = accountId,
                CreatedDate = DateNow,
                DeletedDate = null,
                UpdatedDate = DateNow,
                Name = temp.Last(),
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                DOB = dto.DOB,
                BankNumber = dto.BankNumber,
                Salary = dto.Salary
            };
            _unitOfWorkVigo.SystemEmployees.Create(info);
            await _unitOfWorkVigo.Complete();
        }

        public async Task DeleteBusinessPartner(Guid id, ClaimsPrincipal user)
        {
            var account = await _unitOfWorkVigo.Accounts.GetDetailBy(e => e.Id == id);
            var info = await _unitOfWorkVigo.BusinessPartners.GetDetailBy(e => e.AccountId == id);
            var DateNow = DateTime.Now;
            account!.DeletedDate = DateNow;
            info!.DeletedDate = DateNow;
            await _unitOfWorkVigo.Complete();
        }

        public async Task DeleteEmployee(Guid id, ClaimsPrincipal user)
        {
            var account = await _unitOfWorkVigo.Accounts.GetDetailBy(e => e.Id == id);
            var info = await _unitOfWorkVigo.SystemEmployees.GetDetailBy(e => e.AccountId == id);
            var DateNow = DateTime.Now;
            account!.DeletedDate = DateNow;
            info!.DeletedDate = DateNow;
            await _unitOfWorkVigo.Complete();
        }

        public async Task<BusinessPartnerDetailDTO> GetBusinessPartnerDetail(int id)
        {
            var info = await _unitOfWorkVigo.BusinessPartners.GetById(id);
            var account = await _unitOfWorkVigo.Accounts.GetDetailBy(e => e.Id == info.AccountId);
            BusinessPartnerDetailDTO data = new BusinessPartnerDetailDTO()
            {
                Id = info.Id,
                AccountId = account!.Id,
                BusinessKey = info.BusinessKey,
                CreatedDate = info.CreatedDate,
                DeletedDate = info.DeletedDate,
                UpdatedDate = info.UpdatedDate,
                Email = account.Email,
                Name = info.Name,
                PhoneNumber = info.PhoneNumber,
                RoleId = account.RoleId
            };
            return data;
        }

        public async Task<EmployeeDetailDTO> GetEmployeeDetail(int id)
        {
            var info = await _unitOfWorkVigo.SystemEmployees.GetById(id);
            var account = await _unitOfWorkVigo.Accounts.GetDetailBy(e => e.Id == info.AccountId);
            EmployeeDetailDTO data = new EmployeeDetailDTO()
            {
                Id = info.Id,
                AccountId = account!.Id,
                CreatedDate = info.CreatedDate,
                DeletedDate = info.DeletedDate,
                UpdatedDate = info.UpdatedDate,
                Email = account.Email,
                Name = info.Name,
                PhoneNumber = info.PhoneNumber,
                RoleId = account.RoleId,
                BankNumber = info.BankNumber,
                DOB = info.DOB,
                Salary = info.Salary
            };
            return data;
        }

        public async Task<PagedResult<BusinessPartnerDTO>> GetBusinessPartnerPaging(int page, int perPage, bool? nameSort)
        {
            List<Expression<Func<BusinessPartner, bool>>> conditions = new List<Expression<Func<BusinessPartner, bool>>>()
            {
                e => e.DeletedDate == null
            };
            bool sortDown = false;
            if (nameSort == true)
            {
                sortDown = true;
            }
            var data = await _unitOfWorkVigo.BusinessPartners.GetPaging(conditions,
                                                                        nameSort != null ? e => e.Name : null,
                                                                        null,
                                                                        null,
                                                                        page,
                                                                        perPage,
                                                                        sortDown);
            return new PagedResult<BusinessPartnerDTO>(_mapper.Map<List<BusinessPartnerDTO>>(data.Items), data.TotalPages, data.PageIndex, data.PageSize);
        }

        public async Task<PagedResult<EmployeeDTO>> GetEmployeePaging(int page, int perPage, bool? nameSort, bool? salarySort, bool? dobSort)
        {
            List<Expression<Func<SystemEmployee, bool>>> conditions = new List<Expression<Func<SystemEmployee, bool>>>()
            {
                e => e.DeletedDate == null
            };
            bool sortDown = false;
            if (nameSort == true || salarySort == true || dobSort == true)
            {
                sortDown = true;
            }
            var data = await _unitOfWorkVigo.SystemEmployees.GetPaging(conditions,
                                                                       nameSort != null? e => e.Name : null,
                                                                       salarySort != null? e => e.Salary : null,
                                                                       dobSort != null ? e => e.DOB : null,
                                                                       page,
                                                                       perPage,
                                                                       sortDown);
            return new PagedResult<EmployeeDTO>(_mapper.Map<List<EmployeeDTO>>(data.Items), data.TotalPages, data.PageIndex, data.PageSize);
        }
    }
}
