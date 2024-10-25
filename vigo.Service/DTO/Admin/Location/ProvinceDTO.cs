using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vigo.Service.DTO.Admin.Location
{
    public class ProvinceDTO
    {
        public string Name { get; set; }
        public List<DistrictDTO> Districts { get; set; }
    }
}
