using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Interfaces
{
    public interface IUserService
    {
        Task<ProfileDto> GetUserProfile(string? id);
        Task<ProfileDto> RegisterUser(RegisterUserDto registerDto);
        Task<ProfileDto> RegisterEmployee(RegisterUserDto registerDto);
        Task<TokenResponse> RegisterSelf(RegisterUserDto registerDto);
        Task<ProfileDto> EditUserProfile(string? userId, EditProfileDto profileDto);
        Task<List<ProfileDto>> GetAllUsers();
        Task BanUser(string userId);
    }
}