using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using vigo.Service.DTO.Admin;

namespace vigo.Service.Admin.IService
{
    public interface IRoomTypeService
    {
        Task GetPaging();
        Task GetById();
        Task Create();
        Task Update();
        Task Delete();
    }
}
