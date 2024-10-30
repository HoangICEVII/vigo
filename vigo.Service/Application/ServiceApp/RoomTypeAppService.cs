using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Interface.IUnitOfWork;
using vigo.Service.Application.IServiceApp;
using vigo.Service.DTO.Admin.Room;

namespace vigo.Service.Application.ServiceApp
{
    public class RoomTypeAppService : IRoomTypeAppService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkVigo _unitOfWorkVigo;

        public RoomTypeAppService(IUnitOfWorkVigo unitOfWorkVigo, IMapper mapper)
        {
            _unitOfWorkVigo = unitOfWorkVigo;
            _mapper = mapper;
        }
        public async Task<List<RoomTypeDTO>> GetAll()
        {
            return _mapper.Map<List<RoomTypeDTO>>(await _unitOfWorkVigo.RoomTypes.GetAll(null));
        }
    }
}
