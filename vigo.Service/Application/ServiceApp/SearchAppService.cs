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
using vigo.Service.EmailAuthenModule;

namespace vigo.Service.Application.ServiceApp
{
    public class SearchAppService : ISearchAppService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkVigo _unitOfWorkVigo;

        public SearchAppService(IUnitOfWorkVigo unitOfWorkVigo, IMapper mapper)
        {
            _unitOfWorkVigo = unitOfWorkVigo;
            _mapper = mapper;
        }

        public async Task<SearchResultDTO> ReturnSearchTyping(string searchInput)
        {
            List<Expression<Func<Province, bool>>> conditions = new List<Expression<Func<Province, bool>>>()
            {
                e => e.Name.ToLower().Contains(searchInput.ToLower())
            };
            List<Expression<Func<BusinessPartner, bool>>> conditions2 = new List<Expression<Func<BusinessPartner, bool>>>()
            {
                e => e.Name.ToLower().Contains(searchInput.ToLower())
            };
            var province = await _unitOfWorkVigo.Provinces.GetAll(conditions);
            var business = await _unitOfWorkVigo.BusinessPartners.GetAll(conditions2);
            var result = new SearchResultDTO();
            foreach (var item in province) {
                result.ProvinceShortDTOs.Add(new ProvinceShortDTO()
                {
                    Name = item.Name,
                    Image = item.Image
                });
            }
            foreach (var item in business)
            {
                result.BPShortDTOs.Add(new BPShortDTO()
                {
                    Name = item.Name,
                    Logo = item.Logo
                });
            }
            return result;
        }
    }
}
