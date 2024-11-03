using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
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
using vigo.Service.DTO.Application.Room;

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
            result.BusinessPartnerName = (await _unitOfWorkVigo.BusinessPartners.GetById(result.BusinessPartnerId)).Name;
            return result;
        }

        public async Task<PagedResultCustom<ProvinceV2DTO>> GetPaging(int page, int perPage, int? roomTypeId, string? provinceId, string? districtId, int? star)
        {
            List<Expression<Func<Room, bool>>> conditions = new List<Expression<Func<Room, bool>>>()
            {
            };
            if (roomTypeId != null)
            {
                conditions.Add(e => e.RoomTypeId == roomTypeId);
            }
            if (provinceId != null)
            {
                conditions.Add(e => e.ProvinceId == provinceId);
            }
            if (districtId != null) {
                conditions.Add(e => e.DistrictId == districtId);
            }
            var room = await _unitOfWorkVigo.Rooms.GetAll(conditions);
            if (star != null)
            {
                room = room.Where(e => Math.Floor(e.Star) == star || Math.Floor(e.Star + 0.5m) == star).ToList();
            }
            PagedResultCustom<ProvinceV2DTO> result = new PagedResultCustom<ProvinceV2DTO>(new List<ProvinceV2DTO>(), 0, 0, 0);
            if (provinceId != null)
            {
                var province = await _unitOfWorkVigo.Provinces.GetDetailBy(e => e.Id.Equals(provinceId));
                List<RoomAppDTO> temp = new List<RoomAppDTO>();
                foreach(var item in room)
                {
                    temp.Add(new RoomAppDTO()
                    {
                        ProvinceId = item.ProvinceId,
                        DistrictId = item.DistrictId,
                        Address = item.Address,
                        Avaiable = item.Avaiable,
                        DefaultDiscount = item.DefaultDiscount,
                        Description = item.Description,
                        District = (await _unitOfWorkVigo.Districts.GetDetailBy(e => e.Id.Equals(item.DistrictId)))!.Name,
                        Name = item.Name,
                        Id = item.Id,
                        Price = item.Price,
                        Province = (await _unitOfWorkVigo.Provinces.GetDetailBy(e => e.Id.Equals(item.ProvinceId)))!.Name,
                        Star = item.Star,
                        Thumbnail = item.Thumbnail
                    });
                }
                
                result.Items.Add(new ProvinceV2DTO()
                {
                    Id = province!.Id,
                    Image = province.Image,
                    Name = province.Name,
                    RoomNumber = room.Count(),
                    Rooms = temp
                });
            }
            else
            {
                var province = await _unitOfWorkVigo.Provinces.GetAll(null);
                foreach (var item in province) {
                    var roomTemp = room.Where(e => e.ProvinceId.Equals(item.Id)).ToList();
                    List<RoomAppDTO> temp = new List<RoomAppDTO>();
                    foreach (var aa in roomTemp)
                    {
                        temp.Add(new RoomAppDTO()
                        {
                            Address = aa.Address,
                            Avaiable = aa.Avaiable,
                            DefaultDiscount = aa.DefaultDiscount,
                            Description = aa.Description,
                            District = (await _unitOfWorkVigo.Districts.GetDetailBy(e => e.Id.Equals(aa.DistrictId)))!.Name,
                            Name = aa.Name,
                            Id = aa.Id,
                            Price = aa.Price,
                            Province = (await _unitOfWorkVigo.Provinces.GetDetailBy(e => e.Id.Equals(aa.ProvinceId)))!.Name,
                            Star = aa.Star,
                            Thumbnail = aa.Thumbnail
                        });
                    }
                    result.Items.Add(new ProvinceV2DTO()
                    {
                        Id = item.Id,
                        Image = item.Image,
                        Name = item.Name,
                        RoomNumber = roomTemp.Count(),
                        Rooms = temp
                    });
                }
            }
            return result;
        }

        public async Task<List<RoomAppDTO>> GetRelatedRoom(int businessPartnerId)
        {
            List<Expression<Func<Room, bool>>> conditions = new List<Expression<Func<Room, bool>>>()
            {
                e => e.BusinessPartnerId == businessPartnerId
            };
            var rooms = await _unitOfWorkVigo.Rooms.GetPaging(conditions, null, e => e.Star, null, 1, 8, true);
            List<RoomAppDTO> temp = new List<RoomAppDTO>();
            foreach (var room in rooms.Items)
            {
                temp.Add(new RoomAppDTO()
                {
                    Address = room.Address,
                    Avaiable = room.Avaiable,
                    DefaultDiscount = room.DefaultDiscount,
                    Description = room.Description,
                    District = (await _unitOfWorkVigo.Districts.GetDetailBy(e => e.Id.Equals(room.DistrictId)))!.Name,
                    Name = room.Name,
                    Id = room.Id,
                    Price = room.Price,
                    Province = (await _unitOfWorkVigo.Provinces.GetDetailBy(e => e.Id.Equals(room.ProvinceId)))!.Name,
                    Star = room.Star,
                    Thumbnail = room.Thumbnail
                });
            }
            return temp;
        }
    }
}
