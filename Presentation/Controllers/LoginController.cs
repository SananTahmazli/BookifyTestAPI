using DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Abstracts;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("registeruser")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            try
            {
                await _userRepository.CreateAsync(userDTO);
                return Ok();
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        [HttpPost]
        [Route("loginuser")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserDTO userDTO)
        {
            try
            {
                var result = _userRepository.Login(userDTO);
                Authenticate(result);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        [HttpPost]
        [Route("logoutuser")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync();
                return Ok();
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        private void Authenticate(UserDTO userDTO)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", userDTO.Id.ToString()),
                new Claim("FullName", userDTO.FullName),
                new Claim("Username", userDTO.Username),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var identity = new ClaimsIdentity(claims, "ApplicationCookie");
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(identity));
        }
    }
}