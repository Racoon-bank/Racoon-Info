using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public bool IsBanned { get; set; } = false;
        public Theme Theme { get; set; } = Theme.Light;
    }
}