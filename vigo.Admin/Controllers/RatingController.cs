using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vigo.Admin.Controllers.Base;
using vigo.Domain.Helper;
using vigo.Service.Admin.IService;
using vigo.Service.Admin.Service;
using vigo.Service.DTO;

namespace vigo.Admin.Controllers
{
    [Route("api/admin/ratings")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RatingController : BaseController
    {
        private readonly IRatingService _ratingService;
        private readonly IConfiguration _configuration;
        public RatingController(IRatingService ratingService, IConfiguration configuration)
        {
            _ratingService = ratingService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaging(int page, int perPage, RatingType type)
        {
            try
            {
                var data = await _ratingService.GetPaging(page, perPage, type, User);
                Option options = new Option()
                {
                    Name = "",
                    Page = data.PageIndex,
                    TotalPage = data.TotalPages
                };
                return CreateResponse(data.Items, "get success", 200, options);
            }
            catch (Exception e)
            {
                return CreateResponse(null, "get fail", 500, null);
            }
        }

        [HttpPost("approve")]
        public async Task<IActionResult> Approve(List<int> ids)
        {
            try
            {
                await _ratingService.Approve(ids, User);
                return CreateResponse(null, "approve success", 200, null);
            }
            catch (Exception e)
            {
                return CreateResponse(null, "approve fail", 500, null);
            }
        }

        [HttpPost("un-approve")]
        public async Task<IActionResult> UnApprove(List<int> ids)
        {
            try
            {
                await _ratingService.UnApprove(ids, User);
                return CreateResponse(null, "success", 200, null);
            }
            catch (Exception e)
            {
                return CreateResponse(null, "fail", 500, null);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(List<int> ids)
        {
            try
            {
                await _ratingService.Delete(ids, User);
                return CreateResponse(null, "recieve success", 200, null);
            }
            catch (Exception e)
            {
                return CreateResponse(null, "receive fail", 500, null);
            }
        }
    }
}
