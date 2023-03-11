using GoToMeetingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System;
using GoToMeetingApp.Handler;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GoToMeetingApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GtmAdminDetailsController : ControllerBase
    {
        private readonly Gotomeeting_dbContext _context;
        private readonly ILogger<GtmAdminDetails> logger;
        private readonly JWTSettings _jwtSettings;

        public GtmAdminDetailsController(Gotomeeting_dbContext context,
            ILogger<GtmAdminDetails> logger)
        {
            this._context = context;
            this.logger = logger;
        }
        [HttpPost("AddAdmin")]
        public IActionResult AddAdmin([FromBody] GtmAdminDetails gtmAdminDetails)
        {
            try
            {
                _context.GtmAdminDetails.Add(gtmAdminDetails);
                _context.SaveChanges();
                logger.LogInformation("AdminId added Successfully.");
                return Created("AdminId added", gtmAdminDetails);
            }
            catch (NullReferenceException ex)
            {
                logger.LogWarning("Data not found" + ex.Message);
                return StatusCode(404, "Data Not Found");

            }
            catch (Exception ex)
            {
                logger.LogWarning("Contact To Admin..Server Error" + ex.Message);
                return StatusCode(500, "Internal Server Error");
            }

        }

        [HttpGet("GetAdmin")]
        public IActionResult GetAdmin()
        {
            try
            {
                List<GtmAdminDetails> Admins = _context.GtmAdminDetails.ToList();
                if (Admins.Count() != 0)
                {
                    logger.LogInformation("Admins details are listed");
                    return StatusCode(200, Admins.Where(status => status.IsActive == true).Count());

                }
                else
                {
                    logger.LogInformation("No RoomAdminId is not available");
                    return StatusCode(404, "No Admin data are available");
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning("Contact To Admin..Server Error" + ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] GtmAdminDetails gtmAdminDetails)
        {
            var _gtmAdminDetails = _context.GtmAdminDetails.FirstOrDefault(o => o.RoomAdminId == gtmAdminDetails.RoomAdminId && o.VerificationStatus== gtmAdminDetails.VerificationStatus);
            if (_gtmAdminDetails == null)

                return Unauthorized();
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
                 new Claim(ClaimTypes.Name, gtmAdminDetails.VerificationStatus)
              }),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            string finaltoken = tokenhandler.WriteToken(token);

            return Ok(finaltoken);
        }
    }
}
