using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Exceptions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<ProfileDto> EditUserProfile(string? userId, EditProfileDto profileDto)
        {
            User user = await FindUser(userId);

            if (user.UserName != profileDto.Username)
            {
                if (await IsUsernameTaken(profileDto.Username))
                {
                    throw new EmailTakenException(profileDto.Username);
                }
            }

            user.Email = profileDto.Email;
            user.UserName = profileDto.Username;

            await _userManager.UpdateAsync(user);
            return user.ToProfileDto();
        }

        public async Task BanUser(string userId)
        {
            var user = await FindUser(userId);
            user.IsBanned = true;
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
        }

        public async Task<ProfileDto> GetUserProfile(string? id)
        {
            var user = await FindUser(id);
            return user.ToProfileDto();
        }

        public Task<List<ProfileDto>> GetAllUsers()
        {
            var users = _userManager.Users.Select(u => u.ToProfileDto()).ToListAsync();
            return users;
        }

        public async Task<ProfileDto> RegisterUser(RegisterUserDto registerDto)
        {
            var userDto = await Register(registerDto, "User");
            return userDto;
        }

        public async Task<ProfileDto> RegisterEmployee(RegisterUserDto registerDto)
        {
            var userDto = await Register(registerDto, "Employee");
            return userDto;
        }

        private async Task<ProfileDto> Register(RegisterUserDto registerDto, string Role)
        {
            if (await IsUsernameTaken(registerDto.Username))
            {
                throw new EmailTakenException(registerDto.Username);
            }
            var user = registerDto.ToUser();
            var createdUser = await _userManager.CreateAsync(user, registerDto.Password);
            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, Role);
                if (!roleResult.Succeeded)
                {
                    throw new UnableToCreateUserException();
                }
                return user.ToProfileDto();
            }
            else
            {
                throw new UnableToCreateUserException();
            }
        }

        private async Task<bool> IsUsernameTaken(string email)
        {
            return _userManager.Users.Any(u => u.Email == email);
        }

        private async Task<User> FindUser(string? userId)
        {
            if (userId == null)
            {
                throw new NoIdProvidedException();
            }

            var user = await _userManager.Users
                .OfType<User>()
                .FirstOrDefaultAsync(a => a.Id == userId);

            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }

            return user;
        }
    }
}