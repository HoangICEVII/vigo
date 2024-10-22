using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using vigo.Domain.AccountFolder;
using vigo.Domain.Entity;
using vigo.Service.DTO.Admin.Role;

namespace vigo.Service.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Role, RoleDTO>();
        }
    }
}
