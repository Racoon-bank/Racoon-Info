using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Login endpoint just in case (don't touch)
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _authService.Login(loginDto);
            return Ok(result);
        }


        /// <summary>
        /// Returns login page
        /// </summary>
        [HttpGet("login")]
        [AllowAnonymous]
        public IActionResult LoginPage([FromQuery] string redirectUrl)
        {
            var html = $@"
            <html>
            <body>
                <h2>SSO Login</h2>
                <form method='post' action='/auth/login-form'>
                    <input type='hidden' name='redirectUrl' value='{redirectUrl}' />

                    <label>Email:</label><br/>
                    <input type='text' name='email' /><br/>

                    <label>Password:</label><br/>
                    <input type='password' name='password' /><br/><br/>

                    <button type='submit'>Login</button>
                </form>
            </body>
            </html>";

            return Content(html, "text/html");
        }


        /// <summary>
        /// Login endpoint for sso form (don't touch)
        /// </summary>
        [HttpPost("login-form")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginForm(
            [FromForm] string email,
            [FromForm] string password,
            [FromForm] string redirectUrl)
        {
            try
            {
                var loginDto = new LoginDto
                {
                    Email = email,
                    Password = password
                };
                var token = await _authService.Login(loginDto);
                return Redirect($"{redirectUrl}?access_token={token.AccessToken}");
            }
            catch
            {
                return Unauthorized("Invalid credentials");
            }
        }

        /// <summary>
        /// Refresh a token
        /// </summary>
        [HttpPost("refresh")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Refresh([FromBody] RefreshDto refreshDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var tokens = await _authService.RefreshToken(refreshDto);
            if (tokens == null)
            {
                return Unauthorized();
            }
            return Ok(tokens);
        }

        /// <summary>
        /// Logout
        /// </summary>
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            if (!User.IsAccessToken()) return Unauthorized();
            await _authService.Logout(User.GetId());
            return Ok();
        }
    }
}