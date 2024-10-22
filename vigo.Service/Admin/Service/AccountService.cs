using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.AccountFolder;
using vigo.Domain.Interface.IRepository;
using vigo.Domain.Interface.IUnitOfWork;
using vigo.Infrastructure.UnitOfWork;
using vigo.Service.Admin.IService;
using vigo.Service.DTO;
using vigo.Service.DTO.Admin;

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

        public Task Create(CreateBusinessAccountDTO dto, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }
    }
}
