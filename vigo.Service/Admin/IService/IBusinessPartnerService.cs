using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vigo.Service.Admin.IService
{
    public interface IBusinessPartnerService
    {
        Task GetPaging(int indexPage);
        Task GetById(int id);
        //Task Update(int id);
        Task Delete(int id);
    }
}
