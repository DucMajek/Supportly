using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supportly.Application.Services;
using Supportly.Infrastructure.Models;
using Supportly.Presentation.Models.DTO.Login;
using Supportly.Presentation.Models.DTO.Register;

namespace Supportly.Presentation.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthenticationService _authenticationService;

        public UserController(UserService userService, AuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginRequest loginRequest)
        {
            var user = await _userService.UserLogin(loginRequest.email, loginRequest.password);

            if (user != null)
            {
                var role = user.RoleId;
                var jwtToken = _authenticationService.GenerateJwtToken(user.Email, role.ToString());
                Response.Cookies.Append("jwtToken", jwtToken, new CookieOptions
                {
                    HttpOnly = false
                    // Secure = true, //HTTPS
                });
                HttpContext.Session.SetString("userId", user.UserId.ToString());
                HttpContext.Session.SetString("userEmail", user.Email);
                return Ok(new { succes = true, message = "Login successful" });
            }

            return NotFound(new { success = false, message = "Invalid login credentials" });

        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterRequest request)
        {
            User user = new User()
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = request.Password,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                RoleId = 1
            };
            if (request.Password != request.PasswordConfirmation)
            {
                return Conflict(new { succes = false, message = "Passwords don't match." });
            }

            var result = _userService.UserRegister(user).Result;
            if (result)
            {
                return Ok(new { succes = true, message = "User registered successfully." });
            }

            return Conflict(new { succes = false, message = "User not registered." });
        }
    }
}