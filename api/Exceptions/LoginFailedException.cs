using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Exceptions
{
    public class LoginFailedException : Exception
    {
        public LoginFailedException()
        : base("Login failed. Wrong login or password.")
        {

        }
    }
}