using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Interface.IUnitOfWork;
using vigo.Service.Application.IServiceApp;
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
        public async Task<List<VisitProvinceDTO>> GetVisitProvince()
        {
            return _mapper.Map<List<VisitProvinceDTO>>(await _unitOfWorkVigo.Provinces.GetAll(null));
        }
    }
}
