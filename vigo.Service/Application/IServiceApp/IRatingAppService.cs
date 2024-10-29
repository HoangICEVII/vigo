using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vigo.Service.Application.IServiceApp
{
    public interface IRatingAppService
    {
        Task GetRoomRating();
    }
}
