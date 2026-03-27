using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Interfaces
{
    public interface IAppService
    {
        Task<Theme> SwitchTheme(string? userId);
        Task<Guid> HideBankAccount(Guid id, string? userId);
        Task<Guid> RevealBankAccount(Guid id, string? userId);
        Task<AppInfo> GetAppInfo(string? userId);
    }
}