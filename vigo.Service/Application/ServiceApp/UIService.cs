using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Entity;
using vigo.Domain.Interface.IUnitOfWork;
using vigo.Domain.User;
using vigo.Service.Application.IServiceApp;
using vigo.Service.DTO.Application.Search;
using vigo.Service.DTO.Application.UI;

namespace vigo.Service.Application.ServiceApp
{
    public class UIService : IUIService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkVigo _unitOfWorkVigo;

        public UIService(IUnitOfWorkVigo unitOfWorkVigo, IMapper mapper)
        {
            _unitOfWorkVigo = unitOfWorkVigo;
            _mapper = mapper;
        }

        public async Task<List<ProvinceShortDTO>> GetPopularVisit()
        {
            List<ProvinceShortDTO> result = new List<ProvinceShortDTO>();
            List<string> Ids = new List<string>()
            {
                "01","79","48","77","92","94","31","25","36","73"
            };
            var data = await _unitOfWorkVigo.Provinces.GetAll(null);
            foreach (var item in Ids) {
                int count = 0;
                var temp = data.Where(e => e.Id.Equals(item)).FirstOrDefault();
                List<Expression<Func<BusinessPartner, bool>>> conditions = new List<Expression<Func<BusinessPartner, bool>>>()
                {
                    e => e.ProvinceId.Equals(item)
                };
                var bps = await _unitOfWorkVigo.BusinessPartners.GetAll(conditions);
                foreach(var bp in bps)
                {
                    List<Expression<Func<Room, bool>>> conditions2 = new List<Expression<Func<Room, bool>>>()
                    {
                        e => e.BusinessPartnerId == bp.Id
                    };
                    count += (await _unitOfWorkVigo.Rooms.GetAll(conditions2)).Count();
                }
                result.Add(new ProvinceShortDTO()
                {
                    Image = temp!.Image,
                    Name = temp.Name,
                    RoomNumber = count
                });
            }
            return result;
        }

        public async Task<List<VisitProvinceDTO>> GetVisitProvince()
        {
            return _mapper.Map<List<VisitProvinceDTO>>(await _unitOfWorkVigo.Provinces.GetAll(null));
        }
    }
}
