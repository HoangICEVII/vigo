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
                MetaData = data,
                Options = options
            };
            //if (data != null && data is object && response.MetaData.Rows == null)
            //{
            //    ICollection<object> temp = [data];
            //}
            return Ok(response);
        }
    }
}
