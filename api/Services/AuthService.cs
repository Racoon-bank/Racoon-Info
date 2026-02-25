using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Exceptions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;
        public AuthService(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        // public async Task<TokenResponse?> ChangeLoginCredentials(ChangeLoginDto loginDto, string id)
        // {
        //     var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
        //     if (user == null) {
        //         return null;
        //     }

        //     var result = await _userManager.ChangePasswordAsync(user, loginDto.OldPassword, loginDto.NewPassword);
        //     if (result.Succeeded) {
        //         var roles = await _userManager.GetRolesAsync(user);
        //         return new TokenResponse {
        //             AccessToken = await _tokenService.CreateAccessToken(user.Id, roles),
        //             RefreshToken = await _tokenService.CreateRefreshToken(user.Id,  roles)
        //         };
        //     }
        //     return null;
        // }

        public async Task<TokenResponse> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);
            if (user == null)
            {
                throw new LoginFailedException();
            }
            if (user.IsBanned)
            {
                throw new BannedUserException();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                throw new LoginFailedException();
            }

            var roles = await _userManager.GetRolesAsync(user);
            return new TokenResponse
            {
                AccessToken = await _tokenService.CreateAccessToken(user.Id, roles),
                RefreshToken = await _tokenService.CreateRefreshToken(user.Id, roles),
            };
        }

        public async Task Logout(string? id)
        {
            var user = await FindUser(id);
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
        }

        public async Task<TokenResponse> RefreshToken(RefreshDto refreshDto)
        {
            var validToken = _tokenService.ValidateRefreshToken(refreshDto.RefreshToken);
            if (!validToken)
            {
                throw new InvalidRefreshTokenException();
            }
            var data = _tokenService.GetTokenData(refreshDto.RefreshToken);
            if (data != null)
            {
                return new TokenResponse
                {
                    AccessToken = await _tokenService.CreateAccessToken(data.Id, data.Roles),
                    RefreshToken = await _tokenService.CreateRefreshToken(data.Id, data.Roles),
                };
            }
            throw new InvalidRefreshTokenException();
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