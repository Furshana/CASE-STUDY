using GoToMeetingApp.Handler;
using GoToMeetingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace GoToMeetingApp.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class GtmLoginDetailsController : ControllerBase
    {
        private readonly Gotomeeting_dbContext context;
        private readonly JWTSettings _jwtSettings;
        private readonly ILogger<GtmLoginDetails> _logger;

        public GtmLoginDetailsController(Gotomeeting_dbContext _DbContext,IOptions<JWTSettings> options,
            ILogger<GtmLoginDetails> logger)
        {
            context = _DbContext;
            _jwtSettings = options.Value;
            _logger = logger;
        }
        [HttpGet("GetLoginUsers")]
        public IActionResult GetLoginUsers()
        {
            try
            {
                List<GtmLoginDetails> Logins = context.GtmLoginDetails.ToList();
                if (Logins.Count() != 0)
                {
                    _logger.LogInformation("Login Details are listed");
                    return StatusCode(200, Logins.Where(status => status.IsActive == true).Count());

                }
                else
                {
                    _logger.LogInformation("No such Login User is available");
                    return StatusCode(404, "No Login data are available");
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Contact To Admin..Server Error" + ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] GtmLoginDetails gtmLoginDetails)
        {
            var _gtmLoginDetails = context.GtmLoginDetails.FirstOrDefault(o => o.UserId == gtmLoginDetails.UserId && o.LoginPassword == gtmLoginDetails.LoginPassword);
            if (_gtmLoginDetails == null)

                return Unauthorized();
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
              Subject = new ClaimsIdentity(new Claim[]
              {
                 new Claim(ClaimTypes.Name, gtmLoginDetails.Email)
              }),
                Expires = DateTime.Now.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            string finaltoken = tokenhandler.WriteToken(token);

            return Ok(finaltoken); 
        }
    }
}     
        
