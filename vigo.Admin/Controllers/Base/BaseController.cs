using Microsoft.AspNetCore.Mvc;
using vigo.Service.DTO;

namespace vigo.Admin.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        protected IActionResult CreateResponse(object? data, string message, int status, Option? options)
        {
            var response = new ResponseData
            {
                Message = message,
                Status = status,
                MetaData = new MetaData
                {
                    Count = data is ICollection<object> collection ? collection.Count : 1,
                    Rows = data is ICollection<object> ? (ICollection<object>)data : null
                },
                Options = options
            };
            return Ok(response);
        }
    }
}
