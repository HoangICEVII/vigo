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
            return await _unitOfWorkVigo.Accounts.LoginViaForm(dto.Email, dto.Password);
        }

        public async Task CreateBusinessPartner(CreateBusinessAccountDTO dto, ClaimsPrincipal user)
        {
            var checkUnique = await _unitOfWorkVigo.Accounts.GetDetailBy(e => e.Email == dto.Email);
            if (checkUnique != null)
            {
                throw new CustomException("email đã tồn tại");
            }
            var salt = PasswordHasher.CreateSalt();
            var hashedPassword = PasswordHasher.HashPassword(dto.Password, salt);
            Guid accountId = Guid.NewGuid();
            var DateNow = DateTime.Now;
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
                Address = dto.Address,
                Logo = dto.Logo,
                CreatedDate = DateNow,
                DeletedDate = null,
                UpdatedDate= DateNow,
                Name = dto.Name,
                CompanyName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                DistrictId = dto.DistrictId,
                ProvinceId = dto.ProvinceId,
                StreetId = dto.StreetId,
                BusinessKey = PasswordHasher.HashPassword(dto.FullName, DateNow.ToString())
            };
            _unitOfWorkVigo.BusinessPartners.Create(info);
            await _unitOfWorkVigo.Complete();
        }

        public async Task CreateEmployee(CreateEmployeeAccount dto, ClaimsPrincipal user)
        {
            var checkUnique = await _unitOfWorkVigo.Accounts.GetDetailBy(e => e.Email == dto.Email);
            if (checkUnique != null)
            {
                throw new CustomException("email đã tồn tại");
            }
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
                Salary = dto.Salary,
                Bank = dto.Bank,
                Address = dto.Address,
                Avatar = dto.Avatar
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
                Logo = info.Logo,
                Address = info.Address,
                CompanyName = info.CompanyName,
                RoleId = account.RoleId,
                RoleName = (await _unitOfWorkVigo.Roles.GetById(account.RoleId)).Name
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
                Name = info.FullName,
                PhoneNumber = info.PhoneNumber,
                RoleId = account.RoleId,
                BankNumber = info.BankNumber,
                DOB = info.DOB,
                Salary = info.Salary,
                Avatar = info.Avatar,
                Address = info.Address,
                Bank = info.Bank,
                RoleName = (await _unitOfWorkVigo.Roles.GetById(account.RoleId)).Name
            };
            return data;
        }

        public async Task<PagedResultCustom<BusinessPartnerDTO>> GetBusinessPartnerPaging(int page, int perPage, string? sortType, string? sortField, string? searchName)
        {
            List<Expression<Func<BusinessPartner, bool>>> conditions = new List<Expression<Func<BusinessPartner, bool>>>()
            {
                e => e.DeletedDate == null
            };
            if (searchName != null)
            {
                conditions.Add(e => e.CompanyName.ToLower().Contains(searchName.ToLower()));
            }
            bool sortDown = false;
            if (sortType != null && sortType.Equals("DESC"))
            {
                sortDown = true;
            }
            var data = await _unitOfWorkVigo.BusinessPartners.GetPaging(conditions,
                                                                        sortField != null && sortField.Equals("name") ? e => e.Name : null,
                                                                        null,
                                                                        sortField != null && sortField.Equals("createdDate") ? e => e.CreatedDate : null,
                                                                        page,
                                                                        perPage,
                                                                        sortDown);
            var result = new PagedResultCustom<BusinessPartnerDTO>(_mapper.Map<List<BusinessPartnerDTO>>(data.Items), data.TotalRecords, data.PageIndex, data.PageSize);
            foreach (var item in result.Items) {
                var account = await _unitOfWorkVigo.Accounts.GetDetailBy(e => e.Id == item.AccountId);
                item.RoleId = account!.RoleId;
                item.RoleName = (await _unitOfWorkVigo.Roles.GetById(account.RoleId)).Name;
                item.Email = account.Email;
            }
            return result;
        }

        public async Task<PagedResultCustom<EmployeeDTO>> GetEmployeePaging(int page, int perPage, string? sortType, string? sortField, string? searchName)
        {
            List<Expression<Func<SystemEmployee, bool>>> conditions = new List<Expression<Func<SystemEmployee, bool>>>()
            {
                e => e.DeletedDate == null
            };
            if (searchName != null)
            {
                conditions.Add(e => e.Name.ToLower().Contains(searchName.ToLower()));
            }
            bool sortDown = false;
            if (sortType != null && sortType.Equals("DESC"))
            {
                sortDown = true;
            }
            var data = await _unitOfWorkVigo.SystemEmployees.GetPaging(conditions,
                                                                       sortField != null && sortField.Equals("name") ? e => e.Name : null,
                                                                       sortField != null && sortField.Equals("salary") ? e => e.Salary : null,
                                                                       sortField != null && sortField.Equals("dOB") ? e => e.DOB : null,
                                                                       page,
                                                                       perPage,
                                                                       sortDown);
            var result = new PagedResultCustom<EmployeeDTO>(_mapper.Map<List<EmployeeDTO>>(data.Items), data.TotalRecords, data.PageIndex, data.PageSize);
            foreach (var item in result.Items)
            {
                var account = await _unitOfWorkVigo.Accounts.GetDetailBy(e => e.Id == item.AccountId);
                item.RoleId = account!.RoleId;
                item.RoleName = (await _unitOfWorkVigo.Roles.GetById(account.RoleId)).Name;
                item.Email = account.Email;
            }
            return result;
        }

        public async Task UpdateEmployee(UpdateEmployeeDTO dto, ClaimsPrincipal user)
        {
            var info = await _unitOfWorkVigo.SystemEmployees.GetById(dto.Id);
            var account = await _unitOfWorkVigo.Accounts.GetDetailBy(e => e.Id == info.AccountId);
            var checkUnique = await _unitOfWorkVigo.Accounts.GetDetailBy(e => e.Email == dto.Email);
            if (dto.Email != account!.Email && checkUnique != null)
            {
                throw new CustomException("email đã tồn tại");
            }
            DateTime dateNow = DateTime.Now;
            info.Salary = dto.Salary;
            info.DOB = dto.DOB;
            info.BankNumber = dto.BankNumber;
            info.PhoneNumber = dto.PhoneNumber;
            info.FullName = dto.FullName;
            info.Name = dto.FullName.Split(' ').Last();
            info.UpdatedDate = dateNow;
            info.Address = dto.Address;
            info.Avatar = dto.Avatar;
            info.Bank = dto.Bank;

            account!.UpdatedDate = dateNow;
            account.Email = dto.Email;
            account.RoleId = dto.RoleId;

            await _unitOfWorkVigo.Complete();
        }

        public async Task UpdateBusiness(UpdateBusinessPartnerDTO dto, ClaimsPrincipal user)
        {
            var info = await _unitOfWorkVigo.BusinessPartners.GetById(dto.Id);
            var account = await _unitOfWorkVigo.Accounts.GetDetailBy(e => e.Id == info.AccountId);
            var checkUnique = await _unitOfWorkVigo.Accounts.GetDetailBy(e => e.Email == dto.Email);
            if (dto.Email != account!.Email && checkUnique != null)
            {
                throw new CustomException("email đã tồn tại");
            }
            DateTime dateNow = DateTime.Now;
            info.CompanyName = dto.FullName;
            info.Name = dto.Name;
            info.PhoneNumber = dto.PhoneNumber;
            info.UpdatedDate = dateNow;
            info.Address = dto.Address;
            info.Logo = dto.Logo;

            account!.UpdatedDate = dateNow;
            account.Email = dto.Email;
            account.RoleId = dto.RoleId;

            await _unitOfWorkVigo.Complete();
        }

        public async Task<List<BusinessPartnerShortDTO>> GetAllBusinessPartner()
        {
            List<Expression<Func<BusinessPartner, bool>>> conditions = new List<Expression<Func<BusinessPartner, bool>>>()
            {
                e => e.DeletedDate == null
            };
            return _mapper.Map<List<BusinessPartnerShortDTO>>(await _unitOfWorkVigo.BusinessPartners.GetAll(conditions));
        }
    }
}
