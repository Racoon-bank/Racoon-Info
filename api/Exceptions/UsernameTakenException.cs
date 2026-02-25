using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Exceptions
{
    public class EmailTakenException : Exception
    {
        public EmailTakenException(string email)
        : base($"Email {email} is already taken.") { }
    }
}