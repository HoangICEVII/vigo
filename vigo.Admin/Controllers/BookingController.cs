using Microsoft.AspNetCore.Mvc;
using vigo.Admin.Controllers.Base;

namespace vigo.Admin.Controllers
{
    [Route("api/admin/bookings")]
    [ApiController]
    public class BookingController : BaseController
    {

    }
}



//controller -> service ->(unitOfWork) repository -> dbcontext -> database