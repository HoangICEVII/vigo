using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Service.DTO.Admin.Location;

namespace vigo.Service.Admin.IService
{
    public interface ILocationService
    {
        Task<List<ProvinceDTO>> GetLocation();
    }
}
