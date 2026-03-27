using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos
{
    public class AppInfo
    {
        public Theme Theme { get; set; }
        public List<Guid> HiddenBankAccounts { get; set; } = new();
    }
}