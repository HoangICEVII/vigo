using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Entity;
using vigo.Domain.Interface.IUnitOfWork;
using vigo.Domain.User;
using vigo.Service.Application.IServiceApp;
using vigo.Service.DTO.Application.Rating;

namespace vigo.Service.Application.ServiceApp
{
    public class RatingAppService : IRatingAppService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkVigo _unitOfWorkVigo;

        public RatingAppService(IUnitOfWorkVigo unitOfWorkVigo, IMapper mapper)
        {
            _unitOfWorkVigo = unitOfWorkVigo;
            _mapper = mapper;
        }

        public async Task<List<RoomRatingDTO>> GetRoomRating(int roomId, ClaimsPrincipal user)
        {
            List<Expression<Func<Rating, bool>>> conditions = new List<Expression<Func<Rating, bool>>>()
            {
                e => e.RoomId == roomId,
                e => e.DeletedDate == null
            };
            var rating = await _unitOfWorkVigo.Ratings.GetAll(conditions);
            List<RoomRatingDTO> result = new List<RoomRatingDTO>();
            foreach (var item in rating) {
                var tourist = await _unitOfWorkVigo.Tourists.GetById(item.TouristId);
                result.Add(new RoomRatingDTO()
                {
                    Avatar = tourist.Avatar,
                    Comment = item.Comment,
                    FullName = tourist.FullName,
                    LastUpdatedDate = item.LastUpdatedDate,
                    Id = item.Id,
                    Rate = item.Rate
                });
            }
            return result;
        }

        public Task RateRoom(RateRoomDTO dto, ClaimsPrincipal user)
        {
            int roleId = int.Parse(user.FindFirst("RoleId")!.Value);
            throw new NotImplementedException();
        }

        public Task UpdateRateRoom(UpdateRateRoomDTO dto, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }
    }
}
