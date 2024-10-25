﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vigo.Service.DTO.Admin.ShowRoom
{
    public class CreateShowRoomDTO
    {
        public string Name { get; set; } = string.Empty;
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int BusinessPartnerId { get; set; }
    }
}
