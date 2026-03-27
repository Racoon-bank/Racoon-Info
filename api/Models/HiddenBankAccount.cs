using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class HiddenBankAccount
    {
        public string UserId { get; set; } = string.Empty!;
        public Guid BankAccountId { get; set; }
        public User User { get; set; }
    }
}