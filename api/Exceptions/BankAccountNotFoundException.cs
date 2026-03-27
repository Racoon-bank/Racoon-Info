using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Exceptions
{
    public class BankAccountNotFoundException : Exception
    {
        public BankAccountNotFoundException(Guid id)
        : base($"Hidden bank account with id = {id} not found.") { }
    }
}