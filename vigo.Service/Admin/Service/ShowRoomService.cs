using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Entity;
using vigo.Domain.Helper;
using vigo.Domain.Interface.IUnitOfWork;
using vigo.Domain.User;
using vigo.Service.Admin.IService;
using vigo.Service.DTO.Admin.Account;
using vigo.Service.DTO.Admin.ShowRoom;

namespace vigo.Service.Admin.Service
{
    public class ShowRoomService : IShowRoomService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkVigo _unitOfWorkVigo;

        public ShowRoomService(IUnitOfWorkVigo unitOfWorkVigo, IMapper mapper)
        {
            _unitOfWorkVigo = unitOfWorkVigo;
            _mapper = mapper;
        }

        public async Task Create(CreateShowRoomDTO dto)
        {
            DateTime DateNow = DateTime.Now;
            ShowRoom data = new ShowRoom()
            {
                BusinessPartnerId = dto.BusinessPartnerId,
                CreatedDate = DateNow,
                DeletedDate = null,
                DistrictId = dto.DistrictId,
                Name = dto.Name,
                ProvinceId = dto.ProvinceId,
                UpdatedDate = DateNow,
            };
            _unitOfWorkVigo.ShowRooms.Create(data);
            await _unitOfWorkVigo.Complete();
        }

        public async Task Delete(int id)
        {
            var data = await _unitOfWorkVigo.ShowRooms.GetById(id);
            data.DeletedDate = DateTime.Now;
            await _unitOfWorkVigo.Complete();
        }

        public async Task<ShowRoomDetailDTO> GetDetail(int id)
        {
            return _mapper.Map<ShowRoomDetailDTO>(await _unitOfWorkVigo.ShowRooms.GetById(id));
        }

        public async Task<PagedResultCustom<ShowRoomDTO>> GetPaging(int page, int perPage, int? provinceId, int? districtId, string sortType, string sortField, string? searchName, ClaimsPrincipal user)
        {
            List<Expression<Func<ShowRoom, bool>>> conditions = new List<Expression<Func<ShowRoom, bool>>>()
            {
                e => e.DeletedDate == null
            };
            if (searchName != null)
            {
                conditions.Add(e => e.Name.ToLower().Contains(searchName.ToLower()));
            }
            bool sortDown = false;
            if (sortType.Equals("DESC"))
            {
                sortDown = true;
            }
            var data = await _unitOfWorkVigo.ShowRooms.GetPaging(conditions,
                                                                 null,
                                                                 null,
                                                                 sortField.Equals("createdDate") ? e => e.CreatedDate : null,
                                                                 page,
                                                                 perPage,
                                                                 sortDown);
            return new PagedResultCustom<ShowRoomDTO>(_mapper.Map<List<ShowRoomDTO>>(data.Items), data.TotalRecords, data.PageIndex, data.PageSize);
        }

        public async Task Update(UpdateShowRoomDTO dto)
        {
            var data = await _unitOfWorkVigo.ShowRooms.GetById(dto.Id);
            data.UpdatedDate = DateTime.Now;
            data.Name = dto.Name;
            data.ProvinceId = dto.ProvinceId;
            data.DistrictId = dto.DistrictId;
            await _unitOfWorkVigo.Complete();
        }
    }
}
