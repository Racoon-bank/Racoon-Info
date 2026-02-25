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
using Microsoft.CodeAnalysis.Differencing;

namespace api.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Create an employee (for employees)
        /// </summary>
        [HttpPost("user")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _userService.RegisterUser(registerDto);
            return Ok(result);
        }


        /// <summary>
        /// Create a user (for employees)
        /// </summary>
        [HttpPost("employee")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> RegisterEmployee([FromBody] RegisterUserDto registerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _userService.RegisterEmployee(registerDto);
            return Ok(result);
        }

        /// <summary>
        /// Get profile for current user
        /// </summary>
        [HttpGet("user/profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var profile = await _userService.GetUserProfile(User.GetId());
            return Ok(profile);
        }

        /// <summary>
        /// Get all users (for employees)
        /// </summary>
        [HttpGet("user")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }


        /// <summary>
        /// Edit profile for current user
        /// </summary>
        [HttpPut("user/profile")]
        [Authorize]
        public async Task<IActionResult> EditProfile([FromBody] EditProfileDto profileDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var profile = await _userService.EditUserProfile(User.GetId(), profileDto);
            return Ok(profile);
        }


        /// <summary>
        /// Ban a user (for employees)
        /// </summary>
        [HttpPut("user/{id}/ban")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> BanUser([FromRoute] Guid id)
        {
            await _userService.BanUser(id.ToString());
            return Ok();
        }
    }
}