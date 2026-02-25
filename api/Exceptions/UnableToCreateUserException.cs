using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Exceptions
{
    public class UnableToCreateUserException : Exception
    {
        public UnableToCreateUserException()
        : base("Something went wrong. Unable to create user.") { }
    }
}