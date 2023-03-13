using DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Abstracts;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;
using DataAccess.Entities;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailRepository _emailRepository;

        public LoginController(IUserRepository userRepository, IEmailRepository emailRepository)
        {
            _userRepository = userRepository;
            _emailRepository = emailRepository;
        }

        [HttpPost]
        [Route("registeruser")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            try
            {
                await _userRepository.CreateAsync(userDTO);

                var msg = new Message(
                    new string[] { userDTO.Email }, "Registration", "" +
                    $"<p>Dear { userDTO.Username }! <br> " +
                    $"You registered successfully!</p>"
                    );

                _emailRepository.SendEmail(msg);
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
                new Claim("Email", userDTO.Email),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var identity = new ClaimsIdentity(claims, "ApplicationCookie");
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(identity));
        }
    }
}