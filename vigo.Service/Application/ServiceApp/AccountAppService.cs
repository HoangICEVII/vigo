using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.AccountFolder;
using vigo.Domain.Helper;
using vigo.Domain.Interface.IUnitOfWork;
using vigo.Domain.User;
using vigo.Service.Application.IServiceApp;
using vigo.Service.DTO.Application.Account;

namespace vigo.Service.Application.ServiceApp
{
    public class AccountAppService : IAccountAppService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkVigo _unitOfWorkVigo;

        public AccountAppService(IUnitOfWorkVigo unitOfWorkVigo, IMapper mapper)
        {
            _unitOfWorkVigo = unitOfWorkVigo;
            _mapper = mapper;
        }

        public async Task<TouristDTO> GetTouristInfo(ClaimsPrincipal user)
        {
            int infoId = int.Parse(user.FindFirst("InfoId")!.Value);
            return _mapper.Map<TouristDTO>(await _unitOfWorkVigo.Tourists.GetById(infoId));
        }

        public async Task Register(TouristRegisterDTO dto)
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
                EmailActive = false,
                RoleId = 2,
                UserType = "SystemEmployee",
                Salt = salt
            };
            _unitOfWorkVigo.Accounts.Create(account);
            Tourist info = new Tourist()
            {
                AccountId = accountId,
                CreatedDate = DateNow,
                DeletedDate = null,
                UpdatedDate = DateNow,
                Name = temp.Last(),
                FullName = dto.FullName,
                DOB = dto.DOB,
                Gender = dto.Gender
            };
            _unitOfWorkVigo.Tourists.Create(info);
            await _unitOfWorkVigo.Complete();
        }

        public async Task UpdateTouristInfo(TouristUpdateDTO dto, ClaimsPrincipal user)
        {
            int infoId = int.Parse(user.FindFirst("InfoId")!.Value);
            var info = await _unitOfWorkVigo.Tourists.GetById(infoId);
            var account = await _unitOfWorkVigo.Accounts.GetDetailBy(e => e.Id == info.AccountId);
            var checkUnique = await _unitOfWorkVigo.Accounts.GetDetailBy(e => e.Email == dto.Email);
            if (dto.Email != account!.Email && checkUnique != null)
            {
                throw new CustomException("email đã tồn tại");
            }
            DateTime dateNow = DateTime.Now;
            account.Email = dto.Email;
            account.UpdatedDate = dateNow;

            info.UpdatedDate = dateNow;
            info.DOB = dto.DOB;
            info.Gender = dto.Gender;
            info.FullName = dto.FullName;
            info.Name = dto.FullName.Split(' ').Last();

            await _unitOfWorkVigo.Complete();
        }
    }
}
