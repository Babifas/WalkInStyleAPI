using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalkInStyleAPI.Models.User;
using WalkInStyleAPI.Services.User_Service;

namespace WalkInStyleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) 
        { 
            _userService = userService;   
        }
        [HttpGet]
        public async Task<IActionResult> GetStudents()
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
                var checkLogin = await _userService.Login(user);
                if (checkLogin)
                {
                    return Ok("Login Successfull");  
                }
                return NotFound("Incorrect email or password");
            }catch(Exception ex)
            {
                return BadRequest($"Could not login {ex.Message}");
            }
        }
    }
}
