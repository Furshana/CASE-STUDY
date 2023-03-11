using GoToMeetingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoToMeetingApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GtmBookingDetailsController : ControllerBase
    {
        private readonly Gotomeeting_dbContext _context;
        private readonly ILogger<GtmBookingDetailsController> _logger;
        public GtmBookingDetailsController(Gotomeeting_dbContext context,ILogger<GtmBookingDetailsController> logger)
        {
            _logger = logger;
            _context = context;

        }
        [HttpGet]
        public IEnumerable<GtmBookingDetails> Get()
        {
            return _context.GtmBookingDetails.ToList();
        }
        [HttpGet("{BookingId}")]
        public GtmBookingDetails Get(int BookingId)
        {
            return _context.GtmBookingDetails.FirstOrDefault(o => o.BookingId == BookingId);
        }
        [HttpPost("AddBookingDetails")]
        public IActionResult AddGtmBookingDetails([FromBody] GtmBookingDetails gtmBookingDetails)
        {
            string result = string.Empty;
            try
            {
                var _bookingDetails = _context.GtmBookingDetails.FirstOrDefault(o => o.BookingId == gtmBookingDetails.BookingId);
                if (_bookingDetails != null)
                {
                    _bookingDetails.BookingId = gtmBookingDetails.BookingId;
                    _bookingDetails.FirstName = gtmBookingDetails.FirstName;
                    _bookingDetails.LastName = gtmBookingDetails.LastName;
                    _bookingDetails.ConfirmationStatus = gtmBookingDetails.ConfirmationStatus;
                    _context.SaveChanges();
                    _logger.LogInformation("BookingDetails updated Successfully.");
                    return Created("BookingDetails updated", gtmBookingDetails);

                }
                else
                {
                    GtmBookingDetails bookingDetails = new GtmBookingDetails()
                    {
                        BookingId = gtmBookingDetails.BookingId,
                        FirstName = gtmBookingDetails.FirstName,
                        LastName = gtmBookingDetails.LastName,
                        ConfirmationStatus = gtmBookingDetails.ConfirmationStatus,
                    };
                    _context.GtmBookingDetails.Add(gtmBookingDetails);
                    _context.SaveChanges();
                    _logger.LogInformation("Booking Details added Successfully.");
                    return Created("Booking Details added", gtmBookingDetails);
                }

            }
            catch (Exception ex)
            {
                _logger.LogWarning("Contact To Admin..Server Error" + ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

            [HttpDelete("{BookingId}")]
            public IActionResult Delete(int BookingId)
            {
                GtmBookingDetails id = _context.GtmBookingDetails.Find(BookingId);
                if (id == null)
                {
                    return StatusCode(404, "BookingDetails Not Found");
                }
                else
                {
                    _context.GtmBookingDetails.Remove(id);
                    _context.SaveChanges();
                    return Ok("Deleted Successfully");
                }

            }
        }
    }

        
       

   