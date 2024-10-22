using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.AccountFolder;
using vigo.Domain.Interface.IUnitOfWork;
using vigo.Service.Admin.IService;
using vigo.Service.DTO.Admin.Role;

namespace vigo.Service.Admin.Service
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkVigo _unitOfWorkVigo;
        public RoleService(IMapper mapper, IUnitOfWorkVigo unitOfWorkVigo)
        {
            _mapper = mapper;
            _unitOfWorkVigo = unitOfWorkVigo;
        }
        public async Task Create(RoleCreateDTO dto)
        {
            var role = new Role()
            {
                Name = dto.Name,
                Permission = dto.Permission != null ? String.Join(",",dto.Permission) : "",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                DeletedDate = null
            };
            _unitOfWorkVigo.Roles.Create(role);
            await _unitOfWorkVigo.Complete();
        }

        public async Task Delete(int id)
        {
            var data = await _unitOfWorkVigo.Roles.GetById(id);
            data.DeletedDate = DateTime.Now;
        }

        public async Task<List<RoleDTO>> GetPaging(int indexPage)
        {
            return _mapper.Map<List<RoleDTO>>(await _unitOfWorkVigo.Roles.GetPaging(null, indexPage, 10));
        }

        public async Task<List<string>> GetPermission()
        {
            var data = await _unitOfWorkVigo.RolePermissions.GetAll();
            List<string> permissions = new List<string>();
            foreach (var permission in data)
            {
                permissions.Add(permission.Name);
            }
            return permissions;
        }

        public async Task Update(RoleUpdateDTO dto)
        {
            var data = await _unitOfWorkVigo.Roles.GetById(dto.Id);
            data.Name = dto.Name;
            data.Permission = dto.Permission != null ? String.Join(",", dto.Permission) : "";
            data.UpdatedDate = DateTime.Now;
        }
    }
}
