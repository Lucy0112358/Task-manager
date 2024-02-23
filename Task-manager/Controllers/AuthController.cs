using DataModels;
using Microsoft.AspNetCore.Mvc;
using Task_Management.Models;
using Task_Management.Services.Interfaces;

namespace Task_Management.Controllers
{
    public class AuthController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly string _connectionString = string.Empty;   
        private readonly IUserService _userService; 

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register(UserDto user)
        {                      
            var success = _userService.Register(user);
            return Ok(success);
        }


        [HttpPost("signin")]
        public IActionResult SignIn([FromBody] UserLoginDto signInRequest)
        {
            var success = _userService.SignIn(signInRequest.Username, signInRequest.Password);

            return Ok(success);
        }

    }
}
