using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Exceptions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class AppService : IAppService
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDBContext _context;

        public AppService(UserManager<User> userManager, ApplicationDBContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<AppInfo> GetAppInfo(string? userId)
        {
            var user = await FindUser(userId);
            var hiddenAccounts = await _context.HiddenBankAccounts.Where(a => a.UserId == user.Id).Select(a => a.BankAccountId).ToListAsync();
            var appInfo = new AppInfo
            {
                Theme = user.Theme,
                HiddenBankAccounts = hiddenAccounts
            };
            return appInfo;
        }

        public async Task<Guid> HideBankAccount(Guid id, string? userId)
        {
            var user = await FindUser(userId);
            var hiddenBankAccount = new HiddenBankAccount
            {
                BankAccountId = id,
                UserId = userId!,
                User = user,
            };
            await _context.HiddenBankAccounts.AddAsync(hiddenBankAccount);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<Guid> RevealBankAccount(Guid id, string? userId)
        {
            var user = await FindUser(userId);
            var hiddenBankAccount = await _context.HiddenBankAccounts.FirstOrDefaultAsync(a => a.BankAccountId == id);
            if (hiddenBankAccount == null)
            {
                throw new BankAccountNotFoundException(id);
            }
            if (hiddenBankAccount.UserId != user.Id)
            {
                throw new AccessDeniedException();
            }
            _context.HiddenBankAccounts.Remove(hiddenBankAccount);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<Theme> SwitchTheme(string? userId)
        {
            var user = await FindUser(userId);
            var newTheme = user.Theme == Theme.Light ? Theme.Dark : Theme.Light;
            user.Theme = newTheme;
            await _userManager.UpdateAsync(user);
            return newTheme;
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