using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WalkInStyleAPI.Models;
using WalkInStyleAPI.Models.DTOs.User;
using WalkInStyleAPI.Services.User_Service;

namespace WalkInStyleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService,IConfiguration configuration) 
        { 
            _userService = userService;
            _configuration = configuration;
        }
        [HttpGet("GetUsers")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                return Ok(await _userService.GetAllUsers());
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user != null)
                {
                    return Ok(user);
                }
                return NotFound("User not found");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto register)
        {
            try
            {
                var IsUserExist = await _userService.RegisterUser(register);
                if (IsUserExist)
                {
                    return Ok("Registartion successful");
                }
                return BadRequest("User already exist");
            }catch(Exception ex)
            {
                return StatusCode(500,$"An error occcured {ex.Message}");
            }

        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto user)
        {
            try
            {
                var _user = await _userService.Login(user);
                if (_user!=null)
                {
                    string token = GenerateJwtToken(_user);

                    return Ok(new {id=_user.UserId,token=token});  
                }
                return NotFound("Incorrect email or password");
            }catch(Exception ex)
            {
                return BadRequest($"Could not login {ex.Message}");
            }
        }
        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Role, user.Role),
            // Add additional claims as needed
        };

            var token = new JwtSecurityToken(
                //issuer: _configuration["Jwt:Issuer"],
                //audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
