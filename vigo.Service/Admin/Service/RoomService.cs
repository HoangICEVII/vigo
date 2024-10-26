using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Entity;
using vigo.Domain.Helper;
using vigo.Domain.Interface.IUnitOfWork;
using vigo.Service.Admin.IService;
using vigo.Service.DTO.Admin.Room;
using vigo.Service.DTO.Admin.Service;
using vigo.Service.DTO.Admin.ShowRoom;

namespace vigo.Service.Admin.Service
{
    public class RoomService : IRoomService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkVigo _unitOfWorkVigo;

        public RoomService(IUnitOfWorkVigo unitOfWorkVigo, IMapper mapper)
        {
            _unitOfWorkVigo = unitOfWorkVigo;
            _mapper = mapper;
        }

        public async Task Create(CreateRoomDTO dto)
        {
            DateTime dateNow = DateTime.Now;
            Room room = new Room()
            {
                Name = dto.Name,
                Avaiable = dto.Avaiable,
                CreatedDate = dateNow,
                Days = dto.Days,
                DeletedDate = null,
                Description = dto.Description,
                Price = dto.Price,
                RoomTypeId = dto.RoomTypeId,
                ShowRoomId = dto.ShowRoomId,
                Thumbnail = dto.Thumbnail,
                UpdatedDate = dateNow,
            };
            _unitOfWorkVigo.Rooms.Create(room);
            await _unitOfWorkVigo.Complete();

            try{
                List<RoomServiceR> roomServices = new List<RoomServiceR>();
                foreach (var item in dto.Services)
                {
                    roomServices.Add(new RoomServiceR()
                    {
                        RoomId = room.Id,
                        ServiceId = item
                    });
                }
                _unitOfWorkVigo.RoomServices.CreateRange(roomServices);
                await _unitOfWorkVigo.Complete();
            }
            catch(CustomException)
            {
                throw new CustomException("không đăng ký được dịch vụ cho phòng");
            }
        }

        public async Task Delete(int id)
        {
            var data = await _unitOfWorkVigo.Rooms.GetById(id);
            data.DeletedDate = DateTime.Now;
            await _unitOfWorkVigo.Complete();
        }

        public async Task<RoomDetailDTO> GetDetail(int id)
        {
            var result = _mapper.Map<RoomDetailDTO>(await _unitOfWorkVigo.Rooms.GetById(id));
            List<Expression<Func<RoomServiceR, bool>>> con = new List<Expression<Func<RoomServiceR, bool>>>()
            {
                e => e.RoomId == result.Id,
            };
            var roomServices = await _unitOfWorkVigo.RoomServices.GetAll(con);
            foreach (var service in roomServices)
            {
                result.Services.Add(_mapper.Map<ServiceDTO>(await _unitOfWorkVigo.Services.GetById(service.ServiceId)));
            }
            return result;
        }

        public async Task<PagedResultCustom<RoomDTO>> GetPaging(int page, int perPage, int? roomTypeId, string? sortType, string? sortField, string? searchName)
        {
            List<Expression<Func<Room, bool>>> conditions = new List<Expression<Func<Room, bool>>>()
            {
                e => e.DeletedDate == null,
            };
            if (searchName != null)
            {
                conditions.Add(e => e.Name.ToLower().Contains(searchName.ToLower()));
            }
            if (roomTypeId != null)
            {
                conditions.Add(e => e.RoomTypeId == roomTypeId);
            }
            bool sortDown = false;
            if (sortType != null && sortType.Equals("DESC"))
            {
                sortDown = true;
            }
            Expression<Func<Room, decimal>>? func = null;
            if (sortField != null && sortField.Equals("days"))
            {
                func = e => e.Days;
            }
            if (sortField != null && sortField.Equals("price"))
            {
                func = e => e.Price;
            }
            var data = await _unitOfWorkVigo.Rooms.GetPaging(conditions,
                                                             null,
                                                             func,
                                                             null,
                                                             page,
                                                             perPage,
                                                             sortDown);
            var result = new PagedResultCustom<RoomDTO>(_mapper.Map<List<RoomDTO>>(data.Items), data.TotalRecords, data.PageIndex, data.PageSize);
            foreach (var item in result.Items) {
                List<Expression<Func<RoomServiceR, bool>>> con = new List<Expression<Func<RoomServiceR, bool>>>()
                {
                    e => e.RoomId == item.Id,
                };
                var roomServices = await _unitOfWorkVigo.RoomServices.GetAll(con);
                foreach (var service in roomServices) {
                    item.Services.Add(_mapper.Map<ServiceDTO>(await _unitOfWorkVigo.Services.GetById(service.ServiceId)));
                }
            }
            return result;
        }

        public async Task Update(UpdateRoomDTO dto)
        {
            var data = await _unitOfWorkVigo.Rooms.GetById(dto.Id);
            data.Thumbnail = dto.Thumbnail;
            data.Days = dto.Days;
            data.Price = dto.Price;
            data.UpdatedDate = DateTime.Now;
            data.Avaiable = dto.Avaiable;
            data.Description = dto.Description;
            data.Name = dto.Name;
            data.RoomTypeId = dto.RoomTypeId;

            List<Expression<Func<RoomServiceR, bool>>> con = new List<Expression<Func<RoomServiceR, bool>>>()
            {
                e => e.RoomId == dto.Id,
            };

            var roomService = await _unitOfWorkVigo.RoomServices.GetAll(con);
            foreach (var r in roomService) {
                if (!dto.Services.Contains(r.ServiceId))
                {
                    _unitOfWorkVigo.RoomServices.Delete(r);
                }
                else
                {
                    dto.Services.Remove(r.ServiceId);
                }
            }
            foreach (var item in dto.Services) {
                _unitOfWorkVigo.RoomServices.Create(new RoomServiceR()
                {
                    RoomId = dto.Id,
                    ServiceId = item
                });
            }
            await _unitOfWorkVigo.Complete();
        }
    }
}
