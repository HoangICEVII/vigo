using Microsoft.AspNetCore.Mvc;
using vigo.Controllers.Base;
using vigo.Domain.Helper;
using vigo.Service.Application.IServiceApp;
using vigo.Service.Application.ServiceApp;
using vigo.Service.DTO;

namespace vigo.Controllers
{
    [Route("api/application/search")]
    [ApiController]
    public class SearchController : BaseController
    {
        private readonly ISearchAppService _searchAppService;
        private readonly IConfiguration _configuration;
        public SearchController(ISearchAppService searchAppService, IConfiguration configuration)
        {
            _searchAppService = searchAppService;
            _configuration = configuration;
        }

        [HttpGet("suggest-result")]
        public async Task<IActionResult> ReturnSearchTyping(string? searchInput)
        {
            try
            {
                var data = await _searchAppService.ReturnSearchTyping(searchInput);
                return CreateResponse(data, "get success", 200, null);
            }
            catch (CustomException e)
            {
                return CreateResponse(null, e.Message, 500, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return CreateResponse(null, "get fail", 500, null);
            }
        }

        [HttpGet("get-result")]
        public async Task<IActionResult> ReturnSearchResult(string? searchInput, DateTime checkIn, DateTime checkOut, int? roomTypeId)
        {
            try
            {
                var data = await _searchAppService.ReturnSearchResult(searchInput, checkIn, checkOut, roomTypeId);
                return CreateResponse(data.BusinessPartnerDTOs, "get success", 200, null);
            }
            catch (CustomException e)
            {
                return CreateResponse(null, e.Message, 500, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return CreateResponse(null, "get fail", 500, null);
            }
        }
    }
}
