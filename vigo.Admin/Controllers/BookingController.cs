﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vigo.Admin.Controllers.Base;
using vigo.Service.Admin.IService;
using vigo.Service.Admin.Service;
using vigo.Service.DTO;

namespace vigo.Admin.Controllers
{
    [Route("api/admin/bookings")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BookingController : BaseController
    {
        private readonly IBookingService _bookingService;
        private readonly IConfiguration _configuration;
        public BookingController(IBookingService bookingService, IConfiguration configuration)
        {
            _bookingService = bookingService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaging(int page, int perPage, bool? isReceived, bool? priceSort)
        {
            try
            {
                var data = await _bookingService.GetPaging(page, perPage, isReceived, priceSort, User);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            try
            {
                var data = await _bookingService.GetDetail(id, User);
                return CreateResponse(data, "get success", 200, null);
            }
            catch (Exception e)
            {
                return CreateResponse(null, "get fail", 500, null);
            }
        }

        [HttpPost("received")]
        public async Task<IActionResult> ReceiveBooking(List<int> ids)
        {
            try
            {
                await _bookingService.ReceiveBooking(ids, User);
                return CreateResponse(null, "recieve success", 200, null);
            }
            catch (Exception e)
            {
                return CreateResponse(null, "receive fail", 500, null);
            }
        }
    }
}