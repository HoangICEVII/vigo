using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vigo.Admin.Controllers.Base;
using vigo.Service.Admin.IService;
using vigo.Service.DTO.Admin.Discount;

namespace vigo.Admin.Controllers
{
    [Route("api/admin/images")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ImageController : BaseController
    {
        private readonly IImageService _imageService;
        private readonly IConfiguration _configuration;
        public ImageController(IImageService imageService, IConfiguration configuration)
        {
            _imageService = imageService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile image)
        {
            try
            {
                await _imageService.Upload(image, User);
                return CreateResponse(null, "upload success", 200, null);
            }
            catch (Exception e)
            {
                return CreateResponse(null, "upload fail", 500, null);
            }
        }
    }
}
