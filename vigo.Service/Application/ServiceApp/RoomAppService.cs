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
using vigo.Service.Application.IServiceApp;
using vigo.Service.DTO.Admin.Account;
using vigo.Service.DTO.Admin.Room;
using vigo.Service.DTO.Admin.Service;

namespace vigo.Service.Application.ServiceApp
{
    public class RoomAppService : IRoomAppService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkVigo _unitOfWorkVigo;

        public RoomAppService(IUnitOfWorkVigo unitOfWorkVigo, IMapper mapper)
        {
            _unitOfWorkVigo = unitOfWorkVigo;
            _mapper = mapper;
        }

        public async Task<RoomDetailDTO> GetDetail(int id)
        {
            var result = _mapper.Map<RoomDetailDTO>(await _unitOfWorkVigo.Rooms.GetById(id));
            List<Expression<Func<RoomServiceR, bool>>> con = new List<Expression<Func<RoomServiceR, bool>>>()
            {
                e => e.RoomId == result.Id
            };
            var roomServices = await _unitOfWorkVigo.RoomServices.GetAll(con);
            foreach (var service in roomServices)
            {
                result.Services.Add(_mapper.Map<ServiceDTO>(await _unitOfWorkVigo.Services.GetById(service.ServiceId)));
            }
            List<Expression<Func<Image, bool>>> con2 = new List<Expression<Func<Image, bool>>>()
            {
                e => e.RoomId == result.Id
            };
            var images = await _unitOfWorkVigo.Images.GetAll(con2);
            List<string> type = new List<string>();
            foreach (var image in images)
            {
                if (type.Contains(image.Type))
                {
                    result.Images.Where(e => e.Type == image.Type).FirstOrDefault()!.Urls.Add(image.Url);
                }
                else
                {
                    type.Add(image.Type);
                    var temp = new RoomImageDTO()
                    {
                        Type = image.Type
                    };
                    temp.Urls.Add(image.Url);
                    result.Images.Add(temp);
                }
            }
            return result;
        }

        public async Task<PagedResultCustom<RoomDTO>> GetPaging(int page, int perPage, int? roomTypeId, string? sortType, string? sortField, string? searchName)
        {
            List<Expression<Func<Room, bool>>> conditions = new List<Expression<Func<Room, bool>>>()
            {
                e => e.DeletedDate == null,
            };
            if (roomTypeId != null)
            {
                conditions.Add(e => e.RoomTypeId == roomTypeId);
            }
            if (searchName != null)
            {
                conditions.Add(e => e.Name.ToLower().Contains(searchName.ToLower()));
            }
            bool sortDown = false;
            if (sortType != null && sortType.Equals("DESC"))
            {
                sortDown = true;
            }
            var data = await _unitOfWorkVigo.Rooms.GetPaging(conditions,
                                                             null,
                                                             sortField != null && sortField.Equals("price") ? e => e.Price : null,
                                                             sortField != null && sortField.Equals("createdDate") ? e => e.CreatedDate : null,
                                                             page,
                                                             perPage,
                                                             sortDown);
            var result = new PagedResultCustom<RoomDTO>(_mapper.Map<List<RoomDTO>>(data.Items), data.TotalRecords, data.PageIndex, data.PageSize);
            foreach (var item in result.Items)
            {
                List<Expression<Func<RoomServiceR, bool>>> con = new List<Expression<Func<RoomServiceR, bool>>>()
                {
                    e => e.RoomId == item.Id,
                };
                var roomServices = await _unitOfWorkVigo.RoomServices.GetAll(con);
                foreach (var service in roomServices)
                {
                    item.Services.Add(_mapper.Map<ServiceDTO>(await _unitOfWorkVigo.Services.GetById(service.ServiceId)));
                }
                item.BusinessPartner = _mapper.Map<BusinessPartnerShortDTO>(await _unitOfWorkVigo.BusinessPartners.GetById(item.BusinessPartnerId));
            }
            return result;
        }
    }
}
