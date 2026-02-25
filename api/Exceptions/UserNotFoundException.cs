using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string userId)
        : base($"User with id = {userId} not found.") { }
    }
}