﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.AccountFolder;
using vigo.Domain.Helper;
using vigo.Domain.Interface.IUnitOfWork;
using vigo.Domain.User;
using vigo.Service.Admin.IService;
using vigo.Service.DTO.Admin.Account;
using vigo.Service.DTO.Admin.Role;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                Permission = dto.Permission != null ? string.Join(",",dto.Permission) : "",
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
            await _unitOfWorkVigo.Complete();
        }

        public async Task<List<RoleDTO>> GetAll()
        {
            List<Expression<Func<Role, bool>>> conditions = new List<Expression<Func<Role, bool>>>()
            {
                e => e.DeletedDate == null
            };
            return _mapper.Map<List<RoleDTO>>(await _unitOfWorkVigo.Roles.GetAll(conditions));
        }

        public async Task<PagedResult<RoleDTO>> GetPaging(int page, int perPage)
        {
            List<Expression<Func<Role, bool>>> conditions = new List<Expression<Func<Role, bool>>>()
            {
                e => e.DeletedDate == null
            };
            var data = await _unitOfWorkVigo.Roles.GetPaging(conditions, null, null, null, page, perPage);
            return new PagedResult<RoleDTO>(_mapper.Map<List<RoleDTO>>(data.Items), data.TotalPages, data.PageIndex, data.PageSize);
        }
        public async Task<List<RolePermissionDTO>> GetPermission()
        {
            return _mapper.Map<List<RolePermissionDTO>>(await _unitOfWorkVigo.RolePermissions.GetAll(null));
        }

        public async Task Update(RoleUpdateDTO dto)
        {
            var data = await _unitOfWorkVigo.Roles.GetById(dto.Id);
            data.Name = dto.Name;
            data.Permission = dto.Permission != null ? string.Join(",", dto.Permission) : "";
            data.UpdatedDate = DateTime.Now;
            await _unitOfWorkVigo.Complete();
        }
    }
}
