using GoToMeetingApp.Handler;
using GoToMeetingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoToMeetingApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GtmMeetingRoomDetailsController : ControllerBase
    {
        
        private readonly Gotomeeting_dbContext _Context;
        private readonly ILogger<GtmMeetingRoomDetails> _logger;

        public GtmMeetingRoomDetailsController(Gotomeeting_dbContext Context, ILogger<GtmMeetingRoomDetails> logger)
        {
            _Context = Context;
            _logger = logger;
        }
        [HttpGet("GetRoomId")]
        public List<GtmMeetingRoomDetails> GetRoomId()
        {
            List<GtmMeetingRoomDetails> RoomId = _Context.GtmMeetingRoomDetails.ToList();
            return RoomId;
        }
        [HttpDelete("{id}")]
        public JWTSettings Delete(int RoomId)
        {
            string result = string.Empty;
            var _emp=_Context.GtmMeetingRoomDetails.FirstOrDefault(o => o.RoomId == RoomId);
            if (_emp != null)
            {
                _Context.GtmMeetingRoomDetails.Remove(_emp);
                _Context.SaveChanges();
                _logger.LogInformation("Room Id get deleted");
                result = "pass";
            }
            return new JWTSettings { SecretKey = string.Empty,Result= result };
        }
        [HttpPost("ActivateRoomId")]
        public JWTSettings ActivateUser([FromBody] GtmMeetingRoomDetails value)
        {
            string result = string.Empty;
            try
            {
                var _emp = _Context.GtmMeetingRoomDetails.FirstOrDefault(o => o.RoomId == value.RoomId);
                if (_emp != null)
                {
                    _emp.FirstName = value.FirstName;
                    _emp.LastName = value.LastName;
                    _Context.SaveChanges();
                    _logger.LogInformation("Meeting Room Id get Activated");
                    result = "pass";
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Contact To Admin..Server Error" + ex.Message);
                result = string.Empty;
            }
            return new JWTSettings {SecretKey = string.Empty, Result = result };

        }

    }
    }

