using AutoMapper;
using Microsoft.IdentityModel.Tokens;
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
using vigo.Service.DTO.Admin.Rating;

namespace vigo.Service.Admin.Service
{
    public class RatingService : IRatingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkVigo _unitOfWorkVigo;

        public RatingService(IUnitOfWorkVigo unitOfWorkVigo, IMapper mapper)
        {
            _unitOfWorkVigo = unitOfWorkVigo;
            _mapper = mapper;
        }
        public async Task Approve(List<int> ids, ClaimsPrincipal user)
        {
            List<Rating> data = new List<Rating>();
            foreach (int id in ids) {
                data.Add(await _unitOfWorkVigo.Ratings.GetById(id));
            }
            foreach (Rating rating in data) {
                if (rating.Status == false) {
                    rating.Status = true;
                }
                if (!rating.UpdateComment.IsNullOrEmpty()) {
                    rating.Comment = rating.UpdateComment;
                }
            }
            await _unitOfWorkVigo.Complete();
        }

        public async Task Delete(List<int> ids, ClaimsPrincipal user)
        {
            List<Rating> data = new List<Rating>();
            DateTime Datenow = DateTime.Now;
            foreach (int id in ids)
            {
                data.Add(await _unitOfWorkVigo.Ratings.GetById(id));
            }
            foreach (Rating rating in data)
            {
                rating.DeletedDate = Datenow;
            }
            await _unitOfWorkVigo.Complete();
        }

        public async Task<PagedResultCustom<RatingDTO>> GetPaging(int page, int perPage, RatingType type, ClaimsPrincipal user)
        {
            List<Expression<Func<Rating, bool>>> conditions = new List<Expression<Func<Rating, bool>>>()
            {
                e => e.DeletedDate == null
            };
            if (type.ToString().Equals("unapprove"))
            {
                conditions.Add(e => e.Status == false || !e.UpdateComment.IsNullOrEmpty());
            }
            var data = await _unitOfWorkVigo.Ratings.GetPaging(conditions,
                                                               null,
                                                               null,
                                                               null,
                                                               page,
                                                               perPage);
            return new PagedResultCustom<RatingDTO>(_mapper.Map<List<RatingDTO>>(data.Items), data.TotalRecords, data.PageIndex, data.PageSize);
        }

        public async Task UnApprove(List<int> ids, ClaimsPrincipal user)
        {
            List<Rating> data = new List<Rating>();
            foreach (int id in ids)
            {
                data.Add(await _unitOfWorkVigo.Ratings.GetById(id));
            }
            _unitOfWorkVigo.Ratings.DeleteRange(data);
            await _unitOfWorkVigo.Complete();
        }
    }
}
